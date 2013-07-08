namespace SpectrumLook.Views
{
    partial class SLPlot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SLPlot));
            this.groupBoxClosestPoint = new System.Windows.Forms.GroupBox();
            this.closestRelativeIntensityLabel = new System.Windows.Forms.Label();
            this.mzTextBox = new System.Windows.Forms.TextBox();
            this.closestMZLabel = new System.Windows.Forms.Label();
            this.relativeIntensityTextBox = new System.Windows.Forms.TextBox();
            this.groupBoxAnnotationCoverage = new System.Windows.Forms.GroupBox();
            this.labelMore = new System.Windows.Forms.Label();
            this.labelLess = new System.Windows.Forms.Label();
            this.trackBarAnnotationPercent = new System.Windows.Forms.TrackBar();
            this.buttonHidePlotOptions = new System.Windows.Forms.Button();
            this.buttonDetachPlot = new System.Windows.Forms.Button();
            this.buttonPlotOptions = new System.Windows.Forms.Button();
            this.numberOfPlots = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.msPlot = new SpectrumLook.Views.MyZedGraph();
            this.groupBoxClosestPoint.SuspendLayout();
            this.groupBoxAnnotationCoverage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAnnotationPercent)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxClosestPoint
            // 
            this.groupBoxClosestPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxClosestPoint.Controls.Add(this.closestRelativeIntensityLabel);
            this.groupBoxClosestPoint.Controls.Add(this.mzTextBox);
            this.groupBoxClosestPoint.Controls.Add(this.closestMZLabel);
            this.groupBoxClosestPoint.Controls.Add(this.relativeIntensityTextBox);
            this.groupBoxClosestPoint.Location = new System.Drawing.Point(12, 292);
            this.groupBoxClosestPoint.Name = "groupBoxClosestPoint";
            this.groupBoxClosestPoint.Size = new System.Drawing.Size(196, 70);
            this.groupBoxClosestPoint.TabIndex = 8;
            this.groupBoxClosestPoint.TabStop = false;
            this.groupBoxClosestPoint.Text = "Closest Point";
            // 
            // closestRelativeIntensityLabel
            // 
            this.closestRelativeIntensityLabel.AutoSize = true;
            this.closestRelativeIntensityLabel.Location = new System.Drawing.Point(10, 45);
            this.closestRelativeIntensityLabel.Name = "closestRelativeIntensityLabel";
            this.closestRelativeIntensityLabel.Size = new System.Drawing.Size(88, 13);
            this.closestRelativeIntensityLabel.TabIndex = 6;
            this.closestRelativeIntensityLabel.Text = "Relative Intensity";
            // 
            // mzTextBox
            // 
            this.mzTextBox.Enabled = false;
            this.mzTextBox.Location = new System.Drawing.Point(104, 15);
            this.mzTextBox.Name = "mzTextBox";
            this.mzTextBox.Size = new System.Drawing.Size(81, 20);
            this.mzTextBox.TabIndex = 2;
            // 
            // closestMZLabel
            // 
            this.closestMZLabel.AutoSize = true;
            this.closestMZLabel.Location = new System.Drawing.Point(73, 18);
            this.closestMZLabel.Name = "closestMZLabel";
            this.closestMZLabel.Size = new System.Drawing.Size(25, 13);
            this.closestMZLabel.TabIndex = 3;
            this.closestMZLabel.Text = "m/z";
            // 
            // relativeIntensityTextBox
            // 
            this.relativeIntensityTextBox.Enabled = false;
            this.relativeIntensityTextBox.Location = new System.Drawing.Point(104, 42);
            this.relativeIntensityTextBox.Name = "relativeIntensityTextBox";
            this.relativeIntensityTextBox.Size = new System.Drawing.Size(81, 20);
            this.relativeIntensityTextBox.TabIndex = 5;
            // 
            // groupBoxAnnotationCoverage
            // 
            this.groupBoxAnnotationCoverage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxAnnotationCoverage.Controls.Add(this.labelMore);
            this.groupBoxAnnotationCoverage.Controls.Add(this.labelLess);
            this.groupBoxAnnotationCoverage.Controls.Add(this.trackBarAnnotationPercent);
            this.groupBoxAnnotationCoverage.Location = new System.Drawing.Point(214, 292);
            this.groupBoxAnnotationCoverage.Name = "groupBoxAnnotationCoverage";
            this.groupBoxAnnotationCoverage.Size = new System.Drawing.Size(405, 70);
            this.groupBoxAnnotationCoverage.TabIndex = 9;
            this.groupBoxAnnotationCoverage.TabStop = false;
            this.groupBoxAnnotationCoverage.Text = "Annotation Coverage";
            // 
            // labelMore
            // 
            this.labelMore.AutoSize = true;
            this.labelMore.Location = new System.Drawing.Point(368, 18);
            this.labelMore.Name = "labelMore";
            this.labelMore.Size = new System.Drawing.Size(31, 13);
            this.labelMore.TabIndex = 2;
            this.labelMore.Text = "More";
            // 
            // labelLess
            // 
            this.labelLess.AutoSize = true;
            this.labelLess.Location = new System.Drawing.Point(6, 22);
            this.labelLess.Name = "labelLess";
            this.labelLess.Size = new System.Drawing.Size(29, 13);
            this.labelLess.TabIndex = 1;
            this.labelLess.Text = "Less";
            // 
            // trackBarAnnotationPercent
            // 
            this.trackBarAnnotationPercent.Location = new System.Drawing.Point(41, 18);
            this.trackBarAnnotationPercent.Maximum = 100;
            this.trackBarAnnotationPercent.Name = "trackBarAnnotationPercent";
            this.trackBarAnnotationPercent.Size = new System.Drawing.Size(321, 45);
            this.trackBarAnnotationPercent.TabIndex = 0;
            this.trackBarAnnotationPercent.Scroll += new System.EventHandler(this.trackBarAnnotationPercent_Scroll);
            // 
            // buttonHidePlotOptions
            // 
            this.buttonHidePlotOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHidePlotOptions.Location = new System.Drawing.Point(702, 346);
            this.buttonHidePlotOptions.Name = "buttonHidePlotOptions";
            this.buttonHidePlotOptions.Size = new System.Drawing.Size(48, 19);
            this.buttonHidePlotOptions.TabIndex = 0;
            this.buttonHidePlotOptions.UseVisualStyleBackColor = true;
            this.buttonHidePlotOptions.Click += new System.EventHandler(this.buttonHidePlotOptions_Click);
            // 
            // buttonDetachPlot
            // 
            this.buttonDetachPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDetachPlot.Location = new System.Drawing.Point(623, 296);
            this.buttonDetachPlot.Name = "buttonDetachPlot";
            this.buttonDetachPlot.Size = new System.Drawing.Size(75, 30);
            this.buttonDetachPlot.TabIndex = 11;
            this.buttonDetachPlot.Text = "Detach Plot";
            this.buttonDetachPlot.UseVisualStyleBackColor = true;
            this.buttonDetachPlot.Click += new System.EventHandler(this.buttonDetachPlot_Click);
            // 
            // buttonPlotOptions
            // 
            this.buttonPlotOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlotOptions.Location = new System.Drawing.Point(623, 332);
            this.buttonPlotOptions.Name = "buttonPlotOptions";
            this.buttonPlotOptions.Size = new System.Drawing.Size(75, 30);
            this.buttonPlotOptions.TabIndex = 12;
            this.buttonPlotOptions.Text = "Plot Options";
            this.buttonPlotOptions.UseVisualStyleBackColor = true;
            this.buttonPlotOptions.Click += new System.EventHandler(this.buttonPlotOptions_Click);
            // 
            // numberOfPlots
            // 
            this.numberOfPlots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numberOfPlots.Location = new System.Drawing.Point(702, 320);
            this.numberOfPlots.Name = "numberOfPlots";
            this.numberOfPlots.Size = new System.Drawing.Size(48, 20);
            this.numberOfPlots.TabIndex = 13;
            this.numberOfPlots.TextChanged += new System.EventHandler(this.numberOfPlots_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(702, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "# of plots";
            // 
            // msPlot
            // 
            this.msPlot.Location = new System.Drawing.Point(12, 12);
            this.msPlot.Name = "msPlot";
            this.msPlot.ScrollGrace = 0D;
            this.msPlot.ScrollMaxX = 0D;
            this.msPlot.ScrollMaxY = 0D;
            this.msPlot.ScrollMaxY2 = 0D;
            this.msPlot.ScrollMinX = 0D;
            this.msPlot.ScrollMinY = 0D;
            this.msPlot.ScrollMinY2 = 0D;
            this.msPlot.Size = new System.Drawing.Size(733, 274);
            this.msPlot.TabIndex = 3;
            this.msPlot.DoubleClick += new System.EventHandler(this.msPlot_DoubleClick);
            // 
            // SLPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 374);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numberOfPlots);
            this.Controls.Add(this.buttonDetachPlot);
            this.Controls.Add(this.buttonHidePlotOptions);
            this.Controls.Add(this.groupBoxAnnotationCoverage);
            this.Controls.Add(this.msPlot);
            this.Controls.Add(this.groupBoxClosestPoint);
            this.Controls.Add(this.buttonPlotOptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SLPlot";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBoxClosestPoint.ResumeLayout(false);
            this.groupBoxClosestPoint.PerformLayout();
            this.groupBoxAnnotationCoverage.ResumeLayout(false);
            this.groupBoxAnnotationCoverage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarAnnotationPercent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyZedGraph msPlot;
        private System.Windows.Forms.GroupBox groupBoxClosestPoint;
        private System.Windows.Forms.Label closestRelativeIntensityLabel;
        private System.Windows.Forms.TextBox mzTextBox;
        private System.Windows.Forms.Label closestMZLabel;
        private System.Windows.Forms.TextBox relativeIntensityTextBox;
        private System.Windows.Forms.GroupBox groupBoxAnnotationCoverage;
        private System.Windows.Forms.TrackBar trackBarAnnotationPercent;
        private System.Windows.Forms.Label labelMore;
        private System.Windows.Forms.Label labelLess;
        private System.Windows.Forms.Button buttonHidePlotOptions;
        private System.Windows.Forms.Button buttonDetachPlot;
        private System.Windows.Forms.Button buttonPlotOptions;
        private System.Windows.Forms.TextBox numberOfPlots;
        private System.Windows.Forms.Label label1;
    }
}

