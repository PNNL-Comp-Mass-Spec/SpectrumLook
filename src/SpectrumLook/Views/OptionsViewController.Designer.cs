namespace SpectrumLook.Views
{
    partial class OptionsViewController
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsViewController));
            this.defaultButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.PlotOptionsView = new System.Windows.Forms.TabPage();
            this.mainWindowOptionsGroup = new System.Windows.Forms.GroupBox();
            this.mainDetachPlotCheckBox = new System.Windows.Forms.CheckBox();
            this.multiplePlotGroup = new System.Windows.Forms.GroupBox();
            this.numberOfPlotsLabel = new System.Windows.Forms.Label();
            this.plotNumberOfPlotsTextBox = new System.Windows.Forms.TextBox();
            this.plotAnnotationOptionsGroup = new System.Windows.Forms.GroupBox();
            this.plotChangeColorOpenButton = new System.Windows.Forms.Button();
            this.plotAnnotationColor = new System.Windows.Forms.Panel();
            this.plotAnnotationColorLabel = new System.Windows.Forms.Label();
            this.plotTextSize = new System.Windows.Forms.TextBox();
            this.plotAnnotationTextSizeLabel = new System.Windows.Forms.Label();
            this.plotAnnotationPercentLabel2 = new System.Windows.Forms.Label();
            this.plotAnnotationPercentBox = new System.Windows.Forms.TextBox();
            this.plotAnnotationPercentLabel1 = new System.Windows.Forms.Label();
            this.plotZoomOptionsGroup = new System.Windows.Forms.GroupBox();
            this.plotRightClickUnzoom = new System.Windows.Forms.CheckBox();
            this.plotFragLadderSelectBox = new System.Windows.Forms.TextBox();
            this.plotFragLadderSelectLabel = new System.Windows.Forms.Label();
            this.plotUnzoomKeyLabel = new System.Windows.Forms.Label();
            this.plotUnzoomKeyComboBox = new System.Windows.Forms.ComboBox();
            this.plotBoxZoom = new System.Windows.Forms.RadioButton();
            this.plotHorizontalZoom = new System.Windows.Forms.RadioButton();
            this.plotGeneralOptionsGroup = new System.Windows.Forms.GroupBox();
            this.plotHidePlotTools = new System.Windows.Forms.CheckBox();
            this.plotHideUnmatchedData = new System.Windows.Forms.CheckBox();
            this.plotSnappingCursor = new System.Windows.Forms.CheckBox();
            this.plotShowLegend = new System.Windows.Forms.CheckBox();
            this.optionTabsPage = new System.Windows.Forms.TabControl();
            this.fragLadderTab = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewModList = new System.Windows.Forms.DataGridView();
            this.MainOptionsView = new System.Windows.Forms.TabPage();
            this.mainDataPropertiesGroup = new System.Windows.Forms.GroupBox();
            this.mainUnmatchColorChangeButton = new System.Windows.Forms.Button();
            this.mainUnmatchedColorSample = new System.Windows.Forms.Panel();
            this.mainUnmatchedColorLabel = new System.Windows.Forms.Label();
            this.mainMatchColorChangeButton = new System.Windows.Forms.Button();
            this.mainMatchedColorSample = new System.Windows.Forms.Panel();
            this.mainMatchedColorLabel = new System.Windows.Forms.Label();
            this.mainMatchingPropertiesGroup = new System.Windows.Forms.GroupBox();
            this.lowerMatchingToleranceBox = new System.Windows.Forms.TextBox();
            this.lowerMatchingToleranceLabel = new System.Windows.Forms.Label();
            this.mainMatchingToleranceBox = new System.Windows.Forms.TextBox();
            this.mainMatchingToleranceLabel = new System.Windows.Forms.Label();
            this.mainUserProfileGroup = new System.Windows.Forms.GroupBox();
            this.mainUserBrowseButton = new System.Windows.Forms.Button();
            this.mainProfileFileLocationBox = new System.Windows.Forms.TextBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PlotOptionsView.SuspendLayout();
            this.mainWindowOptionsGroup.SuspendLayout();
            this.multiplePlotGroup.SuspendLayout();
            this.plotAnnotationOptionsGroup.SuspendLayout();
            this.plotZoomOptionsGroup.SuspendLayout();
            this.plotGeneralOptionsGroup.SuspendLayout();
            this.optionTabsPage.SuspendLayout();
            this.fragLadderTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModList)).BeginInit();
            this.MainOptionsView.SuspendLayout();
            this.mainDataPropertiesGroup.SuspendLayout();
            this.mainMatchingPropertiesGroup.SuspendLayout();
            this.mainUserProfileGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultButton
            // 
            this.defaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.defaultButton.Location = new System.Drawing.Point(17, 519);
            this.defaultButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(100, 28);
            this.defaultButton.TabIndex = 14;
            this.defaultButton.Text = "Default";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.DefaultButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(357, 519);
            this.applyButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(100, 28);
            this.applyButton.TabIndex = 16;
            this.applyButton.Text = "Ok";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(465, 519);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 28);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // PlotOptionsView
            // 
            this.PlotOptionsView.Controls.Add(this.mainWindowOptionsGroup);
            this.PlotOptionsView.Controls.Add(this.multiplePlotGroup);
            this.PlotOptionsView.Controls.Add(this.plotAnnotationOptionsGroup);
            this.PlotOptionsView.Controls.Add(this.plotZoomOptionsGroup);
            this.PlotOptionsView.Controls.Add(this.plotGeneralOptionsGroup);
            this.PlotOptionsView.Location = new System.Drawing.Point(4, 25);
            this.PlotOptionsView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PlotOptionsView.Name = "PlotOptionsView";
            this.PlotOptionsView.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PlotOptionsView.Size = new System.Drawing.Size(544, 467);
            this.PlotOptionsView.TabIndex = 0;
            this.PlotOptionsView.Text = "Plot Options";
            this.PlotOptionsView.UseVisualStyleBackColor = true;
            // 
            // mainWindowOptionsGroup
            // 
            this.mainWindowOptionsGroup.Controls.Add(this.mainDetachPlotCheckBox);
            this.mainWindowOptionsGroup.Location = new System.Drawing.Point(268, 367);
            this.mainWindowOptionsGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainWindowOptionsGroup.Name = "mainWindowOptionsGroup";
            this.mainWindowOptionsGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainWindowOptionsGroup.Size = new System.Drawing.Size(265, 79);
            this.mainWindowOptionsGroup.TabIndex = 5;
            this.mainWindowOptionsGroup.TabStop = false;
            this.mainWindowOptionsGroup.Text = "Window Options";
            // 
            // mainDetachPlotCheckBox
            // 
            this.mainDetachPlotCheckBox.AutoSize = true;
            this.mainDetachPlotCheckBox.Location = new System.Drawing.Point(8, 36);
            this.mainDetachPlotCheckBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainDetachPlotCheckBox.Name = "mainDetachPlotCheckBox";
            this.mainDetachPlotCheckBox.Size = new System.Drawing.Size(103, 21);
            this.mainDetachPlotCheckBox.TabIndex = 14;
            this.mainDetachPlotCheckBox.Text = "Detach Plot";
            this.mainDetachPlotCheckBox.UseVisualStyleBackColor = true;
            this.mainDetachPlotCheckBox.CheckedChanged += new System.EventHandler(this.MainDetachPlotCheckBox_CheckedChanged_1);
            // 
            // multiplePlotGroup
            // 
            this.multiplePlotGroup.Controls.Add(this.numberOfPlotsLabel);
            this.multiplePlotGroup.Controls.Add(this.plotNumberOfPlotsTextBox);
            this.multiplePlotGroup.Location = new System.Drawing.Point(9, 367);
            this.multiplePlotGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.multiplePlotGroup.Name = "multiplePlotGroup";
            this.multiplePlotGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.multiplePlotGroup.Size = new System.Drawing.Size(251, 79);
            this.multiplePlotGroup.TabIndex = 4;
            this.multiplePlotGroup.TabStop = false;
            this.multiplePlotGroup.Text = "Multiple Plot Options";
            // 
            // numberOfPlotsLabel
            // 
            this.numberOfPlotsLabel.AutoSize = true;
            this.numberOfPlotsLabel.Location = new System.Drawing.Point(9, 37);
            this.numberOfPlotsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.numberOfPlotsLabel.Name = "numberOfPlotsLabel";
            this.numberOfPlotsLabel.Size = new System.Drawing.Size(113, 17);
            this.numberOfPlotsLabel.TabIndex = 1;
            this.numberOfPlotsLabel.Text = "Number of Plots:";
            // 
            // plotNumberOfPlotsTextBox
            // 
            this.plotNumberOfPlotsTextBox.Location = new System.Drawing.Point(127, 33);
            this.plotNumberOfPlotsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotNumberOfPlotsTextBox.Name = "plotNumberOfPlotsTextBox";
            this.plotNumberOfPlotsTextBox.Size = new System.Drawing.Size(56, 22);
            this.plotNumberOfPlotsTextBox.TabIndex = 13;
            this.plotNumberOfPlotsTextBox.Leave += new System.EventHandler(this.NumberOfPlotsTextBox_Leave);
            // 
            // plotAnnotationOptionsGroup
            // 
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotChangeColorOpenButton);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationColor);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationColorLabel);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotTextSize);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationTextSizeLabel);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationPercentLabel2);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationPercentBox);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationPercentLabel1);
            this.plotAnnotationOptionsGroup.Location = new System.Drawing.Point(9, 234);
            this.plotAnnotationOptionsGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotAnnotationOptionsGroup.Name = "plotAnnotationOptionsGroup";
            this.plotAnnotationOptionsGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotAnnotationOptionsGroup.Size = new System.Drawing.Size(524, 126);
            this.plotAnnotationOptionsGroup.TabIndex = 2;
            this.plotAnnotationOptionsGroup.TabStop = false;
            this.plotAnnotationOptionsGroup.Text = "Annotation Options";
            // 
            // plotChangeColorOpenButton
            // 
            this.plotChangeColorOpenButton.Location = new System.Drawing.Point(131, 80);
            this.plotChangeColorOpenButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotChangeColorOpenButton.Name = "plotChangeColorOpenButton";
            this.plotChangeColorOpenButton.Size = new System.Drawing.Size(115, 28);
            this.plotChangeColorOpenButton.TabIndex = 12;
            this.plotChangeColorOpenButton.Text = "Change Color";
            this.plotChangeColorOpenButton.UseVisualStyleBackColor = true;
            this.plotChangeColorOpenButton.Click += new System.EventHandler(this.PlotChangeColorOpenButton_Click);
            // 
            // plotAnnotationColor
            // 
            this.plotAnnotationColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plotAnnotationColor.Location = new System.Drawing.Point(91, 86);
            this.plotAnnotationColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotAnnotationColor.Name = "plotAnnotationColor";
            this.plotAnnotationColor.Size = new System.Drawing.Size(19, 18);
            this.plotAnnotationColor.TabIndex = 11;
            // 
            // plotAnnotationColorLabel
            // 
            this.plotAnnotationColorLabel.AutoSize = true;
            this.plotAnnotationColorLabel.Location = new System.Drawing.Point(19, 86);
            this.plotAnnotationColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.plotAnnotationColorLabel.Name = "plotAnnotationColorLabel";
            this.plotAnnotationColorLabel.Size = new System.Drawing.Size(41, 17);
            this.plotAnnotationColorLabel.TabIndex = 5;
            this.plotAnnotationColorLabel.Text = "Color";
            // 
            // plotTextSize
            // 
            this.plotTextSize.Location = new System.Drawing.Point(91, 50);
            this.plotTextSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotTextSize.Name = "plotTextSize";
            this.plotTextSize.Size = new System.Drawing.Size(40, 22);
            this.plotTextSize.TabIndex = 10;
            this.plotTextSize.Leave += new System.EventHandler(this.PlotTextSize_TextChanged);
            // 
            // plotAnnotationTextSizeLabel
            // 
            this.plotAnnotationTextSizeLabel.AutoSize = true;
            this.plotAnnotationTextSizeLabel.Location = new System.Drawing.Point(15, 54);
            this.plotAnnotationTextSizeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.plotAnnotationTextSizeLabel.Name = "plotAnnotationTextSizeLabel";
            this.plotAnnotationTextSizeLabel.Size = new System.Drawing.Size(66, 17);
            this.plotAnnotationTextSizeLabel.TabIndex = 3;
            this.plotAnnotationTextSizeLabel.Text = "Text Size";
            // 
            // plotAnnotationPercentLabel2
            // 
            this.plotAnnotationPercentLabel2.AutoSize = true;
            this.plotAnnotationPercentLabel2.Location = new System.Drawing.Point(287, 25);
            this.plotAnnotationPercentLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.plotAnnotationPercentLabel2.Name = "plotAnnotationPercentLabel2";
            this.plotAnnotationPercentLabel2.Size = new System.Drawing.Size(140, 17);
            this.plotAnnotationPercentLabel2.TabIndex = 2;
            this.plotAnnotationPercentLabel2.Text = "% of matched peaks.";
            // 
            // plotAnnotationPercentBox
            // 
            this.plotAnnotationPercentBox.Location = new System.Drawing.Point(239, 21);
            this.plotAnnotationPercentBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotAnnotationPercentBox.Name = "plotAnnotationPercentBox";
            this.plotAnnotationPercentBox.Size = new System.Drawing.Size(37, 22);
            this.plotAnnotationPercentBox.TabIndex = 9;
            this.plotAnnotationPercentBox.Leave += new System.EventHandler(this.PlotAnnotationPercentBox_TextChanged);
            // 
            // plotAnnotationPercentLabel1
            // 
            this.plotAnnotationPercentLabel1.AutoSize = true;
            this.plotAnnotationPercentLabel1.Location = new System.Drawing.Point(15, 25);
            this.plotAnnotationPercentLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.plotAnnotationPercentLabel1.Name = "plotAnnotationPercentLabel1";
            this.plotAnnotationPercentLabel1.Size = new System.Drawing.Size(214, 17);
            this.plotAnnotationPercentLabel1.TabIndex = 0;
            this.plotAnnotationPercentLabel1.Text = "Show only Annotations in the top";
            // 
            // plotZoomOptionsGroup
            // 
            this.plotZoomOptionsGroup.Controls.Add(this.plotRightClickUnzoom);
            this.plotZoomOptionsGroup.Controls.Add(this.plotFragLadderSelectBox);
            this.plotZoomOptionsGroup.Controls.Add(this.plotFragLadderSelectLabel);
            this.plotZoomOptionsGroup.Controls.Add(this.plotUnzoomKeyLabel);
            this.plotZoomOptionsGroup.Controls.Add(this.plotUnzoomKeyComboBox);
            this.plotZoomOptionsGroup.Controls.Add(this.plotBoxZoom);
            this.plotZoomOptionsGroup.Controls.Add(this.plotHorizontalZoom);
            this.plotZoomOptionsGroup.Location = new System.Drawing.Point(9, 102);
            this.plotZoomOptionsGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotZoomOptionsGroup.Name = "plotZoomOptionsGroup";
            this.plotZoomOptionsGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotZoomOptionsGroup.Size = new System.Drawing.Size(524, 124);
            this.plotZoomOptionsGroup.TabIndex = 1;
            this.plotZoomOptionsGroup.TabStop = false;
            this.plotZoomOptionsGroup.Text = "Zoom Options";
            // 
            // plotRightClickUnzoom
            // 
            this.plotRightClickUnzoom.AutoSize = true;
            this.plotRightClickUnzoom.Location = new System.Drawing.Point(8, 53);
            this.plotRightClickUnzoom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotRightClickUnzoom.Name = "plotRightClickUnzoom";
            this.plotRightClickUnzoom.Size = new System.Drawing.Size(219, 21);
            this.plotRightClickUnzoom.TabIndex = 6;
            this.plotRightClickUnzoom.Text = "Enable Right Click Unzooming";
            this.plotRightClickUnzoom.UseVisualStyleBackColor = true;
            this.plotRightClickUnzoom.CheckedChanged += new System.EventHandler(this.CheckBoxRightClickUnzoom_CheckedChanged);
            // 
            // plotFragLadderSelectBox
            // 
            this.plotFragLadderSelectBox.Location = new System.Drawing.Point(261, 85);
            this.plotFragLadderSelectBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotFragLadderSelectBox.Name = "plotFragLadderSelectBox";
            this.plotFragLadderSelectBox.Size = new System.Drawing.Size(132, 22);
            this.plotFragLadderSelectBox.TabIndex = 8;
            this.plotFragLadderSelectBox.Leave += new System.EventHandler(this.PlotFragLadderSelectBox_TextChanged);
            // 
            // plotFragLadderSelectLabel
            // 
            this.plotFragLadderSelectLabel.AutoSize = true;
            this.plotFragLadderSelectLabel.Location = new System.Drawing.Point(8, 89);
            this.plotFragLadderSelectLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.plotFragLadderSelectLabel.Name = "plotFragLadderSelectLabel";
            this.plotFragLadderSelectLabel.Size = new System.Drawing.Size(257, 17);
            this.plotFragLadderSelectLabel.TabIndex = 4;
            this.plotFragLadderSelectLabel.Text = "Fragment Ladder auto zoom select size";
            // 
            // plotUnzoomKeyLabel
            // 
            this.plotUnzoomKeyLabel.AutoSize = true;
            this.plotUnzoomKeyLabel.Location = new System.Drawing.Point(257, 54);
            this.plotUnzoomKeyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.plotUnzoomKeyLabel.Name = "plotUnzoomKeyLabel";
            this.plotUnzoomKeyLabel.Size = new System.Drawing.Size(88, 17);
            this.plotUnzoomKeyLabel.TabIndex = 3;
            this.plotUnzoomKeyLabel.Text = "Unzoom Key";
            // 
            // plotUnzoomKeyComboBox
            // 
            this.plotUnzoomKeyComboBox.FormattingEnabled = true;
            this.plotUnzoomKeyComboBox.Location = new System.Drawing.Point(355, 50);
            this.plotUnzoomKeyComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotUnzoomKeyComboBox.Name = "plotUnzoomKeyComboBox";
            this.plotUnzoomKeyComboBox.Size = new System.Drawing.Size(160, 24);
            this.plotUnzoomKeyComboBox.TabIndex = 7;
            this.plotUnzoomKeyComboBox.Leave += new System.EventHandler(this.PlotUnzoomKeyComboBox_Leave);
            // 
            // plotBoxZoom
            // 
            this.plotBoxZoom.AutoSize = true;
            this.plotBoxZoom.Location = new System.Drawing.Point(153, 25);
            this.plotBoxZoom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotBoxZoom.Name = "plotBoxZoom";
            this.plotBoxZoom.Size = new System.Drawing.Size(92, 21);
            this.plotBoxZoom.TabIndex = 5;
            this.plotBoxZoom.TabStop = true;
            this.plotBoxZoom.Text = "Box Zoom";
            this.plotBoxZoom.UseVisualStyleBackColor = true;
            this.plotBoxZoom.CheckedChanged += new System.EventHandler(this.PlotBoxZoom_CheckedChanged);
            // 
            // plotHorizontalZoom
            // 
            this.plotHorizontalZoom.AutoSize = true;
            this.plotHorizontalZoom.Location = new System.Drawing.Point(9, 25);
            this.plotHorizontalZoom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotHorizontalZoom.Name = "plotHorizontalZoom";
            this.plotHorizontalZoom.Size = new System.Drawing.Size(133, 21);
            this.plotHorizontalZoom.TabIndex = 4;
            this.plotHorizontalZoom.TabStop = true;
            this.plotHorizontalZoom.Text = "Horizontal Zoom";
            this.plotHorizontalZoom.UseVisualStyleBackColor = true;
            this.plotHorizontalZoom.CheckedChanged += new System.EventHandler(this.PlotHorizontalZoom_CheckedChanged);
            // 
            // plotGeneralOptionsGroup
            // 
            this.plotGeneralOptionsGroup.Controls.Add(this.plotHidePlotTools);
            this.plotGeneralOptionsGroup.Controls.Add(this.plotHideUnmatchedData);
            this.plotGeneralOptionsGroup.Controls.Add(this.plotSnappingCursor);
            this.plotGeneralOptionsGroup.Controls.Add(this.plotShowLegend);
            this.plotGeneralOptionsGroup.Location = new System.Drawing.Point(9, 9);
            this.plotGeneralOptionsGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotGeneralOptionsGroup.Name = "plotGeneralOptionsGroup";
            this.plotGeneralOptionsGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotGeneralOptionsGroup.Size = new System.Drawing.Size(524, 86);
            this.plotGeneralOptionsGroup.TabIndex = 0;
            this.plotGeneralOptionsGroup.TabStop = false;
            this.plotGeneralOptionsGroup.Text = "General Options";
            // 
            // plotHidePlotTools
            // 
            this.plotHidePlotTools.AutoSize = true;
            this.plotHidePlotTools.Location = new System.Drawing.Point(204, 54);
            this.plotHidePlotTools.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotHidePlotTools.Name = "plotHidePlotTools";
            this.plotHidePlotTools.Size = new System.Drawing.Size(160, 21);
            this.plotHidePlotTools.TabIndex = 3;
            this.plotHidePlotTools.Text = "Hide Plot Tools Area";
            this.plotHidePlotTools.UseVisualStyleBackColor = true;
            this.plotHidePlotTools.CheckedChanged += new System.EventHandler(this.CheckBoxHidePlotTools_CheckedChanged);
            // 
            // plotHideUnmatchedData
            // 
            this.plotHideUnmatchedData.AutoSize = true;
            this.plotHideUnmatchedData.Location = new System.Drawing.Point(204, 25);
            this.plotHideUnmatchedData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotHideUnmatchedData.Name = "plotHideUnmatchedData";
            this.plotHideUnmatchedData.Size = new System.Drawing.Size(212, 21);
            this.plotHideUnmatchedData.TabIndex = 1;
            this.plotHideUnmatchedData.Text = "Hide Unmatched Data in Plot";
            this.plotHideUnmatchedData.UseVisualStyleBackColor = true;
            this.plotHideUnmatchedData.Visible = false;
            this.plotHideUnmatchedData.CheckedChanged += new System.EventHandler(this.PlotHideUnmatchedData_CheckedChanged);
            // 
            // plotSnappingCursor
            // 
            this.plotSnappingCursor.AutoSize = true;
            this.plotSnappingCursor.Location = new System.Drawing.Point(9, 54);
            this.plotSnappingCursor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotSnappingCursor.Name = "plotSnappingCursor";
            this.plotSnappingCursor.Size = new System.Drawing.Size(174, 21);
            this.plotSnappingCursor.TabIndex = 2;
            this.plotSnappingCursor.Text = "Show Snapping Cursor";
            this.plotSnappingCursor.UseVisualStyleBackColor = true;
            this.plotSnappingCursor.CheckedChanged += new System.EventHandler(this.PlotSnappingCursor_CheckedChanged);
            // 
            // plotShowLegend
            // 
            this.plotShowLegend.AutoSize = true;
            this.plotShowLegend.Location = new System.Drawing.Point(9, 25);
            this.plotShowLegend.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.plotShowLegend.Name = "plotShowLegend";
            this.plotShowLegend.Size = new System.Drawing.Size(116, 21);
            this.plotShowLegend.TabIndex = 0;
            this.plotShowLegend.Text = "Show Legend";
            this.plotShowLegend.UseVisualStyleBackColor = true;
            this.plotShowLegend.CheckedChanged += new System.EventHandler(this.PlotShowLegend_CheckedChanged);
            // 
            // optionTabsPage
            // 
            this.optionTabsPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.optionTabsPage.Controls.Add(this.PlotOptionsView);
            this.optionTabsPage.Controls.Add(this.fragLadderTab);
            this.optionTabsPage.Controls.Add(this.MainOptionsView);
            this.optionTabsPage.Location = new System.Drawing.Point(17, 16);
            this.optionTabsPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.optionTabsPage.Name = "optionTabsPage";
            this.optionTabsPage.SelectedIndex = 0;
            this.optionTabsPage.Size = new System.Drawing.Size(552, 496);
            this.optionTabsPage.TabIndex = 0;
            // 
            // fragLadderTab
            // 
            this.fragLadderTab.Controls.Add(this.groupBox1);
            this.fragLadderTab.Location = new System.Drawing.Point(4, 25);
            this.fragLadderTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.fragLadderTab.Name = "fragLadderTab";
            this.fragLadderTab.Size = new System.Drawing.Size(544, 467);
            this.fragLadderTab.TabIndex = 2;
            this.fragLadderTab.Text = "Fragment Ladder Options";
            this.fragLadderTab.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewModList);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(271, 455);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modifications";
            // 
            // dataGridViewModList
            // 
            this.dataGridViewModList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModList.Location = new System.Drawing.Point(8, 23);
            this.dataGridViewModList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridViewModList.Name = "dataGridViewModList";
            this.dataGridViewModList.RowHeadersWidth = 51;
            this.dataGridViewModList.Size = new System.Drawing.Size(255, 425);
            this.dataGridViewModList.TabIndex = 1;
            // 
            // MainOptionsView
            // 
            this.MainOptionsView.Controls.Add(this.mainDataPropertiesGroup);
            this.MainOptionsView.Controls.Add(this.mainMatchingPropertiesGroup);
            this.MainOptionsView.Controls.Add(this.mainUserProfileGroup);
            this.MainOptionsView.Location = new System.Drawing.Point(4, 25);
            this.MainOptionsView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MainOptionsView.Name = "MainOptionsView";
            this.MainOptionsView.Size = new System.Drawing.Size(544, 467);
            this.MainOptionsView.TabIndex = 1;
            this.MainOptionsView.Text = "General Options";
            this.MainOptionsView.UseVisualStyleBackColor = true;
            // 
            // mainDataPropertiesGroup
            // 
            this.mainDataPropertiesGroup.Controls.Add(this.mainUnmatchColorChangeButton);
            this.mainDataPropertiesGroup.Controls.Add(this.mainUnmatchedColorSample);
            this.mainDataPropertiesGroup.Controls.Add(this.mainUnmatchedColorLabel);
            this.mainDataPropertiesGroup.Controls.Add(this.mainMatchColorChangeButton);
            this.mainDataPropertiesGroup.Controls.Add(this.mainMatchedColorSample);
            this.mainDataPropertiesGroup.Controls.Add(this.mainMatchedColorLabel);
            this.mainDataPropertiesGroup.Location = new System.Drawing.Point(5, 80);
            this.mainDataPropertiesGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainDataPropertiesGroup.Name = "mainDataPropertiesGroup";
            this.mainDataPropertiesGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainDataPropertiesGroup.Size = new System.Drawing.Size(532, 85);
            this.mainDataPropertiesGroup.TabIndex = 1;
            this.mainDataPropertiesGroup.TabStop = false;
            this.mainDataPropertiesGroup.Text = "Data Properties";
            // 
            // mainUnmatchColorChangeButton
            // 
            this.mainUnmatchColorChangeButton.Location = new System.Drawing.Point(172, 47);
            this.mainUnmatchColorChangeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainUnmatchColorChangeButton.Name = "mainUnmatchColorChangeButton";
            this.mainUnmatchColorChangeButton.Size = new System.Drawing.Size(115, 28);
            this.mainUnmatchColorChangeButton.TabIndex = 7;
            this.mainUnmatchColorChangeButton.Text = "Change Color";
            this.mainUnmatchColorChangeButton.UseVisualStyleBackColor = true;
            this.mainUnmatchColorChangeButton.Click += new System.EventHandler(this.MainUnmatchColorChangeButton_Click);
            // 
            // mainUnmatchedColorSample
            // 
            this.mainUnmatchedColorSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainUnmatchedColorSample.Location = new System.Drawing.Point(127, 53);
            this.mainUnmatchedColorSample.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainUnmatchedColorSample.Name = "mainUnmatchedColorSample";
            this.mainUnmatchedColorSample.Size = new System.Drawing.Size(19, 18);
            this.mainUnmatchedColorSample.TabIndex = 6;
            // 
            // mainUnmatchedColorLabel
            // 
            this.mainUnmatchedColorLabel.AutoSize = true;
            this.mainUnmatchedColorLabel.Location = new System.Drawing.Point(8, 53);
            this.mainUnmatchedColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mainUnmatchedColorLabel.Name = "mainUnmatchedColorLabel";
            this.mainUnmatchedColorLabel.Size = new System.Drawing.Size(117, 17);
            this.mainUnmatchedColorLabel.TabIndex = 11;
            this.mainUnmatchedColorLabel.Text = "Unmatched Color";
            // 
            // mainMatchColorChangeButton
            // 
            this.mainMatchColorChangeButton.Location = new System.Drawing.Point(172, 14);
            this.mainMatchColorChangeButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainMatchColorChangeButton.Name = "mainMatchColorChangeButton";
            this.mainMatchColorChangeButton.Size = new System.Drawing.Size(115, 28);
            this.mainMatchColorChangeButton.TabIndex = 5;
            this.mainMatchColorChangeButton.Text = "Change Color";
            this.mainMatchColorChangeButton.UseVisualStyleBackColor = true;
            this.mainMatchColorChangeButton.Click += new System.EventHandler(this.MainMatchColorChangeButton_Click);
            // 
            // mainMatchedColorSample
            // 
            this.mainMatchedColorSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainMatchedColorSample.Location = new System.Drawing.Point(127, 20);
            this.mainMatchedColorSample.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainMatchedColorSample.Name = "mainMatchedColorSample";
            this.mainMatchedColorSample.Size = new System.Drawing.Size(19, 18);
            this.mainMatchedColorSample.TabIndex = 4;
            // 
            // mainMatchedColorLabel
            // 
            this.mainMatchedColorLabel.AutoSize = true;
            this.mainMatchedColorLabel.Location = new System.Drawing.Point(8, 20);
            this.mainMatchedColorLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mainMatchedColorLabel.Name = "mainMatchedColorLabel";
            this.mainMatchedColorLabel.Size = new System.Drawing.Size(99, 17);
            this.mainMatchedColorLabel.TabIndex = 8;
            this.mainMatchedColorLabel.Text = "Matched Color";
            // 
            // mainMatchingPropertiesGroup
            // 
            this.mainMatchingPropertiesGroup.Controls.Add(this.lowerMatchingToleranceBox);
            this.mainMatchingPropertiesGroup.Controls.Add(this.lowerMatchingToleranceLabel);
            this.mainMatchingPropertiesGroup.Controls.Add(this.mainMatchingToleranceBox);
            this.mainMatchingPropertiesGroup.Controls.Add(this.mainMatchingToleranceLabel);
            this.mainMatchingPropertiesGroup.Location = new System.Drawing.Point(5, 172);
            this.mainMatchingPropertiesGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainMatchingPropertiesGroup.Name = "mainMatchingPropertiesGroup";
            this.mainMatchingPropertiesGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainMatchingPropertiesGroup.Size = new System.Drawing.Size(532, 80);
            this.mainMatchingPropertiesGroup.TabIndex = 2;
            this.mainMatchingPropertiesGroup.TabStop = false;
            this.mainMatchingPropertiesGroup.Text = "Matching Properties";
            // 
            // lowerMatchingToleranceBox
            // 
            this.lowerMatchingToleranceBox.Location = new System.Drawing.Point(16, 39);
            this.lowerMatchingToleranceBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lowerMatchingToleranceBox.Name = "lowerMatchingToleranceBox";
            this.lowerMatchingToleranceBox.Size = new System.Drawing.Size(132, 22);
            this.lowerMatchingToleranceBox.TabIndex = 10;
            this.lowerMatchingToleranceBox.Leave += new System.EventHandler(this.LowerMatchingToleranceBox_TextChanged);
            // 
            // lowerMatchingToleranceLabel
            // 
            this.lowerMatchingToleranceLabel.AutoSize = true;
            this.lowerMatchingToleranceLabel.Location = new System.Drawing.Point(12, 20);
            this.lowerMatchingToleranceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lowerMatchingToleranceLabel.Name = "lowerMatchingToleranceLabel";
            this.lowerMatchingToleranceLabel.Size = new System.Drawing.Size(114, 17);
            this.lowerMatchingToleranceLabel.TabIndex = 9;
            this.lowerMatchingToleranceLabel.Text = "Lower Tolerance";
            // 
            // mainMatchingToleranceBox
            // 
            this.mainMatchingToleranceBox.Location = new System.Drawing.Point(216, 39);
            this.mainMatchingToleranceBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainMatchingToleranceBox.Name = "mainMatchingToleranceBox";
            this.mainMatchingToleranceBox.Size = new System.Drawing.Size(132, 22);
            this.mainMatchingToleranceBox.TabIndex = 8;
            this.mainMatchingToleranceBox.Leave += new System.EventHandler(this.MainMatchingToleranceBox_TextChanged);
            // 
            // mainMatchingToleranceLabel
            // 
            this.mainMatchingToleranceLabel.AutoSize = true;
            this.mainMatchingToleranceLabel.Location = new System.Drawing.Point(212, 20);
            this.mainMatchingToleranceLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mainMatchingToleranceLabel.Name = "mainMatchingToleranceLabel";
            this.mainMatchingToleranceLabel.Size = new System.Drawing.Size(115, 17);
            this.mainMatchingToleranceLabel.TabIndex = 0;
            this.mainMatchingToleranceLabel.Text = "Upper Tolerance";
            // 
            // mainUserProfileGroup
            // 
            this.mainUserProfileGroup.Controls.Add(this.mainUserBrowseButton);
            this.mainUserProfileGroup.Controls.Add(this.mainProfileFileLocationBox);
            this.mainUserProfileGroup.Location = new System.Drawing.Point(5, 5);
            this.mainUserProfileGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainUserProfileGroup.Name = "mainUserProfileGroup";
            this.mainUserProfileGroup.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainUserProfileGroup.Size = new System.Drawing.Size(532, 68);
            this.mainUserProfileGroup.TabIndex = 0;
            this.mainUserProfileGroup.TabStop = false;
            this.mainUserProfileGroup.Text = "User Profile";
            // 
            // mainUserBrowseButton
            // 
            this.mainUserBrowseButton.Location = new System.Drawing.Point(424, 22);
            this.mainUserBrowseButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainUserBrowseButton.Name = "mainUserBrowseButton";
            this.mainUserBrowseButton.Size = new System.Drawing.Size(100, 28);
            this.mainUserBrowseButton.TabIndex = 2;
            this.mainUserBrowseButton.Text = "Browse";
            this.mainUserBrowseButton.UseVisualStyleBackColor = true;
            this.mainUserBrowseButton.Click += new System.EventHandler(this.mainUserBrowseButton_Click);
            // 
            // mainProfileFileLocationBox
            // 
            this.mainProfileFileLocationBox.Location = new System.Drawing.Point(9, 25);
            this.mainProfileFileLocationBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mainProfileFileLocationBox.Name = "mainProfileFileLocationBox";
            this.mainProfileFileLocationBox.Size = new System.Drawing.Size(405, 22);
            this.mainProfileFileLocationBox.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "( *.spuf ) | .spuf";
            this.openFileDialog.FileName = "User Profile";
            this.openFileDialog.Filter = "SpectrumLook User File|*.spuf";
            this.openFileDialog.InitialDirectory = "c:\\";
            this.openFileDialog.Title = "Choose the User Profile";
            // 
            // OptionsViewController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 562);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.defaultButton);
            this.Controls.Add(this.optionTabsPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "OptionsViewController";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsViewController_FormClosing);
            this.Load += new System.EventHandler(this.OptionsViewController_Load);
            this.PlotOptionsView.ResumeLayout(false);
            this.mainWindowOptionsGroup.ResumeLayout(false);
            this.mainWindowOptionsGroup.PerformLayout();
            this.multiplePlotGroup.ResumeLayout(false);
            this.multiplePlotGroup.PerformLayout();
            this.plotAnnotationOptionsGroup.ResumeLayout(false);
            this.plotAnnotationOptionsGroup.PerformLayout();
            this.plotZoomOptionsGroup.ResumeLayout(false);
            this.plotZoomOptionsGroup.PerformLayout();
            this.plotGeneralOptionsGroup.ResumeLayout(false);
            this.plotGeneralOptionsGroup.PerformLayout();
            this.optionTabsPage.ResumeLayout(false);
            this.fragLadderTab.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModList)).EndInit();
            this.MainOptionsView.ResumeLayout(false);
            this.mainDataPropertiesGroup.ResumeLayout(false);
            this.mainDataPropertiesGroup.PerformLayout();
            this.mainMatchingPropertiesGroup.ResumeLayout(false);
            this.mainMatchingPropertiesGroup.PerformLayout();
            this.mainUserProfileGroup.ResumeLayout(false);
            this.mainUserProfileGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button defaultButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TabPage PlotOptionsView;
        private System.Windows.Forms.GroupBox plotAnnotationOptionsGroup;
        private System.Windows.Forms.Button plotChangeColorOpenButton;
        private System.Windows.Forms.Panel plotAnnotationColor;
        private System.Windows.Forms.Label plotAnnotationColorLabel;
        private System.Windows.Forms.TextBox plotTextSize;
        private System.Windows.Forms.Label plotAnnotationTextSizeLabel;
        private System.Windows.Forms.Label plotAnnotationPercentLabel2;
        private System.Windows.Forms.TextBox plotAnnotationPercentBox;
        private System.Windows.Forms.Label plotAnnotationPercentLabel1;
        private System.Windows.Forms.GroupBox plotZoomOptionsGroup;
        private System.Windows.Forms.TextBox plotFragLadderSelectBox;
        private System.Windows.Forms.Label plotFragLadderSelectLabel;
        private System.Windows.Forms.Label plotUnzoomKeyLabel;
        private System.Windows.Forms.ComboBox plotUnzoomKeyComboBox;
        private System.Windows.Forms.RadioButton plotBoxZoom;
        private System.Windows.Forms.RadioButton plotHorizontalZoom;
        private System.Windows.Forms.GroupBox plotGeneralOptionsGroup;
        private System.Windows.Forms.CheckBox plotHideUnmatchedData;
        private System.Windows.Forms.CheckBox plotSnappingCursor;
        private System.Windows.Forms.CheckBox plotShowLegend;
        private System.Windows.Forms.TabControl optionTabsPage;
        private System.Windows.Forms.TabPage MainOptionsView;
        private System.Windows.Forms.GroupBox mainMatchingPropertiesGroup;
        private System.Windows.Forms.GroupBox mainUserProfileGroup;
        private System.Windows.Forms.GroupBox mainDataPropertiesGroup;
        private System.Windows.Forms.Button mainUnmatchColorChangeButton;
        private System.Windows.Forms.Panel mainUnmatchedColorSample;
        private System.Windows.Forms.Label mainUnmatchedColorLabel;
        private System.Windows.Forms.Button mainMatchColorChangeButton;
        private System.Windows.Forms.Panel mainMatchedColorSample;
        private System.Windows.Forms.Label mainMatchedColorLabel;
        private System.Windows.Forms.TextBox mainMatchingToleranceBox;
        private System.Windows.Forms.Label mainMatchingToleranceLabel;
        private System.Windows.Forms.Button mainUserBrowseButton;
        private System.Windows.Forms.TextBox mainProfileFileLocationBox;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.CheckBox plotRightClickUnzoom;
        private System.Windows.Forms.CheckBox plotHidePlotTools;
        private System.Windows.Forms.GroupBox multiplePlotGroup;
        private System.Windows.Forms.Label numberOfPlotsLabel;
        private System.Windows.Forms.TextBox plotNumberOfPlotsTextBox;
        private System.Windows.Forms.TabPage fragLadderTab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox mainWindowOptionsGroup;
        private System.Windows.Forms.CheckBox mainDetachPlotCheckBox;
        private System.Windows.Forms.TextBox lowerMatchingToleranceBox;
        private System.Windows.Forms.Label lowerMatchingToleranceLabel;
        private System.Windows.Forms.DataGridView dataGridViewModList;
    }
}