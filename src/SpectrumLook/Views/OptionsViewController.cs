using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SpectrumLook.Views
{
    public partial class OptionsViewController : Form, IObserver
    {
        // 0-10, 13-15  Plot
        //11-12         Main
        //16-17         Frag Ladder
        //18            Main lower Tolerance Value
        // TODO: THIS DOESN'T WORK FOR ANYTHING BUT PRIMITIVES (MAYBE FOR PRIMITIVES)
        private object[] m_valuesForCancel;

        private readonly PlotOptions m_plotOptions;

        private readonly MainFormOptions m_mainFormOptions;

        private readonly Options.FragmentLadderOptions m_fragLadderOptions;

        private readonly SpectrumLook.Views.FragmentLadderView.FragmentLadderView m_fragLadder;

        private Color Unmatched;

        private Color Matched;

        private bool m_createProfile;

        private string m_profileLocation;

        private const int m_numCancelOptions = 19;

        // m_options = new OptionsViewController(m_plot.m_options, m_mainForm.m_currentOptions, m_fragLadder.fragmentLadderOptions ,System.IO.Directory.GetCurrentDirectory() + "\\UserProfile.spuf", createFileFlag);
        public OptionsViewController(PlotOptions referencePlotOptions, MainFormOptions referenceMainFormOptions, Options.FragmentLadderOptions fragmentLadderOptions, string profileLocation, bool createProfile, Views.FragmentLadderView.FragmentLadderView m_fragmentLadder)
        {
            InitializeComponent();

            m_valuesForCancel = new object[m_numCancelOptions];

            for (var i = 0; i < m_numCancelOptions; ++i)
            {
                m_valuesForCancel[i] = null;
            }

            m_plotOptions = referencePlotOptions;
            m_mainFormOptions = referenceMainFormOptions;
            m_fragLadderOptions = fragmentLadderOptions;
            m_fragLadder = m_fragmentLadder;
            Matched = m_plotOptions.matchedColor;
            Unmatched = m_plotOptions.unmatchedColor;

            FillKeyOptions();
            SaveValuesForCancel();
            m_createProfile = createProfile;
            m_profileLocation = profileLocation;

            mainProfileFileLocationBox.Text = m_profileLocation;

            dataGridViewModList.Columns.Add("symbol", "Symbol");
            dataGridViewModList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewModList.Columns.Add("mass", "Mass");
            dataGridViewModList.RowHeadersVisible = false;
            dataGridViewModList.Click += dataGridViewModList_Click;
            dataGridViewModList.EditMode = DataGridViewEditMode.EditProgrammatically; // Disable manual edit

            UpdateOptions();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            if (m_profileLocation != "")
            {
                if (m_createProfile)
                {
                    var writer = new FileStream(m_profileLocation, FileMode.Create, FileAccess.Write);

                    try
                    {
                        var binaryFormatter = new BinaryFormatter();
                        var tmpPlotOptions = new PlotOptions(m_plotOptions);
                        binaryFormatter.Serialize(writer, tmpPlotOptions);
                        var tmpMainOptions = new MainFormOptions(m_mainFormOptions);
                        binaryFormatter.Serialize(writer, tmpMainOptions);
                        var tmpFragLadder = new Options.FragmentLadderOptions(m_fragLadderOptions);
                        binaryFormatter.Serialize(writer, tmpFragLadder);
                    }
                    finally
                    {
                        writer.Close();
                    }
                }
            }
            Hide();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                SaveValuesForCancel();
            }
        }

        public void SelectTab(string newTab)
        {
            UpdateModList();
            if (newTab == "Plot Options")
            {
                optionTabsPage.SelectTab(0);
            }
            else if (newTab == "Fragment Ladder Options")
            {
                optionTabsPage.SelectTab(1);
            }
            else if (newTab == "General Options")
            {
                optionTabsPage.SelectTab(2);
            }
        }

        private void UpdateOptions()
        {
            // PLOT UPDATING
            plotSnappingCursor.Checked = m_plotOptions.showSnappingCursor;
            plotShowLegend.Checked = m_plotOptions.showLegend;
            plotHideUnmatchedData.Checked = m_plotOptions.hideUnmatched;
            plotHorizontalZoom.Checked = m_plotOptions.zoomHorizontal;
            plotBoxZoom.Checked = (!(m_plotOptions.zoomHorizontal));
            plotUnzoomKeyComboBox.SelectedItem = m_plotOptions.unzoomKey;
            plotAnnotationColor.BackColor = m_plotOptions.annotationColor;
            plotTextSize.Text = m_plotOptions.annotationTextSize.ToString();
            plotAnnotationPercentBox.Text = m_plotOptions.annotationPercent.ToString();
            plotRightClickUnzoom.Checked = m_plotOptions.rightClickUnzoom;
            plotNumberOfPlotsTextBox.Text = m_plotOptions.numberOfPlots.ToString();

            // MAIN UPDATING
            mainDetachPlotCheckBox.Checked = !(m_mainFormOptions.isPlotInMainForm);
            mainMatchedColorSample.BackColor = m_plotOptions.matchedColor;
            mainUnmatchedColorSample.BackColor = m_plotOptions.unmatchedColor;
            lowerMatchingToleranceBox.Text = m_mainFormOptions.lowerToleranceValue.ToString();
            mainMatchingToleranceBox.Text = m_mainFormOptions.toleranceValue.ToString();

            // DATA VIEW

            // FRAGMENT LADDER
            UpdateModList();
        }

        public void UpdateModList()
        {
            dataGridViewModList.Rows.Clear();
            foreach (var modPair in m_fragLadderOptions.modificationList)
            {
                dataGridViewModList.Rows.Add(new object[] { modPair.Key, modPair.Value });
            }
        }

        void IObserver.UpdateObserver()
        {
            UpdateOptions();
        }

        private void FillKeyOptions()
        {
            var keyTypes = typeof(Keys);
            var keyValues = (Keys[])keyTypes.GetEnumValues();

            foreach (var targetKey in keyValues)
            {
                plotUnzoomKeyComboBox.Items.Add(targetKey);
            }
        }

        private void mainDetachPlotCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            m_mainFormOptions.isPlotInMainForm = !(mainDetachPlotCheckBox.Checked);
        }

        private void plotSnappingCursor_CheckedChanged(object sender, EventArgs e)
        {
            m_plotOptions.showSnappingCursor = plotSnappingCursor.Checked;
        }

        private void plotShowLegend_CheckedChanged(object sender, EventArgs e)
        {
            m_plotOptions.showLegend = plotShowLegend.Checked;
        }

        private void plotHideUnmatchedData_CheckedChanged(object sender, EventArgs e)
        {
            m_plotOptions.hideUnmatched = plotHideUnmatchedData.Checked;
        }

        private void checkBoxRightClickUnzoom_CheckedChanged(object sender, EventArgs e)
        {
            m_plotOptions.rightClickUnzoom = plotRightClickUnzoom.Checked;
        }

        private void checkBoxHidePlotTools_CheckedChanged(object sender, EventArgs e)
        {
            m_plotOptions.hidePlotTools = plotHidePlotTools.Checked;
        }

        private void plotHorizontalZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (plotHorizontalZoom.Checked)
            {
                m_plotOptions.zoomHorizontal = true;
                plotBoxZoom.Checked = false;
            }
        }

        private void plotBoxZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (plotBoxZoom.Checked)
            {
                m_plotOptions.zoomHorizontal = false;
                plotHorizontalZoom.Checked = false;
            }
        }

        private void plotUnzoomKeyComboBox_Leave(object sender, EventArgs e)
        {
            Keys key;
            try
            {
                key = (Keys)plotUnzoomKeyComboBox.SelectedItem;
                m_plotOptions.unzoomKey = key;
            }
            catch
            {
                plotUnzoomKeyComboBox.SelectedItem = m_plotOptions.unzoomKey;
            }
        }

        private void plotFragLadderSelectBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(plotFragLadderSelectBox.Text, out var outputValue))
            {
                plotFragLadderSelectBox.Text = "";
            }
            else
            {
                if (outputValue > 0)
                {
                    m_plotOptions.focusOffset = outputValue;
                }
                else
                {
                    plotFragLadderSelectBox.Text = "";
                }
            }
        }

        private void plotAnnotationPercentBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newPercent = Convert.ToInt32(plotAnnotationPercentBox.Text);
                if (newPercent > 100 || newPercent < 0)
                {
                    throw new Exception();
                }
                m_plotOptions.annotationPercent = newPercent;
            }
            catch
            {
                plotAnnotationPercentBox.Text = m_plotOptions.annotationPercent.ToString();
            }
        }

        private void plotTextSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newSize = Convert.ToInt32(plotTextSize.Text);
                if (newSize > 100 || newSize < 0)
                {
                    throw new Exception();
                }
                m_plotOptions.annotationTextSize = newSize;
            }
            catch
            {
                plotTextSize.Text = m_plotOptions.annotationTextSize.ToString();
            }
        }

        private void plotChangeColorOpenButton_Click(object sender, EventArgs e)
        {
            var outputResult = colorDialog.ShowDialog();
            if (outputResult == DialogResult.OK)
            {
                m_plotOptions.annotationColor = colorDialog.Color;
                plotAnnotationColor.BackColor = m_plotOptions.annotationColor;
            }
        }

        private void numberOfPlotsTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                var newAmmount = Convert.ToInt32(plotNumberOfPlotsTextBox.Text);
                if (newAmmount > 100 || newAmmount < 1)
                {
                    throw new Exception();
                }
                m_plotOptions.replot = true;
                m_plotOptions.numberOfPlots = newAmmount;
            }
            catch
            {
                plotNumberOfPlotsTextBox.Text = m_plotOptions.numberOfPlots.ToString();
            }
        }

        private void mainMatchColorChangeButton_Click(object sender, EventArgs e)
        {
            var outputResult = colorDialog.ShowDialog();
            if (outputResult == DialogResult.OK)
            {
                Matched = colorDialog.Color;
                // m_plotOptions.matchedColor = colorDialog.Color;
                mainMatchedColorSample.BackColor = Matched;
            }
        }

        private void mainUnmatchColorChangeButton_Click(object sender, EventArgs e)
        {
            var outputResult = colorDialog.ShowDialog();
            if (outputResult == DialogResult.OK)
            {
                Unmatched = colorDialog.Color;
                // m_plotOptions.unmatchedColor = colorDialog.Color;
                mainUnmatchedColorSample.BackColor = Unmatched;
            }
        }

        private void mainMatchingToleranceBox_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(mainMatchingToleranceBox.Text, out var outputValue))
            {
                mainMatchingToleranceBox.Text = "";
            }
            else
            {
                if (outputValue > 1 || outputValue < 0)
                {
                    MessageBox.Show("Please insert a valid tolerance level (0-1)!\n");
                    mainMatchingToleranceBox.Text = "";
                    return;
                }

                m_mainFormOptions.toleranceValue = outputValue;
            }
        }

        private void lowerMatchingToleranceBox_TextChanged(object sender, EventArgs e)
        {
            if (!double.TryParse(lowerMatchingToleranceBox.Text, out var outputValue))
            {
                lowerMatchingToleranceBox.Text = "";
            }
            else
            {
                if (outputValue > 1 || outputValue < 0)
                {
                    MessageBox.Show("Lower tolerance level must be greater than 0 and less than upper tolerance level.\n");
                    lowerMatchingToleranceBox.Text = "";
                    return;
                }

                m_mainFormOptions.lowerToleranceValue = outputValue;
            }
        }

        private void dataGridViewModList_Click(object sender, EventArgs e)
        {
            var selectedCell = dataGridViewModList.CurrentCell;
            var row = selectedCell.RowIndex;
            var symbol = (char?)(dataGridViewModList.Rows[row].Cells[0].Value);
            var mass = (double?)(dataGridViewModList.Rows[row].Cells[1].Value);

            // Configure values for and open a DialogBox for modifying modification data.
            var strSymbol = symbol == null ? null : symbol.ToString();
            var strMass = mass == null ? null : mass.ToString();
            var usedSymbols = "";
            foreach (DataGridViewRow rowData in dataGridViewModList.Rows)
            {
                if (rowData.Cells[0].Value != null)
                {
                    usedSymbols += rowData.Cells[0].Value.ToString();
                }
            }
            var dialogBox = new EditAddModification(strSymbol, strMass, usedSymbols);
            var tmpResult = dialogBox.ShowDialog();

            // Only if they click OK do we perform any updating of the DataGridView.
            if (tmpResult == DialogResult.OK)
            {
                if (row == dataGridViewModList.RowCount - 1)
                {
                    dataGridViewModList.Rows.Add(new object[] { dialogBox.ModificationString[0], double.Parse(dialogBox.MassString) });
                }
                else if (string.IsNullOrWhiteSpace(dialogBox.ModificationString) ||
                         string.IsNullOrWhiteSpace(dialogBox.MassString))
                {
                    dataGridViewModList.Rows.RemoveAt(row);
                }
                else
                {
                    dataGridViewModList.Rows[row].Cells[0].Value = dialogBox.ModificationString[0];
                    dataGridViewModList.Rows[row].Cells[1].Value = double.Parse(dialogBox.MassString);
                }
            }
        }

        // TODO: Don't actually store any values to objects until this handler is called.
        // TODO: This will remove all need for the "cancel button handler" "data restore" (that doesn't restore any data).
        // TODO: This should be easily accomplished by populating all data accordingly when the dialog is opened, and then ONLY storing the data if/when "OK" is clicked.
        private void applyButton_Click(object sender, EventArgs e)
        {
            m_valuesForCancel = new object[m_numCancelOptions];
            int i;
            for (i = 0; i < m_numCancelOptions; ++i)
            {
                m_valuesForCancel[i] = null;
            }

            // Finally modify the modification list.
            m_fragLadderOptions.modificationList.Clear();
            foreach (DataGridViewRow row in dataGridViewModList.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    m_fragLadderOptions.modificationList.Add((char)(row.Cells[0].Value), double.Parse(row.Cells[1].Value.ToString()));
                }
            }
            // update fragment ladder so color changes will take effect
            m_fragLadder.regenerateLadderFromSelection();
            m_fragLadder.setMatchedLabel(Matched);
            m_fragLadder.setUnmatchedLabel(Unmatched);

            // Update Plot options for Color
            m_plotOptions.unmatchedColor = Unmatched;
            m_plotOptions.matchedColor = Matched;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // PLOT UPDATING
            if (m_valuesForCancel[0] != null)
            {
                m_plotOptions.showSnappingCursor = (bool)m_valuesForCancel[0];
            }
            if (m_valuesForCancel[1] != null)
            {
                m_plotOptions.showLegend = (bool)m_valuesForCancel[1];
            }
            if (m_valuesForCancel[2] != null)
            {
                m_plotOptions.hideUnmatched = (bool)m_valuesForCancel[2];
            }
            if (m_valuesForCancel[3] != null)
            {
                m_plotOptions.zoomHorizontal = (bool)m_valuesForCancel[3];
            }
            if (m_valuesForCancel[4] != null)
            {
                m_plotOptions.unzoomKey = (Keys)m_valuesForCancel[4];
            }
            if (m_valuesForCancel[5] != null)
            {
                m_plotOptions.focusOffset = (int)m_valuesForCancel[5];
            }
            if (m_valuesForCancel[6] != null)
            {
                m_plotOptions.annotationPercent = (int)m_valuesForCancel[6];
            }
            if (m_valuesForCancel[7] != null)
            {
                m_plotOptions.annotationTextSize = (int)m_valuesForCancel[7];
            }
            if (m_valuesForCancel[8] != null)
            {
                m_plotOptions.annotationColor = (Color)m_valuesForCancel[8];
            }
            if (m_valuesForCancel[9] != null)
            {
                m_plotOptions.matchedColor = (Color)m_valuesForCancel[9];
            }
            if (m_valuesForCancel[10] != null)
            {
                m_plotOptions.unmatchedColor = (Color)m_valuesForCancel[10];
            }
            if (m_valuesForCancel[13] != null)
            {
                m_plotOptions.rightClickUnzoom = (bool)m_valuesForCancel[13];
            }
            if (m_valuesForCancel[14] != null)
            {
                m_plotOptions.hidePlotTools = (bool)m_valuesForCancel[14];
            }
            if (m_valuesForCancel[15] != null)
            {
                m_plotOptions.replot = true;
                m_plotOptions.numberOfPlots = (int)m_valuesForCancel[15];
            }

            // MAIN UPDATING
            if (m_valuesForCancel[11] != null)
            {
                m_mainFormOptions.isPlotInMainForm = (bool)m_valuesForCancel[11];
            }
            if (m_valuesForCancel[12] != null)
            {
                m_mainFormOptions.toleranceValue = (double)m_valuesForCancel[12];
            }
            if (m_valuesForCancel[18] != null)
            {
                m_mainFormOptions.lowerToleranceValue = (double)m_valuesForCancel[18];
            }

            // DATA VIEW

            // FRAGMENT LADDER

            if (m_valuesForCancel[17] != null)
            {
                m_fragLadderOptions.checkedHeaders = (List<string>)m_valuesForCancel[17];
            }

            Close();
        }

        private void SaveValuesForCancel()
        {
            // PLOT
            m_valuesForCancel[0] = m_plotOptions.showSnappingCursor;
            m_valuesForCancel[1] = m_plotOptions.showLegend;
            m_valuesForCancel[2] = m_plotOptions.hideUnmatched;
            m_valuesForCancel[3] = m_plotOptions.zoomHorizontal;
            m_valuesForCancel[4] = m_plotOptions.unzoomKey;
            m_valuesForCancel[5] = m_plotOptions.focusOffset;
            m_valuesForCancel[6] = m_plotOptions.annotationPercent;
            m_valuesForCancel[7] = m_plotOptions.annotationTextSize;
            m_valuesForCancel[8] = m_plotOptions.annotationColor;
            m_valuesForCancel[9] = m_plotOptions.matchedColor;
            m_valuesForCancel[10] = m_plotOptions.unmatchedColor;
            m_valuesForCancel[13] = m_plotOptions.rightClickUnzoom;
            m_valuesForCancel[14] = m_plotOptions.hidePlotTools;
            m_valuesForCancel[15] = m_plotOptions.numberOfPlots;

            // MAIN
            m_valuesForCancel[11] = m_mainFormOptions.isPlotInMainForm;
            m_valuesForCancel[12] = m_mainFormOptions.toleranceValue;
            m_valuesForCancel[18] = m_mainFormOptions.lowerToleranceValue;

            // DATA VIEW

            // FRAGMENT LADDER
            m_valuesForCancel[17] = m_fragLadderOptions.checkedHeaders;
        }

        private void OptionsViewController_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_profileLocation != "")
            {
                if (m_createProfile)
                {
                    // Lets Save the Profile data.
                    var writer = new FileStream(m_profileLocation, FileMode.Create, FileAccess.Write);

                    try
                    {
                        var binaryFormatter = new BinaryFormatter();
                        var tmpPlotOptions = new PlotOptions(m_plotOptions);
                        binaryFormatter.Serialize(writer, tmpPlotOptions);
                        var tmpMainOptions = new MainFormOptions(m_mainFormOptions);
                        binaryFormatter.Serialize(writer, tmpMainOptions);
                        var tmpFragmentLadderOptions = new Options.FragmentLadderOptions(m_fragLadderOptions);
                        binaryFormatter.Serialize(writer, tmpFragmentLadderOptions);
                    }
                    finally
                    {
                        writer.Close();
                    }
                }
            }
        }

        // Returns a parent directory of a given file or directory.
        // i.e. "~Prototype4\\SpectrumLook\\bin\\Debug\\UserProfile.spuf"
        // goes to "~Prototype4\\SpectrumLook\\bin\\Debug"
        private string getParentDirectory(string directoryWithFile)
        {
            var returnVal = "";
            var words = directoryWithFile.Split('\\');
            var i = 0;
            foreach (var s in words)
            {
                if (i == (words.Count() - 1)) // last word
                {
                }
                else if (i == (words.Count() - 2))  // second to last word
                {
                    returnVal += s;
                }
                else
                {
                    returnVal += s + '\\';
                }
                i++;
            }

            return returnVal;
        }

        private void mainUserBrowseButton_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = getParentDirectory(m_profileLocation);
            var openResult = openFileDialog.ShowDialog();

            if (openResult != DialogResult.Cancel)
            {
                m_createProfile = true;
                m_profileLocation = openFileDialog.FileName;
            }
        }

        private void defaultButton_Click(object sender, EventArgs e)
        {
            if (optionTabsPage.SelectedTab.Text == "Plot Options")
            {
                m_valuesForCancel[0] = m_plotOptions.showSnappingCursor;
                m_valuesForCancel[1] = m_plotOptions.showLegend;
                m_valuesForCancel[2] = m_plotOptions.hideUnmatched;
                m_valuesForCancel[3] = m_plotOptions.zoomHorizontal;
                m_valuesForCancel[4] = m_plotOptions.unzoomKey;
                m_valuesForCancel[5] = m_plotOptions.focusOffset;
                m_valuesForCancel[6] = m_plotOptions.annotationPercent;
                m_valuesForCancel[7] = m_plotOptions.annotationTextSize;
                m_valuesForCancel[8] = m_plotOptions.annotationColor;
                m_valuesForCancel[9] = m_plotOptions.matchedColor;
                m_valuesForCancel[10] = m_plotOptions.unmatchedColor;
                m_valuesForCancel[13] = m_plotOptions.rightClickUnzoom;
                m_valuesForCancel[14] = m_plotOptions.hidePlotTools;
                m_valuesForCancel[15] = m_plotOptions.numberOfPlots;
                // fixed issue 10 here
                m_plotOptions.numberOfPlots = 1;
                m_plotOptions.replot = true;

                m_plotOptions.CopyOptions(new PlotOptions());
            }
            else if (optionTabsPage.SelectedTab.Text == "General Options")
            {
                m_valuesForCancel[11] = m_mainFormOptions.isPlotInMainForm;
                m_valuesForCancel[12] = m_mainFormOptions.toleranceValue;
                m_valuesForCancel[18] = m_mainFormOptions.lowerToleranceValue;

                m_mainFormOptions.CopyOptions(new MainFormOptions());
            }
        }

        private void OptionsViewController_Load(object sender, EventArgs e)
        {
        }
    }
}
