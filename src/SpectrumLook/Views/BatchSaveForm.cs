using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace SpectrumLook
{
    public partial class BatchSaveForm : Form
    {
        private Manager m_manager = null;
        private Manager.UpdateLabelDelegate m_statusLabelUpdate;
        public bool cancelSearch = false;
        public string baseFolderText = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public BatchSaveForm(Manager manager)
        {
            InitializeComponent();

            m_manager = manager;
            m_statusLabelUpdate = UpdateStatusLabel;
            InitializeFields();

        }

        /// <summary>
        /// Handles the batch save according to the parameters set by the user
        /// </summary>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                cancelSearch = false;
                SaveButton.Enabled = false;

                var startDirectory = BaseFolderTextBox.Text;
                var baseName = BaseName.Text;
                var saveType = TypeComboBox.Text;

                if (SaveCurrentRadioButton.Checked)
                {
                    m_manager.HandlePlotSave(startDirectory, baseName, saveType);
                }
                else
                {
                    var saveOnlyInGrid = SaveGridRadioButton.Checked;
                    var usePeptideAndScanName = UsePeptideAndScanRadioButton.Checked;
                    var addDatasetName = AddDatasetNameCheckbox.Checked;

                    m_manager.m_mainForm.Visible = false;
                    m_manager.HandleBatchSave(startDirectory, baseName, saveType,
                        saveOnlyInGrid, usePeptideAndScanName, addDatasetName, UpdateStatusLabel, ref cancelSearch);
                    m_manager.m_mainForm.Visible = true;
                }

                Close();
            }
            else
            {
                MessageBox.Show("Please Check that all of the fields are filled out and that you have supplied a Base File name",
                    "Insufficient Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Closes the form
        /// </summary>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            cancelSearch = true;
            Application.DoEvents();
            Close();
        }

        /// <summary>
        /// Handles stopping the search if the form is closed while it is running
        /// </summary>
        private void BatchSaveForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            cancelSearch = true;
            Application.DoEvents();
        }

        /// <summary>
        /// Event handler that opens the folder browser dialog and sets the text of the folder to the result
        /// </summary>
        private void BrowseFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog.ShowDialog();
            BaseFolderTextBox.Text = FolderBrowserDialog.SelectedPath;
            if (BaseFolderTextBox.Text.ToString() == "")
            {
                BaseFolderTextBox.Text = baseFolderText;
            }
        }

        /// <summary>
        /// Initializes the fields on the form
        /// </summary>
        private void InitializeFields()
        {
            cancelSearch = false;
            BaseFolderTextBox.Text = Environment.CurrentDirectory;
            baseFolderText = BaseFolderTextBox.Text;
            BaseName.Text = "SpectrumLookSave" + string.Format("{0:dd_MM_yyyy}", DateTime.Now);
            SaveAllRadioButton.Checked = true;
            UsePeptideAndScanRadioButton.Checked = true;
            StatusLabel.Text = "";

            foreach (var imageType in m_manager.m_plot.SaveAsImageTypes)
            {
                TypeComboBox.Items.Add(imageType);
            }
            TypeComboBox.SelectedIndex = 0;
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
                //invoke so that we are on the right thread to update the status
                StatusLabel.Invoke(m_statusLabelUpdate, new Object[] { newText });
                return;
            }

            StatusLabel.Text = newText;
            Application.DoEvents();
        }
    }
}
