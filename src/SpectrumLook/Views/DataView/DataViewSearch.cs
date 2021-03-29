using System;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    internal class DataViewSearch
    {
        private int ColumnCount;
        private int RowCount;

        private DataGridView DataGridTable { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataGrid"></param>
        public DataViewSearch(DataGridView dataGrid)
        {
            DataGridTable = dataGrid;
        }

        public void SimpleSearch(string textToFind)
        {
            if (string.IsNullOrEmpty(textToFind))
            {
                // Show every row
                for (var i = 0; i < DataGridTable.Columns.Count; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                for (var i = 0; i < DataGridTable.Rows.Count; i++)
                {
                    DataGridTable.Rows[i].Visible = true;
                }

                for (var i = 0; i < DataGridTable.Columns.Count; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                }

                return;
            }

            for (var i = 0; i < DataGridTable.Columns.Count; i++)
            {
                DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            for (var i = 0; i < DataGridTable.Rows.Count; i++)
            {
                var showRow = false;

                for (var j = 0; j < DataGridTable.Columns.Count; j++)
                {
                    if (DataGridTable.Rows[i].Cells[j].Value.ToString().IndexOf(textToFind, StringComparison.OrdinalIgnoreCase) < 0)
                        continue;

                    showRow = true;
                    break;
                }

                DataGridTable.Rows[i].Visible = showRow;
            }

            for (var i = 0; i < DataGridTable.Columns.Count; i++)
            {
                DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            }
        }

        /// <summary>
        /// Advanced search
        /// </summary>
        /// <param name="joinOperand">Allowed Values: And, Or</param>
        /// <param name="columnNameToSearch">Name of the column to search</param>
        /// <param name="comparisonMethod">Allowed Values: contains, &lt;, &gt;, = --></param>
        /// <param name="textToFind"></param>
        public void AdvancedSearch(string joinOperand, string columnNameToSearch, string comparisonMethod, string textToFind)
        {
            if (DataGridTable.Rows.Count <= 0)
            {
                MessageBox.Show("No data loaded", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var columnIndex = 0;

            for (var i = 0; i < ColumnCount; i++)
            {
                if (DataGridTable.Columns[i].HeaderText.Equals(columnNameToSearch, StringComparison.OrdinalIgnoreCase))
                {
                    columnIndex = i;
                }
            }

            for (var i = 0; i < ColumnCount; i++)
            {
                DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            var useAnd = joinOperand.Equals("AND", StringComparison.OrdinalIgnoreCase);

            if (comparisonMethod == "contains")
            {
                Contains(columnIndex, textToFind, useAnd);
            }
            else if (comparisonMethod == ">")
            {
                GreaterThan(columnIndex, textToFind, useAnd);
            }
            else if (comparisonMethod == "<")
            {
                LessThan(columnIndex, textToFind, useAnd);
            }
            else if (comparisonMethod == "=")
            {
                Equal(columnIndex, textToFind, useAnd);
            }
            for (var i = 0; i < ColumnCount; i++)
            {
                DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            }
        }

        public void Contains(int columnIndex, string textToFind, bool useAnd)
        {
            DataGridTable.CurrentCell = null;

            for (var i = 0; i < RowCount; i++)
            {
                if (useAnd)
                {
                    // Use And
                    if (DataGridTable.Rows[i].Cells[columnIndex].Value.ToString().IndexOf(textToFind, StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        // Text not found
                        DataGridTable.Rows[i].Visible = false;
                    }
                }
                else
                {
                    // Use Or
                    if (DataGridTable.Rows[i].Cells[columnIndex].Value.ToString().IndexOf(textToFind, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        DataGridTable.Rows[i].Visible = true;
                    }
                }
            }
        }

        public void GreaterThan(int columnIndex, string textToFind, bool useAnd)
        {
            DataGridTable.CurrentCell = null;

            for (var i = 0; i < RowCount; i++)
            {
                double valueToFind;
                double comparisonValue;
                if (useAnd)
                {
                    // Use And
                    if (double.TryParse(textToFind, out valueToFind) &&
                        double.TryParse(DataGridTable.Rows[i].Cells[columnIndex].Value.ToString(), out comparisonValue))
                    {
                        // The value from DataGridTable
                        if (comparisonValue <= valueToFind)
                        {
                            // Value in the table is less than or equal to the user input
                            DataGridTable.Rows[i].Visible = false;
                        }
                    }
                }
                else
                {
                    // Use Or
                    if (double.TryParse(textToFind, out valueToFind) &&
                        double.TryParse(DataGridTable.Rows[i].Cells[columnIndex].Value.ToString(), out comparisonValue))
                    {
                        if (comparisonValue > valueToFind)// the condition is matched, the table value is larger than user input
                        {
                            DataGridTable.Rows[i].Visible = true;
                        }
                    }
                }
            }
        }

        public void Equal(int columnIndex, string textToFind, bool useAnd)
        {
            DataGridTable.CurrentCell = null;

            for (var i = 0; i < RowCount; i++)
            {
                if (useAnd)
                {
                    // Use And
                    if (double.TryParse(textToFind, out var valueToFind) &&
                        double.TryParse(DataGridTable.Rows[i].Cells[columnIndex].Value.ToString(), out var comparisonValue))
                    {
                        if (Math.Abs(comparisonValue - valueToFind) >= 0.001)
                        {
                            // Values do not match; hide the row
                            DataGridTable.Rows[i].Visible = false;
                        }
                    }
                }
                else
                {
                    // Use Or
                    if (double.TryParse(textToFind, out var valueToFind) &&
                        double.TryParse(DataGridTable.Rows[i].Cells[columnIndex].Value.ToString(), out var comparisonValue))
                    {
                        if (Math.Abs(comparisonValue - valueToFind) < 0.001)
                        {
                            // Values match; assure that the row is visible
                            DataGridTable.Rows[i].Visible = true;
                        }
                    }
                }
            }
        }

        public void LessThan(int columnIndex, string textToFind, bool useAnd)
        {
            DataGridTable.CurrentCell = null;

            for (var i = 0; i < RowCount; i++)
            {
                double valueToFind;
                double comparisonValue;
                if (useAnd)
                {
                    // Use And
                    if (double.TryParse(textToFind, out valueToFind) &&
                        double.TryParse(DataGridTable.Rows[i].Cells[columnIndex].Value.ToString(), out comparisonValue))
                    {
                        Console.WriteLine(comparisonValue);
                        if (comparisonValue >= valueToFind)
                        {
                            // Value in the table is less than or equal to the user input
                            DataGridTable.Rows[i].Visible = false;
                        }
                    }
                }
                else
                {
                    // Use Or
                    if (double.TryParse(textToFind, out valueToFind) &&
                        double.TryParse(DataGridTable.Rows[i].Cells[columnIndex].Value.ToString(), out comparisonValue))
                    {
                        if (comparisonValue > valueToFind) // condition is matched
                        {
                            DataGridTable.Rows[i].Visible = true;
                        }
                    }
                }
            }
        }

        public void UpdateTableDimensions(int columnCount, int rowCount)
        {
            ColumnCount = columnCount;
            RowCount = rowCount;
        }
    }
}
