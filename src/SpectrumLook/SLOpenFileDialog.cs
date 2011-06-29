using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpectrumLook
{
    public partial class SLOpenFileDialog : Form
    {
        public string m_synopsisPath;
        public string m_dataPath;

        public SLOpenFileDialog(string currentSynopsisPath, string currentDataPath)
        {
            InitializeComponent();

            m_synopsisPath = currentSynopsisPath;
            textBoxSynopsis.Text = currentSynopsisPath;
            m_dataPath = currentDataPath;
            textBoxData.Text = currentDataPath;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            m_synopsisPath = textBoxSynopsis.Text;
            m_dataPath = textBoxData.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonData_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "mzXml Files (*.mzxml)|*.mzxml|All Files (*.*)|*.*";
            try
            {
                openFileDialog.Title = "Open Data File";
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBoxData.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSynopsis_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            try
            {
                openFileDialog.Title = "Open Synopsis File";
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBoxSynopsis.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxSynopsis_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxData_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
