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
        #region MEMBERS

        public MainForm m_mainForm;
        public SLPlot m_plot;
        public DataView m_dataView;
        public Views.FragmentLadderView.FragmentLadderView m_fragLadder;

        private OptionsViewController m_options;

        private BuilderDirector m_builderDirector;

        private Hashtable m_ladderInstancesTable;

        private LadderInstance m_currentInstance;

        private HashtableXmlSerializer m_workFileWriter;

        #endregion

        #region CONSTRUCTOR


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
            string fileLocation = "";
            bool createFileFlag = false;

            /*********************This is to read the UserProfile for spectrumLook**********************/
            try
            {
                reader = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\UserProfile.spuf", FileMode.Open, FileAccess.Read);
                fileLocation = "UserProfile2.spuf";
            }
            catch (Exception ex)
            {
                createFileFlag = true;
            }

            if (reader != null)
            {
                try
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
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
            /***************************************************************/
            //This is used to read and write the the .spuf
            m_workFileWriter = new HashtableXmlSerializer();

            //Options
            m_options = new OptionsViewController(m_plot.m_options, m_mainForm.m_currentOptions, m_fragLadder.fragmentLadderOptions ,System.IO.Directory.GetCurrentDirectory() + "\\UserProfile.spuf", createFileFlag);

            //attach all of the observers to the subjects
            IObserver tempObserver = m_plot as IObserver;
            m_plot.m_options.Attach(ref tempObserver);

            tempObserver = m_mainForm as IObserver;
            m_mainForm.m_currentOptions.Attach(ref tempObserver);

            tempObserver = m_options as IObserver;
            m_mainForm.m_currentOptions.Attach(ref tempObserver);
            m_plot.m_options.Attach(ref tempObserver);//This is because the plot window depends on the mainFormOptions.

            tempObserver = m_fragLadder as IObserver;
            m_fragLadder.fragmentLadderOptions.Attach(ref tempObserver);

            m_plot.m_options.CopyOptions(m_plot.m_options);
            m_options.Hide();

            m_ladderInstancesTable = new Hashtable();
        }

        #endregion

        #region HOTKEY MANAGEMENT
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
                if (e.KeyCode == Keys.P)
                {
                    m_plot.MySaveAs("plot");
                }
            }
        }

        #endregion

        #region CALLBACK FUCNTIONS

        private int m_currentScanNumber;
        private string m_currentPeptide;
        private string m_dataFileLocation;
        private string m_synopsisFileLocation;
        private bool m_isFragmentationModeCID = false;
        private List<Element> m_experimentalList;

        /// <summary>
        /// RunOpenDialog will open the custom SLOpenFileDialog box and prompt the user to
        /// open a experimental file and a theoretical file.
        /// </summary>
        public void RunOpenDialog()
        {
            SLOpenFileDialog openDialog = new SLOpenFileDialog(m_synopsisFileLocation, m_dataFileLocation);
            DialogResult result = openDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                m_dataFileLocation = openDialog.m_dataPath;
                m_synopsisFileLocation = openDialog.m_synopsisPath;

                if (m_synopsisFileLocation != string.Empty)
                {
                    m_dataView.SetDataTable(DataBuilder.GetDataTable(new SequestParser(m_synopsisFileLocation)));
                }
            }
        }

        /// <summary>
        /// This handle is called from the fragmentLadder.
        /// This is created for the purpose of adding a modified peptide to the current Instance List.
        /// </summary>
        /// <param name="Peptide">A peptide string.</param>
        public void HandleInputPeptide(string Peptide)
        {
            SpectrumLook.Views.FragmentLadderView.LadderInstanceBuilder ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

            //use the builder director to crunch all the data
            List<Element> theoreticalList = m_builderDirector.BuildTheoryList(Peptide, m_isFragmentationModeCID, this.m_fragLadder.fragmentLadderOptions.modificationList);
            List<Element> comparedList = m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue, m_experimentalList, ref theoreticalList);

            //now give the data to the views to display
            //Send the FragmentLadder and the Plot the data to show
            if (m_ladderInstancesTable.ContainsKey(m_currentScanNumber.ToString() + m_currentPeptide))
            {
                m_currentInstance = ladderBuilder.GenerateInstance(theoreticalList, Peptide,m_fragLadder.fragmentLadderOptions.modificationList);
                m_currentInstance.scanAndPeptide = m_currentScanNumber.ToString() + "|" + Peptide;
                List<LadderInstance> tmpListHolder = (List<LadderInstance>)m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide];
                tmpListHolder.Add(m_currentInstance);
                m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide] = tmpListHolder;
                m_currentInstance = ((List<LadderInstance>)m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide])[0];
                m_fragLadder.generateLadderFromSelection(0.0, (List<LadderInstance>)m_ladderInstancesTable[m_currentScanNumber.ToString() + m_currentPeptide]);
                m_plot.PlotData(comparedList, m_currentScanNumber.ToString(), Peptide);
            }
            else
            {
                m_plot.PlotData(comparedList, m_currentScanNumber.ToString(), m_currentPeptide);
            }
            
        }


        /// <summary>
        /// This function handle is called from both the fragmentladder and the dataview.
        /// This function handles the case when the user selects a peptide and scan number in dataview.
        /// </summary>
        /// <param name="ScanNumber">The scan number that relates to the experimental data.</param>
        /// <param name="Peptide">The peptide sequence.</param>
        public void HandleSelectScanAndPeptide(string ScanNumber, string Peptide)
        {
            SpectrumLook.Views.FragmentLadderView.LadderInstanceBuilder ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

            m_currentScanNumber = Convert.ToInt32(ScanNumber);
            m_currentPeptide = Peptide;
            //use the builder director to crunch all the data
            List<Element> theoreticalList = m_builderDirector.BuildTheoryList(Peptide, m_isFragmentationModeCID, this.m_fragLadder.fragmentLadderOptions.modificationList);
            m_experimentalList = m_builderDirector.BuildActualList(Convert.ToInt32(ScanNumber), m_dataFileLocation);
            List<Element> comparedList = m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue, m_experimentalList, ref theoreticalList);

            //now give the data to the views to display
            //Send the FragmentLadder and the Plot the data to show
            if (m_ladderInstancesTable.ContainsKey(ScanNumber + Peptide))
            {
                m_currentInstance = ((List<LadderInstance>)m_ladderInstancesTable[ScanNumber + Peptide])[0];
                m_fragLadder.generateLadderFromSelection(0.0, (List<LadderInstance>)m_ladderInstancesTable[ScanNumber + Peptide]);
            }
            else
            {
                List<LadderInstance> tempList = new List<LadderInstance>();
                LadderInstance tempInstance = ladderBuilder.GenerateInstance(theoreticalList, Peptide, m_fragLadder.fragmentLadderOptions.modificationList);
                tempInstance.scanAndPeptide = ScanNumber + "|" + Peptide;
                tempList.Add(tempInstance);
                m_ladderInstancesTable.Add(ScanNumber + Peptide, tempList);

                m_currentInstance = ((List<LadderInstance>)m_ladderInstancesTable[ScanNumber + Peptide])[0];
                m_fragLadder.generateLadderFromSelection(0.0, (List<LadderInstance>)m_ladderInstancesTable[ScanNumber + Peptide]);
            }
            m_plot.PlotData(comparedList, ScanNumber, Peptide);
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
            m_isFragmentationModeCID = setMode;

            string[] allKeyValues = new string[m_ladderInstancesTable.Keys.Count];
            m_ladderInstancesTable.Keys.CopyTo(allKeyValues, 0);
            SpectrumLook.Views.FragmentLadderView.LadderInstanceBuilder ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

            foreach (string currentKey in allKeyValues)
            {
                LadderInstance currentLadderInstances = ((List<LadderInstance>)m_ladderInstancesTable[currentKey])[0];
                m_ladderInstancesTable.Remove(currentKey);
                List<LadderInstance> newInstanceList = new List<LadderInstance>();
                //Give Theoretical List the modified Peptide
                //Assuming The Frag ladder will add the PeptideString of the modified thing.
                List<Element> theoreticalList = m_builderDirector.BuildTheoryList(currentLadderInstances.PeptideString, m_isFragmentationModeCID, this.m_fragLadder.fragmentLadderOptions.modificationList);
                //Give experimental List the original Peptide.
                //WARNING : ASSUMEING THE SCAN NUMBER KEY IS AN INTERGER
                //THIS MAY NOT BE THE CASE IF PARSER CHANGES
                List<Element> experimentalList = m_builderDirector.BuildActualList(int.Parse(currentLadderInstances.scanNumberString), m_dataFileLocation);
                //build the compared list
                List<Element> comparedList = m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue, experimentalList, ref theoreticalList);

                //Generate the Instance based on the builder.
                LadderInstance newLadder = ladderBuilder.GenerateInstance(theoreticalList, currentLadderInstances.PeptideString, m_fragLadder.fragmentLadderOptions.modificationList);

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
            string[] allKeyValues = new string[m_ladderInstancesTable.Keys.Count];
            m_ladderInstancesTable.Keys.CopyTo(allKeyValues, 0);
            SpectrumLook.Views.FragmentLadderView.LadderInstanceBuilder ladderBuilder = new Views.FragmentLadderView.LadderInstanceBuilder();

            foreach (string currentKey in allKeyValues)
            {
                List<LadderInstance> currentLadderInstances = (List<LadderInstance>)m_ladderInstancesTable[currentKey];
                m_ladderInstancesTable.Remove(currentKey);
                List<LadderInstance> newInstanceList = new List<LadderInstance>();
                foreach (LadderInstance currentInstance in currentLadderInstances)
                {
                    //Give Theoretical List the modified Peptide
                    //Assuming The Frag ladder will add the PeptideString of the modified thing.
                    List<Element> theoreticalList = m_builderDirector.BuildTheoryList(currentInstance.PeptideString, m_isFragmentationModeCID, this.m_fragLadder.fragmentLadderOptions.modificationList);
                    //Give experimental List the original Peptide.
                    //WARNING : ASSUMEING THE SCAN NUMBER KEY IS AN INTERGER
                    //THIS MAY NOT BE THE CASE IF PARSER CHANGES
                    List<Element> experimentalList = m_builderDirector.BuildActualList(int.Parse(currentInstance.scanNumberString), m_dataFileLocation);
                    //build the compared list
                    List<Element> comparedList = m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue, experimentalList, ref theoreticalList);
                    
                    //Generate the Instance based on the builder.
                    LadderInstance newLadder = ladderBuilder.GenerateInstance(theoreticalList, currentInstance.PeptideString, m_fragLadder.fragmentLadderOptions.modificationList);

                    newLadder.scanAndPeptide = currentInstance.scanAndPeptide;

                    //Add to List
                    newInstanceList.Add(newLadder);
                }
                //Add List Of Instances to the HashTable.
                m_ladderInstancesTable.Add(currentKey, newInstanceList);
            }
            if (m_ladderInstancesTable.Count > 0)
            {
                m_currentInstance = ((List<LadderInstance>)m_ladderInstancesTable[m_currentScanNumber + m_currentPeptide])[0];
                m_fragLadder.generateLadderFromSelection(0.0, (List<LadderInstance>)m_ladderInstancesTable[m_currentScanNumber + m_currentPeptide]);
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

            List<Element> theoreticalList = m_builderDirector.BuildTheoryList(m_currentInstance.PeptideString, m_isFragmentationModeCID, this.m_fragLadder.fragmentLadderOptions.modificationList);
            List<Element> comparedList = m_builderDirector.BuildComparedList(m_mainForm.m_currentOptions.toleranceValue, m_experimentalList, ref theoreticalList);
            string peptide = m_currentInstance.PeptideString;

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
                m_workFileWriter.WriteHashTable(fileLocation, this.m_ladderInstancesTable);
            else
                m_workFileWriter.WriteHashTable(this.m_ladderInstancesTable);
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

        #endregion
    }
}
