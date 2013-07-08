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

        #region Windows Form Designer generated code

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
            this.buttonOpen.Location = new System.Drawing.Point(200, 137);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 6;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(281, 137);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxSynopsis
            // 
            this.groupBoxSynopsis.Controls.Add(this.textBoxSynopsis);
            this.groupBoxSynopsis.Controls.Add(this.buttonSynopsis);
            this.groupBoxSynopsis.Location = new System.Drawing.Point(12, 9);
            this.groupBoxSynopsis.Name = "groupBoxSynopsis";
            this.groupBoxSynopsis.Size = new System.Drawing.Size(350, 55);
            this.groupBoxSynopsis.TabIndex = 8;
            this.groupBoxSynopsis.TabStop = false;
            this.groupBoxSynopsis.Text = "Synopsis File";
            // 
            // textBoxSynopsis
            // 
            this.textBoxSynopsis.Location = new System.Drawing.Point(6, 21);
            this.textBoxSynopsis.Name = "textBoxSynopsis";
            this.textBoxSynopsis.Size = new System.Drawing.Size(301, 20);
            this.textBoxSynopsis.TabIndex = 0;
            this.textBoxSynopsis.TextChanged += new System.EventHandler(this.textBoxSynopsis_TextChanged);
            // 
            // buttonSynopsis
            // 
            this.buttonSynopsis.Location = new System.Drawing.Point(313, 19);
            this.buttonSynopsis.Name = "buttonSynopsis";
            this.buttonSynopsis.Size = new System.Drawing.Size(31, 23);
            this.buttonSynopsis.TabIndex = 3;
            this.buttonSynopsis.Text = "...";
            this.buttonSynopsis.UseVisualStyleBackColor = true;
            this.buttonSynopsis.Click += new System.EventHandler(this.buttonSynopsis_Click);
            // 
            // groupBoxData
            // 
            this.groupBoxData.Controls.Add(this.textBoxData);
            this.groupBoxData.Controls.Add(this.buttonData);
            this.groupBoxData.Location = new System.Drawing.Point(12, 70);
            this.groupBoxData.Name = "groupBoxData";
            this.groupBoxData.Size = new System.Drawing.Size(350, 55);
            this.groupBoxData.TabIndex = 6;
            this.groupBoxData.TabStop = false;
            this.groupBoxData.Text = "Data File";
            // 
            // textBoxData
            // 
            this.textBoxData.Location = new System.Drawing.Point(6, 21);
            this.textBoxData.Name = "textBoxData";
            this.textBoxData.Size = new System.Drawing.Size(301, 20);
            this.textBoxData.TabIndex = 0;
            this.textBoxData.TextChanged += new System.EventHandler(this.textBoxData_TextChanged);
            // 
            // buttonData
            // 
            this.buttonData.Location = new System.Drawing.Point(313, 19);
            this.buttonData.Name = "buttonData";
            this.buttonData.Size = new System.Drawing.Size(31, 23);
            this.buttonData.TabIndex = 3;
            this.buttonData.Text = "...";
            this.buttonData.UseVisualStyleBackColor = true;
            this.buttonData.Click += new System.EventHandler(this.buttonData_Click);
            // 
            // SLOpenFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 172);
            this.Controls.Add(this.groupBoxData);
            this.Controls.Add(this.groupBoxSynopsis);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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

        #endregion

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