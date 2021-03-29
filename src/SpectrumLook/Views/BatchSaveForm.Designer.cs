namespace SpectrumLook
{
    partial class BatchSaveForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchSaveForm));
            this.SaveButton = new System.Windows.Forms.Button();
            this.TypeComboBox = new System.Windows.Forms.ComboBox();
            this.BaseFolderLabel = new System.Windows.Forms.Label();
            this.BaseFolderTextBox = new System.Windows.Forms.TextBox();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.BaseNameLabel = new System.Windows.Forms.Label();
            this.BaseName = new System.Windows.Forms.TextBox();
            this.InfoGroupBox = new System.Windows.Forms.GroupBox();
            this.BrowseFolderButton = new System.Windows.Forms.Button();
            this.OptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.SaveGridRadioButton = new System.Windows.Forms.RadioButton();
            this.SaveAllRadioButton = new System.Windows.Forms.RadioButton();
            this.CancelBatchSaveButton = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.UsePeptideAndScanRadioButton = new System.Windows.Forms.RadioButton();
            this.UseIndexRadioButton = new System.Windows.Forms.RadioButton();
            this.NamingGroupBox = new System.Windows.Forms.GroupBox();
            this.SaveCurrentRadioButton = new System.Windows.Forms.RadioButton();
            this.AddDatasetNameCheckbox = new System.Windows.Forms.CheckBox();
            this.InfoGroupBox.SuspendLayout();
            this.OptionsGroupBox.SuspendLayout();
            this.NamingGroupBox.SuspendLayout();
            this.SuspendLayout();
            //
            // SaveButton
            //
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.SaveButton.Location = new System.Drawing.Point(246, 282);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            //
            // TypeComboBox
            //
            this.TypeComboBox.FormattingEnabled = true;
            this.TypeComboBox.Location = new System.Drawing.Point(296, 99);
            this.TypeComboBox.Name = "TypeComboBox";
            this.TypeComboBox.Size = new System.Drawing.Size(89, 21);
            this.TypeComboBox.TabIndex = 1;
            //
            // BaseFolderLabel
            //
            this.BaseFolderLabel.AutoSize = true;
            this.BaseFolderLabel.Location = new System.Drawing.Point(21, 31);
            this.BaseFolderLabel.Name = "BaseFolderLabel";
            this.BaseFolderLabel.Size = new System.Drawing.Size(129, 13);
            this.BaseFolderLabel.TabIndex = 2;
            this.BaseFolderLabel.Text = "Folder to Save the files in ";
            //
            // BaseFolderTextBox
            //
            this.BaseFolderTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.BaseFolderTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.BaseFolderTextBox.Location = new System.Drawing.Point(63, 47);
            this.BaseFolderTextBox.Name = "BaseFolderTextBox";
            this.BaseFolderTextBox.Size = new System.Drawing.Size(276, 20);
            this.BaseFolderTextBox.TabIndex = 3;
            //
            // TypeLabel
            //
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Location = new System.Drawing.Point(21, 102);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(63, 13);
            this.TypeLabel.TabIndex = 4;
            this.TypeLabel.Text = "Image Type";
            //
            // BaseNameLabel
            //
            this.BaseNameLabel.AutoSize = true;
            this.BaseNameLabel.Location = new System.Drawing.Point(21, 76);
            this.BaseNameLabel.Name = "BaseNameLabel";
            this.BaseNameLabel.Size = new System.Drawing.Size(133, 13);
            this.BaseNameLabel.TabIndex = 5;
            this.BaseNameLabel.Text = "Base name for each image";
            //
            // BaseName
            //
            this.BaseName.Location = new System.Drawing.Point(165, 73);
            this.BaseName.Name = "BaseName";
            this.BaseName.Size = new System.Drawing.Size(219, 20);
            this.BaseName.TabIndex = 6;
            //
            // InfoGroupBox
            //
            this.InfoGroupBox.Controls.Add(this.BrowseFolderButton);
            this.InfoGroupBox.Controls.Add(this.BaseFolderLabel);
            this.InfoGroupBox.Controls.Add(this.TypeComboBox);
            this.InfoGroupBox.Controls.Add(this.BaseFolderTextBox);
            this.InfoGroupBox.Controls.Add(this.BaseName);
            this.InfoGroupBox.Controls.Add(this.TypeLabel);
            this.InfoGroupBox.Controls.Add(this.BaseNameLabel);
            this.InfoGroupBox.Location = new System.Drawing.Point(12, 12);
            this.InfoGroupBox.Name = "InfoGroupBox";
            this.InfoGroupBox.Size = new System.Drawing.Size(390, 134);
            this.InfoGroupBox.TabIndex = 7;
            this.InfoGroupBox.TabStop = false;
            this.InfoGroupBox.Text = "Base Info";
            //
            // BrowseFolderButton
            //
            this.BrowseFolderButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BrowseFolderButton.Location = new System.Drawing.Point(345, 45);
            this.BrowseFolderButton.Name = "BrowseFolderButton";
            this.BrowseFolderButton.Size = new System.Drawing.Size(39, 23);
            this.BrowseFolderButton.TabIndex = 11;
            this.BrowseFolderButton.Text = "...";
            this.BrowseFolderButton.UseVisualStyleBackColor = true;
            this.BrowseFolderButton.Click += new System.EventHandler(this.BrowseFolderButton_Click);
            //
            // OptionsGroupBox
            //
            this.OptionsGroupBox.Controls.Add(this.SaveCurrentRadioButton);
            this.OptionsGroupBox.Controls.Add(this.SaveGridRadioButton);
            this.OptionsGroupBox.Controls.Add(this.SaveAllRadioButton);
            this.OptionsGroupBox.Location = new System.Drawing.Point(239, 152);
            this.OptionsGroupBox.Name = "OptionsGroupBox";
            this.OptionsGroupBox.Size = new System.Drawing.Size(163, 88);
            this.OptionsGroupBox.TabIndex = 8;
            this.OptionsGroupBox.TabStop = false;
            this.OptionsGroupBox.Text = "Options";
            //
            // SaveGridRadioButton
            //
            this.SaveGridRadioButton.AutoSize = true;
            this.SaveGridRadioButton.Location = new System.Drawing.Point(6, 42);
            this.SaveGridRadioButton.Name = "SaveGridRadioButton";
            this.SaveGridRadioButton.Size = new System.Drawing.Size(140, 17);
            this.SaveGridRadioButton.TabIndex = 3;
            this.SaveGridRadioButton.TabStop = true;
            this.SaveGridRadioButton.Text = "Save Only Scans in Grid";
            this.SaveGridRadioButton.UseVisualStyleBackColor = true;
            //
            // SaveAllRadioButton
            //
            this.SaveAllRadioButton.AutoSize = true;
            this.SaveAllRadioButton.Location = new System.Drawing.Point(6, 19);
            this.SaveAllRadioButton.Name = "SaveAllRadioButton";
            this.SaveAllRadioButton.Size = new System.Drawing.Size(97, 17);
            this.SaveAllRadioButton.TabIndex = 1;
            this.SaveAllRadioButton.TabStop = true;
            this.SaveAllRadioButton.Text = "Save All Scans";
            this.SaveAllRadioButton.UseVisualStyleBackColor = true;
            //
            // CancelBatchSaveButton
            //
            this.CancelBatchSaveButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBatchSaveButton.Location = new System.Drawing.Point(327, 282);
            this.CancelBatchSaveButton.Name = "CancelBatchSaveButton";
            this.CancelBatchSaveButton.Size = new System.Drawing.Size(75, 23);
            this.CancelBatchSaveButton.TabIndex = 9;
            this.CancelBatchSaveButton.Text = "Cancel";
            this.CancelBatchSaveButton.UseVisualStyleBackColor = true;
            this.CancelBatchSaveButton.Click += new System.EventHandler(this.CancelButton_Click);
            //
            // StatusLabel
            //
            this.StatusLabel.AutoSize = true;
            this.StatusLabel.Location = new System.Drawing.Point(17, 254);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(63, 13);
            this.StatusLabel.TabIndex = 10;
            this.StatusLabel.Text = "StatusLabel";
            //
            // UsePeptideAndScanRadioButton
            //
            this.UsePeptideAndScanRadioButton.AutoSize = true;
            this.UsePeptideAndScanRadioButton.Location = new System.Drawing.Point(6, 19);
            this.UsePeptideAndScanRadioButton.Name = "UsePeptideAndScanRadioButton";
            this.UsePeptideAndScanRadioButton.Size = new System.Drawing.Size(172, 17);
            this.UsePeptideAndScanRadioButton.TabIndex = 4;
            this.UsePeptideAndScanRadioButton.TabStop = true;
            this.UsePeptideAndScanRadioButton.Text = "Use Peptide and Scan Number";
            this.UsePeptideAndScanRadioButton.UseVisualStyleBackColor = true;
            //
            // UseIndexRadioButton
            //
            this.UseIndexRadioButton.AutoSize = true;
            this.UseIndexRadioButton.Location = new System.Drawing.Point(6, 42);
            this.UseIndexRadioButton.Name = "UseIndexRadioButton";
            this.UseIndexRadioButton.Size = new System.Drawing.Size(134, 17);
            this.UseIndexRadioButton.TabIndex = 5;
            this.UseIndexRadioButton.TabStop = true;
            this.UseIndexRadioButton.Text = "Use a Numbered Index";
            this.UseIndexRadioButton.UseVisualStyleBackColor = true;
            //
            // NamingGroupBox
            //
            this.NamingGroupBox.Controls.Add(this.AddDatasetNameCheckbox);
            this.NamingGroupBox.Controls.Add(this.UsePeptideAndScanRadioButton);
            this.NamingGroupBox.Controls.Add(this.UseIndexRadioButton);
            this.NamingGroupBox.Location = new System.Drawing.Point(12, 152);
            this.NamingGroupBox.Name = "NamingGroupBox";
            this.NamingGroupBox.Size = new System.Drawing.Size(221, 88);
            this.NamingGroupBox.TabIndex = 11;
            this.NamingGroupBox.TabStop = false;
            this.NamingGroupBox.Text = "Unique file Name Identifiers";
            //
            // SaveCurrentRadioButton
            //
            this.SaveCurrentRadioButton.AutoSize = true;
            this.SaveCurrentRadioButton.Location = new System.Drawing.Point(6, 65);
            this.SaveCurrentRadioButton.Name = "SaveCurrentRadioButton";
            this.SaveCurrentRadioButton.Size = new System.Drawing.Size(139, 17);
            this.SaveCurrentRadioButton.TabIndex = 4;
            this.SaveCurrentRadioButton.TabStop = true;
            this.SaveCurrentRadioButton.Text = "Save Only Current Scan";
            this.SaveCurrentRadioButton.UseMnemonic = false;
            this.SaveCurrentRadioButton.UseVisualStyleBackColor = true;
            //
            // AddDatasetNameCheckbox
            //
            this.AddDatasetNameCheckbox.AutoSize = true;
            this.AddDatasetNameCheckbox.Location = new System.Drawing.Point(6, 66);
            this.AddDatasetNameCheckbox.Name = "AddDatasetNameCheckbox";
            this.AddDatasetNameCheckbox.Size = new System.Drawing.Size(205, 17);
            this.AddDatasetNameCheckbox.TabIndex = 6;
            this.AddDatasetNameCheckbox.Text = "Add Dataset Name (Combined results)";
            this.AddDatasetNameCheckbox.UseVisualStyleBackColor = true;
            //
            // BatchSaveForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 311);
            this.Controls.Add(this.NamingGroupBox);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.CancelBatchSaveButton);
            this.Controls.Add(this.OptionsGroupBox);
            this.Controls.Add(this.InfoGroupBox);
            this.Controls.Add(this.SaveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BatchSaveForm";
            this.Text = "Save Multiple Images";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatchSaveForm_FormClosing);
            this.InfoGroupBox.ResumeLayout(false);
            this.InfoGroupBox.PerformLayout();
            this.OptionsGroupBox.ResumeLayout(false);
            this.OptionsGroupBox.PerformLayout();
            this.NamingGroupBox.ResumeLayout(false);
            this.NamingGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ComboBox TypeComboBox;
        private System.Windows.Forms.Label BaseFolderLabel;
        private System.Windows.Forms.TextBox BaseFolderTextBox;
        private System.Windows.Forms.Label TypeLabel;
        private System.Windows.Forms.Label BaseNameLabel;
        private System.Windows.Forms.TextBox BaseName;
        private System.Windows.Forms.GroupBox InfoGroupBox;
        private System.Windows.Forms.GroupBox OptionsGroupBox;
        private System.Windows.Forms.Button CancelBatchSaveButton;
        private System.Windows.Forms.RadioButton SaveAllRadioButton;
        private System.Windows.Forms.RadioButton SaveGridRadioButton;
        private System.Windows.Forms.Button BrowseFolderButton;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.RadioButton UsePeptideAndScanRadioButton;
        private System.Windows.Forms.RadioButton UseIndexRadioButton;
        private System.Windows.Forms.GroupBox NamingGroupBox;
        private System.Windows.Forms.RadioButton SaveCurrentRadioButton;
        private System.Windows.Forms.CheckBox AddDatasetNameCheckbox;
    }
}