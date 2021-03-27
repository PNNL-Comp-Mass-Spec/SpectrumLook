using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SpectrumLook.Views.Options;

namespace SpectrumLook.Views.FragmentLadderView
{
    //TODO : Need to inherit from IObserver and override the Update function! Otherwise the options will not update properly.
    //CHECKED OUT FOR EDIT TO ROLL BACK TO PREVIOUS VERSION
    public partial class FragmentLadderView : Form, IObserver
    {
        private Manager m_manager;

        private double m_currentParentMZ;
        private List<LadderInstance> m_currentInstances;

        private bool m_clearModifications;

        private bool m_currentlyDrawing;

        public FragmentLadderOptions fragmentLadderOptions { get; set; }

        public FragmentLadderView(Manager manager)
        {
            InitializeComponent();

            m_manager = manager;

            fragmentLadderOptions = new Options.FragmentLadderOptions();

            m_clearModifications = false;

            m_currentlyDrawing = false;

            markIonSeriesHeaders();
        }

        /// <summary>
        /// This is required for the observer pattern.
        /// </summary>
        void IObserver.UpdateObserver()
        {
            this.m_manager.HandleRecalculateAllInHashCode();
        }

        /// <summary>
        /// This function will set the text field in the fragment
        /// </summary>
        /// <param name="peptide"></param>
        public void setPeptideTextBox(string peptide)
        {
            peptideEditorTextBox.Text = peptide.ToString();
        }

        public void markIonSeriesHeaders()
        {
            foreach (var header in fragmentLadderOptions.checkedHeaders)
            {
                for (var i = 0; i < columnCheckedListBox.Items.Count; i++)
                {
                    if (header == columnCheckedListBox.Items[i].ToString())
                    {
                        columnCheckedListBox.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }

        public void regenerateLadderFromSelection()
        {
            if (m_currentInstances != null && m_currentParentMZ > 0)
            {
                generateLadderFromSelection(m_currentParentMZ, m_currentInstances);
            }
        }

        public void setComboBox()
        {
            ////////THIS IS WHERE IT IS!///////
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 0;
        }

        public void setUnmatchedLabel(Color new_color)
        {
            Unmatched_Label.ForeColor = new_color;
        }

        public void setMatchedLabel(Color new_color)
        {
            Matched_Label.ForeColor = new_color;
        }

        public void generateLadderFromSelection(double parentMZ, List<LadderInstance> currentListInstances)
        {
            m_currentInstances = currentListInstances;
            m_currentParentMZ = parentMZ;

            m_currentlyDrawing = true;

            // This is a counter the fragment ladder instances
            var i = 0;

            // This is a counter for the list boxes.
            var listBoxCounter = 0;

            //this.columnCheckedListBox.Items.AddRange(currentListInstances[0].mzValueHeaders.ToArray());
            tabControl1.TabPages.Clear();
            tabControl1.Visible = false;
            tabControl1.TabPages.Add("Original");

            var indexOfFirstHalfEnd = 0;

            //find index of first non "b" checked item
            for (var j = 0; j < columnCheckedListBox.CheckedItems.Count; j++)
            {
                if (columnCheckedListBox.CheckedItems[j].ToString()[0] != 'b' && columnCheckedListBox.CheckedItems[j].ToString()[0] != 'c')
                {
                    indexOfFirstHalfEnd = j;
                    break;
                }
            }

            if(columnCheckedListBox.CheckedItems.Count != 0)
            {
                if (indexOfFirstHalfEnd == 0 && columnCheckedListBox.CheckedItems[columnCheckedListBox.CheckedItems.Count - 1].ToString()[0] == 'b')
                {
                    indexOfFirstHalfEnd = columnCheckedListBox.CheckedItems.Count;
                }
            }

            if (currentListInstances != null)
                foreach (var currentInstance in currentListInstances)
                {
                    // If plot is detached this value is incremented for each list box that represents
                    // an ion series.  For vertical fragment ladder orientation.
                    var xListBoxPosition = 6;

                    // If plot is detached this value is incremented for each list box that represents
                    // an ion series.  For horizontal fragment ladder orientation.
                    var yListBoxPosition = 6;

                    if (i > 0)
                        tabControl1.TabPages.Add("Modified" + i.ToString());
                    var tempListBox = new System.Windows.Forms.ListBox();

                    //print all b or c ions that exist before the peptide sequence
                    for (var index = 0; index < indexOfFirstHalfEnd; index++)
                    {
                        //if (i == tabControl1.SelectedIndex)
                        peptideEditorTextBox.Text = currentInstance.PeptideString.ToString();
                        tempListBox = new System.Windows.Forms.ListBox();
                        tempListBox.BackColor = System.Drawing.SystemColors.Control;
                        tempListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        tempListBox.ColumnWidth = 40;
                        tempListBox.FormattingEnabled = true;
                        tempListBox.Location = new System.Drawing.Point(xListBoxPosition, yListBoxPosition);
                        if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                            tempListBox.MultiColumn = true;
                        else
                            tempListBox.MultiColumn = false;
                        tempListBox.Size = new System.Drawing.Size(45, 15);
                        tempListBox.BorderStyle = BorderStyle.None;
                        tempListBox.DrawMode = DrawMode.OwnerDrawFixed;
                        tempListBox.DrawItem += new DrawItemEventHandler(tempListBox_DrawItem);
                        tempListBox.Click += new EventHandler(tempListBox_Click);
                        tempListBox.DoubleClick += new EventHandler(tempListBox_DoubleClick);
                        tempListBox.Items.Add(columnCheckedListBox.CheckedItems[index].ToString());

                        for (var ind = 0; ind < currentInstance.mzValueHeaders.Count; ind++)
                        {
                            if (currentInstance.mzValueHeaders[ind] == columnCheckedListBox.CheckedItems[index].ToString())
                                listBoxCounter = ind;
                        }

                        // Reverse Array for y and z series
                        //This Reverse stuff should really be done in the ladder Instance Builder.
                        /*if ((columnCheckedListBox.CheckedItems[index].ToString().Contains("y")) ||
                            (columnCheckedListBox.CheckedItems[index].ToString().Contains("z")))
                        {
                            string[] tempArray = currentInstance.mzValue[listBoxCounter];
                            int findNonNull = 0;
                            while ((findNonNull < tempArray.Length) && (tempArray[findNonNull] == ""))
                                ++findNonNull;
                            string[] splittedSubArrayOne = tempArray[findNonNull].Split('|');
                            string[] splittedSubArrayTwo = tempArray[(findNonNull + 1)].Split('|');
                            double outValue1 = 0.0;
                            double outValue2 = 0.0;

                            double.TryParse(splittedSubArrayOne[0], out outValue1);
                            double.TryParse(splittedSubArrayTwo[0], out outValue2);
                            if (outValue2 > outValue1)
                            {
                                Array.Reverse(tempArray);
                            }
                            tempListBox.Items.AddRange(tempArray);
                        }
                        else
                        {*/
                            var currentLadderValueIndex = 0;
                            while (currentLadderValueIndex < currentInstance.mzValue[listBoxCounter].Length)
                            {
                                if (currentInstance.mzValue[listBoxCounter][currentLadderValueIndex] == "")
                                {
                                    tempListBox.Items.Insert((currentLadderValueIndex + 1), "");
                                }
                                else
                                {
                                    tempListBox.Items.Insert((currentLadderValueIndex + 1), currentInstance.mzValue[listBoxCounter][currentLadderValueIndex]);
                                }
                                ++currentLadderValueIndex;
                            }
                        //}

                        /**************This is to find the largest string. **************/
                        var g = tempListBox.CreateGraphics();

                        var largestSize = g.MeasureString(tempListBox.Items[0].ToString(), tempListBox.Font);

                        for (var k = 1; k < tempListBox.Items.Count; ++k)
                        {
                            var tempString = tempListBox.Items[k].ToString();
                            if (tempString != "")
                                if (largestSize.Width < g.MeasureString(tempString.Split('|')[0], tempListBox.Font).Width)
                                {
                                    largestSize = g.MeasureString(tempString.Split('|')[0], tempListBox.Font);
                                }
                        }

                        if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                            tempListBox.Size = new System.Drawing.Size(((tempListBox.Items.Count * 40) + 40), 13);
                        else
                            tempListBox.Size = new System.Drawing.Size((int)largestSize.Width, ((tempListBox.Items.Count * 13) + 15));
                        tempListBox.Location = new Point(xListBoxPosition, yListBoxPosition);
                        /**************************************************/

                        if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                            yListBoxPosition += 20 + 3;
                        else
                            xListBoxPosition += (int)largestSize.Width + 3;
                        tabControl1.TabPages[i].CreateControl();
                        tabControl1.TabPages[i].Controls.Add(tempListBox);
                    }

                    //print the index from the front of the peptide sequence
                    tempListBox = new System.Windows.Forms.ListBox();
                    tempListBox.BackColor = System.Drawing.SystemColors.Control;
                    tempListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    tempListBox.ColumnWidth = 40;
                    tempListBox.FormattingEnabled = true;
                    tempListBox.Location = new System.Drawing.Point(xListBoxPosition, yListBoxPosition);
                    if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                        tempListBox.MultiColumn = true;
                    else
                        tempListBox.MultiColumn = false;
                    tempListBox.Size = new System.Drawing.Size(45, 15);
                    tempListBox.BorderStyle = BorderStyle.None;
                    tempListBox.Click += new EventHandler(tempListBox_Click);
                    tempListBox.Items.Add((object)(" "));

                    var lengthMinusMods = 0;
                    var modificationList = "*+@!&#$%~`";

                    //calculate length of string minus modifications
                    foreach (var curChar in currentInstance.PeptideString.ToCharArray())
                    {
                        if (!modificationList.Contains(curChar))
                        {
                            lengthMinusMods++;
                        }
                    }

                    for (var peptideIndex = 0; peptideIndex < lengthMinusMods; peptideIndex++)
                    {
                        var indexStr = (peptideIndex + 1).ToString();
                        tempListBox.Items.Add((object)indexStr);
                    }

                    if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                        tempListBox.Size = new System.Drawing.Size(((tempListBox.Items.Count * 40) + 40), 13);
                    else
                        tempListBox.Size = new System.Drawing.Size(40, ((tempListBox.Items.Count * 13) + 15));
                    tempListBox.Location = new Point(xListBoxPosition, yListBoxPosition);
                    tabControl1.TabPages[i].CreateControl();
                    tabControl1.TabPages[i].Controls.Add(tempListBox);
                    if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                        yListBoxPosition += 20 + 3;
                    else
                        xListBoxPosition += 40 + 3;

                    //draw the peptide sequence
                    tempListBox = new System.Windows.Forms.ListBox();
                    tempListBox.BackColor = System.Drawing.SystemColors.Control;
                    tempListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    tempListBox.ColumnWidth = 40;
                    tempListBox.FormattingEnabled = true;
                    tempListBox.Location = new System.Drawing.Point(xListBoxPosition, yListBoxPosition);
                    if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                        tempListBox.MultiColumn = true;
                    else
                        tempListBox.MultiColumn = false;
                    tempListBox.Size = new System.Drawing.Size(45, 15);
                    tempListBox.BorderStyle = BorderStyle.None;
                    tempListBox.Click += new EventHandler(tempListBox_Click);
                    var peptide = currentInstance.PeptideString.ToCharArray();
                    tempListBox.Items.Add(" ");

                    for (var peptideStringIndex = 0; peptideStringIndex < peptide.Length; ++peptideStringIndex)
                    {
                        var aminoAcidCode = " " + peptide[peptideStringIndex];
                        if ((peptideStringIndex + 1) < peptide.Length &&
                            this.fragmentLadderOptions.modificationList.ContainsKey(peptide[(peptideStringIndex + 1)]))
                        {
                            aminoAcidCode += peptide[(peptideStringIndex + 1)];
                            ++peptideStringIndex;
                        }
                        tempListBox.Items.Add(aminoAcidCode);
                    }

                    if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                        tempListBox.Size = new System.Drawing.Size(((tempListBox.Items.Count * 40) + 40), 13);
                    else
                        tempListBox.Size = new System.Drawing.Size(40, ((tempListBox.Items.Count * 13) + 15));
                    tempListBox.Location = new Point(xListBoxPosition, yListBoxPosition);
                    tabControl1.TabPages[i].CreateControl();
                    tabControl1.TabPages[i].Controls.Add(tempListBox);
                    if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                        yListBoxPosition += 20 + 3;
                    else
                        xListBoxPosition += 40 + 3;
                    //end drawing peptide sequence

                    //print the index from the front of the peptide sequence
                    tempListBox = new System.Windows.Forms.ListBox();
                    tempListBox.BackColor = System.Drawing.SystemColors.Control;
                    tempListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    tempListBox.ColumnWidth = 40;
                    tempListBox.FormattingEnabled = true;
                    tempListBox.Location = new System.Drawing.Point(xListBoxPosition, yListBoxPosition);
                    if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                        tempListBox.MultiColumn = true;
                    else
                        tempListBox.MultiColumn = false;
                    tempListBox.Size = new System.Drawing.Size(45, 15);
                    tempListBox.BorderStyle = BorderStyle.None;
                    tempListBox.Click += new EventHandler(tempListBox_Click);
                    tempListBox.Items.Add((object)(" "));

                    for (var peptideIndex = 0; peptideIndex < lengthMinusMods; peptideIndex++)
                    {
                        var indexStr = (lengthMinusMods - peptideIndex).ToString();
                        tempListBox.Items.Add((object)indexStr);
                    }

                    if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                        tempListBox.Size = new System.Drawing.Size(((tempListBox.Items.Count * 40) + 40), 13);
                    else
                        tempListBox.Size = new System.Drawing.Size(40, ((tempListBox.Items.Count * 13) + 15));
                    tempListBox.Location = new Point(xListBoxPosition, yListBoxPosition);
                    tabControl1.TabPages[i].CreateControl();
                    tabControl1.TabPages[i].Controls.Add(tempListBox);
                    if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                        yListBoxPosition += 20 + 3;
                    else
                        xListBoxPosition += 40 + 3;

                    //draw the y or z ions that occur after the peptide sequence
                    for (var index = indexOfFirstHalfEnd; index < columnCheckedListBox.CheckedItems.Count; index++)
                    {
                        //if (i == tabControl1.SelectedIndex)
                        peptideEditorTextBox.Text = currentInstance.PeptideString.ToString();
                        tempListBox = new System.Windows.Forms.ListBox();
                        tempListBox.BackColor = System.Drawing.SystemColors.Control;
                        tempListBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        tempListBox.ColumnWidth = 40;
                        tempListBox.FormattingEnabled = true;
                        tempListBox.Location = new System.Drawing.Point(xListBoxPosition, yListBoxPosition);
                        if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                            tempListBox.MultiColumn = true;
                        else
                            tempListBox.MultiColumn = false;
                        tempListBox.Size = new System.Drawing.Size(45, 15);
                        tempListBox.BorderStyle = BorderStyle.None;
                        tempListBox.DrawMode = DrawMode.OwnerDrawFixed;
                        tempListBox.DrawItem += new DrawItemEventHandler(tempListBox_DrawItem);
                        tempListBox.Click += new EventHandler(tempListBox_Click);
                        tempListBox.DoubleClick += new EventHandler(tempListBox_DoubleClick);
                        tempListBox.Items.Add(columnCheckedListBox.CheckedItems[index].ToString());

                        for (var ind = 0; ind < currentInstance.mzValueHeaders.Count; ind++)
                        {
                            if (currentInstance.mzValueHeaders[ind] == columnCheckedListBox.CheckedItems[index].ToString())
                                listBoxCounter = ind;
                        }

                        // Reverse Array for y and z series
                        //This Reverse stuff should really be done in the ladder Instance Builder.
                        /*if ((columnCheckedListBox.CheckedItems[index].ToString().Contains("y")) ||
                            (columnCheckedListBox.CheckedItems[index].ToString().Contains("z")))
                        {*/
                            var tempArray = currentInstance.mzValue[listBoxCounter];
                            var findNonNull = 0;
                            while ((findNonNull < tempArray.Length) && (tempArray[findNonNull] == ""))
                                ++findNonNull;
                            var splittedSubArrayOne = tempArray[findNonNull].Split('|');
                            var splittedSubArrayTwo = tempArray[(findNonNull + 1)].Split('|');
                            var outValue1 = 0.0;
                            var outValue2 = 0.0;

                            double.TryParse(splittedSubArrayOne[0], out outValue1);
                            double.TryParse(splittedSubArrayTwo[0], out outValue2);
                            if (outValue2 > outValue1)
                            {
                                Array.Reverse(tempArray);
                            }
                            tempListBox.Items.AddRange(tempArray);
                        /*}
                        else
                        {
                            int currentLadderValueIndex = 0;
                            while (currentLadderValueIndex < currentInstance.mzValue[listBoxCounter].Length)
                            {
                                if (currentInstance.mzValue[listBoxCounter][currentLadderValueIndex] == "")
                                {
                                    tempListBox.Items.Insert((currentLadderValueIndex + 1), "");
                                }
                                else
                                {
                                    tempListBox.Items.Insert((currentLadderValueIndex + 1), currentInstance.mzValue[listBoxCounter][currentLadderValueIndex]);
                                }
                                ++currentLadderValueIndex;
                            }
                        }*/

                        /**************This is to find the largest string. **************/
                        var g = tempListBox.CreateGraphics();

                        var largestSize = g.MeasureString(tempListBox.Items[0].ToString(), tempListBox.Font);

                        for (var k = 1; k < tempListBox.Items.Count; ++k)
                        {
                            var tempString = tempListBox.Items[k].ToString();
                            if (tempString != "")
                                if (largestSize.Width < g.MeasureString(tempString.Split('|')[0], tempListBox.Font).Width)
                                {
                                    largestSize = g.MeasureString(tempString.Split('|')[0], tempListBox.Font);
                                }
                        }

                        if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                            tempListBox.Size = new System.Drawing.Size(((tempListBox.Items.Count * 40) + 40), 13);
                        else
                            tempListBox.Size = new System.Drawing.Size((int)largestSize.Width, ((tempListBox.Items.Count * 13) + 15));
                        tempListBox.Location = new Point(xListBoxPosition, yListBoxPosition);
                        /**************************************************/

                        if (!m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)// m_fragmentLadderOptions.plotDetached)
                            yListBoxPosition += 20 + 3;
                        else
                            xListBoxPosition += (int)largestSize.Width + 3;
                        tabControl1.TabPages[i].CreateControl();
                        tabControl1.TabPages[i].Controls.Add(tempListBox);
                    }

                    tabControl1.TabPages[i].AutoScroll = true;
                    tabControl1.SelectedIndex = i;
                    ++i;
                }
            tabControl1.SelectedIndex = 0;
            tabControl1.Visible = true;
            m_currentlyDrawing = false;
        }

        /// <summary>
        /// handles focusing on the plot when an element is double clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tempListBox_DoubleClick(object sender, EventArgs e)
        {
            var selectedBox = (ListBox)sender;
            double selectedValue = -1;

            try
            {
                var selectedItem = (string)selectedBox.Items[selectedBox.SelectedIndex];
                var selectedMZ = selectedItem.Substring(0, selectedItem.IndexOf('|'));
                selectedValue = Convert.ToDouble(selectedMZ);
            }
            catch { }

            if (selectedValue != -1)
            {
                m_manager.FocusPlotOnPoint(selectedValue);
            }
        }

        /// <summary>
        /// Handles selecting only one textbox at a time in the FragmentLadder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tempListBox_Click(object sender, EventArgs e)
        {
            var selectedBox = (ListBox)sender;
            var selectedIndex = selectedBox.SelectedIndex;
            var currentTab = tabControl1.TabPages[tabControl1.SelectedIndex];

            foreach (Control ctrl in currentTab.Controls)
            {
                var box = (ListBox)ctrl;
                box.ClearSelected();
            }

            selectedBox.SelectedIndex = selectedIndex;
        }

        /// <summary>
        /// This is an event that is called when ever an Item is added to a list box.
        /// This allows us to draw items with Red or Black.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tempListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            var selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);
            var itemIndex = e.Index;
            var matchedItem = new SolidBrush(this.m_manager.m_plot.m_options.matchedColor);
            var unmatchedItem = new SolidBrush(this.m_manager.m_plot.m_options.unmatchedColor);

            var listBoxHandler = (ListBox)sender;

            if (itemIndex >= 0 && itemIndex < listBoxHandler.Items.Count)
            {
                var textToPrint = listBoxHandler.Items[itemIndex].ToString();
                var g = e.Graphics;

                Color color;
                if (selected)
                {
                    color = Color.FromKnownColor(KnownColor.Highlight);
                }
                else
                {
                    color = Color.FromKnownColor(KnownColor.Transparent);
                }
                g.FillRectangle(new SolidBrush(color), e.Bounds);

                if (textToPrint != "" && itemIndex != 0)
                {
                    if (bool.Parse(textToPrint.Split('|')[1]))
                        g.DrawString(textToPrint.Split('|')[0], e.Font, matchedItem, listBoxHandler.GetItemRectangle(itemIndex).Location);
                    else
                        g.DrawString(textToPrint.Split('|')[0], e.Font, unmatchedItem, listBoxHandler.GetItemRectangle(itemIndex).Location);
                }
                else
                {
                    g.DrawString(textToPrint, e.Font, unmatchedItem, listBoxHandler.GetItemRectangle(itemIndex).Location);
                }
            }
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// This is an event that displays or hides the the check box list of ion series
        /// on the fragment ladder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void columnButton_Click(object sender, EventArgs e)
        {
            if (columnPanel.Visible == true)
            {
                columnPanel.Visible = false;
                columnLabel.Visible = false;
                columnCheckedListBox.Visible = false;
                columnSaveButton.Visible = false;
                columnClearButton.Visible = false;
            }
            else
            {
                columnPanel.Visible = true;
                columnLabel.Visible = true;
                columnCheckedListBox.Visible = true;
                columnSaveButton.Visible = true;
                columnClearButton.Visible = true;
            }
        }

        /// <summary>
        /// This event is called when the user choses the Apply button after making a change
        /// to the list of checked columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void columnSaveButton_Click(object sender, EventArgs e)
        {
            columnPanel.Visible = false;
            columnLabel.Visible = false;
            columnCheckedListBox.Visible = false;
            columnSaveButton.Visible = false;
            try
            {
                if (comboBox1.Text == "CID")
                    m_manager.HandlefragmentLadderModeChange(false);
                else
                    m_manager.HandlefragmentLadderModeChange(true);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// This event is called when the user choses the Clear button
        /// in the list of checked columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void columnClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                 foreach(int indexChecked in columnCheckedListBox.CheckedIndices) {
                     columnCheckedListBox.SetItemChecked(indexChecked, false);
                 }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// This event is called when a user makes a modification to the peptide sequence.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateLadderFromPeptideInput(object sender, EventArgs e)
        {
            var peptide_exists = false;
            for (var i = 0; i < peptideEditorTextBox.Text.Length; i++)
            {
                if (Char.IsUpper(peptideEditorTextBox.Text[i]))
                {
                    peptide_exists = true;
                    break;
                }
            }
            if (peptide_exists == false)
            {
                peptideEditorTextBox.Text = peptideEditorTextBox.Text.ToUpper();
            }

            var newTabIndex = tabControl1.TabCount;
            if (peptideEditorTextBox.Text == "")
                return;

            var changeTab = false;

            changeTab = m_manager.HandleInputPeptide(peptideEditorTextBox.Text);
            if (changeTab)
            {
                tabControl1.SelectedIndex = newTabIndex;
            }
        }

        /// <summary>
        /// This event is called when the fragmentation mode is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeFragmentLadderMode(object sender, EventArgs e)
        {
            if (comboBox1.Text == "CID")
            {
                columnCheckedListBox.Items.Clear();
                columnCheckedListBox.Items.AddRange(new object[] {
                "b",
                "b++",
                "b+++",
                "b+++-H2O",
                "b+++-NH3",
                "b++-H2O",
                "b++-NH3",
                "b-H2O",
                "b-NH3",
                "y",
                "y++",
                "y+++",
                "y+++-H2O",
                "y+++-NH3",
                "y++-H2O",
                "y++-NH3",
                "y-H2O",
                "y-NH3"});
                columnCheckedListBox.SetItemChecked(0, true);
                columnCheckedListBox.SetItemChecked(1, true);
                columnCheckedListBox.SetItemChecked(9, true);
                columnCheckedListBox.SetItemChecked(10, true);
                m_manager.HandlefragmentLadderModeChange(false);
            }
            else
            {
                columnCheckedListBox.Items.Clear();
                columnCheckedListBox.Items.AddRange(new object[] {
                "c",
                "c++",
                "c+++",
                "c+++-H2O",
                "c+++-NH3",
                "c++-H2O",
                "c++-NH3",
                "c-H2O",
                "c-NH3",
                "z",
                "z++",
                "z+++",
                "z+++-H2O",
                "z+++-NH3",
                "z++-H2O",
                "z++-NH3",
                "z-H2O",
                "z-NH3"});
                columnCheckedListBox.SetItemChecked(0, true);
                columnCheckedListBox.SetItemChecked(1, true);
                columnCheckedListBox.SetItemChecked(9, true);
                columnCheckedListBox.SetItemChecked(10, true);
                m_manager.HandlefragmentLadderModeChange(true);
            }
        }

        private void changeTab(object sender, EventArgs e)
        {
            if(!m_currentlyDrawing)
            if (this.tabControl1.SelectedIndex != -1)
            {
                m_manager.UpdateCurrentInstance(this.tabControl1.SelectedIndex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (peptideEditorTextBox.Text == "")
                return;
            m_manager.ClearLadderInstances();
        }

        private void FragmentLadderView_Load(object sender, EventArgs e)
        {
        }

        private void ClearSingleMod_Click(object sender, EventArgs e)
        {
            var index = tabControl1.SelectedIndex;
            if (index > 0)
            {
                m_manager.RemoveModificationFromList(index);
            }
            if (index > 0)
            {
                if (tabControl1.TabCount == index)
                {
                    tabControl1.SelectedIndex = (index - 1);
                }
                else
                {
                    tabControl1.SelectedIndex = index;
                }
            }
        }

        private void columnLabel_Click(object sender, EventArgs e)
        {
        }

        private void peptideEditorTextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            var newTabIndex = tabControl1.TabCount;
            if (e.KeyCode == Keys.Enter)
            {
                if (peptideEditorTextBox.Text == "")
                    return;

                var changeTab = false;
                changeTab = m_manager.HandleInputPeptide(peptideEditorTextBox.Text);

                if (changeTab)
                {
                    tabControl1.SelectedIndex = newTabIndex;
                }
            }
        }
    }
}
