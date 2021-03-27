using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    /// <summary>
    /// This is the top level form that contains all of the Views, with the exception of Plot View being detached
    /// </summary>
    // TODO : Need to inherit from IObserver and override the Update function! Otherwise the options will not update properly.
    public partial class MainForm : Form, IObserver
    {
        private Manager m_manager;

        public MainFormOptions m_currentOptions;
        private const string PROGRAM_DATE = "March 26, 2021";

        public MainForm()
        {
            InitializeComponent();

            m_currentOptions = new MainFormOptions();

            m_manager = new Manager(this);

            // Fragment Ladder
            panelFragmentLadder.Controls.Add(m_manager.m_fragLadder);
            panelFragmentLadder.Controls[m_manager.m_fragLadder.Name].Dock = DockStyle.Fill;

            panelFragmentLadder.Resize += new EventHandler(panel_Resize);

            // Data View
            panelDataView.Controls.Add(m_manager.m_dataView);
            panelDataView.Controls[m_manager.m_dataView.Name].Dock = DockStyle.Fill;
            panelDataView.Resize += new EventHandler(panel_Resize);

            // Plot
            panelPlot.Controls.Add(m_manager.m_plot);
            panelPlot.Controls[m_manager.m_plot.Name].Dock = DockStyle.Fill;
            panelPlot.Resize += new EventHandler(panel_Resize);

            //// FileOpen
            //// FileOpen = new OpenMenu();

            splitContainer2.IsSplitterFixed = false;
            splitContainer2.BorderStyle = BorderStyle.FixedSingle;

            splitContainer1.BorderStyle = BorderStyle.FixedSingle;

            splitContainer1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            splitContainer2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            detachPlotFromMainForm();

            MainForm_Resize(this, null);
            Text = "Spectrum Look - " + GetAppVersion();

            KeyDown += new KeyEventHandler(MainForm_KeyDown);
            KeyPreview = true; // set this true so we can get all the key events for child controls
        }

        private static string GetAppVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " (" + PROGRAM_DATE + ")";
        }

        /// <summary>
        /// Handles resizing the control in the panel.
        /// </summary>
        void panel_Resize(object sender, EventArgs e)
        {
            /*Panel senderPanel = (Panel)sender;
            if (senderPanel.Controls.Count > 0)
            {
                senderPanel.Controls[0].Width = senderPanel.Width;
                senderPanel.Controls[0].Height = senderPanel.Height;
            }
             */
        }

        /// <summary>
        /// Handles any keyPress within the SpectrumLook Form
        /// </summary>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            m_manager.HandleKeyboardShortcuts(e);
        }

        private void detachPlotFromMainForm()
        {
            Point pointOnTheScreen;
            if(!m_currentOptions.isPlotInMainForm)
            {
                if (panelPlot.Controls.Contains(m_manager.m_plot))
                {
                    pointOnTheScreen = m_manager.m_plot.PointToScreen(m_manager.m_plot.Location);
                    panelPlot.Controls.Remove(m_manager.m_plot);
                    m_manager.m_plot.Hide();
                    m_manager.m_plot.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
                    m_manager.m_plot.TopLevel = true;
                    m_manager.m_plot.ShowInTaskbar = true;// false;
                    m_manager.m_plot.ShowIcon = false;
                    m_manager.m_plot.ControlBox = true;// false;
                    m_manager.m_plot.FormClosing += new FormClosingEventHandler(m_plot_FormClosing);
                    m_manager.m_plot.Location = pointOnTheScreen;
                }
                MainForm_Resize(this, null);
                m_manager.m_plot.Show();
            }
            else if (m_currentOptions.isPlotInMainForm && !(panelPlot.Controls.Contains(m_manager.m_plot)))
            {
                m_manager.m_plot.TopLevel = false;
                m_manager.m_plot.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                m_manager.m_plot.Location = new Point(0, 0); // Sets it to the corner of the screen... May need to save the original location though.
                m_manager.m_plot.Hide();
                panelPlot.Controls.Add(m_manager.m_plot);
                panelPlot.Controls[m_manager.m_plot.Name].Dock = DockStyle.Fill;
                MainForm_Resize(this, null);
                m_manager.m_plot.Show();
            }
        }

        private void MoveCollapseButton()
        {
            // cmdShowHideFragmentIons.Height = splitContainer2.Panel1.Height / 2;
            // cmdShowHideFragmentIons.Location = new Point(splitContainer2.Panel1.Left + 4, splitContainer2.Panel1.Top + cmdShowHideFragmentIons.Height / 4);
        }

        void m_plot_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            m_currentOptions.isPlotInMainForm = true;
            // detachPlotFromMainForm();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            MoveCollapseButton();
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            MoveCollapseButton();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // if (cmdShowHideFragmentIons.Text == "<<")
            //{
            //    if (m_currentOptions != null && m_currentOptions.isPlotInMainForm)
            //    {
            //        splitContainer2.Panel2Collapsed = false;
            //        splitContainer2.SplitterDistance = 420;
            //        cmdShowHideFragmentIons.Location = new Point(panelFragmentLadder.Width, 2);
            //    }
            //    else
            //    {
            //        splitContainer2.Panel2Collapsed = true;
            //        splitContainer2.SplitterDistance = splitContainer1.Width;
            //        cmdShowHideFragmentIons.Location = new Point(((splitContainer1.Width - cmdShowHideFragmentIons.Width) - 5), 2);
            //    }
            //    // m_manager.m_fragmentLadder.Width = cmdShowHideFragmentIons.Location.X;
            //}
            // if (cmdShowHideFragmentIons.Text == ">>")
            //{
            //    cmdShowHideFragmentIons.Location = new Point(0, 2);
            //    splitContainer2.SplitterDistance = cmdShowHideFragmentIons.Width;
            //}

            // panelFragmentLadder.Height = splitContainer2.Panel1.Height;
            //// m_manager.m_fragmentLadder.Height = splitContainer2.Panel1.Height;

            // MoveCollapseButton();

            // panelDataView.Height = splitContainer1.Panel1.Height;
            // panelDataView.Width = splitContainer1.Panel1.Width;

            // if (m_currentOptions != null && m_currentOptions.isPlotInMainForm) // This is so that the resize doesn't try to resize the plot window when it is detached.
            //{
            //    panelPlot.Height = splitContainer2.Panel2.Height;
            //    panelPlot.Width = splitContainer2.Panel2.Width;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            if (cmdShowHideFragmentIons.Text == "<<")
            {
                splitContainer2.SplitterDistance = cmdShowHideFragmentIons.Width;
                panelFragmentLadder.Visible = false;
                cmdShowHideFragmentIons.Location = new Point(0, 2);
                cmdShowHideFragmentIons.Text = ">>";
                if (m_currentOptions.isPlotInMainForm)
                {
                    panelPlot.Height = splitContainer2.Panel2.Height;
                    panelPlot.Width = splitContainer2.Panel2.Width;
                    m_manager.m_plot.Height = splitContainer2.Panel2.Height;
                    m_manager.m_plot.Width = splitContainer2.Panel2.Width;
                }
            }
            else
            {
                splitContainer2.SplitterDistance = 420;
                panelFragmentLadder.Visible = true;
                cmdShowHideFragmentIons.Text = "<<";
                MainForm_Resize(sender, null);
            }
             */
        }

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
            // if (((MainFormOptions)sender).Visible == false)
            //{
            //    detachPlotFromMainForm(); // This uses the boolean value isPlotDetached.
            //}
        }

        private void saveWorkStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveWorkFileDialog = new SaveFileDialog();
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
            var openWorkFileDialog = new OpenFileDialog();
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

        private void SavePlotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_manager.SynopsisLoaded)
            {
                var batchSaveForm = new BatchSaveForm(m_manager);
                batchSaveForm.Show();
            }
            else
            {
                MessageBox.Show("Please open a File before attempting to batch save.", "Open A File First", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateObserver()
        {
            // TODO: add code that updates the options in main form
            detachPlotFromMainForm();
            // TODO: add code to regenerate fragmentLadder possibly horizontally
            m_manager.m_fragLadder.regenerateLadderFromSelection();
        }
    }
}
