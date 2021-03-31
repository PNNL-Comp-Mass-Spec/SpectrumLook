// TEST CHECKOUT

// RYAN YOUR SOLUTION TO DISCUSS WITH THE GROUP IS IN FragmentLadderView.cs at the bottom

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PRISM;
using SpectrumLook.Views;
using SpectrumLook.Builders;

namespace SpectrumLook
{
    public class Manager
    {
        public struct SynFileColumnIndices
        {
            public int Scan;
            public int Peptide;
            public int Dataset;
            public int PrecursorMz;
            public int ParentMH;
            public int Charge;
        }

        public MainForm mMainForm;

        public SLPlot mPlot;

        public DataView mDataView;

        public Views.FragmentLadderView.FragmentLadderView mFragmentationLadder;

        private readonly OptionsViewController mOptions;

        private readonly BuilderDirector mBuilderDirector;

        private Dictionary<string, List<LadderInstance>> mLadderInstances = new Dictionary<string, List<LadderInstance>>();

        private LadderInstance mCurrentInstance;

        private readonly LadderInstanceDictionaryXmlSerializer mWorkFileWriter;

        /// <summary>
        /// This is the main class the manages the interactions between the plot, data view, main form, and fragment ladder.
        /// </summary>
        /// <param name="mainForm">This should be the main form that will contain the fragment ladder, plot and the data view.</param>
        public Manager(MainForm mainForm)
        {
            // Plot
            mPlot = new SLPlot(this)
            {
                TopLevel = false,
                Visible = true,
                FormBorderStyle = FormBorderStyle.None,
                Text = "Plot"
            };

            // Data View
            mDataView = new DataView(this)
            {
                TopLevel = false,
                Visible = true,
                FormBorderStyle = FormBorderStyle.None
            };

            // Fragment Ladder
            mFragmentationLadder = new Views.FragmentLadderView.FragmentLadderView(this)
            {
                TopLevel = false,
                Visible = true,
                FormBorderStyle = FormBorderStyle.None
            };

            // MainForm
            mMainForm = mainForm;

            // BuilderDirector
            mBuilderDirector = new BuilderDirector();

            // For loading profile settings :
            FileStream reader = null;
            var createFileFlag = false;

            // check to see if data is loaded
            DataLoaded = false;

            // This is to read the UserProfile for spectrumLook
            try
            {
                reader = new FileStream(Path.Combine(Directory.GetCurrentDirectory() + "UserProfile.spuf"), FileMode.Open, FileAccess.Read);
            }
            catch (Exception)
            {
                createFileFlag = true;
            }

            if (reader != null)
            {
                try
                {
                    var binaryFormatter = new BinaryFormatter();
                    mPlot.Options = new PlotOptions((PlotOptions)binaryFormatter.Deserialize(reader));
                    mMainForm.mCurrentOptions = new MainFormOptions((MainFormOptions)binaryFormatter.Deserialize(reader));
                    mFragmentationLadder.FragmentLadderOptions = new Views.Options.FragmentLadderOptions((Views.Options.FragmentLadderOptions)binaryFormatter.Deserialize(reader));
                }
                catch { }
                finally
                {
                    reader.Close();
                }
            }

            // This is used to read and write the .spuf
            mWorkFileWriter = new LadderInstanceDictionaryXmlSerializer();

            var userProfileFilePath = Path.Combine(Directory.GetCurrentDirectory() + "UserProfile.spuf");

            // Options
            mOptions = new OptionsViewController(mPlot.Options, mMainForm.mCurrentOptions, mFragmentationLadder.FragmentLadderOptions, userProfileFilePath, createFileFlag, mFragmentationLadder);
            mMainForm.mCurrentOptions.ToleranceValue = 0.7;

            // attach all of the observers to the subjects
            var tempObserver = mPlot as IObserver;
            mPlot.Options.Attach(ref tempObserver);

            tempObserver = mMainForm as IObserver;
            mMainForm.mCurrentOptions.Attach(ref tempObserver);

            tempObserver = mOptions as IObserver;
            mMainForm.mCurrentOptions.Attach(ref tempObserver);
            mPlot.Options.Attach(ref tempObserver);// This is because the plot window depends on the mainFormOptions.

            tempObserver = mFragmentationLadder as IObserver;
            // Add pre-defined symbols to modifications list
            mFragmentationLadder.FragmentLadderOptions.Attach(ref tempObserver);
            if (mFragmentationLadder.FragmentLadderOptions.ModificationList.Count == 0)
            {
                mFragmentationLadder.FragmentLadderOptions.ResetModificationsToDefault();
            }

            mPlot.Options.SetOptions(mPlot.Options);
            mOptions.Hide();
        }

        /// <summary>
        /// This is a handle that is hooked to the keyboard.
        /// If the key that invoked this Handle is equal to the
        /// key that is set in the plot options then the
        /// HandleZoomOut() in the plot is called.
        /// </summary>
        /// <param name="e">The keyboard key that caused this event.</param>
        public void HandleKeyboardShortcuts(KeyEventArgs e)
        {
            // handle ZoomOut
            if (e.Control)
            {
                if (e.KeyCode == mPlot.Options.UnzoomKey)
                {
                    mPlot.HandleZoomOut();
                    e.Handled = true;
                }
            }
        }

        public bool SynopsisLoaded { get; private set; }
        public bool DataLoaded { get; private set; }
        public string DataFileName { get; set; } = string.Empty;

        public double PrecursorMZ { get; set; }

        private string mDataDirectoryPath = string.Empty;

        private string DataFilePath
        {
            get => Path.Combine(mDataDirectoryPath, DataFileName);
            set
            {
                mDataDirectoryPath = Path.GetDirectoryName(value);
                DataFileName = Path.GetFileName(value);
            }
        }

        private int mCurrentScanNumber;
        private string mCurrentPeptide;
        private string mSynopsisFilePath;
        private bool mIsETD = false;
        private List<Element> mExperimentalList;

        /// <summary>
        /// RunOpenDialog will open the custom SLOpenFileDialog box and prompt the user to
        /// open a experimental file and a theoretical file.
        /// </summary>
        public void RunOpenDialog()
        {
            var openDialog = new SLOpenFileDialog(mSynopsisFilePath, DataFilePath);
            var result = openDialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            DataFilePath = openDialog.mDataPath;
            mSynopsisFilePath = openDialog.mSynopsisPath;

            if (string.IsNullOrWhiteSpace(mSynopsisFilePath))
                return;

            try
            {
                var reader = new PHRPReaderParser(mSynopsisFilePath, mFragmentationLadder.FragmentLadderOptions);
                mDataView.SetDataTable(DataBuilder.GetDataTable(reader, out var synFileColumns));

                mDataView.SetColumnIndices(synFileColumns);
                SynopsisLoaded = true;
            }
            catch (Exception ex)
            {
                SynopsisLoaded = false;

                var errorMsg = string.Format(
                    "There was an error opening the Synopsis file, are you sure you picked the right file?\n\n{0}\n\nStack Trace:\n{1}",
                    ex.Message,
                    StackTraceFormatter.GetExceptionStackTraceMultiLine(ex));

                MessageBox.Show(errorMsg, "Error reading file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Select the first row
            mDataView.HandleRowSelection();
        }

        // This function simply calls the combo box function within mFragmentationLadder
        public void CallComboBox()
        {
            mFragmentationLadder.SetComboBox();
        }

        /// <summary>
        /// This handle is called from the fragmentLadder.
        /// This is created for the purpose of adding a modified peptide to the current Instance List.
        /// </summary>
        /// <param name="peptide">A peptide string.</param>
        public bool HandleInputPeptide(string peptide)
        {
            if (mCurrentScanNumber == 0)
            {
                var msg = string.Format("Please open a synopsis and data file and then load a scan from the data view to calculate: \"{0}\"", peptide);
                MessageBox.Show(msg, "Not ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (peptide.Length == 1)
            {
                MessageBox.Show("Invalid peptide string (single character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            /*MG: Generating a regular expression string with the current modification
                 * symbols to check if the peptide string from the user is a valid peptide string.
                 * if it's not, giving feedback to user and returning out*/
            var modList = new string(mFragmentationLadder.FragmentLadderOptions.ModificationList.Keys.ToArray());

            // The automatic way, using .NET
            var escapedModList = System.Text.RegularExpressions.Regex.Escape(modList);

            // One or more characters, A-Z, which may be followed by a valid modification symbol, that does not end with a modification symbol be itself?
            // The escaping of all symbols is likely a problem.

            // Match any set of one or more groups of "character a-z followed by 0-2 valid modification symbols"
            string validCharactersRegEx;

            if (escapedModList.Length > 0)
                validCharactersRegEx = "([A-Z][" + escapedModList + "]{0,5})+";
            else
                validCharactersRegEx = "([A-Z])+";

            // This test will always be evaluate to 'not false' whenever there are lowercase characters or symbols that are not in the modification list,
            // or if the peptide begins with a modification symbol.

            if (!System.Text.RegularExpressions.Regex.IsMatch(peptide, "^" + validCharactersRegEx + "$"))
            {
                // Get a string with all invalid characters by removing the valid characters
                var invalidChars = System.Text.RegularExpressions.Regex.Replace(peptide, validCharactersRegEx, string.Empty);
                if (modList.Contains(peptide[0]))
                {
                    MessageBox.Show("The peptide string cannot start with a modification.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                if (escapedModList.Length > 0 &&
                    System.Text.RegularExpressions.Regex.Replace(invalidChars, "[" + escapedModList + "]", string.Empty).Length == 0)
                {
                    MessageBox.Show("Invalid peptide string: Too many modifications on a single amino acid.", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return false;
                }

                var msg = string.Format(
                    "Invalid characters in peptide string \"{0}\", please remove the characters or define them in options/fragment ladder",
                    invalidChars);
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // check to see if peptide exists in the ladder instances table and don't add a modification if it does
            var isSame = mLadderInstances[mCurrentScanNumber + mCurrentPeptide].Any(instance => instance.PeptideString == peptide);

            if (!isSame)
            {
                var ladderBuilder =
                    new Views.FragmentLadderView.LadderInstanceBuilder();

                // use the builder director to crunch all the data
                var theoreticalList = mBuilderDirector.BuildTheoryList(peptide, mIsETD,
                    mFragmentationLadder.FragmentLadderOptions.ModificationList);
                var comparedList =
                    mBuilderDirector.BuildComparedList(mMainForm.mCurrentOptions.ToleranceValue,
                        mMainForm.mCurrentOptions.LowerToleranceValue, mExperimentalList, PrecursorMZ, ref theoreticalList);

                // now give the data to the views to display
                // Send the FragmentLadder and the Plot the data to show
                if (mLadderInstances.ContainsKey(mCurrentScanNumber.ToString() + mCurrentPeptide))
                {
                    mCurrentInstance = ladderBuilder.GenerateInstance(theoreticalList, peptide,
                        mFragmentationLadder.FragmentLadderOptions.ModificationList);
                    mCurrentInstance.ScanAndPeptide = mCurrentScanNumber.ToString() + "|" + peptide;
                    mLadderInstances[mCurrentScanNumber.ToString() + mCurrentPeptide].Add(mCurrentInstance);
                    mCurrentInstance = mLadderInstances[mCurrentScanNumber.ToString() + mCurrentPeptide][0];
                    mFragmentationLadder.GenerateLadderFromSelection(0.0, mLadderInstances[mCurrentScanNumber.ToString() + mCurrentPeptide]);
                    mPlot.PlotData(comparedList, mCurrentScanNumber.ToString(), peptide);
                }
                else
                {
                    mPlot.PlotData(comparedList, mCurrentScanNumber.ToString(), mCurrentPeptide);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// This function will remove a specific modification from the ladder instances table
        /// </summary>
        /// <param name="index"></param>
        public void RemoveModificationFromList(int index)
        {
            var x = mLadderInstances[mCurrentScanNumber.ToString() + mCurrentPeptide][index].PeptideString;
            mLadderInstances[mCurrentScanNumber.ToString() + mCurrentPeptide].RemoveAt(index);
            // mLadderInstances.Remove(((List<LadderInstance>)mLadderInstances[mCurrentScanNumber.ToString() + mCurrentPeptide])[index]);
            mDataView.HandleRowSelection();
        }

        /// <summary>
        /// This function handle is called from both the fragment ladder and the data view.
        /// This function handles the case when the user selects a peptide and scan number in the data view.
        /// </summary>
        /// <param name="scanNumber">The scan number that relates to the experimental data.</param>
        /// <param name="peptide">The peptide sequence.</param>
        public void HandleSelectScanAndPeptide(string scanNumber, string peptide)
        {
            DataLoaded = true;
            try
            {
                var ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

                mCurrentScanNumber = Convert.ToInt32(scanNumber);
                mCurrentPeptide = peptide;
                // use the builder director to crunch all the data
                var theoreticalList = mBuilderDirector.BuildTheoryList(peptide, mIsETD, mFragmentationLadder.FragmentLadderOptions.ModificationList);
                mExperimentalList = mBuilderDirector.BuildActualList(Convert.ToInt32(scanNumber), DataFilePath);
                var comparedList = mBuilderDirector.BuildComparedList(mMainForm.mCurrentOptions.ToleranceValue, mMainForm.mCurrentOptions.LowerToleranceValue, mExperimentalList, PrecursorMZ, ref theoreticalList);

                // now give the data to the views to display
                // Send the FragmentLadder and the Plot the data to show
                if (mLadderInstances.ContainsKey(scanNumber + peptide))
                {
                    mCurrentInstance = mLadderInstances[scanNumber + peptide][0];
                    mFragmentationLadder.GenerateLadderFromSelection(0.0, mLadderInstances[scanNumber + peptide]);
                }
                else
                {
                    var tempList = new List<LadderInstance>();
                    var tempInstance = ladderBuilder.GenerateInstance(theoreticalList, peptide, mFragmentationLadder.FragmentLadderOptions.ModificationList);
                    tempInstance.ScanAndPeptide = scanNumber + "|" + peptide;
                    tempList.Add(tempInstance);
                    mLadderInstances.Add(scanNumber + peptide, tempList);

                    mCurrentInstance = mLadderInstances[scanNumber + peptide][0];
                    mFragmentationLadder.GenerateLadderFromSelection(0.0, mLadderInstances[scanNumber + peptide]);
                }
                mPlot.PlotData(comparedList, scanNumber, peptide);
                // mExperimentalList.Clear();
                // mExperimentalList = null;
            }
            catch (InvalidProgramException ex)
            {
                if (ex.Message.Contains("Invalid File Type"))
                {
                    MessageBox.Show(
                        "Invalid File Type for the Data file.  Allowed types are .mzML, .mzXml, .mzData, and Thermo .raw files.",
                        "File Type Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Focuses the plot on a specific point.  This is used for when the user clicks the fragment ladder and we want to highlight that point
        /// </summary>
        /// <param name="focusValue">The x value to focus on in the plot</param>
        public void FocusPlotOnPoint(double focusValue)
        {
            mPlot.FocusPlotOnPoint(focusValue);
        }

        /// <summary>
        /// Handles re-plotting the data when the fragment ladder peptide has changed without selecting a new set of data
        /// </summary>
        public void HandlefragmentLadderModeChange(bool setMode)
        {
            mIsETD = setMode;

            var allKeyValues = mLadderInstances.Keys.ToArray();
            var ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

            foreach (var currentKey in allKeyValues)
            {
                var currentLadderInstances = mLadderInstances[currentKey][0];
                mLadderInstances.Remove(currentKey);
                var newInstanceList = new List<LadderInstance>();

                // Give Theoretical List the modified Peptide
                // Assuming The Fragmentation ladder will add the PeptideString of the modified thing.
                var theoreticalList = mBuilderDirector.BuildTheoryList(currentLadderInstances.PeptideString, mIsETD, mFragmentationLadder.FragmentLadderOptions.ModificationList);

                // Give experimental List the original Peptide.
                // WARNING : ASSUMING THE SCAN NUMBER KEY IS AN INTEGER
                // THIS MAY NOT BE THE CASE IF PARSER CHANGES
                var experimentalList = mBuilderDirector.BuildActualList(int.Parse(currentLadderInstances.ScanNumberString), DataFilePath);

                // build the compared list
                var comparedList = mBuilderDirector.BuildComparedList(mMainForm.mCurrentOptions.ToleranceValue, mMainForm.mCurrentOptions.LowerToleranceValue, experimentalList, PrecursorMZ, ref theoreticalList);

                // Generate the Instance based on the builder.
                var newLadder = ladderBuilder.GenerateInstance(theoreticalList, currentLadderInstances.PeptideString, mFragmentationLadder.FragmentLadderOptions.ModificationList);

                newLadder.ScanAndPeptide = currentLadderInstances.ScanAndPeptide;

                // Add to List
                newInstanceList.Add(newLadder);

                // Add List Of Instances to the HashTable.
                mLadderInstances.Add(currentKey, newInstanceList);
            }

            // Going to delete all the values and recalculate the original.
            mLadderInstances.Remove(mCurrentScanNumber.ToString() + mCurrentPeptide);
            // We should just then call HandleSelectScanAndPeptide
            HandleSelectScanAndPeptide(mCurrentScanNumber.ToString(), mCurrentPeptide);
        }

        /// <summary>
        /// This will be called when the fragment ladder is to clear ladder instances.
        /// </summary>
        public void ClearLadderInstances()
        {
            mLadderInstances.Remove(mCurrentScanNumber.ToString() + mCurrentPeptide);
            HandleSelectScanAndPeptide(mCurrentScanNumber.ToString(), mCurrentPeptide);
        }

        /// <summary>
        /// This function handles recalculating all the ladder instances in the hashtable.
        /// This is normally called when the fragment ladder options change.
        /// </summary>
        public void HandleRecalculateAllInHashCode()
        {
            // Get all the Keys in the HashTable.
            var allKeyValues = mLadderInstances.Keys.ToArray();
            var ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

            foreach (var currentKey in allKeyValues)
            {
                var currentLadderInstances = mLadderInstances[currentKey];
                mLadderInstances.Remove(currentKey);
                var newInstanceList = new List<LadderInstance>();
                foreach (var currentInstance in currentLadderInstances)
                {
                    // Give Theoretical List the modified Peptide
                    // Assuming The Frag ladder will add the PeptideString of the modified thing.
                    var theoreticalList = mBuilderDirector.BuildTheoryList(currentInstance.PeptideString, mIsETD, mFragmentationLadder.FragmentLadderOptions.ModificationList);
                    // Give experimental List the original Peptide.
                    // WARNING : ASSUMING THE SCAN NUMBER KEY IS AN INTEGER
                    // THIS MAY NOT BE THE CASE IF PARSER CHANGES
                    var experimentalList = mBuilderDirector.BuildActualList(int.Parse(currentInstance.ScanNumberString), DataFilePath);
                    // build the compared list
                    var comparedList = mBuilderDirector.BuildComparedList(mMainForm.mCurrentOptions.ToleranceValue, mMainForm.mCurrentOptions.LowerToleranceValue, experimentalList, PrecursorMZ, ref theoreticalList);

                    // Generate the Instance based on the builder.
                    var newLadder = ladderBuilder.GenerateInstance(theoreticalList, currentInstance.PeptideString, mFragmentationLadder.FragmentLadderOptions.ModificationList);

                    newLadder.ScanAndPeptide = currentInstance.ScanAndPeptide;

                    // Add to List
                    newInstanceList.Add(newLadder);
                }
                // Add List Of Instances to the HashTable.
                mLadderInstances.Add(currentKey, newInstanceList);
            }
            if (mLadderInstances.Count > 0)
            {
                mCurrentInstance = mLadderInstances[mCurrentScanNumber + mCurrentPeptide][0];
                mFragmentationLadder.GenerateLadderFromSelection(0.0, mLadderInstances[mCurrentScanNumber + mCurrentPeptide]);
            }
        }

        /// <summary>
        /// This function is called by the main form.
        /// This opens the Options View Controller with the desired tab selected.
        /// </summary>
        /// <param name="startingTab">The title of the desired starting tab.</param>
        public void OpenOptionsMenu(string startingTab)
        {
            mOptions.SelectTab(startingTab);
            mOptions.Show();
            mOptions.BringToFront();
        }

        public void FocusOnControl(Control attentionSeeker)
        {
            if (mMainForm.Contains(attentionSeeker))
            {
                mMainForm.ActiveControl = attentionSeeker;
            }
        }

        /// <summary>
        /// This function is used in the fragment ladder.
        /// When the user selects a different tab the graph will plot the spectrum of that current instance.
        /// </summary>
        /// <param name="newInstanceIndex">The index in the current List of ladder instances.</param>
        public void UpdateCurrentInstance(int newInstanceIndex)
        {
            mCurrentInstance = ((List<LadderInstance>)mLadderInstances[mCurrentScanNumber + mCurrentPeptide])[newInstanceIndex];
            mCurrentScanNumber = int.Parse(mCurrentInstance.ScanNumberString);

            var theoreticalList = mBuilderDirector.BuildTheoryList(mCurrentInstance.PeptideString, mIsETD, mFragmentationLadder.FragmentLadderOptions.ModificationList);
            var comparedList = mBuilderDirector.BuildComparedList(mMainForm.mCurrentOptions.ToleranceValue, mMainForm.mCurrentOptions.LowerToleranceValue, mExperimentalList, PrecursorMZ, ref theoreticalList);
            var peptide = mCurrentInstance.PeptideString;

            mPlot.PlotData(comparedList, mCurrentScanNumber.ToString(), peptide);

            mFragmentationLadder.setPeptideTextBox(peptide);
        }

        /// <summary>
        /// A property to get the current selected ladder instance.
        /// </summary>
        /// <returns>the current selected ladder instance or null if none is selected.</returns>
        public LadderInstance GetCurrentInstance()
        {
            return mCurrentInstance;
        }

        /// <summary>
        /// This function calls the HashtableXmlSerializer.WriteHashTable function and requests a write to the given or save location.
        /// </summary>
        /// <param name="filePath">The location in which you want to save the .spwf</param>
        public void HandleSaveWorkFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
                mWorkFileWriter.WriteLadderInstanceDictionary(filePath, mLadderInstances);
            else
                mWorkFileWriter.WriteLadderInstanceDictionary(mLadderInstances);
        }

        /// <summary>
        /// This function calls the HashtableXmlSerializer.ReadXmlWorkFile with the given file location.
        /// This function fully populates the mLadderInstances with the data in the .spwf.
        /// </summary>
        /// <param name="filePath">The location of the .spwf that the user wants to open.</param>
        public void HandleOpenWorkFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                mLadderInstances = mWorkFileWriter.ReadXmlWorkFile(filePath);
            }
        }

        /// <summary>
        /// Handles A batch save
        /// </summary>
        public void HandleBatchSave(
            string directory,
            string baseName,
            string saveType,
            bool useScansInGrid,
            bool usePeptideAndScanName,
            bool addDatasetName,
            UpdateLabelDelegate updateLabelCallback,
            ref bool cancelSearch)
        {
            updateLabelCallback("Starting Batch Save...");
            mBatchSaveCounter = 0;
            var mostRecentFileName = string.Empty;

            foreach (var row in mDataView.GetPeptidesAndScansInGrid(useScansInGrid))
            {
                if (cancelSearch)
                    break;

                var nextFileBase = baseName;
                if (!string.IsNullOrWhiteSpace(row.DatasetName))
                {
                    DataFileName = row.DatasetName;
                    if (addDatasetName)
                    {
                        nextFileBase += "_" + Path.GetFileNameWithoutExtension(row.DatasetName);
                    }
                }

                PrecursorMZ = row.DblPrecursorMZ;

                var nextFileName = CreateNextFileName(nextFileBase, usePeptideAndScanName, row.Peptide, row.ScanNumber) + saveType;
                updateLabelCallback("Saving \"" + nextFileName + "\"");

                HandleSelectScanAndPeptide(row.ScanNumber, row.Peptide);
                mPlot.SavePlotImageAs(Path.Combine(directory, nextFileName));
                mostRecentFileName = nextFileName;
            }

            if (mBatchSaveCounter == 0)
            {
                updateLabelCallback("Batch Save did not create any files, due to either an empty data grid or a program error");
            }
            else if (mBatchSaveCounter == 1)
            {
                updateLabelCallback(string.Format("Finished Batch Save; created {0}", Path.Combine(directory, mostRecentFileName)));
            }
            else
            {
                updateLabelCallback(string.Format("Finished Batch Save; created {0} file(s) in {1}", mBatchSaveCounter, directory));
            }
        }

        /// <summary>
        /// Handles a plot save
        /// </summary>
        public void HandlePlotSave(string directory, string fileName, string saveType)
        {
            mPlot.SavePlotImageAs(Path.Combine(directory, fileName + saveType));
        }

        /// <summary>
        /// Generates a new filename to use based off of the users options and the information used in the plot
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="usePeptideAndScanName"></param>
        /// <param name="peptide"></param>
        /// <param name="scanNumber"></param>
        /// <returns></returns>
        private static string CreateNextFileName(string baseName, bool usePeptideAndScanName, string peptide, string scanNumber)
        {
            var nextFileName = baseName;

            if (usePeptideAndScanName)
            {
                nextFileName += "_" + peptide + "_" + scanNumber;
            }
            else
            {
                // Attach a unique number to the saved file, since we are not guaranteed uniqueness from peptide or scan number alone.
                nextFileName += "_" + string.Format("{0:0000}", mBatchSaveCounter);
            }

            // Magic number 232, appears to be the max filename length in Windows.  For now, just truncate what they have.
            if (nextFileName.Length > 232)
            {
                nextFileName = nextFileName.Remove(232);
            }

            mBatchSaveCounter++;
            return nextFileName;
        }

        private static int mBatchSaveCounter = 0;

        public delegate void UpdateLabelDelegate(string newText);
    }
}
