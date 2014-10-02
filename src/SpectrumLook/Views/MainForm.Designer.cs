namespace SpectrumLook.Views
{
    partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.saveWorkStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openWorkStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.batchSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.fragLadderStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.plotStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openDlg = new System.Windows.Forms.OpenFileDialog();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panelDataView = new System.Windows.Forms.Panel();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.cmdShowHideFragmentIons = new System.Windows.Forms.Button();
			this.panelFragmentLadder = new System.Windows.Forms.Panel();
			this.panelPlot = new System.Windows.Forms.Panel();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			this.panelFragmentLadder.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(1141, 28);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveWorkStripMenuItem,
            this.openWorkStripMenuItem,
            this.toolStripSeparator2,
            this.batchSaveToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.openToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
			this.openToolStripMenuItem.Text = "&Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(233, 6);
			// 
			// saveWorkStripMenuItem
			// 
			this.saveWorkStripMenuItem.Name = "saveWorkStripMenuItem";
			this.saveWorkStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.saveWorkStripMenuItem.Size = new System.Drawing.Size(236, 24);
			this.saveWorkStripMenuItem.Text = "&Save Work File";
			this.saveWorkStripMenuItem.Click += new System.EventHandler(this.saveWorkStripMenuItem_Click);
			// 
			// openWorkStripMenuItem
			// 
			this.openWorkStripMenuItem.Name = "openWorkStripMenuItem";
			this.openWorkStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.openWorkStripMenuItem.Size = new System.Drawing.Size(236, 24);
			this.openWorkStripMenuItem.Text = "Open &Work File";
			this.openWorkStripMenuItem.Click += new System.EventHandler(this.openWorkStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(233, 6);
			this.toolStripSeparator2.Visible = false;
			// 
			// batchSaveToolStripMenuItem
			// 
			this.batchSaveToolStripMenuItem.Name = "batchSaveToolStripMenuItem";
			this.batchSaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.batchSaveToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
			this.batchSaveToolStripMenuItem.Text = "&Batch Save";
			this.batchSaveToolStripMenuItem.Click += new System.EventHandler(this.batchSaveToolStripMenuItem_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(233, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(236, 24);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem1,
            this.fragLadderStripMenuItem,
            this.plotStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
			this.optionsToolStripMenuItem.Text = "&Options";
			// 
			// optionsToolStripMenuItem1
			// 
			this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
			this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(247, 24);
			this.optionsToolStripMenuItem1.Text = "&General Options";
			this.optionsToolStripMenuItem1.Click += new System.EventHandler(this.optionsToolStripMenuItem1_Click);
			// 
			// fragLadderStripMenuItem
			// 
			this.fragLadderStripMenuItem.Name = "fragLadderStripMenuItem";
			this.fragLadderStripMenuItem.Size = new System.Drawing.Size(247, 24);
			this.fragLadderStripMenuItem.Text = "Fragment &Ladder Options";
			this.fragLadderStripMenuItem.Click += new System.EventHandler(this.fragmentLadderToolStripMenuItem1_Click);
			// 
			// plotStripMenuItem
			// 
			this.plotStripMenuItem.Name = "plotStripMenuItem";
			this.plotStripMenuItem.Size = new System.Drawing.Size(247, 24);
			this.plotStripMenuItem.Text = "&Plot Options";
			this.plotStripMenuItem.Click += new System.EventHandler(this.plottingToolStripMenuItem_Click);
			// 
			// openDlg
			// 
			this.openDlg.FileName = "openDlg";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 28);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.panelDataView);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Size = new System.Drawing.Size(1141, 491);
			this.splitContainer1.SplitterDistance = 178;
			this.splitContainer1.SplitterWidth = 5;
			this.splitContainer1.TabIndex = 4;
			this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
			// 
			// panelDataView
			// 
			this.panelDataView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelDataView.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panelDataView.Location = new System.Drawing.Point(0, 0);
			this.panelDataView.Margin = new System.Windows.Forms.Padding(4);
			this.panelDataView.Name = "panelDataView";
			this.panelDataView.Size = new System.Drawing.Size(1141, 176);
			this.panelDataView.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
			this.splitContainer2.Name = "splitContainer2";
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.panelFragmentLadder);
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.panelPlot);
			this.splitContainer2.Size = new System.Drawing.Size(1141, 308);
			this.splitContainer2.SplitterDistance = 512;
			this.splitContainer2.SplitterWidth = 5;
			this.splitContainer2.TabIndex = 0;
			this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
			// 
			// cmdShowHideFragmentIons
			// 
			this.cmdShowHideFragmentIons.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.cmdShowHideFragmentIons.Location = new System.Drawing.Point(481, 207);
			this.cmdShowHideFragmentIons.Margin = new System.Windows.Forms.Padding(4);
			this.cmdShowHideFragmentIons.Name = "cmdShowHideFragmentIons";
			this.cmdShowHideFragmentIons.Size = new System.Drawing.Size(27, 97);
			this.cmdShowHideFragmentIons.TabIndex = 3;
			this.cmdShowHideFragmentIons.Text = "<<";
			this.cmdShowHideFragmentIons.UseVisualStyleBackColor = true;
			this.cmdShowHideFragmentIons.Click += new System.EventHandler(this.button1_Click);
			// 
			// panelFragmentLadder
			// 
			this.panelFragmentLadder.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panelFragmentLadder.Controls.Add(this.cmdShowHideFragmentIons);
			this.panelFragmentLadder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelFragmentLadder.Location = new System.Drawing.Point(0, 0);
			this.panelFragmentLadder.Margin = new System.Windows.Forms.Padding(4);
			this.panelFragmentLadder.Name = "panelFragmentLadder";
			this.panelFragmentLadder.Size = new System.Drawing.Size(512, 308);
			this.panelFragmentLadder.TabIndex = 2;
			// 
			// panelPlot
			// 
			this.panelPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panelPlot.BackColor = System.Drawing.SystemColors.Control;
			this.panelPlot.Location = new System.Drawing.Point(4, 4);
			this.panelPlot.Margin = new System.Windows.Forms.Padding(4);
			this.panelPlot.Name = "panelPlot";
			this.panelPlot.Size = new System.Drawing.Size(615, 301);
			this.panelPlot.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1141, 519);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.Text = "SpectrumLook";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.panelFragmentLadder.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelPlot;
        private System.Windows.Forms.Panel panelDataView;
        private System.Windows.Forms.Panel panelFragmentLadder;

        //private System.Windows.Forms.Panel panelPlot;
        //private System.Windows.Forms.Panel panelDataView;
        //private System.Windows.Forms.Panel panelFragmentLadder;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openDlg;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button cmdShowHideFragmentIons;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveWorkStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWorkStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem fragLadderStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plotStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

