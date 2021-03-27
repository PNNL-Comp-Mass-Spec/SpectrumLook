namespace SpectrumLook.Views.FragmentLadderView
{
    partial class FragmentLadderView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.peptideEditorTextBox = new System.Windows.Forms.TextBox();
            this.recalculateMatchesButton = new System.Windows.Forms.Button();
            this.parentMZLabel = new System.Windows.Forms.Label();
            this.parentMZValue = new System.Windows.Forms.Label();
            this.pMZ98Label = new System.Windows.Forms.Label();
            this.pMZ98Value = new System.Windows.Forms.Label();
            this.pMZ49Label = new System.Windows.Forms.Label();
            this.pMZ49Value = new System.Windows.Forms.Label();
            this.pMZ32Label = new System.Windows.Forms.Label();
            this.pMZ32Value = new System.Windows.Forms.Label();
            this.ionsLabel = new System.Windows.Forms.Label();
            this.ionsValue = new System.Windows.Forms.Label();
            this.neuLIonsLabel = new System.Windows.Forms.Label();
            this.neuLIonsValue = new System.Windows.Forms.Label();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.lengthValue = new System.Windows.Forms.Label();
            this.columnPanel = new System.Windows.Forms.Panel();
            this.columnClearButton = new System.Windows.Forms.Button();
            this.columnLabel = new System.Windows.Forms.Label();
            this.columnCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.columnSaveButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.columnButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.ClearSingleMod = new System.Windows.Forms.Button();
            this.Unmatched_Label = new System.Windows.Forms.Label();
            this.Matched_Label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.columnPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // peptideEditorTextBox
            //
            this.peptideEditorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.peptideEditorTextBox.Location = new System.Drawing.Point(13, 29);
            this.peptideEditorTextBox.Name = "peptideEditorTextBox";
            this.peptideEditorTextBox.Size = new System.Drawing.Size(287, 20);
            this.peptideEditorTextBox.TabIndex = 3;
            this.peptideEditorTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.peptideEditorTextBox_KeyDown_1);
            //
            // recalculateMatchesButton
            //
            this.recalculateMatchesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.recalculateMatchesButton.Location = new System.Drawing.Point(305, 29);
            this.recalculateMatchesButton.Name = "recalculateMatchesButton";
            this.recalculateMatchesButton.Size = new System.Drawing.Size(82, 23);
            this.recalculateMatchesButton.TabIndex = 4;
            this.recalculateMatchesButton.Text = "Calculate";
            this.recalculateMatchesButton.UseVisualStyleBackColor = true;
            this.recalculateMatchesButton.Click += new System.EventHandler(this.generateLadderFromPeptideInput);
            //
            // parentMZLabel
            //
            this.parentMZLabel.AutoSize = true;
            this.parentMZLabel.Location = new System.Drawing.Point(13, 13);
            this.parentMZLabel.Name = "parentMZLabel";
            this.parentMZLabel.Size = new System.Drawing.Size(65, 13);
            this.parentMZLabel.TabIndex = 4;
            this.parentMZLabel.Text = "Parent m/z=";
            this.parentMZLabel.Visible = false;
            //
            // parentMZValue
            //
            this.parentMZValue.AutoSize = true;
            this.parentMZValue.Location = new System.Drawing.Point(75, 13);
            this.parentMZValue.Name = "parentMZValue";
            this.parentMZValue.Size = new System.Drawing.Size(22, 13);
            this.parentMZValue.TabIndex = 5;
            this.parentMZValue.Text = "0.0";
            this.parentMZValue.Visible = false;
            //
            // pMZ98Label
            //
            this.pMZ98Label.AutoSize = true;
            this.pMZ98Label.Location = new System.Drawing.Point(116, 13);
            this.pMZ98Label.Name = "pMZ98Label";
            this.pMZ98Label.Size = new System.Drawing.Size(53, 13);
            this.pMZ98Label.TabIndex = 6;
            this.pMZ98Label.Text = "Pm/z-98=";
            this.pMZ98Label.Visible = false;
            //
            // pMZ98Value
            //
            this.pMZ98Value.AutoSize = true;
            this.pMZ98Value.Location = new System.Drawing.Point(166, 13);
            this.pMZ98Value.Name = "pMZ98Value";
            this.pMZ98Value.Size = new System.Drawing.Size(13, 13);
            this.pMZ98Value.TabIndex = 7;
            this.pMZ98Value.Text = "0";
            this.pMZ98Value.Visible = false;
            //
            // pMZ49Label
            //
            this.pMZ49Label.AutoSize = true;
            this.pMZ49Label.Location = new System.Drawing.Point(207, 13);
            this.pMZ49Label.Name = "pMZ49Label";
            this.pMZ49Label.Size = new System.Drawing.Size(53, 13);
            this.pMZ49Label.TabIndex = 8;
            this.pMZ49Label.Text = "Pm/z-49=";
            this.pMZ49Label.Visible = false;
            //
            // pMZ49Value
            //
            this.pMZ49Value.AutoSize = true;
            this.pMZ49Value.Location = new System.Drawing.Point(257, 13);
            this.pMZ49Value.Name = "pMZ49Value";
            this.pMZ49Value.Size = new System.Drawing.Size(13, 13);
            this.pMZ49Value.TabIndex = 9;
            this.pMZ49Value.Text = "0";
            this.pMZ49Value.Visible = false;
            //
            // pMZ32Label
            //
            this.pMZ32Label.AutoSize = true;
            this.pMZ32Label.Location = new System.Drawing.Point(302, 13);
            this.pMZ32Label.Name = "pMZ32Label";
            this.pMZ32Label.Size = new System.Drawing.Size(53, 13);
            this.pMZ32Label.TabIndex = 10;
            this.pMZ32Label.Text = "Pm/z-32=";
            this.pMZ32Label.Visible = false;
            //
            // pMZ32Value
            //
            this.pMZ32Value.AutoSize = true;
            this.pMZ32Value.Location = new System.Drawing.Point(353, 13);
            this.pMZ32Value.Name = "pMZ32Value";
            this.pMZ32Value.Size = new System.Drawing.Size(13, 13);
            this.pMZ32Value.TabIndex = 11;
            this.pMZ32Value.Text = "0";
            this.pMZ32Value.Visible = false;
            //
            // ionsLabel
            //
            this.ionsLabel.AutoSize = true;
            this.ionsLabel.Location = new System.Drawing.Point(116, 35);
            this.ionsLabel.Name = "ionsLabel";
            this.ionsLabel.Size = new System.Drawing.Size(33, 13);
            this.ionsLabel.TabIndex = 12;
            this.ionsLabel.Text = "Ions=";
            this.ionsLabel.Visible = false;
            //
            // ionsValue
            //
            this.ionsValue.AutoSize = true;
            this.ionsValue.Location = new System.Drawing.Point(144, 35);
            this.ionsValue.Name = "ionsValue";
            this.ionsValue.Size = new System.Drawing.Size(13, 13);
            this.ionsValue.TabIndex = 13;
            this.ionsValue.Text = "0";
            this.ionsValue.Visible = false;
            //
            // neuLIonsLabel
            //
            this.neuLIonsLabel.AutoSize = true;
            this.neuLIonsLabel.Location = new System.Drawing.Point(207, 35);
            this.neuLIonsLabel.Name = "neuLIonsLabel";
            this.neuLIonsLabel.Size = new System.Drawing.Size(62, 13);
            this.neuLIonsLabel.TabIndex = 14;
            this.neuLIonsLabel.Text = "NeuL Ions=";
            this.neuLIonsLabel.Visible = false;
            //
            // neuLIonsValue
            //
            this.neuLIonsValue.AutoSize = true;
            this.neuLIonsValue.Location = new System.Drawing.Point(266, 35);
            this.neuLIonsValue.Name = "neuLIonsValue";
            this.neuLIonsValue.Size = new System.Drawing.Size(13, 13);
            this.neuLIonsValue.TabIndex = 15;
            this.neuLIonsValue.Text = "0";
            this.neuLIonsValue.Visible = false;
            //
            // lengthLabel
            //
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point(306, 35);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(46, 13);
            this.lengthLabel.TabIndex = 16;
            this.lengthLabel.Text = "Length=";
            this.lengthLabel.Visible = false;
            //
            // lengthValue
            //
            this.lengthValue.AutoSize = true;
            this.lengthValue.Location = new System.Drawing.Point(353, 35);
            this.lengthValue.Name = "lengthValue";
            this.lengthValue.Size = new System.Drawing.Size(13, 13);
            this.lengthValue.TabIndex = 17;
            this.lengthValue.Text = "0";
            this.lengthValue.Visible = false;
            //
            // columnPanel
            //
            this.columnPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.columnPanel.Controls.Add(this.columnClearButton);
            this.columnPanel.Controls.Add(this.columnLabel);
            this.columnPanel.Controls.Add(this.columnCheckedListBox);
            this.columnPanel.Controls.Add(this.columnSaveButton);
            this.columnPanel.Location = new System.Drawing.Point(131, 29);
            this.columnPanel.Name = "columnPanel";
            this.columnPanel.Size = new System.Drawing.Size(119, 293);
            this.columnPanel.TabIndex = 21;
            this.columnPanel.Visible = false;
            //
            // columnClearButton
            //
            this.columnClearButton.Location = new System.Drawing.Point(8, 254);
            this.columnClearButton.Name = "columnClearButton";
            this.columnClearButton.Size = new System.Drawing.Size(48, 32);
            this.columnClearButton.TabIndex = 4;
            this.columnClearButton.Text = "Clear";
            this.columnClearButton.UseVisualStyleBackColor = true;
            this.columnClearButton.Visible = false;
            this.columnClearButton.Click += new System.EventHandler(this.columnClearButton_Click);
            //
            // columnLabel
            //
            this.columnLabel.AutoSize = true;
            this.columnLabel.Location = new System.Drawing.Point(19, 3);
            this.columnLabel.Name = "columnLabel";
            this.columnLabel.Size = new System.Drawing.Size(79, 13);
            this.columnLabel.TabIndex = 3;
            this.columnLabel.Text = "Column Display";
            this.columnLabel.Visible = false;
            this.columnLabel.Click += new System.EventHandler(this.columnLabel_Click);
            //
            // columnCheckedListBox
            //
            this.columnCheckedListBox.CheckOnClick = true;
            this.columnCheckedListBox.FormattingEnabled = true;
            this.columnCheckedListBox.Items.AddRange(new object[] {
            "b",
            "b++",
            "b+++",
            "b+++-H20",
            "b+++-NH3",
            "b++-H20",
            "b++-NH3",
            "b-H20",
            "b-NH3",
            "y",
            "y++",
            "y+++",
            "y+++-H20",
            "y+++-NH3",
            "y++-H20",
            "y++-NH3",
            "y-H20",
            "y-NH3"});
            this.columnCheckedListBox.Location = new System.Drawing.Point(8, 19);
            this.columnCheckedListBox.Name = "columnCheckedListBox";
            this.columnCheckedListBox.Size = new System.Drawing.Size(102, 229);
            this.columnCheckedListBox.TabIndex = 2;
            this.columnCheckedListBox.Visible = false;
            //
            // columnSaveButton
            //
            this.columnSaveButton.Location = new System.Drawing.Point(61, 254);
            this.columnSaveButton.Name = "columnSaveButton";
            this.columnSaveButton.Size = new System.Drawing.Size(48, 32);
            this.columnSaveButton.TabIndex = 0;
            this.columnSaveButton.Text = "Apply";
            this.columnSaveButton.UseVisualStyleBackColor = true;
            this.columnSaveButton.Visible = false;
            this.columnSaveButton.Click += new System.EventHandler(this.columnSaveButton_Click);
            //
            // tabControl1
            //
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(4, 101);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(387, 307);
            this.tabControl1.TabIndex = 7;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.changeTab);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.changeTab);
            //
            // tabPage1
            //
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(379, 281);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Original";
            this.tabPage1.UseVisualStyleBackColor = true;
            //
            // comboBox1
            //
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "CID",
            "ETD"});
            this.comboBox1.Location = new System.Drawing.Point(12, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(102, 21);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.changeFragmentLadderMode);
            //
            // columnButton
            //
            this.columnButton.Location = new System.Drawing.Point(131, 3);
            this.columnButton.Name = "columnButton";
            this.columnButton.Size = new System.Drawing.Size(59, 23);
            this.columnButton.TabIndex = 2;
            this.columnButton.Text = "Ions";
            this.columnButton.UseVisualStyleBackColor = true;
            this.columnButton.Click += new System.EventHandler(this.columnButton_Click);
            //
            // button1
            //
            this.button1.Location = new System.Drawing.Point(131, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Clear All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // ClearSingleMod
            //
            this.ClearSingleMod.Location = new System.Drawing.Point(12, 72);
            this.ClearSingleMod.Name = "ClearSingleMod";
            this.ClearSingleMod.Size = new System.Drawing.Size(102, 23);
            this.ClearSingleMod.TabIndex = 5;
            this.ClearSingleMod.Text = "Clear ";
            this.ClearSingleMod.UseVisualStyleBackColor = true;
            this.ClearSingleMod.Click += new System.EventHandler(this.ClearSingleMod_Click);
            //
            // Unmatched_Label
            //
            this.Unmatched_Label.AutoSize = true;
            this.Unmatched_Label.Location = new System.Drawing.Point(23, 16);
            this.Unmatched_Label.Name = "Unmatched_Label";
            this.Unmatched_Label.Size = new System.Drawing.Size(95, 13);
            this.Unmatched_Label.TabIndex = 22;
            this.Unmatched_Label.Text = "Unmatched Peaks";
            //
            // Matched_Label
            //
            this.Matched_Label.AutoSize = true;
            this.Matched_Label.ForeColor = System.Drawing.Color.Red;
            this.Matched_Label.Location = new System.Drawing.Point(23, 29);
            this.Matched_Label.Name = "Matched_Label";
            this.Matched_Label.Size = new System.Drawing.Size(82, 13);
            this.Matched_Label.TabIndex = 23;
            this.Matched_Label.Text = "Matched Peaks";
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.Unmatched_Label);
            this.groupBox1.Controls.Add(this.Matched_Label);
            this.groupBox1.Location = new System.Drawing.Point(243, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 49);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "key";
            //
            // FragmentLadderView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 411);
            this.Controls.Add(this.columnPanel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.columnButton);
            this.Controls.Add(this.ClearSingleMod);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lengthValue);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.neuLIonsValue);
            this.Controls.Add(this.neuLIonsLabel);
            this.Controls.Add(this.ionsValue);
            this.Controls.Add(this.ionsLabel);
            this.Controls.Add(this.pMZ32Value);
            this.Controls.Add(this.pMZ32Label);
            this.Controls.Add(this.pMZ49Value);
            this.Controls.Add(this.pMZ49Label);
            this.Controls.Add(this.pMZ98Value);
            this.Controls.Add(this.pMZ98Label);
            this.Controls.Add(this.parentMZValue);
            this.Controls.Add(this.parentMZLabel);
            this.Controls.Add(this.recalculateMatchesButton);
            this.Controls.Add(this.peptideEditorTextBox);
            this.Controls.Add(this.comboBox1);
            this.Name = "FragmentLadderView";
            this.Text = "Fragment Ladder";
            this.Load += new System.EventHandler(this.FragmentLadderView_Load);
            this.columnPanel.ResumeLayout(false);
            this.columnPanel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TextBox peptideEditorTextBox;
        private System.Windows.Forms.Button recalculateMatchesButton;
        private System.Windows.Forms.Label parentMZLabel;
        private System.Windows.Forms.Label parentMZValue;
        private System.Windows.Forms.Label pMZ98Label;
        private System.Windows.Forms.Label pMZ98Value;
        private System.Windows.Forms.Label pMZ49Label;
        private System.Windows.Forms.Label pMZ49Value;
        private System.Windows.Forms.Label pMZ32Label;
        private System.Windows.Forms.Label pMZ32Value;
        private System.Windows.Forms.Label ionsLabel;
        private System.Windows.Forms.Label ionsValue;
        private System.Windows.Forms.Label neuLIonsLabel;
        private System.Windows.Forms.Label neuLIonsValue;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label lengthValue;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button columnButton;
        private System.Windows.Forms.Panel columnPanel;
        private System.Windows.Forms.Button columnSaveButton;
        private System.Windows.Forms.CheckedListBox columnCheckedListBox;
        private System.Windows.Forms.Label columnLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ClearSingleMod;
        private System.Windows.Forms.Label Unmatched_Label;
        private System.Windows.Forms.Label Matched_Label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button columnClearButton;
    }
}
