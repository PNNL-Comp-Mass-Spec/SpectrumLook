namespace SpectrumLook.Views.PlotView
{
    partial class AnnotationEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnnotationEdit));
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelAnnotation = new System.Windows.Forms.Label();
            this.labelVisibility = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.buttonResetAnnotation = new System.Windows.Forms.Button();
            this.radioButtonVisibleAuto = new System.Windows.Forms.RadioButton();
            this.radioButtonVisibleNever = new System.Windows.Forms.RadioButton();
            this.radioButtonVisibleAlways = new System.Windows.Forms.RadioButton();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            //
            // buttonApply
            //
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(180, 117);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 5;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            //
            // buttonCancel
            //
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(261, 117);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            //
            // textBox1
            //
            this.textBox1.Location = new System.Drawing.Point(44, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            //
            // labelAnnotation
            //
            this.labelAnnotation.AutoSize = true;
            this.labelAnnotation.Location = new System.Drawing.Point(6, 29);
            this.labelAnnotation.Name = "labelAnnotation";
            this.labelAnnotation.Size = new System.Drawing.Size(31, 13);
            this.labelAnnotation.TabIndex = 4;
            this.labelAnnotation.Text = "Text:";
            //
            // labelVisibility
            //
            this.labelVisibility.AutoSize = true;
            this.labelVisibility.Location = new System.Drawing.Point(6, 68);
            this.labelVisibility.Name = "labelVisibility";
            this.labelVisibility.Size = new System.Drawing.Size(46, 13);
            this.labelVisibility.TabIndex = 5;
            this.labelVisibility.Text = "Visibility:";
            //
            // groupBox
            //
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.buttonResetAnnotation);
            this.groupBox.Controls.Add(this.radioButtonVisibleAuto);
            this.groupBox.Controls.Add(this.radioButtonVisibleNever);
            this.groupBox.Controls.Add(this.radioButtonVisibleAlways);
            this.groupBox.Controls.Add(this.textBox1);
            this.groupBox.Controls.Add(this.labelAnnotation);
            this.groupBox.Controls.Add(this.labelVisibility);
            this.groupBox.Location = new System.Drawing.Point(12, 12);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(324, 96);
            this.groupBox.TabIndex = 8;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Annotation Options";
            //
            // buttonResetAnnotation
            //
            this.buttonResetAnnotation.Location = new System.Drawing.Point(255, 24);
            this.buttonResetAnnotation.Name = "buttonResetAnnotation";
            this.buttonResetAnnotation.Size = new System.Drawing.Size(62, 23);
            this.buttonResetAnnotation.TabIndex = 1;
            this.buttonResetAnnotation.Text = "Restore";
            this.buttonResetAnnotation.UseVisualStyleBackColor = true;
            this.buttonResetAnnotation.Click += new System.EventHandler(this.buttonResetAnnotation_Click);
            //
            // radioButtonVisibleAuto
            //
            this.radioButtonVisibleAuto.AutoSize = true;
            this.radioButtonVisibleAuto.Location = new System.Drawing.Point(242, 68);
            this.radioButtonVisibleAuto.Name = "radioButtonVisibleAuto";
            this.radioButtonVisibleAuto.Size = new System.Drawing.Size(77, 17);
            this.radioButtonVisibleAuto.TabIndex = 4;
            this.radioButtonVisibleAuto.TabStop = true;
            this.radioButtonVisibleAuto.Text = "Auto Show";
            this.radioButtonVisibleAuto.UseVisualStyleBackColor = true;
            this.radioButtonVisibleAuto.CheckedChanged += new System.EventHandler(this.radioButtonVisibleAuto_CheckedChanged);
            //
            // radioButtonVisibleNever
            //
            this.radioButtonVisibleNever.AutoSize = true;
            this.radioButtonVisibleNever.Location = new System.Drawing.Point(151, 68);
            this.radioButtonVisibleNever.Name = "radioButtonVisibleNever";
            this.radioButtonVisibleNever.Size = new System.Drawing.Size(83, 17);
            this.radioButtonVisibleNever.TabIndex = 3;
            this.radioButtonVisibleNever.TabStop = true;
            this.radioButtonVisibleNever.Text = "Always Hide";
            this.radioButtonVisibleNever.UseVisualStyleBackColor = true;
            this.radioButtonVisibleNever.CheckedChanged += new System.EventHandler(this.checkBoxVisibleNo_CheckedChanged);
            //
            // radioButtonVisibleAlways
            //
            this.radioButtonVisibleAlways.AutoSize = true;
            this.radioButtonVisibleAlways.Location = new System.Drawing.Point(57, 68);
            this.radioButtonVisibleAlways.Name = "radioButtonVisibleAlways";
            this.radioButtonVisibleAlways.Size = new System.Drawing.Size(88, 17);
            this.radioButtonVisibleAlways.TabIndex = 2;
            this.radioButtonVisibleAlways.TabStop = true;
            this.radioButtonVisibleAlways.Text = "Always Show";
            this.radioButtonVisibleAlways.UseVisualStyleBackColor = true;
            this.radioButtonVisibleAlways.CheckedChanged += new System.EventHandler(this.checkBoxVisibleYes_CheckedChanged);
            //
            // AnnotationEdit
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 147);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AnnotationEdit";
            this.ShowInTaskbar = false;
            this.Text = "AnnotationEdit";
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelAnnotation;
        private System.Windows.Forms.Label labelVisibility;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton radioButtonVisibleAuto;
        private System.Windows.Forms.RadioButton radioButtonVisibleNever;
        private System.Windows.Forms.RadioButton radioButtonVisibleAlways;
        private System.Windows.Forms.Button buttonResetAnnotation;
    }
}