namespace SpectrumLook.Views.FragmentLadderView
{
    partial class LadderTab
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
            this.listBoxB = new System.Windows.Forms.ListBox();
            this.listBoxY2plus = new System.Windows.Forms.ListBox();
            this.listBoxY = new System.Windows.Forms.ListBox();
            this.listBoxSeq = new System.Windows.Forms.ListBox();
            this.listBoxB2plus = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            //
            // listBoxB
            //
            this.listBoxB.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxB.ColumnWidth = 40;
            this.listBoxB.FormattingEnabled = true;
            this.listBoxB.Location = new System.Drawing.Point(70, 46);
            this.listBoxB.Name = "listBoxB";
            this.listBoxB.Size = new System.Drawing.Size(53, 15);
            this.listBoxB.TabIndex = 14;
            //
            // listBoxY2plus
            //
            this.listBoxY2plus.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxY2plus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxY2plus.ColumnWidth = 40;
            this.listBoxY2plus.FormattingEnabled = true;
            this.listBoxY2plus.Location = new System.Drawing.Point(252, 46);
            this.listBoxY2plus.Name = "listBoxY2plus";
            this.listBoxY2plus.Size = new System.Drawing.Size(51, 15);
            this.listBoxY2plus.TabIndex = 13;
            //
            // listBoxY
            //
            this.listBoxY.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxY.ColumnWidth = 40;
            this.listBoxY.FormattingEnabled = true;
            this.listBoxY.Location = new System.Drawing.Point(191, 46);
            this.listBoxY.Name = "listBoxY";
            this.listBoxY.Size = new System.Drawing.Size(55, 15);
            this.listBoxY.TabIndex = 12;
            //
            // listBoxSeq
            //
            this.listBoxSeq.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxSeq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxSeq.ColumnWidth = 40;
            this.listBoxSeq.FormattingEnabled = true;
            this.listBoxSeq.Location = new System.Drawing.Point(133, 46);
            this.listBoxSeq.Name = "listBoxSeq";
            this.listBoxSeq.Size = new System.Drawing.Size(52, 15);
            this.listBoxSeq.TabIndex = 11;
            //
            // listBoxB2plus
            //
            this.listBoxB2plus.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxB2plus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxB2plus.ColumnWidth = 40;
            this.listBoxB2plus.FormattingEnabled = true;
            this.listBoxB2plus.Location = new System.Drawing.Point(12, 46);
            this.listBoxB2plus.Name = "listBoxB2plus";
            this.listBoxB2plus.Size = new System.Drawing.Size(52, 15);
            this.listBoxB2plus.TabIndex = 10;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(302, 28);
            this.tableLayoutPanel1.TabIndex = 9;
            //
            // LadderTab
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 297);
            this.Controls.Add(this.listBoxB);
            this.Controls.Add(this.listBoxY2plus);
            this.Controls.Add(this.listBoxY);
            this.Controls.Add(this.listBoxSeq);
            this.Controls.Add(this.listBoxB2plus);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "LadderTab";
            this.Text = "LadderTab";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ListBox listBoxB;
        private System.Windows.Forms.ListBox listBoxY2plus;
        private System.Windows.Forms.ListBox listBoxY;
        private System.Windows.Forms.ListBox listBoxSeq;
        private System.Windows.Forms.ListBox listBoxB2plus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

    }
}