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
        private object[] mValuesForCancel;

        private readonly PlotOptions mPlotOptions;

        private readonly MainFormOptions mMainFormOptions;

        private readonly Options.FragmentLadderOptions mFragmentationLadderOptions;

        private readonly SpectrumLook.Views.FragmentLadderView.FragmentLadderView mFragmentationLadder;

        private Color Unmatched;

        private Color Matched;

        private bool mCreateProfile;

        private string mProfileFilePath;

        private const int mNumCancelOptions = 19;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="referencePlotOptions"></param>
        /// <param name="referenceMainFormOptions"></param>
        /// <param name="fragmentLadderOptions"></param>
        /// <param name="profileFilePath"></param>
        /// <param name="createProfile"></param>
        /// <param name="fragmentLadder"></param>
        public OptionsViewController(
            PlotOptions referencePlotOptions,
            MainFormOptions referenceMainFormOptions,
            Options.FragmentLadderOptions
            fragmentLadderOptions,
            string profileFilePath,
            bool createProfile,
            FragmentLadderView.FragmentLadderView fragmentLadder)
        {
            InitializeComponent();

            mValuesForCancel = new object[mNumCancelOptions];

            for (var i = 0; i < mNumCancelOptions; ++i)
            {
                mValuesForCancel[i] = null;
            }

            mPlotOptions = referencePlotOptions;
            mMainFormOptions = referenceMainFormOptions;
            mFragmentationLadderOptions = fragmentLadderOptions;
            mFragmentationLadder = fragmentLadder;
            Matched = mPlotOptions.MatchedColor;
            Unmatched = mPlotOptions.UnmatchedColor;

            FillKeyOptions();
            SaveValuesForCancel();
            mCreateProfile = createProfile;
            mProfileFilePath = profileFilePath;

            mainProfileFileLocationBox.Text = mProfileFilePath;

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
            if (mProfileFilePath != "")
            {
                if (mCreateProfile)
                {
                    var writer = new FileStream(mProfileFilePath, FileMode.Create, FileAccess.Write);

                    try
                    {
                        var binaryFormatter = new BinaryFormatter();
                        var tmpPlotOptions = new PlotOptions(mPlotOptions);
                        binaryFormatter.Serialize(writer, tmpPlotOptions);
                        var tmpMainOptions = new MainFormOptions(mMainFormOptions);
                        binaryFormatter.Serialize(writer, tmpMainOptions);
                        var tmpFragLadder = new Options.FragmentLadderOptions(mFragmentationLadderOptions);
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
            plotSnappingCursor.Checked = mPlotOptions.ShowSnappingCursor;
            plotShowLegend.Checked = mPlotOptions.ShowLegend;
            plotHideUnmatchedData.Checked = mPlotOptions.HideUnmatched;
            plotHorizontalZoom.Checked = mPlotOptions.ZoomHorizontal;
            plotBoxZoom.Checked = (!(mPlotOptions.ZoomHorizontal));
            plotUnzoomKeyComboBox.SelectedItem = mPlotOptions.UnzoomKey;
            plotAnnotationColor.BackColor = mPlotOptions.AnnotationColor;
            plotTextSize.Text = mPlotOptions.AnnotationTextSize.ToString();
            plotAnnotationPercentBox.Text = mPlotOptions.AnnotationPercent.ToString();
            plotRightClickUnzoom.Checked = mPlotOptions.RightClickUnzoom;
            plotNumberOfPlotsTextBox.Text = mPlotOptions.NumberOfPlots.ToString();

            // MAIN UPDATING
            mainDetachPlotCheckBox.Checked = !(mMainFormOptions.IsPlotInMainForm);
            mainMatchedColorSample.BackColor = mPlotOptions.MatchedColor;
            mainUnmatchedColorSample.BackColor = mPlotOptions.UnmatchedColor;
            lowerMatchingToleranceBox.Text = mMainFormOptions.LowerToleranceValue.ToString();
            mainMatchingToleranceBox.Text = mMainFormOptions.ToleranceValue.ToString();

            // DATA VIEW

            // FRAGMENT LADDER
            UpdateModList();
        }

        public void UpdateModList()
        {
            dataGridViewModList.Rows.Clear();
            foreach (var modPair in mFragmentationLadderOptions.ModificationList)
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

        private void MainDetachPlotCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            mMainFormOptions.IsPlotInMainForm = !(mainDetachPlotCheckBox.Checked);
        }

        private void PlotSnappingCursor_CheckedChanged(object sender, EventArgs e)
        {
            mPlotOptions.ShowSnappingCursor = plotSnappingCursor.Checked;
        }

        private void PlotShowLegend_CheckedChanged(object sender, EventArgs e)
        {
            mPlotOptions.ShowLegend = plotShowLegend.Checked;
        }

        private void PlotHideUnmatchedData_CheckedChanged(object sender, EventArgs e)
        {
            mPlotOptions.HideUnmatched = plotHideUnmatchedData.Checked;
        }

        private void checkBoxRightClickUnzoom_CheckedChanged(object sender, EventArgs e)
        {
            mPlotOptions.RightClickUnzoom = plotRightClickUnzoom.Checked;
        }

        private void CheckBoxHidePlotTools_CheckedChanged(object sender, EventArgs e)
        {
            mPlotOptions.HidePlotTools = plotHidePlotTools.Checked;
        }

        private void PlotHorizontalZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (plotHorizontalZoom.Checked)
            {
                mPlotOptions.ZoomHorizontal = true;
                plotBoxZoom.Checked = false;
            }
        }

        private void PlotBoxZoom_CheckedChanged(object sender, EventArgs e)
        {
            if (plotBoxZoom.Checked)
            {
                mPlotOptions.ZoomHorizontal = false;
                plotHorizontalZoom.Checked = false;
            }
        }

        private void PlotUnzoomKeyComboBox_Leave(object sender, EventArgs e)
        {
            Keys key;
            try
            {
                key = (Keys)plotUnzoomKeyComboBox.SelectedItem;
                mPlotOptions.UnzoomKey = key;
            }
            catch
            {
                plotUnzoomKeyComboBox.SelectedItem = mPlotOptions.UnzoomKey;
            }
        }

        private void PlotFragLadderSelectBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(plotFragLadderSelectBox.Text, out var outputValue))
            {
                plotFragLadderSelectBox.Text = "";
            }
            else
            {
                if (outputValue > 0)
                {
                    mPlotOptions.FocusOffset = outputValue;
                }
                else
                {
                    plotFragLadderSelectBox.Text = "";
                }
            }
        }

        private void PlotAnnotationPercentBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newPercent = Convert.ToInt32(plotAnnotationPercentBox.Text);
                if (newPercent > 100 || newPercent < 0)
                {
                    throw new Exception();
                }
                mPlotOptions.AnnotationPercent = newPercent;
            }
            catch
            {
                plotAnnotationPercentBox.Text = mPlotOptions.AnnotationPercent.ToString();
            }
        }

        private void PlotTextSize_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var newSize = Convert.ToInt32(plotTextSize.Text);
                if (newSize > 100 || newSize < 0)
                {
                    throw new Exception();
                }
                mPlotOptions.AnnotationTextSize = newSize;
            }
            catch
            {
                plotTextSize.Text = mPlotOptions.AnnotationTextSize.ToString();
            }
        }

        private void PlotChangeColorOpenButton_Click(object sender, EventArgs e)
        {
            var outputResult = colorDialog.ShowDialog();
            if (outputResult == DialogResult.OK)
            {
                mPlotOptions.AnnotationColor = colorDialog.Color;
                plotAnnotationColor.BackColor = mPlotOptions.AnnotationColor;
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
                mPlotOptions.Replot = true;
                mPlotOptions.NumberOfPlots = newAmmount;
            }
            catch
            {
                plotNumberOfPlotsTextBox.Text = mPlotOptions.NumberOfPlots.ToString();
            }
        }

        private void mainMatchColorChangeButton_Click(object sender, EventArgs e)
        {
            var outputResult = colorDialog.ShowDialog();
            if (outputResult == DialogResult.OK)
            {
                Matched = colorDialog.Color;
                // mPlotOptions.MatchedColor = colorDialog.Color;
                mainMatchedColorSample.BackColor = Matched;
            }
        }

        private void mainUnmatchColorChangeButton_Click(object sender, EventArgs e)
        {
            var outputResult = colorDialog.ShowDialog();
            if (outputResult == DialogResult.OK)
            {
                Unmatched = colorDialog.Color;
                // mPlotOptions.UnmatchedColor = colorDialog.Color;
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
                    MessageBox.Show(
                        "Please enter a valid tolerance level (0-1)!",
                        "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    mainMatchingToleranceBox.Text = "";
                    return;
                }

                mMainFormOptions.ToleranceValue = outputValue;
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
                    MessageBox.Show(
                        "Lower tolerance level must be greater than 0 and less than upper tolerance level.",
                        "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    lowerMatchingToleranceBox.Text = "";
                    return;
                }

                mMainFormOptions.LowerToleranceValue = outputValue;
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
            mValuesForCancel = new object[mNumCancelOptions];
            int i;
            for (i = 0; i < mNumCancelOptions; ++i)
            {
                mValuesForCancel[i] = null;
            }

            // Finally modify the modification list.
            mFragmentationLadderOptions.ModificationList.Clear();
            foreach (DataGridViewRow row in dataGridViewModList.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    mFragmentationLadderOptions.ModificationList.Add((char)(row.Cells[0].Value), double.Parse(row.Cells[1].Value.ToString()));
                }
            }
            // update fragment ladder so color changes will take effect
            mFragmentationLadder.RegenerateLadderFromSelection();
            mFragmentationLadder.SetMatchedLabel(Matched);
            mFragmentationLadder.SetUnmatchedLabel(Unmatched);

            // Update Plot options for Color
            mPlotOptions.UnmatchedColor = Unmatched;
            mPlotOptions.MatchedColor = Matched;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // PLOT UPDATING
            if (mValuesForCancel[0] != null)
            {
                mPlotOptions.ShowSnappingCursor = (bool)mValuesForCancel[0];
            }
            if (mValuesForCancel[1] != null)
            {
                mPlotOptions.ShowLegend = (bool)mValuesForCancel[1];
            }
            if (mValuesForCancel[2] != null)
            {
                mPlotOptions.HideUnmatched = (bool)mValuesForCancel[2];
            }
            if (mValuesForCancel[3] != null)
            {
                mPlotOptions.ZoomHorizontal = (bool)mValuesForCancel[3];
            }
            if (mValuesForCancel[4] != null)
            {
                mPlotOptions.UnzoomKey = (Keys)mValuesForCancel[4];
            }
            if (mValuesForCancel[5] != null)
            {
                mPlotOptions.FocusOffset = (int)mValuesForCancel[5];
            }
            if (mValuesForCancel[6] != null)
            {
                mPlotOptions.AnnotationPercent = (int)mValuesForCancel[6];
            }
            if (mValuesForCancel[7] != null)
            {
                mPlotOptions.AnnotationTextSize = (int)mValuesForCancel[7];
            }
            if (mValuesForCancel[8] != null)
            {
                mPlotOptions.AnnotationColor = (Color)mValuesForCancel[8];
            }
            if (mValuesForCancel[9] != null)
            {
                mPlotOptions.MatchedColor = (Color)mValuesForCancel[9];
            }
            if (mValuesForCancel[10] != null)
            {
                mPlotOptions.UnmatchedColor = (Color)mValuesForCancel[10];
            }
            if (mValuesForCancel[13] != null)
            {
                mPlotOptions.RightClickUnzoom = (bool)mValuesForCancel[13];
            }
            if (mValuesForCancel[14] != null)
            {
                mPlotOptions.HidePlotTools = (bool)mValuesForCancel[14];
            }
            if (mValuesForCancel[15] != null)
            {
                mPlotOptions.Replot = true;
                mPlotOptions.NumberOfPlots = (int)mValuesForCancel[15];
            }

            // MAIN UPDATING
            if (mValuesForCancel[11] != null)
            {
                mMainFormOptions.IsPlotInMainForm = (bool)mValuesForCancel[11];
            }
            if (mValuesForCancel[12] != null)
            {
                mMainFormOptions.ToleranceValue = (double)mValuesForCancel[12];
            }
            if (mValuesForCancel[18] != null)
            {
                mMainFormOptions.LowerToleranceValue = (double)mValuesForCancel[18];
            }

            // DATA VIEW

            // FRAGMENT LADDER

            if (mValuesForCancel[17] != null)
            {
                mFragmentationLadderOptions.CheckedHeaders = (List<string>)mValuesForCancel[17];
            }

            Close();
        }

        private void SaveValuesForCancel()
        {
            // PLOT
            mValuesForCancel[0] = mPlotOptions.ShowSnappingCursor;
            mValuesForCancel[1] = mPlotOptions.ShowLegend;
            mValuesForCancel[2] = mPlotOptions.HideUnmatched;
            mValuesForCancel[3] = mPlotOptions.ZoomHorizontal;
            mValuesForCancel[4] = mPlotOptions.UnzoomKey;
            mValuesForCancel[5] = mPlotOptions.FocusOffset;
            mValuesForCancel[6] = mPlotOptions.AnnotationPercent;
            mValuesForCancel[7] = mPlotOptions.AnnotationTextSize;
            mValuesForCancel[8] = mPlotOptions.AnnotationColor;
            mValuesForCancel[9] = mPlotOptions.MatchedColor;
            mValuesForCancel[10] = mPlotOptions.UnmatchedColor;
            mValuesForCancel[13] = mPlotOptions.RightClickUnzoom;
            mValuesForCancel[14] = mPlotOptions.HidePlotTools;
            mValuesForCancel[15] = mPlotOptions.NumberOfPlots;

            // MAIN
            mValuesForCancel[11] = mMainFormOptions.IsPlotInMainForm;
            mValuesForCancel[12] = mMainFormOptions.ToleranceValue;
            mValuesForCancel[18] = mMainFormOptions.LowerToleranceValue;

            // DATA VIEW

            // FRAGMENT LADDER
            mValuesForCancel[17] = mFragmentationLadderOptions.CheckedHeaders;
        }

        private void OptionsViewController_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mProfileFilePath != "")
            {
                if (mCreateProfile)
                {
                    // Lets Save the Profile data.
                    var writer = new FileStream(mProfileFilePath, FileMode.Create, FileAccess.Write);

                    try
                    {
                        var binaryFormatter = new BinaryFormatter();
                        var tmpPlotOptions = new PlotOptions(mPlotOptions);
                        binaryFormatter.Serialize(writer, tmpPlotOptions);
                        var tmpMainOptions = new MainFormOptions(mMainFormOptions);
                        binaryFormatter.Serialize(writer, tmpMainOptions);
                        var tmpFragmentLadderOptions = new Options.FragmentLadderOptions(mFragmentationLadderOptions);
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
                mCreateProfile = true;
                mProfileFilePath = openFileDialog.FileName;
            }
        }

        private void defaultButton_Click(object sender, EventArgs e)
        {
            if (optionTabsPage.SelectedTab.Text == "Plot Options")
            {
                mValuesForCancel[0] = mPlotOptions.ShowSnappingCursor;
                mValuesForCancel[1] = mPlotOptions.ShowLegend;
                mValuesForCancel[2] = mPlotOptions.HideUnmatched;
                mValuesForCancel[3] = mPlotOptions.ZoomHorizontal;
                mValuesForCancel[4] = mPlotOptions.UnzoomKey;
                mValuesForCancel[5] = mPlotOptions.FocusOffset;
                mValuesForCancel[6] = mPlotOptions.AnnotationPercent;
                mValuesForCancel[7] = mPlotOptions.AnnotationTextSize;
                mValuesForCancel[8] = mPlotOptions.AnnotationColor;
                mValuesForCancel[9] = mPlotOptions.MatchedColor;
                mValuesForCancel[10] = mPlotOptions.UnmatchedColor;
                mValuesForCancel[13] = mPlotOptions.RightClickUnzoom;
                mValuesForCancel[14] = mPlotOptions.HidePlotTools;
                mValuesForCancel[15] = mPlotOptions.NumberOfPlots;

                mPlotOptions.NumberOfPlots = 1;
                mPlotOptions.Replot = true;

                mPlotOptions.SetOptions(new PlotOptions());
            }
            else if (optionTabsPage.SelectedTab.Text == "General Options")
            {
                mValuesForCancel[11] = mMainFormOptions.IsPlotInMainForm;
                mValuesForCancel[12] = mMainFormOptions.ToleranceValue;
                mValuesForCancel[18] = mMainFormOptions.LowerToleranceValue;

                mMainFormOptions.SetOptions(new MainFormOptions());
            }
        }

        private void OptionsViewController_Load(object sender, EventArgs e)
        {
        }
    }
}
