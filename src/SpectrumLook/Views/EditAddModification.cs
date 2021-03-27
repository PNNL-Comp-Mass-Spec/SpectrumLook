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
        private string m_usedSymbols;
        private string m_validSymbols = "!#$%&'*+?@^_`~-"; // Allow use of '-'
        //    string allowedSymbols = "!#$%&'*+?@^_`~"; // bby molecular weight calculator
        private string m_availableSymbols;
        private string m_symbolRegex;

        public string ModificationString { get; private set; }

        public string MassString { get; private set; }

        public EditAddModification(string editedModString, string editedMassString, string usedSymbols = "")
        {
            InitializeComponent();

            this.ModificationString = editedModString;
            this.MassString = editedMassString;

            this.symbolTextBox.Text = this.ModificationString;
            this.massTextBox.Text = this.MassString;

            m_usedSymbols = usedSymbols;
            if (!string.IsNullOrWhiteSpace(editedModString))
            {
                m_usedSymbols = usedSymbols.Remove(usedSymbols.IndexOf(ModificationString[0]), 1);
            }
            foreach (var c in m_validSymbols)
            {
                if (!m_usedSymbols.Contains(c))
                {
                    m_availableSymbols += c;
                }
            }

            this.symbolTextBox.TextChanged += symbolTextBox_TextChanged;
            this.massTextBox.TextChanged += massTextBox_TextChanged;

            m_symbolRegex = "^[" + System.Text.RegularExpressions.Regex.Escape(m_availableSymbols) + "]{0,1}$";
        }

        private void okModButton_Click(object sender, EventArgs e)
        {
            var outValue = 0.0;
            if (double.TryParse(massTextBox.Text, out outValue))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.ModificationString = this.symbolTextBox.Text;
                this.MassString = outValue.ToString();
                this.Close();
            }
        }

        private void symbolTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(this.symbolTextBox.Text, m_symbolRegex))
            {
                ModificationString = this.symbolTextBox.Text;
            }
            else
            {
                this.symbolTextBox.Text = ModificationString;
            }

            this.symbolTextBox.SelectAll();
        }

        private void massTextBox_TextChanged(object sender, EventArgs e)
        {
            var selLength = this.massTextBox.SelectionLength;
            var pos = this.massTextBox.SelectionStart;
            if (this.massTextBox.Text.Length == 0)
            {
                MassString = this.massTextBox.Text;
            }
            else
            {
                double tempVal = 0;
                if (double.TryParse(this.massTextBox.Text, out tempVal))
                {
                    MassString = this.massTextBox.Text;
                }
                else
                {
                    this.massTextBox.Text = MassString;
                    this.massTextBox.SelectionStart = pos - 1;
                    this.massTextBox.SelectionLength = selLength;
                }
            }
        }
    }
}
