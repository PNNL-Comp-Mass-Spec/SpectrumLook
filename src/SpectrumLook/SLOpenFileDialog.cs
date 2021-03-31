using System;
using System.IO;
using System.Windows.Forms;

namespace SpectrumLook
{
    public partial class SLOpenFileDialog : Form
    {
        public string mSynopsisPath;
        public string mDataPath;

        public SLOpenFileDialog(string currentSynopsisPath, string currentDataPath)
        {
            InitializeComponent();

            mSynopsisPath = currentSynopsisPath;
            textBoxSynopsis.Text = currentSynopsisPath;
            mDataPath = currentDataPath;
            textBoxData.Text = currentDataPath;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            mSynopsisPath = textBoxSynopsis.Text;
            mDataPath = textBoxData.Text;

            try
            {

                var synopsisFile = new FileInfo(mSynopsisPath);
                if (synopsisFile.Directory != null && synopsisFile.Directory.Exists)
                    Environment.CurrentDirectory = synopsisFile.Directory.FullName;
            }
            catch (Exception ex)
            {
                // Ignore errors here
                Console.WriteLine("Unable to update the working directory: " + ex.Message);
            }

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
            openFileDialog.Filter = "Spectrum files (*.mzML, *.mzXML, *.raw)|*.mzML;*.mzXML;*.raw|All Files (*.*)|*.*";
            try
            {
                openFileDialog.Title = "Open Data File";
                openFileDialog.FileName = "mzXml File";
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBoxData.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSynopsis_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            try
            {
                openFileDialog.Title = "Open Synopsis File";
                openFileDialog.FileName = "Synopsis File";
                var result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBoxSynopsis.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
