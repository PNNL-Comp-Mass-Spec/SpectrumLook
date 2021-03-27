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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
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
            this.groupBoxClosestPoint.Location = new System.Drawing.Point(16, 314);
            this.groupBoxClosestPoint.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxClosestPoint.Name = "groupBoxClosestPoint";
            this.groupBoxClosestPoint.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxClosestPoint.Size = new System.Drawing.Size(261, 86);
            this.groupBoxClosestPoint.TabIndex = 8;
            this.groupBoxClosestPoint.TabStop = false;
            this.groupBoxClosestPoint.Text = "Closest Point";
            //
            // closestRelativeIntensityLabel
            //
            this.closestRelativeIntensityLabel.AutoSize = true;
            this.closestRelativeIntensityLabel.Location = new System.Drawing.Point(13, 55);
            this.closestRelativeIntensityLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.closestRelativeIntensityLabel.Name = "closestRelativeIntensityLabel";
            this.closestRelativeIntensityLabel.Size = new System.Drawing.Size(115, 17);
            this.closestRelativeIntensityLabel.TabIndex = 6;
            this.closestRelativeIntensityLabel.Text = "Relative Intensity";
            //
            // mzTextBox
            //
            this.mzTextBox.Enabled = false;
            this.mzTextBox.Location = new System.Drawing.Point(139, 18);
            this.mzTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.mzTextBox.Name = "mzTextBox";
            this.mzTextBox.Size = new System.Drawing.Size(107, 22);
            this.mzTextBox.TabIndex = 2;
            //
            // closestMZLabel
            //
            this.closestMZLabel.AutoSize = true;
            this.closestMZLabel.Location = new System.Drawing.Point(97, 22);
            this.closestMZLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.closestMZLabel.Name = "closestMZLabel";
            this.closestMZLabel.Size = new System.Drawing.Size(30, 17);
            this.closestMZLabel.TabIndex = 3;
            this.closestMZLabel.Text = "m/z";
            //
            // relativeIntensityTextBox
            //
            this.relativeIntensityTextBox.Enabled = false;
            this.relativeIntensityTextBox.Location = new System.Drawing.Point(139, 52);
            this.relativeIntensityTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.relativeIntensityTextBox.Name = "relativeIntensityTextBox";
            this.relativeIntensityTextBox.Size = new System.Drawing.Size(107, 22);
            this.relativeIntensityTextBox.TabIndex = 5;
            //
            // groupBoxAnnotationCoverage
            //
            this.groupBoxAnnotationCoverage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAnnotationCoverage.Controls.Add(this.labelMore);
            this.groupBoxAnnotationCoverage.Controls.Add(this.labelLess);
            this.groupBoxAnnotationCoverage.Controls.Add(this.trackBarAnnotationPercent);
            this.groupBoxAnnotationCoverage.Location = new System.Drawing.Point(285, 314);
            this.groupBoxAnnotationCoverage.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxAnnotationCoverage.Name = "groupBoxAnnotationCoverage";
            this.groupBoxAnnotationCoverage.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxAnnotationCoverage.Size = new System.Drawing.Size(309, 86);
            this.groupBoxAnnotationCoverage.TabIndex = 9;
            this.groupBoxAnnotationCoverage.TabStop = false;
            this.groupBoxAnnotationCoverage.Text = "Annotation Coverage";
            //
            // labelMore
            //
            this.labelMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMore.AutoSize = true;
            this.labelMore.Location = new System.Drawing.Point(254, 22);
            this.labelMore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelMore.Name = "labelMore";
            this.labelMore.Size = new System.Drawing.Size(40, 17);
            this.labelMore.TabIndex = 2;
            this.labelMore.Text = "More";
            //
            // labelLess
            //
            this.labelLess.AutoSize = true;
            this.labelLess.Location = new System.Drawing.Point(8, 27);
            this.labelLess.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLess.Name = "labelLess";
            this.labelLess.Size = new System.Drawing.Size(38, 17);
            this.labelLess.TabIndex = 1;
            this.labelLess.Text = "Less";
            //
            // trackBarAnnotationPercent
            //
            this.trackBarAnnotationPercent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarAnnotationPercent.Location = new System.Drawing.Point(55, 22);
            this.trackBarAnnotationPercent.Margin = new System.Windows.Forms.Padding(4);
            this.trackBarAnnotationPercent.Maximum = 100;
            this.trackBarAnnotationPercent.Name = "trackBarAnnotationPercent";
            this.trackBarAnnotationPercent.Size = new System.Drawing.Size(191, 56);
            this.trackBarAnnotationPercent.TabIndex = 0;
            this.trackBarAnnotationPercent.Scroll += new System.EventHandler(this.trackBarAnnotationPercent_Scroll);
            //
            // buttonHidePlotOptions
            //
            this.buttonHidePlotOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHidePlotOptions.Location = new System.Drawing.Point(707, 381);
            this.buttonHidePlotOptions.Margin = new System.Windows.Forms.Padding(4);
            this.buttonHidePlotOptions.Name = "buttonHidePlotOptions";
            this.buttonHidePlotOptions.Size = new System.Drawing.Size(64, 23);
            this.buttonHidePlotOptions.TabIndex = 0;
            this.buttonHidePlotOptions.UseVisualStyleBackColor = true;
            this.buttonHidePlotOptions.Click += new System.EventHandler(this.buttonHidePlotOptions_Click);
            //
            // buttonDetachPlot
            //
            this.buttonDetachPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDetachPlot.Location = new System.Drawing.Point(602, 319);
            this.buttonDetachPlot.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDetachPlot.Name = "buttonDetachPlot";
            this.buttonDetachPlot.Size = new System.Drawing.Size(100, 37);
            this.buttonDetachPlot.TabIndex = 11;
            this.buttonDetachPlot.Text = "Detach Plot";
            this.buttonDetachPlot.UseVisualStyleBackColor = true;
            this.buttonDetachPlot.Click += new System.EventHandler(this.buttonDetachPlot_Click);
            //
            // buttonPlotOptions
            //
            this.buttonPlotOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPlotOptions.Location = new System.Drawing.Point(602, 364);
            this.buttonPlotOptions.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPlotOptions.Name = "buttonPlotOptions";
            this.buttonPlotOptions.Size = new System.Drawing.Size(100, 37);
            this.buttonPlotOptions.TabIndex = 12;
            this.buttonPlotOptions.Text = "Plot Options";
            this.buttonPlotOptions.UseVisualStyleBackColor = true;
            this.buttonPlotOptions.Click += new System.EventHandler(this.buttonPlotOptions_Click);
            //
            // numberOfPlots
            //
            this.numberOfPlots.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numberOfPlots.Location = new System.Drawing.Point(707, 349);
            this.numberOfPlots.Margin = new System.Windows.Forms.Padding(4);
            this.numberOfPlots.Name = "numberOfPlots";
            this.numberOfPlots.Size = new System.Drawing.Size(63, 22);
            this.numberOfPlots.TabIndex = 13;
            this.numberOfPlots.TextChanged += new System.EventHandler(this.numberOfPlots_TextChanged);
            //
            // label1
            //
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(707, 325);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "# of plots";
            //
            // msPlot
            //
            this.msPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msPlot.Location = new System.Drawing.Point(16, 15);
            this.msPlot.Margin = new System.Windows.Forms.Padding(5);
            this.msPlot.Name = "msPlot";
            this.msPlot.ScrollGrace = 0D;
            this.msPlot.ScrollMaxX = 0D;
            this.msPlot.ScrollMaxY = 0D;
            this.msPlot.ScrollMaxY2 = 0D;
            this.msPlot.ScrollMinX = 0D;
            this.msPlot.ScrollMinY = 0D;
            this.msPlot.ScrollMinY2 = 0D;
            this.msPlot.Size = new System.Drawing.Size(752, 291);
            this.msPlot.TabIndex = 3;
            this.msPlot.DoubleClick += new System.EventHandler(this.msPlot_DoubleClick);
            //
            // SLPlot
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 415);
            this.Controls.Add(this.buttonPlotOptions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numberOfPlots);
            this.Controls.Add(this.buttonDetachPlot);
            this.Controls.Add(this.buttonHidePlotOptions);
            this.Controls.Add(this.groupBoxAnnotationCoverage);
            this.Controls.Add(this.msPlot);
            this.Controls.Add(this.groupBoxClosestPoint);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
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

