// TEST CHECKOUT

// RYAN YOUR SOLUTION TO DISCUSS WITH THE GROUP IS IN FragmentLadderView.cs at the bottom

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SpectrumLook;
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

        public MainForm m_mainForm;
        public SLPlot m_plot;
        public DataView m_dataView;
        public Views.FragmentLadderView.FragmentLadderView m_fragLadder;

        private OptionsViewController m_options;

        private BuilderDirector m_builderDirector;

        private Dictionary<string, List<LadderInstance>> m_ladderInstancesTable = new Dictionary<string, List<LadderInstance>>();

        private LadderInstance m_currentInstance;

        private LadderInstanceDictionaryXmlSerializer m_workFileWriter;

        /// <summary>
        /// Ths is the main class the manages the interactions between the plot, dataview, mainform, and fragment ladder.
        /// </summary>
        /// <param name="mainForm">This should be the mainform that will contain the fragment ladder, plot and the dataview.</param>
        public Manager(MainForm mainForm)
        {
            //Plot
            m_plot = new SLPlot(this);
            m_plot.TopLevel = false;
            m_plot.Visible = true;
            m_plot.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            m_plot.Text = "Plot";

            //Data View
            m_dataView = new DataView(this);
            m_dataView.TopLevel = false;
            m_dataView.Visible = true;
            m_dataView.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //Fragment Ladder
            m_fragLadder = new Views.FragmentLadderView.FragmentLadderView(this);
            m_fragLadder.TopLevel = false;
            m_fragLadder.Visible = true;
            m_fragLadder.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            //MainForm
            m_mainForm = mainForm;

            //BuilderDirector
            m_builderDirector = new BuilderDirector();

            //For loading profile settings :
            FileStream reader = null;
            var createFileFlag = false;

            // check to see if data is loaded
            DataLoaded = false;

            // This is to read the UserProfile for spectrumLook
            try
            {
                reader = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\UserProfile.spuf", FileMode.Open, FileAccess.Read);
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
                    m_plot.m_options = new PlotOptions((PlotOptions)binaryFormatter.Deserialize(reader));
                    m_mainForm.m_currentOptions = new MainFormOptions((MainFormOptions)binaryFormatter.Deserialize(reader));
                    m_fragLadder.fragmentLadderOptions = new Views.Options.FragmentLadderOptions((Views.Options.FragmentLadderOptions)binaryFormatter.Deserialize(reader));
                }
                catch { }
                finally
                {
                    reader.Close();
                }
            }

            //This is used to read and write the the .spuf
            m_workFileWriter = new LadderInstanceDictionaryXmlSerializer();

            //Options
            m_options = new OptionsViewController(m_plot.m_options, m_mainForm.m_currentOptions, m_fragLadder.fragmentLadderOptions ,System.IO.Directory.GetCurrentDirectory() + "\\UserProfile.spuf", createFileFlag, m_fragLadder);
            m_mainForm.m_currentOptions.toleranceValue = 0.7;

            //attach all of the observers to the subjects
            var tempObserver = m_plot as IObserver;
            m_plot.m_options.Attach(ref tempObserver);

            tempObserver = m_mainForm as IObserver;
            m_mainForm.m_currentOptions.Attach(ref tempObserver);

            tempObserver = m_options as IObserver;
            m_mainForm.m_currentOptions.Attach(ref tempObserver);
            m_plot.m_options.Attach(ref tempObserver);//This is because the plot window depends on the mainFormOptions.

            tempObserver = m_fragLadder as IObserver;
            // Add pre-defined symbols to modifications list
            m_fragLadder.fragmentLadderOptions.Attach(ref tempObserver);
            if (m_fragLadder.fragmentLadderOptions.modificationList.Count == 0)
            {
                m_fragLadder.fragmentLadderOptions.modificationList.Add('*', 79.9663326);
                m_fragLadder.fragmentLadderOptions.modificationList.Add('+', 14.01565);
                m_fragLadder.fragmentLadderOptions.modificationList.Add('@', 15.99492);
                m_fragLadder.fragmentLadderOptions.modificationList.Add('!', 57.02146);
                m_fragLadder.fragmentLadderOptions.modificationList.Add('&', 58.00548);
                m_fragLadder.fragmentLadderOptions.modificationList.Add('#', 71.03711);
                m_fragLadder.fragmentLadderOptions.modificationList.Add('$', 227.127);
                m_fragLadder.fragmentLadderOptions.modificationList.Add('%', 236.127);
                m_fragLadder.fragmentLadderOptions.modificationList.Add('~', 442.225);
                m_fragLadder.fragmentLadderOptions.modificationList.Add('`', 450.274);
            }

            m_plot.m_options.CopyOptions(m_plot.m_options);
            m_options.Hide();
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
            //handle ZoomOut
            if (e.Control)
            {
                if (e.KeyCode == m_plot.m_options.unzoomKey)
                {
                    m_plot.HandleZoomOut();
                    e.Handled = true;
                }
            }
        }

        public bool SynopsisLoaded { get; private set; }
        public bool DataLoaded { get; private set; }
        public string DataFileName = string.Empty;
        public double PrecursorMZ = 0;
        private string m_dataFileDirectory = string.Empty;
        private string m_dataFileLocation
        {
            get { return Path.Combine(m_dataFileDirectory, DataFileName); }
            set
            {
                m_dataFileDirectory = Path.GetDirectoryName(value);
                DataFileName = Path.GetFileName(value);
            }
        }
        private int m_currentScanNumber;
        private string m_currentPeptide;
        private string m_synopsisFileLocation;
        private bool m_isFragmentationModeETD = false;
        private List<Element> m_experimentalList;

        /// <summary>
        /// RunOpenDialog will open the custom SLOpenFileDialog box and prompt the user to
        /// open a experimental file and a theoretical file.
        /// </summary>
        public void RunOpenDialog()
        {
            var openDialog = new SLOpenFileDialog(m_synopsisFileLocation, m_dataFileLocation);
            var result = openDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (m_dataFileLocation == openDialog.m_dataPath && m_synopsisFileLocation == openDialog.m_synopsisPath)
                {
                    return;
                }
                m_dataFileLocation = openDialog.m_dataPath;
                m_synopsisFileLocation = openDialog.m_synopsisPath;

                if (m_synopsisFileLocation != string.Empty)
                {
                    try
                    {
                        // New code, uses PHRPReader
                        var reader = new PHRPReaderParser(m_synopsisFileLocation, m_fragLadder.fragmentLadderOptions);
                        m_dataView.SetDataTable(DataBuilder.GetDataTable(reader, out var synFileColumns));

                        m_dataView.SetColumnIndices(synFileColumns);
                        SynopsisLoaded = true;
                    }
                    catch (Exception ex)
                    {
                        SynopsisLoaded = false;
                        MessageBox.Show("There was an error opening the Synopsis file, are you sure you picked the right file?\n\n" + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace,
                            "Synopsis open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // select first row
                    m_dataView.HandleRowSelection();
                }
            }
        }

        // This function simply calls the combo box function within m_fragLadder
        public void callcombobox()
        {
            m_fragLadder.setComboBox();
        }

        /// <summary>
        /// This handle is called from the fragmentLadder.
        /// This is created for the purpose of adding a modified peptide to the current Instance List.
        /// </summary>
        /// <param name="Peptide">A peptide string.</param>
        public bool HandleInputPeptide(string Peptide)
        {
            if (m_currentScanNumber == 0)
            {
                MessageBox.Show(
                    "Please open a synopsis and data file and then load a scan from the data view to calculate: \"" +
                    Peptide + "\"");
                return false;
            }

            if (Peptide.Length == 1)
            {
                MessageBox.Show("Invalid peptide string (single character.");
                return false;
            }

            /*MG: Generating a regular expression string with the current modification
                 * symbols to check if the peptide string from the user is a valid peptide string.
                 * if its not, giving feedback to user and returning out*/
            var modList = new string(m_fragLadder.fragmentLadderOptions.modificationList.Keys.ToArray());
            var escapedModList = "";
            // The manual way, based on a list of metacharacters that need to be escaped
            /*foreach (char c in modList)
            {
                // Characters that must be escaped: \[^$.|?*+(){}
                if ("\\[^$.|?*+(){}".Contains(c))
                {
                    escapedModList += "\\" + c;
                }
                else
                {
                    escapedModList += c;
                }
            }*/
            // The automatic way, using .NET
            escapedModList = System.Text.RegularExpressions.Regex.Escape(modList);

            // One or more characters, A-Z, which may be followed by a valid modification symbol, that does not end with a modification symbol be itself?
            // The escaping of all symbols is likely a problem.
            //string spattern = "^([A-Z]{1,}[" + escapedModList + "]{0,1})*[^" + escapedModList + "]$";
            // Match any set of one or more groups of "character a-z followed by 0-2 valid modification symbols"
            var spattern = "([A-Z][" + escapedModList + "]{0,5})+";
            // This test will always be evaluate to 'not false' whenever there are lowercase characters or symbols that are not in the modification list,
            // or if the peptide begins with a modification symbol.
            // TODO: can a peptide begin with a modification symbol? (Not with any software Matt Monroe has written/uses - put mod after first amino acid)
            if (!System.Text.RegularExpressions.Regex.IsMatch(Peptide, "^" + spattern + "$"))
            {
                // Get a string with all invalid characters
                var invalid = System.Text.RegularExpressions.Regex.Replace(Peptide, spattern, "");
                if (modList.Contains(Peptide[0]))
                {
                    MessageBox.Show("The peptide string cannot start with a modification.");
                    return false;
                }
                if (System.Text.RegularExpressions.Regex.Replace(invalid, "[" + escapedModList + "]", "").Length == 0)
                {
                    MessageBox.Show("Invalid peptide string: Too many modifications on a single amino acid.");
                    return false;
                }

                MessageBox.Show("Invalid characters in peptide string \"" + invalid + "\", please remove the characters or define them in options/fragment ladder");
                return false;
            }

            // check to see if peptide exists in the ladder instances table and don't add a modification if it does
            var is_same = false;
            foreach (var instance in m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide])
            {
                if (instance.PeptideString == Peptide)
                {
                    is_same = true;
                }
            }

            if (!is_same)
            {
                var ladderBuilder =
                    new Views.FragmentLadderView.LadderInstanceBuilder();

                //use the builder director to crunch all the data
                var theoreticalList = m_builderDirector.BuildTheoryList(Peptide, m_isFragmentationModeETD,
                    this.m_fragLadder.fragmentLadderOptions.modificationList);
                var comparedList =
                    m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue,
                        m_mainForm.m_currentOptions.lowerToleranceValue, m_experimentalList, PrecursorMZ, ref theoreticalList);

                //now give the data to the views to display
                //Send the FragmentLadder and the Plot the data to show
                if (m_ladderInstancesTable.ContainsKey(m_currentScanNumber.ToString() + m_currentPeptide))
                {
                    m_currentInstance = ladderBuilder.GenerateInstance(theoreticalList, Peptide,
                        m_fragLadder.fragmentLadderOptions.modificationList);
                    m_currentInstance.scanAndPeptide = m_currentScanNumber.ToString() + "|" + Peptide;
                    m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide].Add(m_currentInstance);
                    m_currentInstance = m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide][0];
                    m_fragLadder.generateLadderFromSelection(0.0, m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide]);
                    m_plot.PlotData(comparedList, m_currentScanNumber.ToString(), Peptide);
                }
                else
                {
                    m_plot.PlotData(comparedList, m_currentScanNumber.ToString(), m_currentPeptide);
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// This function will remove a specific modification from the ladder instances table
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        public void RemoveModificationFromList(int index)
        {
            var x = m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide][index].PeptideString;
            m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide].RemoveAt(index);
            //m_ladderInstancesTable.Remove(((List<LadderInstance>)m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide])[index]);
            m_dataView.HandleRowSelection();
        }

        /// <summary>
        /// This function handle is called from both the fragmentladder and the dataview.
        /// This function handles the case when the user selects a peptide and scan number in dataview.
        /// </summary>
        /// <param name="ScanNumber">The scan number that relates to the experimental data.</param>
        /// <param name="Peptide">The peptide sequence.</param>
        public void HandleSelectScanAndPeptide(string ScanNumber, string Peptide)
        {
            DataLoaded = true;
            try
            {
                var ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

                m_currentScanNumber = Convert.ToInt32(ScanNumber);
                m_currentPeptide = Peptide;
                //use the builder director to crunch all the data
                var theoreticalList = m_builderDirector.BuildTheoryList(Peptide, m_isFragmentationModeETD, this.m_fragLadder.fragmentLadderOptions.modificationList);
                m_experimentalList = m_builderDirector.BuildActualList(Convert.ToInt32(ScanNumber), m_dataFileLocation);
                var comparedList = m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue, m_mainForm.m_currentOptions.lowerToleranceValue, m_experimentalList, PrecursorMZ, ref theoreticalList);

                //now give the data to the views to display
                //Send the FragmentLadder and the Plot the data to show
                if (m_ladderInstancesTable.ContainsKey(ScanNumber + Peptide))
                {
                    m_currentInstance = m_ladderInstancesTable[ScanNumber + Peptide][0];
                    m_fragLadder.generateLadderFromSelection(0.0, m_ladderInstancesTable[ScanNumber + Peptide]);
                }
                else
                {
                    var tempList = new List<LadderInstance>();
                    var tempInstance = ladderBuilder.GenerateInstance(theoreticalList, Peptide, m_fragLadder.fragmentLadderOptions.modificationList);
                    tempInstance.scanAndPeptide = ScanNumber + "|" + Peptide;
                    tempList.Add(tempInstance);
                    m_ladderInstancesTable.Add(ScanNumber + Peptide, tempList);

                    m_currentInstance = m_ladderInstancesTable[ScanNumber + Peptide][0];
                    m_fragLadder.generateLadderFromSelection(0.0, m_ladderInstancesTable[ScanNumber + Peptide]);
                }
                m_plot.PlotData(comparedList, ScanNumber, Peptide);
                //m_experimentalList.Clear();
                //m_experimentalList = null;
            }
            catch (InvalidProgramException ex)
            {
                if (ex.Message.Contains("Invalid File Type"))
                {
                    MessageBox.Show("Invalid File Type for the Data file.  Only .mzXml, .mzData, and .raw types are allowed.", "File Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Focuses the plot on a specific point.  This is used for when the user clicks the fragment ladder and we want to hilight that point
        /// </summary>
        /// <param name="focusValue">The x value to focus on in the plot</param>
        public void FocusPlotOnPoint(double focusValue)
        {
            m_plot.FocusPlotOnPoint(focusValue);
        }

        /// <summary>
        /// Handles replotting the data when the fragment ladder peptide has changed without selecting a new set of data
        /// </summary>
        public void HandlefragmentLadderModeChange(bool setMode)
        {
            m_isFragmentationModeETD = setMode;

            var allKeyValues = m_ladderInstancesTable.Keys.ToArray();
            var ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

            foreach (var currentKey in allKeyValues)
            {
                var currentLadderInstances = m_ladderInstancesTable[currentKey][0];
                m_ladderInstancesTable.Remove(currentKey);
                var newInstanceList = new List<LadderInstance>();
                //Give Theoretical List the modified Peptide
                //Assuming The Frag ladder will add the PeptideString of the modified thing.
                var theoreticalList = m_builderDirector.BuildTheoryList(currentLadderInstances.PeptideString, m_isFragmentationModeETD, this.m_fragLadder.fragmentLadderOptions.modificationList);
                //Give experimental List the original Peptide.
                //WARNING : ASSUMEING THE SCAN NUMBER KEY IS AN INTERGER
                //THIS MAY NOT BE THE CASE IF PARSER CHANGES
                var experimentalList = m_builderDirector.BuildActualList(int.Parse(currentLadderInstances.scanNumberString), m_dataFileLocation);
                //build the compared list
                var comparedList = m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue, m_mainForm.m_currentOptions.lowerToleranceValue, experimentalList, PrecursorMZ, ref theoreticalList);

                //Generate the Instance based on the builder.
                var newLadder = ladderBuilder.GenerateInstance(theoreticalList, currentLadderInstances.PeptideString, m_fragLadder.fragmentLadderOptions.modificationList);

                newLadder.scanAndPeptide = currentLadderInstances.scanAndPeptide;

                //Add to List
                newInstanceList.Add(newLadder);

                //Add List Of Instances to the HashTable.
                m_ladderInstancesTable.Add(currentKey, newInstanceList);
            }

            //Going to delete all the values and recalculate the original.
            m_ladderInstancesTable.Remove(m_currentScanNumber.ToString() + m_currentPeptide);
            //We should just then call HandleSelectScanAndPeptide
            HandleSelectScanAndPeptide(m_currentScanNumber.ToString(), m_currentPeptide);
        }

        /// <summary>
        /// This will be called when the fragment ladder is to clear ladder instances.
        /// </summary>
        public void ClearLadderInstances()
        {
           m_ladderInstancesTable.Remove(m_currentScanNumber.ToString() + m_currentPeptide);
           HandleSelectScanAndPeptide(m_currentScanNumber.ToString(), m_currentPeptide);
        }

        /// <summary>
        /// This function handles recalculating all the ladder instances in the hashtable.
        /// This is normally called when the fragment ladder options change.
        /// </summary>
        public void HandleRecalculateAllInHashCode()
        {
            //Get all the Keys in the HashTable.
            var allKeyValues = m_ladderInstancesTable.Keys.ToArray();
            var ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

            foreach (var currentKey in allKeyValues)
            {
                var currentLadderInstances = m_ladderInstancesTable[currentKey];
                m_ladderInstancesTable.Remove(currentKey);
                var newInstanceList = new List<LadderInstance>();
                foreach (var currentInstance in currentLadderInstances)
                {
                    //Give Theoretical List the modified Peptide
                    //Assuming The Frag ladder will add the PeptideString of the modified thing.
                    var theoreticalList = m_builderDirector.BuildTheoryList(currentInstance.PeptideString, m_isFragmentationModeETD, this.m_fragLadder.fragmentLadderOptions.modificationList);
                    //Give experimental List the original Peptide.
                    //WARNING : ASSUMEING THE SCAN NUMBER KEY IS AN INTERGER
                    //THIS MAY NOT BE THE CASE IF PARSER CHANGES
                    var experimentalList = m_builderDirector.BuildActualList(int.Parse(currentInstance.scanNumberString), m_dataFileLocation);
                    //build the compared list
                    var comparedList = m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue, m_mainForm.m_currentOptions.lowerToleranceValue, experimentalList, PrecursorMZ, ref theoreticalList);

                    //Generate the Instance based on the builder.
                    var newLadder = ladderBuilder.GenerateInstance(theoreticalList, currentInstance.PeptideString, m_fragLadder.fragmentLadderOptions.modificationList);

                    newLadder.scanAndPeptide = currentInstance.scanAndPeptide;

                    //Add to List
                    newInstanceList.Add(newLadder);
                }
                //Add List Of Instances to the HashTable.
                m_ladderInstancesTable.Add(currentKey, newInstanceList);
            }
            if (m_ladderInstancesTable.Count > 0)
            {
                m_currentInstance = m_ladderInstancesTable[m_currentScanNumber + m_currentPeptide][0];
                m_fragLadder.generateLadderFromSelection(0.0, m_ladderInstancesTable[m_currentScanNumber + m_currentPeptide]);
            }
        }

        /// <summary>
        /// This function is called by the main form.
        /// This opens the Options View Controller with the desired tab selected.
        /// </summary>
        /// <param name="startingTab">The title of the desired starting tab.</param>
        public void OpenOptionsMenu(string startingTab)
        {
            m_options.SelectTab(startingTab);
            m_options.Show();
            m_options.BringToFront();
        }

        public void FocusOnControl(Control attentionSeeker)
        {
            if (m_mainForm.Contains(attentionSeeker))
            {
                m_mainForm.ActiveControl = attentionSeeker;
            }
        }

        /// <summary>
        /// This function is used in the fragment ladder.
        /// When the user selects a different tab the graph will plot the spectrum of that current instance.
        /// </summary>
        /// <param name="newInstanceIndex">The index in the current List of ladder instances.</param>
        public void UpdateCurrentInstance(int newInstanceIndex)
        {
            m_currentInstance = ((List<LadderInstance>)m_ladderInstancesTable[m_currentScanNumber + m_currentPeptide])[newInstanceIndex];
            m_currentScanNumber = int.Parse(m_currentInstance.scanNumberString);

            var theoreticalList = m_builderDirector.BuildTheoryList(m_currentInstance.PeptideString, m_isFragmentationModeETD, this.m_fragLadder.fragmentLadderOptions.modificationList);
            var comparedList = m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue, m_mainForm.m_currentOptions.lowerToleranceValue, m_experimentalList, PrecursorMZ, ref theoreticalList);
            var peptide = m_currentInstance.PeptideString;

            m_plot.PlotData(comparedList, m_currentScanNumber.ToString(), peptide);

            m_fragLadder.setPeptideTextBox(peptide);
        }

        /// <summary>
        /// A property to get the current selected ladder instance.
        /// </summary>
        /// <returns>the current selected ladder instance or null if none is selected.</returns>
        public LadderInstance GetCurrentInstance()
        {
            return m_currentInstance;
        }

        /// <summary>
        /// This function calls the HashtableXmlSerializer.WriteHashTable fuction and requests a write to the given or save location.
        /// </summary>
        /// <param name="fileLocation">The location in which you want to save the .spwf</param>
        public void HandleSaveWorkFile(string fileLocation)
        {
            if (fileLocation != null && fileLocation != "")
                m_workFileWriter.WriteLadderInstanceDictionary(fileLocation, this.m_ladderInstancesTable);
            else
                m_workFileWriter.WriteLadderInstanceDictionary(this.m_ladderInstancesTable);
        }

        /// <summary>
        /// This function calls the HashtableXmlSerializer.ReadXmlWorkFile with the given file location.
        /// This function fully populates the m_ladderInstancesTable with the data in the .spwf.
        /// </summary>
        /// <param name="fileLocation">The location of the .spwf that the user wants to open.</param>
        public void HandleOpenWorkFile(string fileLocation)
        {
            if(File.Exists(fileLocation))
            {
                this.m_ladderInstancesTable = m_workFileWriter.ReadXmlWorkFile(fileLocation);
            }
        }

        /// <summary>
        /// Handles A batch save
        /// </summary>
        public void HandleBatchSave(string directory, string baseName, string saveType, bool useScansInGrid, bool usePeptideAndScanName, bool addDatasetName, UpdateLabelDelegate updateLabelCallback, ref bool cancelSearch)
        {
            updateLabelCallback("Starting Batch Save...");
            m_batchSaveCounter = 0;

            foreach (var row in m_dataView.GetPeptidesAndScansInGrid(useScansInGrid))
            {
                if (!cancelSearch)
                {
                    var nextFilebase = baseName;
                    if (!string.IsNullOrWhiteSpace(row.DatasetName))
                    {
                        DataFileName = row.DatasetName;
                        if (addDatasetName)
                        {
                            nextFilebase += "_" + Path.GetFileNameWithoutExtension(row.DatasetName);
                        }
                    }
                    PrecursorMZ = row.DblPrecursorMZ;

                    var nextFileName = createNextFileName(nextFilebase, usePeptideAndScanName, row.Peptide, row.ScanNumber) + saveType;
                    updateLabelCallback("Saving \"" + nextFileName + "\"");

                    this.HandleSelectScanAndPeptide(row.ScanNumber, row.Peptide);
                    m_plot.SavePlotImageAs(Path.Combine(directory, nextFileName));
                }
            }

            updateLabelCallback("Finished Batch Save");
        }

        /// <summary>
        /// Handles a plot save
        /// </summary>
        public void HandlePlotSave(string directory, string fileName, string saveType)
        {
            m_plot.SavePlotImageAs(Path.Combine(directory, fileName + saveType));
        }

        /// <summary>
        /// Generates a new filename to use based off of the users options and the information used in the plot
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="peptide"></param>
        /// <param name="usePeptide"></param>
        /// <param name="scanNumber"></param>
        /// <param name="useScanNumber"></param>
        /// <returns></returns>
        private static string createNextFileName(string baseName, bool usePeptideAndScanName, string peptide, string scanNumber)
        {
            var nextFileName = baseName;

            if (usePeptideAndScanName)
            {
                nextFileName += "_" + peptide + "_" + scanNumber;
            }
            else
            {
                //Attach a unique number to the saved file, since we are not garenteed uniqueness from peptide or scan number alone.
                nextFileName += "_" + String.Format("{0:0000}", m_batchSaveCounter);
            }

            // Magic number 232, appears to be the max filename legth in Windows.  For now, just truncate what they have.
            if (nextFileName.Length > 232)
            {
                nextFileName = nextFileName.Remove(232);
            }

            m_batchSaveCounter++;
            return nextFileName;
        }
        private static int m_batchSaveCounter = 0;

        public delegate void UpdateLabelDelegate(string newText);
    }
}
