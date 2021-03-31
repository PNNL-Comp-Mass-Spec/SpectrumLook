using System;
using System.Windows.Forms;
using System.IO;

namespace SpectrumLook
{
    public partial class BatchSaveForm : Form
    {
        // Ignore Spelling: Cancelled

        private readonly Manager mManager;
        private readonly Manager.UpdateLabelDelegate mStatusLabelUpdate;

        private bool mCancelSearch;

        private string mBaseFolderText;

        /// <summary>
        /// Constructor
        /// </summary>
        public BatchSaveForm(Manager manager)
        {
            InitializeComponent();

            mManager = manager;
            mStatusLabelUpdate = UpdateStatusLabel;
            InitializeFields();
        }

        /// <summary>
        /// Handles the batch save according to the parameters set by the user
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                mCancelSearch = false;
                SaveButton.Enabled = false;

                var startDirectory = BaseFolderTextBox.Text;
                var baseName = BaseName.Text;
                var saveType = TypeComboBox.Text;

                CancelBatchSaveButton.DialogResult = DialogResult.None;

                try
                {
                    if (SaveCurrentRadioButton.Checked)
                    {
                        mManager.HandlePlotSave(startDirectory, baseName, saveType);
                    }
                    else
                    {
                        var saveOnlyInGrid = SaveGridRadioButton.Checked;
                        var usePeptideAndScanName = UsePeptideAndScanRadioButton.Checked;
                        var addDatasetName = AddDatasetNameCheckbox.Checked;

                        mManager.mMainForm.Visible = false;

                        mManager.HandleBatchSave(startDirectory, baseName, saveType,
                            saveOnlyInGrid, usePeptideAndScanName, addDatasetName, UpdateStatusLabel, ref mCancelSearch);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving plots: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                CancelBatchSaveButton.DialogResult = DialogResult.Cancel;
                mManager.mMainForm.Visible = true;
                SaveButton.Enabled = true;
                CloseButton.Visible = true;

                if (mCancelSearch)
                {
                    MessageBox.Show("Cancelled saving", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Please Check that all of the fields are filled out and that you have supplied a Base File name",
                    "Insufficient Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Either cancels in-progress plot generation or closes the form
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            mCancelSearch = true;
            Application.DoEvents();

            if (CancelBatchSaveButton.DialogResult == DialogResult.Cancel)
                Close();
        }

        /// <summary>
        /// Handles stopping the search if the form is closed while it is running
        /// </summary>
        private void BatchSaveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mCancelSearch = true;
            Application.DoEvents();
        }

        /// <summary>
        /// Event handler that opens the folder browser dialog and sets the text of the folder to the result
        /// </summary>
        private void BrowseFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog.ShowDialog();
            BaseFolderTextBox.Text = FolderBrowserDialog.SelectedPath;
            if (BaseFolderTextBox.Text?.Length == 0)
            {
                BaseFolderTextBox.Text = mBaseFolderText;
            }
        }

        /// <summary>
        /// Initializes the fields on the form
        /// </summary>
        private void InitializeFields()
        {
            mCancelSearch = false;

            BaseFolderTextBox.Text = Environment.CurrentDirectory.StartsWith(@"C:\Program Files") ?
                                         Path.GetTempPath() :
                                         Environment.CurrentDirectory;

            mBaseFolderText = BaseFolderTextBox.Text;
            BaseName.Text = "Spectrum_" + string.Format("{0:yyyy_MM_dd}", DateTime.Now);
            SaveAllRadioButton.Checked = true;
            UsePeptideAndScanRadioButton.Checked = true;
            StatusLabel.Text = "";
            CloseButton.Visible = false;

            TypeComboBox.Items.Clear();
            foreach (var imageType in mManager.mPlot.SaveAsImageTypes)
            {
                TypeComboBox.Items.Add(imageType);
            }

            // Default to .png
            TypeComboBox.SelectedIndex = 1;
        }

        /// <summary>
        /// Determines if the Fields on the form are properly filled out so that we can start batch saving
        /// </summary>
        /// <returns>true if the fields are valid, false otherwise</returns>
        private bool ValidateFields()
        {
            var success = true;

            success &= Directory.Exists(BaseFolderTextBox.Text);
            success &= !string.IsNullOrEmpty(BaseName.Text);
            success &= !string.IsNullOrEmpty(TypeComboBox.Text);
            success &= SaveAllRadioButton.Checked || SaveGridRadioButton.Checked || SaveCurrentRadioButton.Checked;

            return success;
        }

        /// <summary>
        /// Updates the status label with the text.
        /// </summary>
        /// <param name="newText"></param>
        private void UpdateStatusLabel(string newText)
        {
            if (StatusLabel.InvokeRequired)
            {
                // invoke so that we are on the right thread to update the status
                StatusLabel.Invoke(mStatusLabelUpdate, newText);
                return;
            }

            StatusLabel.Text = newText;
            Application.DoEvents();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
