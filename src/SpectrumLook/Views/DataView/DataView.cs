using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using MolecularWeightCalculator;
using PHRPReader;
using SpectrumLook.Builders;

namespace SpectrumLook.Views
{
    // TODO : Need to inherit from IObserver and override the Update function! Otherwise the options will not update properly.
    public partial class DataView : Form
    {
        //////// private DataViewOptions m_dataViewOptions;
        private readonly Manager m_manager;

        private readonly DataViewSearch mViewSearch;

        public DataTable DataTableForDisplay;
        public DataViewAdvance DataAdvanceOption;
        public volatile bool shouldStop;

        private readonly MolecularWeightTool mMolecularWeightTool = new MolecularWeightTool();

        private int ColumnCount;
        private int RowCount;

        private readonly List<string> HeaderList = new List<string>();

        private new readonly ContextMenu Menu = new ContextMenu();

        private Manager.SynFileColumnIndices mSynFileColumns;

        public DataView(Manager manager)
        {
            InitializeComponent();
            m_manager = manager;
            DataTableForDisplay = new DataTable();
            mViewSearch = new DataViewSearch(DataGridTable);

            DataGridTable.KeyDown += DataGridTable_KeyDown;
            DataGridTable.TabIndex = 1;
        }
        private void DataGridTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandleRowSelection();
                e.Handled = true;
            }
        }
        private void DataGridTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HandleRowSelection();
        }

        public void HandleRowSelection()
        {
            if (DataGridTable.CurrentCell != null)
            {
                var selectedRow = DataGridTable.SelectedRows[0];

                GetValuesFromDataRow(selectedRow, out var peptide, out var scanNumber, out var dataset, out _, out var precursorMzValue);

                if (!string.IsNullOrWhiteSpace(dataset))
                {
                    m_manager.DataFileName = dataset;
                }

                m_manager.PrecursorMZ = precursorMzValue;

                PeptideCleavageStateCalculator.SplitPrefixAndSuffixFromSequence(peptide, out var sequence, out _, out _);

                m_manager.HandleSelectScanAndPeptide(scanNumber, sequence);
                m_manager.FocusOnControl(DataGridTable);
                m_manager.CallComboBox();
            }
        }

        public void DisplayProgress()
        {
            var ProgressWindow = new DataViewProgress();
            ProgressWindow.Show();
            while (!shouldStop)
            {
                Application.DoEvents();
            }
            ProgressWindow.Close();
        }

        public void RequestStop()
        {
            shouldStop = true;
        }

        public void SetColumnIndices(Manager.SynFileColumnIndices synFileColumns)
        {
            mSynFileColumns = synFileColumns;
        }

        public void SetDataTable(DataTable newTable)
        {
            // DataViewProgress ProgressWindow = new DataViewProgress();
            // ProgressWindow.Show();
            var workerThread = new Thread(DisplayProgress);
            workerThread.Start();

            DataTableForDisplay = new DataTable();
            DataGridTable.SelectionChanged -= DataGridTable_SelectionChanged;
            DataGridTable.Rows.Clear();
            DataGridTable.Columns.Clear();
            ColumnCount = 0;
            RowCount = 0;
            HeaderList.Clear();
            Menu.MenuItems.Clear();
            DataTableForDisplay = newTable;
            ColumnCount = DataTableForDisplay.Columns.Count;
            RowCount = DataTableForDisplay.Rows.Count;

            mViewSearch.UpdateTableDimensions(ColumnCount, RowCount);

            for (var i = 0; i < ColumnCount; i++)
            {
                DataGridTable.Columns.Add(DataTableForDisplay.Columns[i].ColumnName, DataTableForDisplay.Columns[i].ColumnName);
            }
            for (var i = 0; i < RowCount; i++)
            {
                DataGridTable.Rows.Add();
                for (var j = 0; j < ColumnCount; j++)
                {
                    DataGridTable.Rows[i].Cells[j].Value = DataTableForDisplay.Rows[i][j].ToString();
                }
            }

            DataGridTable.ReadOnly = true;
            DataGridTable.AutoResizeColumns();
            DataGridTable.AllowUserToResizeColumns = true;

            for (var i = 0; i < ColumnCount; i++)
            {
                HeaderList.Insert(i, DataTableForDisplay.Columns[i].ColumnName);
                var insertItem = new ToolStripMenuItem
                {
                    Text = HeaderList[i],
                    ImageIndex = i,
                    CheckOnClick = true,
                    Checked = true
                };

                insertItem.CheckedChanged += InsertItem_CheckedChanged;
                ColcontextMenuStrip.Items.Insert(i, insertItem);
            }
            for (var i = 0; i < DataGridTable.ColumnCount; i++)
            {
                Menu.MenuItems.Add(DataGridTable.Columns[i].ToString(), Menu_Click);
            }

            DataAdvanceOption = new DataViewAdvance();
            SetAdvancedOptions();
            DataAdvanceOption.Visible = false;
            DataAdvanceOption.AdvSrchOption.Click += AdvancedSearchClick;
            DataAdvanceOption.ColmDisplayOption.Click += ColumnDisplayClick;
            DataAdvanceOption.AdvSrcCancel.Click += AdvSrcCancel_Click;
            DataAdvanceOption.FormClosing += DataAdvanceOption_FormClosing;

            DataGridTable.SortCompare += Custom_SortCompare;

            // Adding event back
            DataGridTable.SelectionChanged += DataGridTable_SelectionChanged;

            RequestStop();
        }

        private void DataAdvanceOption_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            DataAdvanceOption.Visible = false;
        }
        private void AdvSrcCancel_Click(object sender, EventArgs e)
        {
            DataAdvanceOption.Visible = false;
        }
        private void InsertItem_CheckedChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < ColumnCount; i++)
            {
                var InsertItem = (ToolStripMenuItem)ColcontextMenuStrip.Items[i];
                DataGridTable.Columns[i].Visible = InsertItem.Checked;
            }
        }
        public void SetAdvancedOptions()
        {
            DataAdvanceOption.SetAdvance(HeaderList);
        }
        public void SearchButton_Click(object sender, EventArgs e)
        {
            var textToFind = SearchBox.Text;
            if (DataGridTable != null)
            {
                mViewSearch.SimpleSearch(textToFind);
            }
            else
            {
                MessageBox.Show("The File should be loaded first", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AdvancedSearchClick(object sender, EventArgs e)
        {
            if (DataGridTable.Rows.Count > 0)
            {
                /* for (int i = 0; i < ColNum; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }*/

                // Assure that all rows are visible
                for (var i = 0; i < RowCount; i++)
                {
                    DataGridTable.Rows[i].Visible = true;
                }

                /*for (int i = 0; i < 19; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                }*/

                try
                {
                    if (DataAdvanceOption.SeaFil_Pep1.Text == null || DataAdvanceOption.FilterComboBox1.Text == null || DataAdvanceOption.Filter_Value1.Text == null)
                    {
                        MessageBox.Show("No Option Selected", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        mViewSearch.AdvancedSearch("AND", DataAdvanceOption.SeaFil_Pep1.Text, DataAdvanceOption.FilterComboBox1.Text, DataAdvanceOption.Filter_Value1.Text);
                        if (DataAdvanceOption.AndOrComboBox1.Text != null && DataAdvanceOption.SeaFil_Pep2.Text != null && DataAdvanceOption.FilterComboBox2.Text != null && DataAdvanceOption.Filter_Value2.Text != null)
                        {
                            mViewSearch.AdvancedSearch(DataAdvanceOption.AndOrComboBox1.Text, DataAdvanceOption.SeaFil_Pep2.Text, DataAdvanceOption.FilterComboBox2.Text, DataAdvanceOption.Filter_Value2.Text);
                            if (DataAdvanceOption.AndOrComboBox2.Text != null && DataAdvanceOption.SeaFil_Pep3.Text != null && DataAdvanceOption.FilterComboBox3.Text != null && DataAdvanceOption.Filter_Value3.Text != null)
                            {
                                mViewSearch.AdvancedSearch(DataAdvanceOption.AndOrComboBox2.Text, DataAdvanceOption.SeaFil_Pep3.Text, DataAdvanceOption.FilterComboBox3.Text, DataAdvanceOption.Filter_Value3.Text);
                                if (DataAdvanceOption.AndOrComboBox3.Text != null && DataAdvanceOption.SeaFil_Pep4.Text != null && DataAdvanceOption.FilterComboBox4.Text != null && DataAdvanceOption.Filter_Value4.Text != null)
                                {
                                    mViewSearch.AdvancedSearch(DataAdvanceOption.AndOrComboBox3.Text, DataAdvanceOption.SeaFil_Pep4.Text, DataAdvanceOption.FilterComboBox4.Text, DataAdvanceOption.Filter_Value4.Text);
                                }
                            }
                        }
                    }
                    DataAdvanceOption.Visible = false;
                }
                catch
                {
                    MessageBox.Show("Error occurred; perhaps the File is not loaded correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("The File is not loaded correctly", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void ColumnDisplayClick(object sender, EventArgs e)
        {
            for (var i = 0; i < ColumnCount; i++)
            {
                DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            for (var i = 0; i < ColumnCount; i++)
            {
                DataGridTable.Columns[i].Visible = true;
            }

            foreach (DataRowView row in DataAdvanceOption.checkedListBox1.CheckedItems)
            {
                var displaySelected = int.Parse(row[DataAdvanceOption.checkedListBox1.ValueMember].ToString());
                DataGridTable.Columns[displaySelected].Visible = false;
            }
            for (var i = 0; i < 19; i++)
            {
                DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
            }
            DataGridTable.Visible = true;
            DataAdvanceOption.Close();
            // DataAdvanceOption.panel1.Visible = false;
        }
        /*private void SortClick(object sender, System.EventArgs e)
        {
            ListSortDirection direction;
            if (DataAdvanceOption.SrtClm1.DataSource != null)
            {
                DataGridViewColumn Column1 = DataGridTable.Columns[DataAdvanceOption.SrtClm1.ValueMember];
                if(DataAdvanceOption.SrtClmOption1.Text=="ASC")
                {
                    direction = ListSortDirection.Ascending;
                }
                else
                {
                    direction = ListSortDirection.Descending;
                }
                DataGridTable.Sort(Column1, direction);
                Column1.HeaderCell.SortGlyphDirection =
                direction == ListSortDirection.Ascending ?
                System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
                if (DataAdvanceOption.SrtClm2.DataSource != null)
                {
                    DataGridViewColumn Column2 = DataGridTable.Columns[DataAdvanceOption.SrtClm2.ValueMember];
                    // Column1.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                    DataGridTable.Sort(Column2, direction);
                    Column1.HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ?
                    System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
                    if (DataAdvanceOption.SrtClm3.DataSource != null)
                    {

                        DataGridViewColumn Column3 = DataGridTable.Columns[DataAdvanceOption.SrtClm3.ValueMember];
                        // Column2.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                        DataGridTable.Sort(Column3, direction);
                        Column1.HeaderCell.SortGlyphDirection =
                        direction == ListSortDirection.Ascending ?
                        System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;

                    }
                }
            }
            DataAdvanceOption.Visible=false;
        }*/

        private void AdvFilter_Click(object sender, EventArgs e)
        {
            if (DataGridTable.Rows.Count != 0)
            {
                DataAdvanceOption.Visible = true;
            }
            else
            {
                MessageBox.Show("The File is not Loaded", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void DataGridTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var list = new ListBox { Parent = this, Dock = DockStyle.Fill };
            DataGridTable.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            DataGridTable.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.None;
            DataGridTable.Columns[e.ColumnIndex].Selected = true;
            if (e.Button == MouseButtons.Right)
            {
                // show context menu
                list.ContextMenu = Menu;
            }
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            var item = sender as MenuItem;

            MessageBox.Show(item.Text);
        }

        private void DataGridTable_ColumnHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            var Column_index = e.ColumnIndex;

            if (e.Button == MouseButtons.Right)
            {
                ColcontextMenuStrip.Show((Control)sender, PointToClient(MousePosition).X, e.Location.Y);// Somehow, PointToClick(Control.MousePosition) X axis is correct, but the Y is not. Also e.X is not correct, but e.Y has correct location, so it is combined
            }
            else if (e.Button == MouseButtons.Left)
            {
            }
            else
            {
                // Error
            }
        }

        private void Custom_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.CellValue1 != null && e.CellValue2 != null)
            {
                e.SortResult = string.CompareOrdinal(e.CellValue1.ToString(), e.CellValue2.ToString());
                if (double.TryParse(e.CellValue1.ToString(), out var InputDoubleValue))
                {
                    if (double.TryParse(e.CellValue2.ToString(), out var SelDoubleValue))
                    {
                        if (InputDoubleValue < SelDoubleValue)
                        {
                            e.SortResult = 1;
                        }
                        else
                        {
                            e.SortResult = -1;
                        }
                    }
                }
                e.Handled = true;
            }
        }

        public List<ResultRowData> GetPeptidesAndScansInGrid(bool useOnlyVisible)
        {
            var peptidesAndScans = new List<ResultRowData>();

            if (mSynFileColumns.Scan < 0)
            {
                MessageBox.Show("GetPeptidesAndScansInGrid: could not find a column with 'scan' in the name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return peptidesAndScans;
            }

            if (mSynFileColumns.Peptide < 0)
            {
                MessageBox.Show("GetPeptidesAndScansInGrid: could not find a column with 'peptide' in the name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return peptidesAndScans;
            }

            // get the scan numbers and peptides for the rows
            foreach (DataGridViewRow row in DataGridTable.Rows)
            {
                if (row.Visible || !useOnlyVisible)
                {
                    GetValuesFromDataRow(row, out var peptide, out var scanNumber, out var dataset, out var precursorMz);

                    PeptideCleavageStateCalculator.SplitPrefixAndSuffixFromSequence(peptide, out _,
                        out _, out _);

                    peptidesAndScans.Add(new ResultRowData(dataset, scanNumber, peptide, precursorMz));
                }
            }

            return peptidesAndScans;
        }

        private void GetValuesFromDataRow(
            DataGridViewRow currentRow,
            out string peptide,
            out string scanNumber,
            out string dataset,
            out string precursorMz)
        {
            GetValuesFromDataRow(currentRow, out peptide, out scanNumber, out dataset, out precursorMz, out _);
        }

        private void GetValuesFromDataRow(
            DataGridViewRow currentRow,
            out string peptide,
            out string scanNumber,
            out string dataset,
            out string precursorMz,
            out double precursorMzValue)
        {
            peptide = currentRow.Cells[mSynFileColumns.Peptide].Value.ToString();
            scanNumber = currentRow.Cells[mSynFileColumns.Scan].Value.ToString();
            if (mSynFileColumns.Dataset >= 0)
            {
                dataset = currentRow.Cells[mSynFileColumns.Dataset].Value.ToString();
            }
            else
            {
                dataset = string.Empty;
            }

            precursorMz = string.Empty;
            precursorMzValue = 0;

            if (mSynFileColumns.PrecursorMz >= 0)
            {
                precursorMz = currentRow.Cells[mSynFileColumns.PrecursorMz].Value.ToString();
                if (double.TryParse(precursorMz, out var value))
                    precursorMzValue = value;
                else
                    precursorMzValue = 0;
            }
            else if (mSynFileColumns.ParentMH >= 0 && mSynFileColumns.Charge >= 0)
            {
                if (double.TryParse(currentRow.Cells[mSynFileColumns.ParentMH].Value.ToString(), out var parentMH) &&
                    short.TryParse(currentRow.Cells[mSynFileColumns.Charge].Value.ToString(), out var charge))
                {
                    precursorMzValue = mMolecularWeightTool.ConvoluteMass(parentMH, 1, charge);
                    precursorMz = precursorMzValue.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private void DataGridTable_SelectionChanged(object sender, EventArgs e)
        {
            if (m_manager.DataLoaded)
            {
                HandleRowSelection();
            }
        }

        private void DataGridTable_Click(object sender, EventArgs e)
        {
            if (!m_manager.DataLoaded)
            {
                HandleRowSelection();
            }
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var SearchSubString = SearchBox.Text;
                if (DataGridTable != null)
                {
                    mViewSearch.SimpleSearch(SearchSubString);
                }
                else
                {
                    MessageBox.Show("The File should be loaded first", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                e.Handled = true;
            }
        }
    }
}
