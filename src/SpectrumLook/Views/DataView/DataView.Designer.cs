namespace SpectrumLook.Views
{
    partial class DataView
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
            this.components = new System.ComponentModel.Container();
            this.DataGridTable = new System.Windows.Forms.DataGridView();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.AdvFilter = new System.Windows.Forms.Button();
            this.ColcontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridTable)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGridTable
            // 
            this.DataGridTable.AllowUserToAddRows = false;
            this.DataGridTable.AllowUserToDeleteRows = false;
            this.DataGridTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridTable.Location = new System.Drawing.Point(0, 39);
            this.DataGridTable.MultiSelect = false;
            this.DataGridTable.Name = "DataGridTable";
            this.DataGridTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridTable.Size = new System.Drawing.Size(518, 223);
            this.DataGridTable.TabIndex = 0;
            this.DataGridTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridTable_CellDoubleClick);
            this.DataGridTable.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridTable_ColumnHeaderMouseClick_1);
            this.DataGridTable.SelectionChanged += new System.EventHandler(this.DataGridTable_SelectionChanged);
            this.DataGridTable.Click += new System.EventHandler(this.DataGridTable_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(130, 11);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 1;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(12, 12);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(112, 20);
            this.SearchBox.TabIndex = 0;
            this.SearchBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchBox_KeyDown);
            // 
            // AdvFilter
            // 
            this.AdvFilter.Enabled = false;
            this.AdvFilter.Location = new System.Drawing.Point(211, 11);
            this.AdvFilter.Name = "AdvFilter";
            this.AdvFilter.Size = new System.Drawing.Size(75, 23);
            this.AdvFilter.TabIndex = 2;
            this.AdvFilter.Text = "Filter";
            this.AdvFilter.UseVisualStyleBackColor = true;
            this.AdvFilter.Visible = false;
            this.AdvFilter.Click += new System.EventHandler(this.AdvFilter_Click);
            // 
            // ColcontextMenuStrip
            // 
            this.ColcontextMenuStrip.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ColcontextMenuStrip.Name = "ColcontextMenuStrip";
            this.ColcontextMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // DataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 262);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.DataGridTable);
            this.Controls.Add(this.AdvFilter);
            this.Name = "DataView";
            this.Text = "DataView";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGridTable;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button AdvFilter;
        private System.Windows.Forms.ContextMenuStrip ColcontextMenuStrip;

    }
}
