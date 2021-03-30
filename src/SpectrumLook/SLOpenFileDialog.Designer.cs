namespace SpectrumLook
{
    partial class SLOpenFileDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SLOpenFileDialog));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxSynopsis = new System.Windows.Forms.GroupBox();
            this.textBoxSynopsis = new System.Windows.Forms.TextBox();
            this.buttonSynopsis = new System.Windows.Forms.Button();
            this.groupBoxData = new System.Windows.Forms.GroupBox();
            this.textBoxData = new System.Windows.Forms.TextBox();
            this.buttonData = new System.Windows.Forms.Button();
            this.groupBoxSynopsis.SuspendLayout();
            this.groupBoxData.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpen.Location = new System.Drawing.Point(267, 169);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(100, 28);
            this.buttonOpen.TabIndex = 6;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(375, 169);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxSynopsis
            // 
            this.groupBoxSynopsis.Controls.Add(this.textBoxSynopsis);
            this.groupBoxSynopsis.Controls.Add(this.buttonSynopsis);
            this.groupBoxSynopsis.Location = new System.Drawing.Point(16, 11);
            this.groupBoxSynopsis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxSynopsis.Name = "groupBoxSynopsis";
            this.groupBoxSynopsis.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxSynopsis.Size = new System.Drawing.Size(467, 68);
            this.groupBoxSynopsis.TabIndex = 8;
            this.groupBoxSynopsis.TabStop = false;
            this.groupBoxSynopsis.Text = "Synopsis File";
            // 
            // textBoxSynopsis
            // 
            this.textBoxSynopsis.Location = new System.Drawing.Point(8, 26);
            this.textBoxSynopsis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSynopsis.Name = "textBoxSynopsis";
            this.textBoxSynopsis.Size = new System.Drawing.Size(400, 22);
            this.textBoxSynopsis.TabIndex = 0;
            this.textBoxSynopsis.TextChanged += new System.EventHandler(this.textBoxSynopsis_TextChanged);
            // 
            // buttonSynopsis
            // 
            this.buttonSynopsis.Location = new System.Drawing.Point(417, 23);
            this.buttonSynopsis.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSynopsis.Name = "buttonSynopsis";
            this.buttonSynopsis.Size = new System.Drawing.Size(41, 28);
            this.buttonSynopsis.TabIndex = 3;
            this.buttonSynopsis.Text = "...";
            this.buttonSynopsis.UseVisualStyleBackColor = true;
            this.buttonSynopsis.Click += new System.EventHandler(this.buttonSynopsis_Click);
            // 
            // groupBoxData
            // 
            this.groupBoxData.Controls.Add(this.textBoxData);
            this.groupBoxData.Controls.Add(this.buttonData);
            this.groupBoxData.Location = new System.Drawing.Point(16, 86);
            this.groupBoxData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBoxData.Size = new System.Drawing.Size(467, 68);
            this.groupBoxData.TabIndex = 6;
            this.groupBoxData.TabStop = false;
            this.groupBoxData.Text = "Data File";
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(8, 26);
            this.textBoxData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(400, 22);
            this.textBoxData.TabIndex = 0;
            this.textBoxData.TextChanged += new System.EventHandler(this.textBoxData_TextChanged);
            // 
            // buttonData
            // 
            this.buttonData.Location = new System.Drawing.Point(417, 23);
            this.buttonData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonData.Name = "buttonData";
            this.buttonData.Size = new System.Drawing.Size(41, 28);
            this.buttonData.TabIndex = 3;
            this.buttonData.Text = "...";
            this.buttonData.UseVisualStyleBackColor = true;
            this.buttonData.Click += new System.EventHandler(this.buttonData_Click);
            // 
            // SLOpenFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 212);
            this.Controls.Add(this.groupBoxData);
            this.Controls.Add(this.groupBoxSynopsis);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SLOpenFileDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open Files";
            this.groupBoxSynopsis.ResumeLayout(false);
            this.groupBoxSynopsis.PerformLayout();
            this.groupBoxData.ResumeLayout(false);
            this.groupBoxData.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxSynopsis;
        private System.Windows.Forms.TextBox textBoxSynopsis;
        private System.Windows.Forms.Button buttonSynopsis;
        private System.Windows.Forms.GroupBox groupBoxData;
        private System.Windows.Forms.TextBox textBoxData;
        private System.Windows.Forms.Button buttonData;
    }
}