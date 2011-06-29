namespace SpectrumLook.Views
{
    partial class EditAddModification
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
            this.symbolTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.okModButton = new System.Windows.Forms.Button();
            this.cancelModButton = new System.Windows.Forms.Button();
            this.massTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // symbolTextBox
            // 
            this.symbolTextBox.Location = new System.Drawing.Point(12, 23);
            this.symbolTextBox.Name = "symbolTextBox";
            this.symbolTextBox.Size = new System.Drawing.Size(100, 20);
            this.symbolTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Symbol";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mass";
            // 
            // okModButton
            // 
            this.okModButton.Location = new System.Drawing.Point(62, 49);
            this.okModButton.Name = "okModButton";
            this.okModButton.Size = new System.Drawing.Size(75, 23);
            this.okModButton.TabIndex = 4;
            this.okModButton.Text = "Ok";
            this.okModButton.UseVisualStyleBackColor = true;
            this.okModButton.Click += new System.EventHandler(this.okModButton_Click);
            // 
            // cancelModButton
            // 
            this.cancelModButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelModButton.Location = new System.Drawing.Point(143, 49);
            this.cancelModButton.Name = "cancelModButton";
            this.cancelModButton.Size = new System.Drawing.Size(75, 23);
            this.cancelModButton.TabIndex = 5;
            this.cancelModButton.Text = "Cancel";
            this.cancelModButton.UseVisualStyleBackColor = true;
            // 
            // massTextBox
            // 
            this.massTextBox.Location = new System.Drawing.Point(118, 23);
            this.massTextBox.Name = "massTextBox";
            this.massTextBox.Size = new System.Drawing.Size(100, 20);
            this.massTextBox.TabIndex = 6;
            // 
            // EditAddModification
            // 
            this.AcceptButton = this.okModButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelModButton;
            this.ClientSize = new System.Drawing.Size(233, 81);
            this.Controls.Add(this.massTextBox);
            this.Controls.Add(this.cancelModButton);
            this.Controls.Add(this.okModButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.symbolTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditAddModification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EditAddModification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox symbolTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okModButton;
        private System.Windows.Forms.Button cancelModButton;
        private System.Windows.Forms.TextBox massTextBox;
    }
}