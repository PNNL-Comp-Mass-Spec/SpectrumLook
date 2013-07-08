using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    /// <summary>
    /// This is the top level form that contains all of the Views, with the exception of Plot View being detached
    /// </summary>
    //TODO : Need to inherit from IObserver and override the Update function! Otherwise the options will not update properly.
    public partial class MainForm : Form, IObserver
    {
        private Manager m_manager;

        public MainFormOptions m_currentOptions;

        #region CONSTRUCTOR
        public MainForm()
        {
            InitializeComponent();

            m_currentOptions = new MainFormOptions();

            m_manager = new Manager(this);

            //Fragment Ladder
            panelFragmentLadder.Controls.Add(m_manager.m_fragLadder);
            panelFragmentLadder.Resize += new EventHandler(panel_Resize);

            //Data View
            panelDataView.Controls.Add(m_manager.m_dataView);
            panelDataView.Resize += new EventHandler(panel_Resize);

            //Plot
            panelPlot.Controls.Add(m_manager.m_plot);
            panelPlot.Resize += new EventHandler(panel_Resize);
            
            //////FileOpen
            ////FileOpen = new OpenMenu();

            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer1.BorderStyle = BorderStyle.FixedSingle;

            splitContainer1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            splitContainer2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            this.detachPlotFromMainForm();

            this.MainForm_Resize(this, null);
            this.Text = "Spectrum Look Version - 1.0.1014";

            KeyDown += new KeyEventHandler(MainForm_KeyDown);
            KeyPreview = true; //set this true so we can get all the key events for child controls           
        }

        /// <summary>
        /// Handles resizing the control in the panel.
        /// </summary>
        void panel_Resize(object sender, EventArgs e)
        {
            Panel senderPanel = (Panel)sender;
            if (senderPanel.Controls.Count > 0)
            {
                senderPanel.Controls[0].Width = senderPanel.Width;
                senderPanel.Controls[0].Height = senderPanel.Height;
            }
        }
        #endregion

        #region HOTKEY MANAGEMENT
        /// <summary>
        /// Handles any keyPress within the SpectrumLook Form
        /// </summary>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            m_manager.HandleKeyboardShortcuts(e);
        }
        #endregion

        #region RESIZE WINDOW AND PANEL

        private void detachPlotFromMainForm()
        {
            Point pointOnTheScreen;
            if(!this.m_currentOptions.isPlotInMainForm)
            {
                if (panelPlot.Controls.Contains(m_manager.m_plot))
                {
                    pointOnTheScreen = m_manager.m_plot.PointToScreen(this.m_manager.m_plot.Location);
                    panelPlot.Controls.Remove(m_manager.m_plot);
                    m_manager.m_plot.Hide();
                    m_manager.m_plot.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
                    m_manager.m_plot.TopLevel = true;
                    m_manager.m_plot.ShowInTaskbar = true;//false;
                    m_manager.m_plot.ShowIcon = false;
                    m_manager.m_plot.ControlBox = true;//false;
                    m_manager.m_plot.FormClosing += new FormClosingEventHandler(m_plot_FormClosing);
                    m_manager.m_plot.Location = pointOnTheScreen;
                }
                MainForm_Resize(this, null);
                m_manager.m_plot.Show();
            }
            else if (this.m_currentOptions.isPlotInMainForm && !(panelPlot.Controls.Contains(m_manager.m_plot)))
            {
                m_manager.m_plot.TopLevel = false;
                m_manager.m_plot.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                m_manager.m_plot.Location = new Point(0, 0); // Sets it to the corner of the screen... May need to save the original location though.
                m_manager.m_plot.Hide();
                panelPlot.Controls.Add(m_manager.m_plot);
                MainForm_Resize(this, null);
                m_manager.m_plot.Show();
            }
        }

        void m_plot_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.m_currentOptions.isPlotInMainForm = true;
            //detachPlotFromMainForm();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.CollapsButton.Height = this.splitContainer2.Panel1.Height - 4;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (CollapsButton.Text == "<<")
            {
                if (this.m_currentOptions.isPlotInMainForm)
                {
                    this.splitContainer2.Panel2Collapsed = false;
                    this.splitContainer2.SplitterDistance = 420;
                    this.CollapsButton.Location = new Point(this.panelFragmentLadder.Width, 2);
                }
                else
                {
                    this.splitContainer2.Panel2Collapsed = true;
                    this.splitContainer2.SplitterDistance = this.splitContainer1.Width;
                    this.CollapsButton.Location = new Point(((this.splitContainer1.Width - this.CollapsButton.Width) - 5), 2);
                }
                //m_manager.m_fragmentLadder.Width = this.CollapsButton.Location.X;
            }
            if (CollapsButton.Text == ">>")
            {
                this.CollapsButton.Location = new Point(0, 2);
                this.splitContainer2.SplitterDistance = this.CollapsButton.Width;
            }

            panelFragmentLadder.Height = this.splitContainer2.Panel1.Height;
            //m_manager.m_fragmentLadder.Height = this.splitContainer2.Panel1.Height;
            this.CollapsButton.Height = this.splitContainer2.Panel1.Height - 4;

            panelDataView.Height = this.splitContainer1.Panel1.Height;
            panelDataView.Width = this.splitContainer1.Panel1.Width;

            if (this.m_currentOptions.isPlotInMainForm) // This is so that the resize doesn't try to resize the plot window when it is detached.
            {
                panelPlot.Height = this.splitContainer2.Panel2.Height;
                panelPlot.Width = this.splitContainer2.Panel2.Width;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CollapsButton.Text == "<<")
            {
                this.splitContainer2.SplitterDistance = this.CollapsButton.Width;
                this.panelFragmentLadder.Visible = false;
                this.CollapsButton.Location = new Point(0, 2);
                CollapsButton.Text = ">>";
                if (this.m_currentOptions.isPlotInMainForm)
                {
                    panelPlot.Height = this.splitContainer2.Panel2.Height;
                    panelPlot.Width = this.splitContainer2.Panel2.Width;
                    m_manager.m_plot.Height = this.splitContainer2.Panel2.Height;
                    m_manager.m_plot.Width = this.splitContainer2.Panel2.Width;
                }
            }
            else
            {
                this.splitContainer2.SplitterDistance = 420;
                this.panelFragmentLadder.Visible = true;
                CollapsButton.Text = "<<";
                MainForm_Resize(sender, null);
            }
        }

        #endregion

        #region MENU STRIP EVENTS
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_manager.RunOpenDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm_FormClosing(sender, null);
            Environment.Exit(0);
        }

        private void plottingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_manager.OpenOptionsMenu("Plot Options");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_manager.HandleSaveWorkFile("");
        }

        private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_manager.OpenOptionsMenu("General Options");
        }

        private void fragmentLadderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_manager.OpenOptionsMenu("Fragment Ladder Options");
        }

        private void dataViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            m_manager.OpenOptionsMenu("Data View Options");
        }

        private void globalOptionsHiding(object sender, EventArgs e)
        {
            //if (((MainFormOptions)sender).Visible == false)
            //{
            //    this.detachPlotFromMainForm(); //This uses the boolean value isPlotDetached.
            //}
        }


        private void saveWorkStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveWorkFileDialog = new SaveFileDialog();
            saveWorkFileDialog.Filter = "SpectrumLook Work File (*.slwf)|*.slwf|All Files (*.*)|*.*";
            saveWorkFileDialog.Title = "Save Work File";

            try
            {
                if (saveWorkFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_manager.HandleSaveWorkFile(saveWorkFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openWorkStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openWorkFileDialog = new OpenFileDialog();
            openWorkFileDialog.Filter = "SpectrumLook Work File (*.slwf)|*.slwf|All Files (*.*)|*.*";
            openWorkFileDialog.Title = "Open Work File";

            try
            {
                if (openWorkFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    m_manager.HandleOpenWorkFile(openWorkFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void batchSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_manager.m_isSynopsisLoaded)
            {
                BatchSaveForm batchSaveForm = new BatchSaveForm(m_manager);
                batchSaveForm.Show();
            }
            else
            {
                MessageBox.Show("Please open a File before attempting to batch save.", "Open A File First", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        public void UpdateObserver()
        {
            //TODO: add code that updates the options in main form
            detachPlotFromMainForm();
            //TODO: add code to regenerate fragmentLadder possibly horizontally
            m_manager.m_fragLadder.regenerateLadderFromSelection();
        }
    }
}
