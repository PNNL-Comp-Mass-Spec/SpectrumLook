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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.defaultButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.PlotOptionsView = new System.Windows.Forms.TabPage();
            this.multiplePlotGroup = new System.Windows.Forms.GroupBox();
            this.numberOfPlotsLabel = new System.Windows.Forms.Label();
            this.plotNumberOfPlotsTextBox = new System.Windows.Forms.TextBox();
            this.plotAnnotationOptionsGroup = new System.Windows.Forms.GroupBox();
            this.plotChangeColorOpenButton = new System.Windows.Forms.Button();
            this.plotAnnotationColor = new System.Windows.Forms.Panel();
            this.plotAnnotatioColorLabel = new System.Windows.Forms.Label();
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
            this.fragModRemoveButton = new System.Windows.Forms.Button();
            this.fragModEditButton = new System.Windows.Forms.Button();
            this.fragModAddButton = new System.Windows.Forms.Button();
            this.fragModListBox = new System.Windows.Forms.ListBox();
            this.MainOptionsView = new System.Windows.Forms.TabPage();
            this.mainDataPropertiesGroup = new System.Windows.Forms.GroupBox();
            this.mainUnmatchColorChangeButton = new System.Windows.Forms.Button();
            this.mainUnmatchedColorSample = new System.Windows.Forms.Panel();
            this.mainUnmatchedColorLabel = new System.Windows.Forms.Label();
            this.mainMatchColorChangeButton = new System.Windows.Forms.Button();
            this.mainMatchedColorSample = new System.Windows.Forms.Panel();
            this.mainMatchedColorLabel = new System.Windows.Forms.Label();
            this.mainMatchingPropertiesGroup = new System.Windows.Forms.GroupBox();
            this.mainMatchingToleranceBox = new System.Windows.Forms.TextBox();
            this.mainMatchingToleranceLabel = new System.Windows.Forms.Label();
            this.mainWindowOptionsGroup = new System.Windows.Forms.GroupBox();
            this.mainDetachPlotCheckBox = new System.Windows.Forms.CheckBox();
            this.mainUserProfileGroup = new System.Windows.Forms.GroupBox();
            this.mainUserBrowseButton = new System.Windows.Forms.Button();
            this.mainProfileFileLocationBox = new System.Windows.Forms.TextBox();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.PlotOptionsView.SuspendLayout();
            this.multiplePlotGroup.SuspendLayout();
            this.plotAnnotationOptionsGroup.SuspendLayout();
            this.plotZoomOptionsGroup.SuspendLayout();
            this.plotGeneralOptionsGroup.SuspendLayout();
            this.optionTabsPage.SuspendLayout();
            this.fragLadderTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.MainOptionsView.SuspendLayout();
            this.mainDataPropertiesGroup.SuspendLayout();
            this.mainMatchingPropertiesGroup.SuspendLayout();
            this.mainWindowOptionsGroup.SuspendLayout();
            this.mainUserProfileGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultButton
            // 
            this.defaultButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.defaultButton.Location = new System.Drawing.Point(13, 422);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(75, 23);
            this.defaultButton.TabIndex = 1;
            this.defaultButton.Text = "Default";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Location = new System.Drawing.Point(352, 422);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 2;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(271, 422);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // PlotOptionsView
            // 
            this.PlotOptionsView.Controls.Add(this.multiplePlotGroup);
            this.PlotOptionsView.Controls.Add(this.plotAnnotationOptionsGroup);
            this.PlotOptionsView.Controls.Add(this.plotZoomOptionsGroup);
            this.PlotOptionsView.Controls.Add(this.plotGeneralOptionsGroup);
            this.PlotOptionsView.Location = new System.Drawing.Point(4, 22);
            this.PlotOptionsView.Name = "PlotOptionsView";
            this.PlotOptionsView.Padding = new System.Windows.Forms.Padding(3);
            this.PlotOptionsView.Size = new System.Drawing.Size(406, 377);
            this.PlotOptionsView.TabIndex = 0;
            this.PlotOptionsView.Text = "Plot Options";
            this.PlotOptionsView.UseVisualStyleBackColor = true;
            // 
            // multiplePlotGroup
            // 
            this.multiplePlotGroup.Controls.Add(this.numberOfPlotsLabel);
            this.multiplePlotGroup.Controls.Add(this.plotNumberOfPlotsTextBox);
            this.multiplePlotGroup.Location = new System.Drawing.Point(7, 298);
            this.multiplePlotGroup.Name = "multiplePlotGroup";
            this.multiplePlotGroup.Size = new System.Drawing.Size(393, 64);
            this.multiplePlotGroup.TabIndex = 4;
            this.multiplePlotGroup.TabStop = false;
            this.multiplePlotGroup.Text = "Multiple Plot Options";
            // 
            // numberOfPlotsLabel
            // 
            this.numberOfPlotsLabel.AutoSize = true;
            this.numberOfPlotsLabel.Location = new System.Drawing.Point(7, 30);
            this.numberOfPlotsLabel.Name = "numberOfPlotsLabel";
            this.numberOfPlotsLabel.Size = new System.Drawing.Size(85, 13);
            this.numberOfPlotsLabel.TabIndex = 1;
            this.numberOfPlotsLabel.Text = "Number of Plots:";
            // 
            // plotNumberOfPlotsTextBox
            // 
            this.plotNumberOfPlotsTextBox.Location = new System.Drawing.Point(95, 27);
            this.plotNumberOfPlotsTextBox.Name = "plotNumberOfPlotsTextBox";
            this.plotNumberOfPlotsTextBox.Size = new System.Drawing.Size(43, 20);
            this.plotNumberOfPlotsTextBox.TabIndex = 0;
            this.plotNumberOfPlotsTextBox.Leave += new System.EventHandler(this.numberOfPlotsTextBox_Leave);
            // 
            // plotAnnotationOptionsGroup
            // 
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotChangeColorOpenButton);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationColor);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotatioColorLabel);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotTextSize);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationTextSizeLabel);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationPercentLabel2);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationPercentBox);
            this.plotAnnotationOptionsGroup.Controls.Add(this.plotAnnotationPercentLabel1);
            this.plotAnnotationOptionsGroup.Location = new System.Drawing.Point(7, 190);
            this.plotAnnotationOptionsGroup.Name = "plotAnnotationOptionsGroup";
            this.plotAnnotationOptionsGroup.Size = new System.Drawing.Size(393, 102);
            this.plotAnnotationOptionsGroup.TabIndex = 2;
            this.plotAnnotationOptionsGroup.TabStop = false;
            this.plotAnnotationOptionsGroup.Text = "Annotation Options";
            // 
            // plotChangeColorOpenButton
            // 
            this.plotChangeColorOpenButton.Location = new System.Drawing.Point(98, 65);
            this.plotChangeColorOpenButton.Name = "plotChangeColorOpenButton";
            this.plotChangeColorOpenButton.Size = new System.Drawing.Size(86, 23);
            this.plotChangeColorOpenButton.TabIndex = 7;
            this.plotChangeColorOpenButton.Text = "Change Color";
            this.plotChangeColorOpenButton.UseVisualStyleBackColor = true;
            this.plotChangeColorOpenButton.Click += new System.EventHandler(this.plotChangeColorOpenButton_Click);
            // 
            // plotAnnotationColor
            // 
            this.plotAnnotationColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.plotAnnotationColor.Location = new System.Drawing.Point(68, 70);
            this.plotAnnotationColor.Name = "plotAnnotationColor";
            this.plotAnnotationColor.Size = new System.Drawing.Size(15, 15);
            this.plotAnnotationColor.TabIndex = 6;
            // 
            // plotAnnotatioColorLabel
            // 
            this.plotAnnotatioColorLabel.AutoSize = true;
            this.plotAnnotatioColorLabel.Location = new System.Drawing.Point(14, 70);
            this.plotAnnotatioColorLabel.Name = "plotAnnotatioColorLabel";
            this.plotAnnotatioColorLabel.Size = new System.Drawing.Size(31, 13);
            this.plotAnnotatioColorLabel.TabIndex = 5;
            this.plotAnnotatioColorLabel.Text = "Color";
            // 
            // plotTextSize
            // 
            this.plotTextSize.Location = new System.Drawing.Point(68, 41);
            this.plotTextSize.Name = "plotTextSize";
            this.plotTextSize.Size = new System.Drawing.Size(31, 20);
            this.plotTextSize.TabIndex = 4;
            this.plotTextSize.Leave += new System.EventHandler(this.plotTextSize_TextChanged);
            // 
            // plotAnnotationTextSizeLabel
            // 
            this.plotAnnotationTextSizeLabel.AutoSize = true;
            this.plotAnnotationTextSizeLabel.Location = new System.Drawing.Point(11, 44);
            this.plotAnnotationTextSizeLabel.Name = "plotAnnotationTextSizeLabel";
            this.plotAnnotationTextSizeLabel.Size = new System.Drawing.Size(51, 13);
            this.plotAnnotationTextSizeLabel.TabIndex = 3;
            this.plotAnnotationTextSizeLabel.Text = "Text Size";
            // 
            // plotAnnotationPercentLabel2
            // 
            this.plotAnnotationPercentLabel2.AutoSize = true;
            this.plotAnnotationPercentLabel2.Location = new System.Drawing.Point(215, 20);
            this.plotAnnotationPercentLabel2.Name = "plotAnnotationPercentLabel2";
            this.plotAnnotationPercentLabel2.Size = new System.Drawing.Size(106, 13);
            this.plotAnnotationPercentLabel2.TabIndex = 2;
            this.plotAnnotationPercentLabel2.Text = "% of matched peaks.";
            // 
            // plotAnnotationPercentBox
            // 
            this.plotAnnotationPercentBox.Location = new System.Drawing.Point(179, 17);
            this.plotAnnotationPercentBox.Name = "plotAnnotationPercentBox";
            this.plotAnnotationPercentBox.Size = new System.Drawing.Size(29, 20);
            this.plotAnnotationPercentBox.TabIndex = 1;
            this.plotAnnotationPercentBox.Leave += new System.EventHandler(this.plotAnnotationPercentBox_TextChanged);
            // 
            // plotAnnotationPercentLabel1
            // 
            this.plotAnnotationPercentLabel1.AutoSize = true;
            this.plotAnnotationPercentLabel1.Location = new System.Drawing.Point(11, 20);
            this.plotAnnotationPercentLabel1.Name = "plotAnnotationPercentLabel1";
            this.plotAnnotationPercentLabel1.Size = new System.Drawing.Size(162, 13);
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
            this.plotZoomOptionsGroup.Location = new System.Drawing.Point(7, 83);
            this.plotZoomOptionsGroup.Name = "plotZoomOptionsGroup";
            this.plotZoomOptionsGroup.Size = new System.Drawing.Size(393, 101);
            this.plotZoomOptionsGroup.TabIndex = 1;
            this.plotZoomOptionsGroup.TabStop = false;
            this.plotZoomOptionsGroup.Text = "Zoom Options";
            // 
            // plotRightClickUnzoom
            // 
            this.plotRightClickUnzoom.AutoSize = true;
            this.plotRightClickUnzoom.Location = new System.Drawing.Point(6, 43);
            this.plotRightClickUnzoom.Name = "plotRightClickUnzoom";
            this.plotRightClickUnzoom.Size = new System.Drawing.Size(169, 17);
            this.plotRightClickUnzoom.TabIndex = 6;
            this.plotRightClickUnzoom.Text = "Enable Right Click Unzooming";
            this.plotRightClickUnzoom.UseVisualStyleBackColor = true;
            this.plotRightClickUnzoom.CheckedChanged += new System.EventHandler(this.checkBoxRightClickUnzoom_CheckedChanged);
            // 
            // plotFragLadderSelectBox
            // 
            this.plotFragLadderSelectBox.Location = new System.Drawing.Point(196, 69);
            this.plotFragLadderSelectBox.Name = "plotFragLadderSelectBox";
            this.plotFragLadderSelectBox.Size = new System.Drawing.Size(100, 20);
            this.plotFragLadderSelectBox.TabIndex = 5;
            this.plotFragLadderSelectBox.Leave += new System.EventHandler(this.plotFragLadderSelectBox_TextChanged);
            // 
            // plotFragLadderSelectLabel
            // 
            this.plotFragLadderSelectLabel.AutoSize = true;
            this.plotFragLadderSelectLabel.Location = new System.Drawing.Point(6, 72);
            this.plotFragLadderSelectLabel.Name = "plotFragLadderSelectLabel";
            this.plotFragLadderSelectLabel.Size = new System.Drawing.Size(191, 13);
            this.plotFragLadderSelectLabel.TabIndex = 4;
            this.plotFragLadderSelectLabel.Text = "Fragment Ladder auto zoom select size";
            // 
            // plotUnzoomKeyLabel
            // 
            this.plotUnzoomKeyLabel.AutoSize = true;
            this.plotUnzoomKeyLabel.Location = new System.Drawing.Point(193, 44);
            this.plotUnzoomKeyLabel.Name = "plotUnzoomKeyLabel";
            this.plotUnzoomKeyLabel.Size = new System.Drawing.Size(67, 13);
            this.plotUnzoomKeyLabel.TabIndex = 3;
            this.plotUnzoomKeyLabel.Text = "Unzoom Key";
            // 
            // plotUnzoomKeyComboBox
            // 
            this.plotUnzoomKeyComboBox.FormattingEnabled = true;
            this.plotUnzoomKeyComboBox.Location = new System.Drawing.Point(266, 41);
            this.plotUnzoomKeyComboBox.Name = "plotUnzoomKeyComboBox";
            this.plotUnzoomKeyComboBox.Size = new System.Drawing.Size(121, 21);
            this.plotUnzoomKeyComboBox.TabIndex = 2;
            this.plotUnzoomKeyComboBox.Leave += new System.EventHandler(this.plotUnzoomKeyComboBox_Leave);
            // 
            // plotBoxZoom
            // 
            this.plotBoxZoom.AutoSize = true;
            this.plotBoxZoom.Location = new System.Drawing.Point(115, 20);
            this.plotBoxZoom.Name = "plotBoxZoom";
            this.plotBoxZoom.Size = new System.Drawing.Size(73, 17);
            this.plotBoxZoom.TabIndex = 1;
            this.plotBoxZoom.TabStop = true;
            this.plotBoxZoom.Text = "Box Zoom";
            this.plotBoxZoom.UseVisualStyleBackColor = true;
            this.plotBoxZoom.CheckedChanged += new System.EventHandler(this.plotBoxZoom_CheckedChanged);
            // 
            // plotHorizontalZoom
            // 
            this.plotHorizontalZoom.AutoSize = true;
            this.plotHorizontalZoom.Location = new System.Drawing.Point(7, 20);
            this.plotHorizontalZoom.Name = "plotHorizontalZoom";
            this.plotHorizontalZoom.Size = new System.Drawing.Size(102, 17);
            this.plotHorizontalZoom.TabIndex = 0;
            this.plotHorizontalZoom.TabStop = true;
            this.plotHorizontalZoom.Text = "Horizontal Zoom";
            this.plotHorizontalZoom.UseVisualStyleBackColor = true;
            this.plotHorizontalZoom.CheckedChanged += new System.EventHandler(this.plotHorizontalZoom_CheckedChanged);
            // 
            // plotGeneralOptionsGroup
            // 
            this.plotGeneralOptionsGroup.Controls.Add(this.plotHidePlotTools);
            this.plotGeneralOptionsGroup.Controls.Add(this.plotHideUnmatchedData);
            this.plotGeneralOptionsGroup.Controls.Add(this.plotSnappingCursor);
            this.plotGeneralOptionsGroup.Controls.Add(this.plotShowLegend);
            this.plotGeneralOptionsGroup.Location = new System.Drawing.Point(7, 7);
            this.plotGeneralOptionsGroup.Name = "plotGeneralOptionsGroup";
            this.plotGeneralOptionsGroup.Size = new System.Drawing.Size(393, 70);
            this.plotGeneralOptionsGroup.TabIndex = 0;
            this.plotGeneralOptionsGroup.TabStop = false;
            this.plotGeneralOptionsGroup.Text = "General Options";
            // 
            // plotHidePlotTools
            // 
            this.plotHidePlotTools.AutoSize = true;
            this.plotHidePlotTools.Location = new System.Drawing.Point(153, 44);
            this.plotHidePlotTools.Name = "plotHidePlotTools";
            this.plotHidePlotTools.Size = new System.Drawing.Size(123, 17);
            this.plotHidePlotTools.TabIndex = 3;
            this.plotHidePlotTools.Text = "Hide Plot Tools Area";
            this.plotHidePlotTools.UseVisualStyleBackColor = true;
            this.plotHidePlotTools.CheckedChanged += new System.EventHandler(this.checkBoxHidePlotTools_CheckedChanged);
            // 
            // plotHideUnmatchedData
            // 
            this.plotHideUnmatchedData.AutoSize = true;
            this.plotHideUnmatchedData.Location = new System.Drawing.Point(153, 20);
            this.plotHideUnmatchedData.Name = "plotHideUnmatchedData";
            this.plotHideUnmatchedData.Size = new System.Drawing.Size(164, 17);
            this.plotHideUnmatchedData.TabIndex = 2;
            this.plotHideUnmatchedData.Text = "Hide Unmatched Data in Plot";
            this.plotHideUnmatchedData.UseVisualStyleBackColor = true;
            this.plotHideUnmatchedData.CheckedChanged += new System.EventHandler(this.plotHideUnmatchedData_CheckedChanged);
            // 
            // plotSnappingCursor
            // 
            this.plotSnappingCursor.AutoSize = true;
            this.plotSnappingCursor.Location = new System.Drawing.Point(7, 44);
            this.plotSnappingCursor.Name = "plotSnappingCursor";
            this.plotSnappingCursor.Size = new System.Drawing.Size(134, 17);
            this.plotSnappingCursor.TabIndex = 1;
            this.plotSnappingCursor.Text = "Show Snapping Cursor";
            this.plotSnappingCursor.UseVisualStyleBackColor = true;
            this.plotSnappingCursor.CheckedChanged += new System.EventHandler(this.plotSnappingCursor_CheckedChanged);
            // 
            // plotShowLegend
            // 
            this.plotShowLegend.AutoSize = true;
            this.plotShowLegend.Location = new System.Drawing.Point(7, 20);
            this.plotShowLegend.Name = "plotShowLegend";
            this.plotShowLegend.Size = new System.Drawing.Size(92, 17);
            this.plotShowLegend.TabIndex = 0;
            this.plotShowLegend.Text = "Show Legend";
            this.plotShowLegend.UseVisualStyleBackColor = true;
            this.plotShowLegend.CheckedChanged += new System.EventHandler(this.plotShowLegend_CheckedChanged);
            // 
            // optionTabsPage
            // 
            this.optionTabsPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.optionTabsPage.Controls.Add(this.PlotOptionsView);
            this.optionTabsPage.Controls.Add(this.fragLadderTab);
            this.optionTabsPage.Controls.Add(this.MainOptionsView);
            this.optionTabsPage.Location = new System.Drawing.Point(13, 13);
            this.optionTabsPage.Name = "optionTabsPage";
            this.optionTabsPage.SelectedIndex = 0;
            this.optionTabsPage.Size = new System.Drawing.Size(414, 403);
            this.optionTabsPage.TabIndex = 0;
            // 
            // fragLadderTab
            // 
            this.fragLadderTab.Controls.Add(this.groupBox1);
            this.fragLadderTab.Location = new System.Drawing.Point(4, 22);
            this.fragLadderTab.Name = "fragLadderTab";
            this.fragLadderTab.Size = new System.Drawing.Size(406, 377);
            this.fragLadderTab.TabIndex = 2;
            this.fragLadderTab.Text = "Fragment Ladder Options";
            this.fragLadderTab.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fragModRemoveButton);
            this.groupBox1.Controls.Add(this.fragModEditButton);
            this.groupBox1.Controls.Add(this.fragModAddButton);
            this.groupBox1.Controls.Add(this.fragModListBox);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(203, 370);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Modifications";
            // 
            // fragModRemoveButton
            // 
            this.fragModRemoveButton.Location = new System.Drawing.Point(7, 341);
            this.fragModRemoveButton.Name = "fragModRemoveButton";
            this.fragModRemoveButton.Size = new System.Drawing.Size(60, 23);
            this.fragModRemoveButton.TabIndex = 3;
            this.fragModRemoveButton.Text = "Remove";
            this.fragModRemoveButton.UseVisualStyleBackColor = true;
            this.fragModRemoveButton.Click += new System.EventHandler(this.fragModRemoveButton_Click);
            // 
            // fragModEditButton
            // 
            this.fragModEditButton.Location = new System.Drawing.Point(71, 341);
            this.fragModEditButton.Name = "fragModEditButton";
            this.fragModEditButton.Size = new System.Drawing.Size(60, 23);
            this.fragModEditButton.TabIndex = 2;
            this.fragModEditButton.Text = "Edit";
            this.fragModEditButton.UseVisualStyleBackColor = true;
            this.fragModEditButton.Click += new System.EventHandler(this.fragModEditButton_Click);
            // 
            // fragModAddButton
            // 
            this.fragModAddButton.Location = new System.Drawing.Point(137, 341);
            this.fragModAddButton.Name = "fragModAddButton";
            this.fragModAddButton.Size = new System.Drawing.Size(60, 23);
            this.fragModAddButton.TabIndex = 1;
            this.fragModAddButton.Text = "Add";
            this.fragModAddButton.UseVisualStyleBackColor = true;
            this.fragModAddButton.Click += new System.EventHandler(this.fragModAddButton_Click);
            // 
            // fragModListBox
            // 
            this.fragModListBox.FormattingEnabled = true;
            this.fragModListBox.Location = new System.Drawing.Point(7, 20);
            this.fragModListBox.Name = "fragModListBox";
            this.fragModListBox.Size = new System.Drawing.Size(190, 316);
            this.fragModListBox.TabIndex = 0;
            // 
            // MainOptionsView
            // 
            this.MainOptionsView.Controls.Add(this.mainDataPropertiesGroup);
            this.MainOptionsView.Controls.Add(this.mainMatchingPropertiesGroup);
            this.MainOptionsView.Controls.Add(this.mainWindowOptionsGroup);
            this.MainOptionsView.Controls.Add(this.mainUserProfileGroup);
            this.MainOptionsView.Location = new System.Drawing.Point(4, 22);
            this.MainOptionsView.Name = "MainOptionsView";
            this.MainOptionsView.Size = new System.Drawing.Size(406, 377);
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
            this.mainDataPropertiesGroup.Location = new System.Drawing.Point(4, 124);
            this.mainDataPropertiesGroup.Name = "mainDataPropertiesGroup";
            this.mainDataPropertiesGroup.Size = new System.Drawing.Size(399, 69);
            this.mainDataPropertiesGroup.TabIndex = 2;
            this.mainDataPropertiesGroup.TabStop = false;
            this.mainDataPropertiesGroup.Text = "Data Properties";
            // 
            // mainUnmatchColorChangeButton
            // 
            this.mainUnmatchColorChangeButton.Location = new System.Drawing.Point(129, 38);
            this.mainUnmatchColorChangeButton.Name = "mainUnmatchColorChangeButton";
            this.mainUnmatchColorChangeButton.Size = new System.Drawing.Size(86, 23);
            this.mainUnmatchColorChangeButton.TabIndex = 13;
            this.mainUnmatchColorChangeButton.Text = "Change Color";
            this.mainUnmatchColorChangeButton.UseVisualStyleBackColor = true;
            this.mainUnmatchColorChangeButton.Click += new System.EventHandler(this.mainUnmatchColorChangeButton_Click);
            // 
            // mainUnmatchedColorSample
            // 
            this.mainUnmatchedColorSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainUnmatchedColorSample.Location = new System.Drawing.Point(95, 43);
            this.mainUnmatchedColorSample.Name = "mainUnmatchedColorSample";
            this.mainUnmatchedColorSample.Size = new System.Drawing.Size(15, 15);
            this.mainUnmatchedColorSample.TabIndex = 12;
            // 
            // mainUnmatchedColorLabel
            // 
            this.mainUnmatchedColorLabel.AutoSize = true;
            this.mainUnmatchedColorLabel.Location = new System.Drawing.Point(6, 43);
            this.mainUnmatchedColorLabel.Name = "mainUnmatchedColorLabel";
            this.mainUnmatchedColorLabel.Size = new System.Drawing.Size(89, 13);
            this.mainUnmatchedColorLabel.TabIndex = 11;
            this.mainUnmatchedColorLabel.Text = "Unmatched Color";
            // 
            // mainMatchColorChangeButton
            // 
            this.mainMatchColorChangeButton.Location = new System.Drawing.Point(129, 11);
            this.mainMatchColorChangeButton.Name = "mainMatchColorChangeButton";
            this.mainMatchColorChangeButton.Size = new System.Drawing.Size(86, 23);
            this.mainMatchColorChangeButton.TabIndex = 10;
            this.mainMatchColorChangeButton.Text = "Change Color";
            this.mainMatchColorChangeButton.UseVisualStyleBackColor = true;
            this.mainMatchColorChangeButton.Click += new System.EventHandler(this.mainMatchColorChangeButton_Click);
            // 
            // mainMatchedColorSample
            // 
            this.mainMatchedColorSample.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainMatchedColorSample.Location = new System.Drawing.Point(95, 16);
            this.mainMatchedColorSample.Name = "mainMatchedColorSample";
            this.mainMatchedColorSample.Size = new System.Drawing.Size(15, 15);
            this.mainMatchedColorSample.TabIndex = 9;
            // 
            // mainMatchedColorLabel
            // 
            this.mainMatchedColorLabel.AutoSize = true;
            this.mainMatchedColorLabel.Location = new System.Drawing.Point(6, 16);
            this.mainMatchedColorLabel.Name = "mainMatchedColorLabel";
            this.mainMatchedColorLabel.Size = new System.Drawing.Size(76, 13);
            this.mainMatchedColorLabel.TabIndex = 8;
            this.mainMatchedColorLabel.Text = "Matched Color";
            // 
            // mainMatchingPropertiesGroup
            // 
            this.mainMatchingPropertiesGroup.Controls.Add(this.mainMatchingToleranceBox);
            this.mainMatchingPropertiesGroup.Controls.Add(this.mainMatchingToleranceLabel);
            this.mainMatchingPropertiesGroup.Location = new System.Drawing.Point(4, 199);
            this.mainMatchingPropertiesGroup.Name = "mainMatchingPropertiesGroup";
            this.mainMatchingPropertiesGroup.Size = new System.Drawing.Size(399, 65);
            this.mainMatchingPropertiesGroup.TabIndex = 0;
            this.mainMatchingPropertiesGroup.TabStop = false;
            this.mainMatchingPropertiesGroup.Text = "Matching Properties";
            // 
            // mainMatchingToleranceBox
            // 
            this.mainMatchingToleranceBox.Location = new System.Drawing.Point(10, 37);
            this.mainMatchingToleranceBox.Name = "mainMatchingToleranceBox";
            this.mainMatchingToleranceBox.Size = new System.Drawing.Size(100, 20);
            this.mainMatchingToleranceBox.TabIndex = 1;
            this.mainMatchingToleranceBox.Leave += new System.EventHandler(this.mainMatchingToleranceBox_TextChanged);
            // 
            // mainMatchingToleranceLabel
            // 
            this.mainMatchingToleranceLabel.AutoSize = true;
            this.mainMatchingToleranceLabel.Location = new System.Drawing.Point(7, 20);
            this.mainMatchingToleranceLabel.Name = "mainMatchingToleranceLabel";
            this.mainMatchingToleranceLabel.Size = new System.Drawing.Size(102, 13);
            this.mainMatchingToleranceLabel.TabIndex = 0;
            this.mainMatchingToleranceLabel.Text = "Matching Tolerance";
            // 
            // mainWindowOptionsGroup
            // 
            this.mainWindowOptionsGroup.Controls.Add(this.mainDetachPlotCheckBox);
            this.mainWindowOptionsGroup.Location = new System.Drawing.Point(4, 65);
            this.mainWindowOptionsGroup.Name = "mainWindowOptionsGroup";
            this.mainWindowOptionsGroup.Size = new System.Drawing.Size(399, 52);
            this.mainWindowOptionsGroup.TabIndex = 1;
            this.mainWindowOptionsGroup.TabStop = false;
            this.mainWindowOptionsGroup.Text = "Window Options";
            // 
            // mainDetachPlotCheckBox
            // 
            this.mainDetachPlotCheckBox.AutoSize = true;
            this.mainDetachPlotCheckBox.Location = new System.Drawing.Point(7, 20);
            this.mainDetachPlotCheckBox.Name = "mainDetachPlotCheckBox";
            this.mainDetachPlotCheckBox.Size = new System.Drawing.Size(82, 17);
            this.mainDetachPlotCheckBox.TabIndex = 0;
            this.mainDetachPlotCheckBox.Text = "Detach Plot";
            this.mainDetachPlotCheckBox.UseVisualStyleBackColor = true;
            this.mainDetachPlotCheckBox.CheckedChanged += new System.EventHandler(this.mainDetachPlotCheckBox_CheckedChanged);
            // 
            // mainUserProfileGroup
            // 
            this.mainUserProfileGroup.Controls.Add(this.mainUserBrowseButton);
            this.mainUserProfileGroup.Controls.Add(this.mainProfileFileLocationBox);
            this.mainUserProfileGroup.Location = new System.Drawing.Point(4, 4);
            this.mainUserProfileGroup.Name = "mainUserProfileGroup";
            this.mainUserProfileGroup.Size = new System.Drawing.Size(399, 55);
            this.mainUserProfileGroup.TabIndex = 0;
            this.mainUserProfileGroup.TabStop = false;
            this.mainUserProfileGroup.Text = "User Profile";
            // 
            // mainUserBrowseButton
            // 
            this.mainUserBrowseButton.Location = new System.Drawing.Point(318, 18);
            this.mainUserBrowseButton.Name = "mainUserBrowseButton";
            this.mainUserBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.mainUserBrowseButton.TabIndex = 1;
            this.mainUserBrowseButton.Text = "Browse";
            this.mainUserBrowseButton.UseVisualStyleBackColor = true;
            this.mainUserBrowseButton.Click += new System.EventHandler(this.mainUserBrowseButton_Click);
            // 
            // mainProfileFileLocationBox
            // 
            this.mainProfileFileLocationBox.Location = new System.Drawing.Point(7, 20);
            this.mainProfileFileLocationBox.Name = "mainProfileFileLocationBox";
            this.mainProfileFileLocationBox.Size = new System.Drawing.Size(305, 20);
            this.mainProfileFileLocationBox.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "( *.spuf ) | .spuf";
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // OptionsViewController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 457);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.defaultButton);
            this.Controls.Add(this.optionTabsPage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "OptionsViewController";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "GlobalOptions";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsViewController_FormClosing);
            this.PlotOptionsView.ResumeLayout(false);
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
            this.MainOptionsView.ResumeLayout(false);
            this.mainDataPropertiesGroup.ResumeLayout(false);
            this.mainDataPropertiesGroup.PerformLayout();
            this.mainMatchingPropertiesGroup.ResumeLayout(false);
            this.mainMatchingPropertiesGroup.PerformLayout();
            this.mainWindowOptionsGroup.ResumeLayout(false);
            this.mainWindowOptionsGroup.PerformLayout();
            this.mainUserProfileGroup.ResumeLayout(false);
            this.mainUserProfileGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button defaultButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TabPage PlotOptionsView;
        private System.Windows.Forms.GroupBox plotAnnotationOptionsGroup;
        private System.Windows.Forms.Button plotChangeColorOpenButton;
        private System.Windows.Forms.Panel plotAnnotationColor;
        private System.Windows.Forms.Label plotAnnotatioColorLabel;
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
        private System.Windows.Forms.GroupBox mainWindowOptionsGroup;
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
        private System.Windows.Forms.CheckBox mainDetachPlotCheckBox;
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
        private System.Windows.Forms.Button fragModRemoveButton;
        private System.Windows.Forms.Button fragModEditButton;
        private System.Windows.Forms.Button fragModAddButton;
        private System.Windows.Forms.ListBox fragModListBox;
    }
}