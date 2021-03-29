using System;
using System.Collections.Generic;
using System.Data;
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

        private int ColNum;
        private int RowNum;

        private readonly List<string> HeaderList = new List<string>();

        private readonly ContextMenu Menu = new ContextMenu();

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

                var peptide = selectedRow.Cells[mSynFileColumns.Peptide].Value.ToString();
                var scanNumber = selectedRow.Cells[mSynFileColumns.Scan].Value.ToString();
                if (mSynFileColumns.Dataset >= 0)
                {
                    m_manager.DataFileName = selectedRow.Cells[mSynFileColumns.Dataset].Value.ToString();
                }

                if (mSynFileColumns.PrecursorMz >= 0)
                {
                    m_manager.PrecursorMZ = double.Parse(selectedRow.Cells[mSynFileColumns.PrecursorMz].Value.ToString());
                }
                else if (mSynFileColumns.ParentMH >= 0 && mSynFileColumns.Charge >= 0)
                {
                    var parentMH = double.Parse(selectedRow.Cells[mSynFileColumns.ParentMH].Value.ToString());
                    var charge = short.Parse(selectedRow.Cells[mSynFileColumns.Charge].Value.ToString());

                    m_manager.PrecursorMZ = mMolecularWeightTool.ConvoluteMass(parentMH, 1, charge);
                }

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

            ToolStripMenuItem InsertItem = null;
            DataTableForDisplay = new DataTable();
            DataGridTable.SelectionChanged -= new EventHandler(DataGridTable_SelectionChanged);
            DataGridTable.Rows.Clear();
            DataGridTable.Columns.Clear();
            ColNum = 0;
            RowNum = 0;
            HeaderList.Clear();
            Menu.MenuItems.Clear();
            DataTableForDisplay = newTable;
            ColNum = DataTableForDisplay.Columns.Count;
            RowNum = DataTableForDisplay.Rows.Count;
            for (var i = 0; i < ColNum; i++)

            mViewSearch.UpdateTableDimensions(ColumnCount, RowCount);

            {
                DataGridTable.Columns.Add(DataTableForDisplay.Columns[i].ColumnName, DataTableForDisplay.Columns[i].ColumnName);
            }
            for (var i = 0; i < RowNum; i++)
            {
                DataGridTable.Rows.Add();
                for (var j = 0; j < ColNum; j++)
                {
                    DataGridTable.Rows[i].Cells[j].Value = DataTableForDisplay.Rows[i][j].ToString();
                }
            }

            DataGridTable.ReadOnly = true;
            DataGridTable.AutoResizeColumns();
            DataGridTable.AllowUserToResizeColumns = true;

            for (var i = 0; i < ColNum; i++)
            {
                HeaderList.Insert(i, DataTableForDisplay.Columns[i].ColumnName);
                InsertItem = new ToolStripMenuItem();
                InsertItem.Text = HeaderList[i];
                InsertItem.ImageIndex = i;
                InsertItem.CheckOnClick = true;
                InsertItem.Checked = true;
                InsertItem.CheckedChanged += new EventHandler(InsertItem_CheckedChanged);
                ColcontextMenuStrip.Items.Insert(i, InsertItem);
            }
            for (var i = 0; i < DataGridTable.ColumnCount; i++)
            {
                Menu.MenuItems.Add(DataGridTable.Columns[i].ToString(), Menu_Click);
            }

            DataAdvanceOption = new DataViewAdvance();
            SetAdvancedOptions();
            DataAdvanceOption.Visible = false;
            DataAdvanceOption.AdvSrchOption.Click += new EventHandler(AdvancedSearchClick);
            DataAdvanceOption.ColmDisplayOption.Click += new EventHandler(ColumnDisplayClick);
            DataAdvanceOption.AdvSrcCancel.Click += new EventHandler(AdvSrcCancel_Click);
            DataAdvanceOption.FormClosing += new FormClosingEventHandler(DataAdvanceOption_FormClosing);

            DataGridTable.SortCompare += new DataGridViewSortCompareEventHandler(Custom_SortCompare);

            // Adding event back
            DataGridTable.SelectionChanged += new EventHandler(DataGridTable_SelectionChanged);

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
            for (var i = 0; i < ColNum; i++)
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
                MessageBox.Show("The File should be loaded first");
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
                for (var i = 0; i < RowNum; i++)
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
                        MessageBox.Show("No Option Selected");
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
                    MessageBox.Show("The File is not loaded correctly ?");
                }
            }
            else
            {
                MessageBox.Show("The File is not loaded correctly");
            }
        }
        private void ColumnDisplayClick(object sender, EventArgs e)
        {
            int DisplaySelected;
            for (var i = 0; i < ColNum; i++)
            {
                DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
            for (var i = 0; i < ColNum; i++)
            {
                DataGridTable.Columns[i].Visible = true;
            }

            foreach (DataRowView drowVw in DataAdvanceOption.checkedListBox1.CheckedItems)
            {
                DisplaySelected = int.Parse(drowVw[DataAdvanceOption.checkedListBox1.ValueMember].ToString());
                DataGridTable.Columns[DisplaySelected].Visible = false;
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
                MessageBox.Show("The File is not Loaded");
            }
        }

        private void DataGridTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ListBox list;
            list = new ListBox() { Parent = this, Dock = DockStyle.Fill };
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
            // MessageBox.Show("THe custom_sortcompare working");
            var Column_index = e.Column.Index;
            if (e.CellValue1 != null && e.CellValue2 != null)
            {
                e.SortResult = String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
                if (Double.TryParse(e.CellValue1.ToString(), out var InputDoubleValue))
                {
                    if (Double.TryParse(e.CellValue2.ToString(), out var SelDoubleValue))
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

            // calculate the indexes for scan numbers and peptides
            int scanNumIndex = 0, peptideIndex = 0;
            var datasetIndex = -1;
            var precursorIndex = 0;
            for (var i = 0; i < ColNum; i++)
            {
                if (DataGridTable.Columns[i].HeaderText.ToLower().Contains("scan"))
                {
                    scanNumIndex = i;
                }
                if (DataGridTable.Columns[i].HeaderText.ToLower().Contains("peptide"))
                {
                    peptideIndex = i;
                }
                if (DataGridTable.Columns[i].HeaderText.ToLower().Contains("dataset"))
                {
                    datasetIndex = i;
                }
                if (DataGridTable.Columns[i].HeaderText.ToLower().Contains("precursormz"))
                {
                    precursorIndex = i;
                }
            }

            // get the scan numbers and peptides for the rows
            foreach (DataGridViewRow row in DataGridTable.Rows)
            {
                if (row.Visible || !useOnlyVisible)
                {
                    var scanNumber = row.Cells[scanNumIndex].Value.ToString();
                    var peptide = row.Cells[peptideIndex].Value.ToString();
                    var dataset = datasetIndex != -1 ? row.Cells[datasetIndex].Value.ToString() : null;
                    var precursor = row.Cells[precursorIndex].Value.ToString();

                    PeptideCleavageStateCalculator.SplitPrefixAndSuffixFromSequence(peptide, out var sequence,
                        out var prefix, out var suffix);

                    peptidesAndScans.Add(new ResultRowData(dataset, scanNumber, peptide, precursor));
                }
            }

            return peptidesAndScans;
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
                    MessageBox.Show("The File should be loaded first");
                }
                e.Handled = true;
            }
        }
    }
}
