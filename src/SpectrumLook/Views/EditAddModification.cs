using System;
using System.Linq;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    public partial class EditAddModification : Form
    {
        private readonly string mValidSymbols = "!#$%&'*+?@^_`~-"; // Allow use of '-'
        //    string allowedSymbols = "!#$%&'*+?@^_`~";    // by molecular weight calculator
        private readonly string mAvailableSymbols;
        private readonly string mSymbolRegex;

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

            foreach (var c in mValidSymbols)
            {
                if (!usedSymbolsToCheck.Contains(c))
                {
                    mAvailableSymbols += c;
                }
            }

            symbolTextBox.TextChanged += SymbolTextBox_TextChanged;
            massTextBox.TextChanged += MassTextBox_TextChanged;

            mSymbolRegex = "^[" + System.Text.RegularExpressions.Regex.Escape(mAvailableSymbols) + "]{0,1}$";
        }

        private void OkModButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(massTextBox.Text, out var outValue))
            {
                DialogResult = DialogResult.OK;
                ModificationString = symbolTextBox.Text;
                MassString = outValue.ToString();
                Close();
            }
        }

        private void SymbolTextBox_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(symbolTextBox.Text, mSymbolRegex))
            {
                ModificationString = symbolTextBox.Text;
            }
            else
            {
                symbolTextBox.Text = ModificationString;
            }

            symbolTextBox.SelectAll();
        }

        private void MassTextBox_TextChanged(object sender, EventArgs e)
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
