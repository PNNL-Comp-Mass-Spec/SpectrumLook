using System;
using System.Linq;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    public partial class EditAddModification : Form
    {
        private string m_validSymbols = "!#$%&'*+?@^_`~-"; // Allow use of '-'
        //    string allowedSymbols = "!#$%&'*+?@^_`~";    // by molecular weight calculator
        private readonly string m_availableSymbols;
        private readonly string m_symbolRegex;

        public string ModificationString { get; private set; }

        public string MassString { get; private set; }

        public EditAddModification(string editedModString, string editedMassString, string usedSymbols = "")
        {
            InitializeComponent();

            ModificationString = editedModString;
            MassString = editedMassString;

            symbolTextBox.Text = ModificationString;
            massTextBox.Text = MassString;

            string usedSymbolsToCheck;

            if (string.IsNullOrWhiteSpace(editedModString))
            {
                usedSymbolsToCheck = usedSymbols;
            }
            else
            {
                usedSymbolsToCheck = usedSymbols.Remove(usedSymbols.IndexOf(ModificationString[0]), 1);
            }

            foreach (var c in m_validSymbols)
            {
                if (!usedSymbolsToCheck.Contains(c))
                {
                    m_availableSymbols += c;
                }
            }

            symbolTextBox.TextChanged += symbolTextBox_TextChanged;
            massTextBox.TextChanged += massTextBox_TextChanged;

            m_symbolRegex = "^[" + System.Text.RegularExpressions.Regex.Escape(m_availableSymbols) + "]{0,1}$";
        }

        private void okModButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(massTextBox.Text, out var outValue))
            {
                DialogResult = DialogResult.OK;
                ModificationString = symbolTextBox.Text;
                MassString = outValue.ToString();
                Close();
            }
        }

        private void symbolTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(symbolTextBox.Text, m_symbolRegex))
            {
                ModificationString = symbolTextBox.Text;
            }
            else
            {
                symbolTextBox.Text = ModificationString;
            }

            symbolTextBox.SelectAll();
        }

        private void massTextBox_TextChanged(object sender, EventArgs e)
        {
            var selLength = massTextBox.SelectionLength;
            var pos = massTextBox.SelectionStart;
            if (massTextBox.Text.Length == 0)
            {
                MassString = massTextBox.Text;
            }
            else
            {
                if (double.TryParse(massTextBox.Text, out _))
                {
                    MassString = massTextBox.Text;
                }
                else
                {
                    massTextBox.Text = MassString;
                    massTextBox.SelectionStart = pos - 1;
                    massTextBox.SelectionLength = selLength;
                }
            }
        }
    }
}
