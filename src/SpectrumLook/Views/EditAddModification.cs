using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    public partial class EditAddModification : Form
    {
        private string m_modificationString;
        private string m_massString;

        public string modificationString
        {
            get
            {
                return m_modificationString;
            }
        }
        public string massString
        {
            get
            {
                return m_massString;
            }
        }

        public EditAddModification(string editedModString, string editedMassString)
        {
            InitializeComponent();

            this.m_modificationString = editedModString;
            this.m_massString = editedMassString;

            this.symbolTextBox.Text = this.m_modificationString;
            this.massTextBox.Text = this.m_massString;
        }

        private void okModButton_Click(object sender, EventArgs e)
        {
            double outValue = 0.0;
            if (double.TryParse(massTextBox.Text, out outValue))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.m_modificationString = this.symbolTextBox.Text;
                this.m_massString = outValue.ToString();
                this.Close();
            }
        }
    }
}
