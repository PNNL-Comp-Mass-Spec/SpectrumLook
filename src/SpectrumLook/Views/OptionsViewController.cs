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
        private enum OptionField
        {
            // Plot options
            ShowSnappingCursor = 0,
            ShowLegend = 1,
            HideUnmatched = 2,
            ZoomHorizontal = 3,
            UnzoomKey = 4,
            FocusOffset = 5,
            AnnotationPercent = 6,
            AnnotationTextSize = 7,
            AnnotationColor = 8,
            MatchedColor = 9,
            UnmatchedColor = 10,
            RightClickUnzoom = 13,
            HidePlotTools = 14,
            NumberOfPlots = 15,

            // Main form options
            IsPlotInMainForm = 11,
            ToleranceValue = 12,
            LowerToleranceValue = 18,

            // FragmentationLadderOptions
            CheckedHeaders = 17
        }

        /// <summary>
        /// This array holds settings in place when the options window is shown
        /// If the user clicks cancel, the original values are restored
        /// </summary>
        /// <remarks>
        /// Only works with primitives (bool, int, double, Color, etc.)
        /// Values are initially set to null
        /// </remarks>
        private readonly Dictionary<OptionField, object> mSavedOptions;

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

            mSavedOptions = new Dictionary<OptionField, object>();

            foreach (OptionField optionField in Enum.GetValues(typeof(OptionField)))
            {
                mSavedOptions.Add(optionField, null);
            }

            mPlotOptions = referencePlotOptions;
            mMainFormOptions = referenceMainFormOptions;
            mFragmentationLadderOptions = fragmentLadderOptions;
            mFragmentationLadder = fragmentLadder;
            Matched = mPlotOptions.MatchedColor;
            Unmatched = mPlotOptions.UnmatchedColor;

            FillKeyOptions();
            CacheCurrentOptions();
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
            if (string.IsNullOrWhiteSpace(mProfileFilePath) || !mCreateProfile)
            {
                Hide();
                return;
            }

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

            Hide();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                CacheCurrentOptions();
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
            plotBoxZoom.Checked = !mPlotOptions.ZoomHorizontal;
            plotUnzoomKeyComboBox.SelectedItem = mPlotOptions.UnzoomKey;
            plotAnnotationColor.BackColor = mPlotOptions.AnnotationColor;
            plotTextSize.Text = mPlotOptions.AnnotationTextSize.ToString();
            plotAnnotationPercentBox.Text = mPlotOptions.AnnotationPercent.ToString();
            plotRightClickUnzoom.Checked = mPlotOptions.RightClickUnzoom;
            plotNumberOfPlotsTextBox.Text = mPlotOptions.NumberOfPlots.ToString();

            // MAIN UPDATING
            mainDetachPlotCheckBox.Checked = !mMainFormOptions.IsPlotInMainForm;
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
            mMainFormOptions.IsPlotInMainForm = !mainDetachPlotCheckBox.Checked;
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
            var symbol = (char?)dataGridViewModList.Rows[row].Cells[0].Value;
            var mass = (double?)dataGridViewModList.Rows[row].Cells[1].Value;

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

            if (tmpResult != DialogResult.OK)
                return;

            // Update the data grid
            if (row == dataGridViewModList.RowCount - 1)
            {
                dataGridViewModList.Rows.Add(dialogBox.ModificationString[0], double.Parse(dialogBox.MassString));
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

        // TODO: Don't actually store any values to objects until this handler is called.
        // TODO: This will remove all need for the "cancel button handler" "data restore" (that doesn't restore any data).
        // TODO: This should be easily accomplished by populating all data accordingly when the dialog is opened, and then ONLY storing the data if/when "OK" is clicked.
        private void applyButton_Click(object sender, EventArgs e)
        {
            foreach (var keyName in mSavedOptions.Keys.ToList())
            {
                mSavedOptions[keyName] = null;
            }

            // Finally modify the modification list.
            mFragmentationLadderOptions.ModificationList.Clear();
            foreach (DataGridViewRow row in dataGridViewModList.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    mFragmentationLadderOptions.ModificationList.Add((char)row.Cells[0].Value, double.Parse(row.Cells[1].Value.ToString()));
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

        private void CancelButton_Click(object sender, EventArgs e)
        {
            // Plot options

            mPlotOptions.ShowSnappingCursor = GetSavedValue(OptionField.ShowSnappingCursor, mPlotOptions.ShowSnappingCursor);

            mPlotOptions.ShowLegend = GetSavedValue(OptionField.ShowLegend, mPlotOptions.ShowLegend);
            mPlotOptions.HideUnmatched = GetSavedValue(OptionField.HideUnmatched, mPlotOptions.HideUnmatched);
            mPlotOptions.ZoomHorizontal = GetSavedValue(OptionField.ZoomHorizontal, mPlotOptions.ZoomHorizontal);
            mPlotOptions.UnzoomKey = GetSavedValue( OptionField.UnzoomKey, mPlotOptions.UnzoomKey);
            mPlotOptions.FocusOffset = GetSavedValue(OptionField.FocusOffset, mPlotOptions.FocusOffset);
            mPlotOptions.AnnotationPercent = GetSavedValue(OptionField.AnnotationPercent, mPlotOptions.AnnotationPercent);
            mPlotOptions.AnnotationTextSize = GetSavedValue(OptionField.AnnotationTextSize, mPlotOptions.AnnotationTextSize);
            mPlotOptions.AnnotationColor = GetSavedValue(OptionField.AnnotationColor, mPlotOptions.AnnotationColor);
            mPlotOptions.MatchedColor = GetSavedValue(OptionField.MatchedColor, mPlotOptions.MatchedColor);
            mPlotOptions.UnmatchedColor = GetSavedValue(OptionField.UnmatchedColor, mPlotOptions.UnmatchedColor);
            mPlotOptions.RightClickUnzoom = GetSavedValue(OptionField.RightClickUnzoom, mPlotOptions.RightClickUnzoom);
            mPlotOptions.HidePlotTools = GetSavedValue(OptionField.HidePlotTools, mPlotOptions.HidePlotTools);

            if (mSavedOptions[OptionField.NumberOfPlots] != null)
            {
                mPlotOptions.Replot = true;
                mPlotOptions.NumberOfPlots = GetSavedValue(OptionField.NumberOfPlots, mPlotOptions.NumberOfPlots);
            }

            // Main Form

            mMainFormOptions.IsPlotInMainForm = GetSavedValue(OptionField.IsPlotInMainForm, mMainFormOptions.IsPlotInMainForm);
            mMainFormOptions.ToleranceValue = GetSavedValue(OptionField.ToleranceValue, mMainFormOptions.ToleranceValue);
            mMainFormOptions.LowerToleranceValue = GetSavedValue(OptionField.LowerToleranceValue, mMainFormOptions.LowerToleranceValue);

            // Fragmentation Ladder

            mFragmentationLadderOptions.CheckedHeaders = GetSavedValue(OptionField.CheckedHeaders, mFragmentationLadderOptions.CheckedHeaders);

            Close();
        }

        private T GetSavedValue<T>(OptionField optionField, T currentValue)
        {
            var originalValue = mSavedOptions[optionField];
            if (originalValue != null)
            {
                return (T)originalValue;
            }

            return currentValue;
        }

        private void CacheCurrentOptions()
        {
            // Plot options
            mSavedOptions[OptionField.ShowSnappingCursor] = mPlotOptions.ShowSnappingCursor;
            mSavedOptions[OptionField.ShowLegend] = mPlotOptions.ShowLegend;
            mSavedOptions[OptionField.HideUnmatched] = mPlotOptions.HideUnmatched;
            mSavedOptions[OptionField.ZoomHorizontal] = mPlotOptions.ZoomHorizontal;
            mSavedOptions[OptionField.UnzoomKey] = mPlotOptions.UnzoomKey;
            mSavedOptions[OptionField.FocusOffset] = mPlotOptions.FocusOffset;
            mSavedOptions[OptionField.AnnotationPercent] = mPlotOptions.AnnotationPercent;
            mSavedOptions[OptionField.AnnotationTextSize] = mPlotOptions.AnnotationTextSize;
            mSavedOptions[OptionField.AnnotationColor] = mPlotOptions.AnnotationColor;
            mSavedOptions[OptionField.MatchedColor] = mPlotOptions.MatchedColor;
            mSavedOptions[OptionField.UnmatchedColor] = mPlotOptions.UnmatchedColor;
            mSavedOptions[OptionField.RightClickUnzoom] = mPlotOptions.RightClickUnzoom;
            mSavedOptions[OptionField.HidePlotTools] = mPlotOptions.HidePlotTools;
            mSavedOptions[OptionField.NumberOfPlots] = mPlotOptions.NumberOfPlots;

            // Main form
            mSavedOptions[OptionField.IsPlotInMainForm] = mMainFormOptions.IsPlotInMainForm;
            mSavedOptions[OptionField.ToleranceValue] = mMainFormOptions.ToleranceValue;
            mSavedOptions[OptionField.LowerToleranceValue] = mMainFormOptions.LowerToleranceValue;

            // Fragmentation Ladder
            mSavedOptions[OptionField.CheckedHeaders] = mFragmentationLadderOptions.CheckedHeaders;
        }

        private void OptionsViewController_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mCreateProfile || string.IsNullOrWhiteSpace(mProfileFilePath))
                return;

            // Save the Profile data.
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

        // Returns a parent directory of a given file or directory. For example:
        // SpectrumLook\bin\Debug\UserProfile.spuf
        // results in
        // SpectrumLook\bin\Debug
        private string GetParentDirectory(string directoryWithFile)
        {
            var pathParts = directoryWithFile.Split(Path.DirectorySeparatorChar).ToList();

            if (pathParts.Count <= 1)
                return string.Empty;

            return string.Join(Path.DirectorySeparatorChar.ToString(), pathParts.Take(pathParts.Count - 1).ToList());
        }

        private void mainUserBrowseButton_Click(object sender, EventArgs e)
        {
            var parentDirectory = GetParentDirectory(mProfileFilePath);
            if (!string.IsNullOrWhiteSpace(parentDirectory))
            {
                openFileDialog.InitialDirectory = parentDirectory;
            }

            var openResult = openFileDialog.ShowDialog();

            if (openResult != DialogResult.Cancel)
            {
                mCreateProfile = true;
                mProfileFilePath = openFileDialog.FileName;
            }
        }

        private void DefaultButton_Click(object sender, EventArgs e)
        {
            if (optionTabsPage.SelectedTab.Text == "Plot Options")
            {
                mSavedOptions[OptionField.ShowSnappingCursor] = mPlotOptions.ShowSnappingCursor;
                mSavedOptions[OptionField.ShowLegend] = mPlotOptions.ShowLegend;
                mSavedOptions[OptionField.HideUnmatched] = mPlotOptions.HideUnmatched;
                mSavedOptions[OptionField.ZoomHorizontal] = mPlotOptions.ZoomHorizontal;
                mSavedOptions[OptionField.UnzoomKey] = mPlotOptions.UnzoomKey;
                mSavedOptions[OptionField.FocusOffset] = mPlotOptions.FocusOffset;
                mSavedOptions[OptionField.AnnotationPercent] = mPlotOptions.AnnotationPercent;
                mSavedOptions[OptionField.AnnotationTextSize] = mPlotOptions.AnnotationTextSize;
                mSavedOptions[OptionField.AnnotationColor] = mPlotOptions.AnnotationColor;
                mSavedOptions[OptionField.MatchedColor] = mPlotOptions.MatchedColor;
                mSavedOptions[OptionField.UnmatchedColor] = mPlotOptions.UnmatchedColor;
                mSavedOptions[OptionField.RightClickUnzoom] = mPlotOptions.RightClickUnzoom;
                mSavedOptions[OptionField.HidePlotTools] = mPlotOptions.HidePlotTools;
                mSavedOptions[OptionField.NumberOfPlots] = mPlotOptions.NumberOfPlots;

                mPlotOptions.NumberOfPlots = 1;
                mPlotOptions.Replot = true;

                mPlotOptions.SetOptions(new PlotOptions());
            }
            else if (optionTabsPage.SelectedTab.Text == "General Options")
            {
                mSavedOptions[OptionField.IsPlotInMainForm] = mMainFormOptions.IsPlotInMainForm;
                mSavedOptions[OptionField.ToleranceValue] = mMainFormOptions.ToleranceValue;
                mSavedOptions[OptionField.LowerToleranceValue] = mMainFormOptions.LowerToleranceValue;

                mMainFormOptions.SetOptions(new MainFormOptions());
            }
        }

        private void OptionsViewController_Load(object sender, EventArgs e)
        {
        }
    }
}
