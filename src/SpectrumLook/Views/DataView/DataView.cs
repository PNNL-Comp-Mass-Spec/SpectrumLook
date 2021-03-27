using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PHRPReader;
using SpectrumLook.Builders;

namespace SpectrumLook.Views
{
    //TODO : Need to inherit from IObserver and override the Update function! Otherwise the options will not update properly.
    public partial class DataView : Form
    {
        ////////private DataViewOptions m_dataViewOptions;
        Manager m_manager;
        public DataTable DataTableForDisplay;
        public DataViewAdvance DataAdvanceOption;
        public volatile bool shouldStop;
        private int ColNum;
        private int RowNum;
        List<string> HeaderList = new List<string>();
        private ContextMenu Menu = new ContextMenu();
        private int ScanNumColumn;
        private int PetideStringColumn;
        private int DatasetColumn = -1;
        private int PrecursorMzColumn;

        public DataView(Manager manager)
        {
            InitializeComponent();
            m_manager = manager;
            DataTableForDisplay = new DataTable();
            DataGridTable.KeyDown += new KeyEventHandler(DataGridTable_KeyDown);
            DataGridTable.TabIndex = 1;
        }
        void DataGridTable_KeyDown(object sender, KeyEventArgs e)
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

                //ScanNumber and Peptide Need to be selected based off file type.
                var Peptide = selectedRow.Cells[PetideStringColumn].Value.ToString();
                var ScanNumber = selectedRow.Cells[ScanNumColumn].Value.ToString();
                if (DatasetColumn != -1)
                {
                    m_manager.DataFileName = selectedRow.Cells[DatasetColumn].Value.ToString();
                }
                m_manager.PrecursorMZ = double.Parse(selectedRow.Cells[PrecursorMzColumn].Value.ToString());

                string sequence;
                string prefix;
                string suffix;
                PeptideCleavageStateCalculator.SplitPrefixAndSuffixFromSequence(Peptide, out sequence, out prefix, out suffix);

                m_manager.HandleSelectScanAndPeptide(ScanNumber, sequence);
                m_manager.FocusOnControl(DataGridTable);
                m_manager.callcombobox();
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

        public void SetScanIndexAndPeptideIndex(int peptideIndex, int scanIndex, int precursorIndex, int datasetIndex = -1)
        {
            ScanNumColumn = scanIndex;
            PetideStringColumn = peptideIndex;
            DatasetColumn = datasetIndex;
            PrecursorMzColumn = precursorIndex;
        }

        public void SetDataTable(DataTable newTable)
        {
            //DataViewProgress ProgressWindow = new DataViewProgress();
            //ProgressWindow.Show();
            var workerThread = new Thread(DisplayProgress);
            workerThread.Start();

            ToolStripMenuItem InsertItem = null;
            this.DataTableForDisplay = new DataTable();
            DataGridTable.SelectionChanged -= new System.EventHandler(this.DataGridTable_SelectionChanged);
            DataGridTable.Rows.Clear();
            DataGridTable.Columns.Clear();
            this.ColNum = 0;
            this.RowNum = 0;
            HeaderList.Clear();
            Menu.MenuItems.Clear();
            this.DataTableForDisplay = newTable;
            this.ColNum = DataTableForDisplay.Columns.Count;
            this.RowNum = DataTableForDisplay.Rows.Count;
            for (var i = 0; i < ColNum; i++)
            {
                this.DataGridTable.Columns.Add(DataTableForDisplay.Columns[i].ColumnName, DataTableForDisplay.Columns[i].ColumnName);
            }
            for (var i = 0; i < RowNum; i++)
            {
                DataGridTable.Rows.Add();
                for (var j = 0; j < ColNum; j++)
                {
                    this.DataGridTable.Rows[i].Cells[j].Value = DataTableForDisplay.Rows[i][j].ToString();
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
                this.ColcontextMenuStrip.Items.Insert(i, InsertItem);
            }
            for (var i = 0; i < DataGridTable.ColumnCount; i++)
            {
                Menu.MenuItems.Add(DataGridTable.Columns[i].ToString(), Menu_Click);
            }


            DataAdvanceOption = new DataViewAdvance();
            this.SetAdvancedOptions();
            DataAdvanceOption.Visible = false;
            DataAdvanceOption.AdvSrchOption.Click += new EventHandler(AdvancedSearchClick);
            DataAdvanceOption.ColmDisplayOption.Click += new EventHandler(ColumnDisplayClick);
            DataAdvanceOption.AdvSrcCancel.Click += new EventHandler(AdvSrcCancel_Click);
            DataAdvanceOption.FormClosing += new FormClosingEventHandler(DataAdvanceOption_FormClosing);

            this.DataGridTable.SortCompare += new DataGridViewSortCompareEventHandler(Custom_SortCompare);

            //Adding event back
            DataGridTable.SelectionChanged += new System.EventHandler(this.DataGridTable_SelectionChanged);

            RequestStop();
        }

        void DataAdvanceOption_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.DataAdvanceOption.Visible = false;
        }
        void AdvSrcCancel_Click(object sender, EventArgs e)
        {
            this.DataAdvanceOption.Visible = false;
        }
        void InsertItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem InsertItem = null;
            for (var i = 0; i < ColNum; i++)
            {
                InsertItem = (ToolStripMenuItem)ColcontextMenuStrip.Items[i];
                DataGridTable.Columns[i].Visible = InsertItem.Checked;

            }
        }
        public void SetAdvancedOptions()
        {
            DataAdvanceOption.SetAdvance(HeaderList);
        }
        public void SearchButton_Click(object sender, EventArgs e)
        {
            var SearchSubString = this.SearchBox.Text;
            if (DataGridTable != null)
            {
                SimpleSearch(SearchSubString);
            }
            else
            {
                MessageBox.Show("The File should be loaded first");
            }

        }
        public void SimpleSearch(string inputS)
        {
            bool VisibleRows;
            var SearchText = inputS.ToLower();
            string StringFromCell = null;
            var j = 0;
            var i = 0;
            if (SearchText == "")//Null string
            {
                //show All the table
                for (i = 0; i < DataGridTable.Columns.Count; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                for (i = 0; i < this.DataGridTable.Rows.Count; i++)
                {
                    this.DataGridTable.Rows[i].Visible = true;
                }
                for (i = 0; i < DataGridTable.Columns.Count; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                }
            }
            else
            {
                for (i = 0; i < DataGridTable.Columns.Count; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }
                for (i = 0; i < DataGridTable.Rows.Count; i++)
                {
                    VisibleRows = false;

                    for (j = 0; j < DataGridTable.Columns.Count && VisibleRows == false; j++)
                    {
                        StringFromCell = DataGridTable.Rows[i].Cells[j].Value.ToString().ToLower();
                        if (StringFromCell.Contains(SearchText))
                        {
                            VisibleRows = true;
                        }
                    }


                    if (VisibleRows == true)
                    {
                        DataGridTable.Rows[i].Visible = true;
                    }
                    else
                    {
                        DataGridTable.Rows[i].Visible = false;
                    }
                }
                for (i = 0; i < DataGridTable.Columns.Count; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                }
            }
        }
        public void AdvancedSearch(string AndOr, string SelCol, string SelOpt, string TextInput)
        {
            bool IsAnd;
            var CellNumber = 0;
            for (var i = 0; i < ColNum; i++)
            {
                if (DataGridTable.Columns[i].HeaderText.Equals(SelCol))
                {
                    CellNumber = i;
                }
            }
            if (DataGridTable.Rows.Count <= 0)
            {
                MessageBox.Show("No data loaded");
            }
            else
            {
                for (var i = 0; i < ColNum; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

                if (AndOr == "AND")//and
                {
                    IsAnd = true;
                }
                else
                {
                    IsAnd = false;//or
                }
                if (SelOpt == "contains")//contains
                {
                    Contains(CellNumber, TextInput, IsAnd);
                }
                else if (SelOpt == ">")//>
                {
                    GreaterThan(CellNumber, TextInput, IsAnd);
                }
                else if (SelOpt == "<")//<
                {
                    LessThan(CellNumber, TextInput, IsAnd);
                }
                else if (SelOpt == "=")//=
                {
                    Equal(CellNumber, TextInput, IsAnd);
                }
                for (var i = 0; i < ColNum; i++)
                {
                    DataGridTable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
                }
            }
        }
        public void Contains(int CellCount, string Input, bool AND)
        {
            var indexR = 0;
            var SearchInput = Input.ToLower();
            DataGridTable.CurrentCell = null;
            for (indexR = 0; indexR < RowNum; indexR++)
            {
                if (AND)
                {
                    if (!DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString().ToLower().Contains(SearchInput))
                    {
                        if (DataGridTable.Rows[indexR].Visible == true)
                        {
                            DataGridTable.Rows[indexR].Visible = false;
                        }
                        else
                        {
                            DataGridTable.Rows[indexR].Visible = false;
                        }
                    }
                }
                else//DataGridTable.Rows[RowNum].Visible == false ->OR function
                {
                    if (DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString().ToLower().Contains(Input))//condition is matched
                    {
                        DataGridTable.Rows[indexR].Visible = true;
                    }
                }
            }
        }
        public void GreaterThan(int CellCount, string Input, bool AND)
        {
            double InputDoubleValue = 0;
            double SelDoubleValue = 0;
            var Input_lower = Input.ToLower();
            var indexR = 0;
            DataGridTable.CurrentCell = null;
            for (indexR = 0; indexR < RowNum; indexR++)
            {
                if (AND)
                {
                    if (Double.TryParse(Input_lower, out InputDoubleValue))//what user input
                    {
                        if (Double.TryParse(DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString().ToLower(), out SelDoubleValue))//The value from DataGridTable
                        {
                            if (SelDoubleValue <= InputDoubleValue)//if the value in the table is less than or equal to the user input
                            {
                                if (DataGridTable.Rows[indexR].Visible == true)
                                {
                                    DataGridTable.Rows[indexR].Visible = false;
                                }
                                else//not necessary
                                {
                                    DataGridTable.Rows[indexR].Visible = false;
                                }
                            }
                        }

                    }
                }
                else//DataGridTable.Rows[indexR].Visible != AND --> OR
                {
                    if (Double.TryParse(Input_lower, out InputDoubleValue))
                    {
                        if (Double.TryParse(DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString().ToLower(), out SelDoubleValue))
                        {
                            if (SelDoubleValue > InputDoubleValue)//the condition is matched, the table value is larger than user input
                            {
                                DataGridTable.Rows[indexR].Visible = true;
                            }
                        }
                    }
                }
            }
        }
        public void Equal(int CellCount, string Input, bool AND)
        {
            double InputDoubleValue;
            double SelDoubleValue;
            var Input_lower = Input.ToLower();
            var indexR = 0;
            DataGridTable.CurrentCell = null;
            for (indexR = 0; indexR < RowNum; indexR++)
            {
                if (AND)
                {

                    if (Double.TryParse(Input_lower, out InputDoubleValue))
                    {
                        if (Double.TryParse(DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString().ToLower(), out SelDoubleValue))
                        {
                            if (SelDoubleValue > InputDoubleValue || SelDoubleValue > InputDoubleValue)//If the input is less or larger than the cell value
                            {
                                if (DataGridTable.Rows[indexR].Visible == true)
                                {
                                    DataGridTable.Rows[indexR].Visible = false;
                                }
                                else//not necessary
                                {
                                    DataGridTable.Rows[indexR].Visible = false;
                                }

                            }
                        }
                    }


                }
                else//OR
                {

                    if (Double.TryParse(Input_lower, out InputDoubleValue))
                    {
                        if (Double.TryParse(DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString().ToLower(), out SelDoubleValue))
                        {
                            if (SelDoubleValue == InputDoubleValue)//condition is matched
                            {
                                DataGridTable.Rows[indexR].Visible = true;
                            }
                        }
                    }

                }
            }
        }
        public void LessThan(int CellCount, string Input, bool AND)
        {
            double InputDoubleValue;
            double SelDoubleValue;
            var Input_lower = Input.ToLower();
            var indexR = 0;
            DataGridTable.CurrentCell = null;
            for (indexR = 0; indexR < RowNum; indexR++)
            {
                if (AND)
                {

                    if (Double.TryParse(Input_lower, out InputDoubleValue))
                    {
                        if (Double.TryParse(DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString().ToLower(), out SelDoubleValue))
                        {
                            Console.WriteLine(SelDoubleValue);
                            if (SelDoubleValue >= InputDoubleValue && DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString() != null)//if the input value is less than the cell value
                            {
                                if (DataGridTable.Rows[indexR].Visible == true)
                                {
                                    DataGridTable.Rows[indexR].Visible = false;
                                }
                                else
                                {
                                    DataGridTable.Rows[indexR].Visible = false;
                                }
                            }
                        }
                    }

                }
                else
                {

                    if (Double.TryParse(Input_lower, out InputDoubleValue))
                    {
                        if (Double.TryParse(DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString(), out SelDoubleValue))
                        {
                            if (SelDoubleValue > InputDoubleValue && DataGridTable.Rows[indexR].Cells[CellCount].Value.ToString() != null)//condition is matched
                            {
                                DataGridTable.Rows[indexR].Visible = true;
                            }
                        }
                    }

                }
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
                        AdvancedSearch("AND", DataAdvanceOption.SeaFil_Pep1.Text, DataAdvanceOption.FilterComboBox1.Text, DataAdvanceOption.Filter_Value1.Text);
                        if (DataAdvanceOption.AndOrComboBox1.Text != null && DataAdvanceOption.SeaFil_Pep2.Text != null && DataAdvanceOption.FilterComboBox2.Text != null && DataAdvanceOption.Filter_Value2.Text != null)
                        {
                            AdvancedSearch(DataAdvanceOption.AndOrComboBox1.Text, DataAdvanceOption.SeaFil_Pep2.Text, DataAdvanceOption.FilterComboBox2.Text, DataAdvanceOption.Filter_Value2.Text);
                            if (DataAdvanceOption.AndOrComboBox2.Text != null && DataAdvanceOption.SeaFil_Pep3.Text != null && DataAdvanceOption.FilterComboBox3.Text != null && DataAdvanceOption.Filter_Value3.Text != null)
                            {
                                AdvancedSearch(DataAdvanceOption.AndOrComboBox2.Text, DataAdvanceOption.SeaFil_Pep3.Text, DataAdvanceOption.FilterComboBox3.Text, DataAdvanceOption.Filter_Value3.Text);
                                if (DataAdvanceOption.AndOrComboBox3.Text != null && DataAdvanceOption.SeaFil_Pep4.Text != null && DataAdvanceOption.FilterComboBox4.Text != null && DataAdvanceOption.Filter_Value4.Text != null)
                                {
                                    AdvancedSearch(DataAdvanceOption.AndOrComboBox3.Text, DataAdvanceOption.SeaFil_Pep4.Text, DataAdvanceOption.FilterComboBox4.Text, DataAdvanceOption.Filter_Value4.Text);
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
            //DataAdvanceOption.panel1.Visible = false;
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
                    //Column1.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                    DataGridTable.Sort(Column2, direction);
                    Column1.HeaderCell.SortGlyphDirection =
                    direction == ListSortDirection.Ascending ?
                    System.Windows.Forms.SortOrder.Ascending : System.Windows.Forms.SortOrder.Descending;
                    if (DataAdvanceOption.SrtClm3.DataSource != null)
                    {

                        DataGridViewColumn Column3 = DataGridTable.Columns[DataAdvanceOption.SrtClm3.ValueMember];
                        //Column2.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
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

        void DataGridTable_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ListBox list;
            list = new ListBox() { Parent = this, Dock = DockStyle.Fill };
            this.DataGridTable.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
            this.DataGridTable.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.None;
            this.DataGridTable.Columns[e.ColumnIndex].Selected = true;
            if (e.Button == MouseButtons.Right)
            {
                //show context menu
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
                ColcontextMenuStrip.Show((Control)sender, PointToClient(Control.MousePosition).X, e.Location.Y);//Somehow, PointToClick(Control.MousePosition) X axis is correct, but the Y is not. Also e.X is not correct, but e.Y has correct location, so it is combined
            }
            else if (e.Button == MouseButtons.Left)
            {

            }
            else
            {
                //Error
            }
        }

        private void Custom_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //MessageBox.Show("THe custom_sortcompare working");
            double InputDoubleValue;
            double SelDoubleValue;
            var Column_index = e.Column.Index;
            if (e.CellValue1 != null && e.CellValue2 != null)
            {
                e.SortResult = System.String.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
                if (Double.TryParse(e.CellValue1.ToString(), out InputDoubleValue))
                {
                    if (Double.TryParse(e.CellValue2.ToString(), out SelDoubleValue))
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

            //calculate the indexes for scan numbers and peptides
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

            //get the scan numbers and peptides for the rows
            foreach (DataGridViewRow row in DataGridTable.Rows)
            {
                if (row.Visible || !useOnlyVisible)
                {
                    var scanNumber = row.Cells[scanNumIndex].Value.ToString();
                    var peptide = row.Cells[peptideIndex].Value.ToString();
                    var dataset = datasetIndex != -1 ? row.Cells[datasetIndex].Value.ToString() : null;
                    var precursor = row.Cells[precursorIndex].Value.ToString();

                    string sequence;
                    string prefix;
                    string suffix;
                    PeptideCleavageStateCalculator.SplitPrefixAndSuffixFromSequence(peptide, out sequence,
                        out prefix, out suffix);

                    peptidesAndScans.Add(new ResultRowData(dataset, scanNumber, peptide, precursor));
                }
            }

            return peptidesAndScans;
        }

        private void DataGridTable_SelectionChanged(object sender, EventArgs e)
        {
            if (m_manager.DataLoaded == true)
            {
                HandleRowSelection();
            }
        }

        private void DataGridTable_Click(object sender, EventArgs e)
        {
            if (m_manager.DataLoaded == false)
            {
                HandleRowSelection();
            }
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var SearchSubString = this.SearchBox.Text;
                if (DataGridTable != null)
                {
                    SimpleSearch(SearchSubString);
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
