using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    /// <summary>
    /// This is the top level form that contains all of the Views, with the exception of Plot View being detached
    /// </summary>
    public partial class MainForm : Form, IObserver
    {
        private readonly Manager mManager;

        public MainFormOptions mCurrentOptions;
        private const string PROGRAM_DATE = "March 31, 2021";

        public MainForm()
        {
            InitializeComponent();

            mCurrentOptions = new MainFormOptions();

            mManager = new Manager(this);

            // Fragment Ladder
            panelFragmentLadder.Controls.Add(mManager.mFragmentationLadder);
            panelFragmentLadder.Controls[mManager.mFragmentationLadder.Name].Dock = DockStyle.Fill;

            panelFragmentLadder.Resize += panel_Resize;

            // Data View
            panelDataView.Controls.Add(mManager.mDataView);
            panelDataView.Controls[mManager.mDataView.Name].Dock = DockStyle.Fill;
            panelDataView.Resize += panel_Resize;

            // Plot
            panelPlot.Controls.Add(mManager.mPlot);
            panelPlot.Controls[mManager.mPlot.Name].Dock = DockStyle.Fill;
            panelPlot.Resize += panel_Resize;

            splitContainer2.IsSplitterFixed = false;
            splitContainer2.BorderStyle = BorderStyle.FixedSingle;

            splitContainer1.BorderStyle = BorderStyle.FixedSingle;

            splitContainer1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            splitContainer2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            detachPlotFromMainForm();

            MainForm_Resize(this, null);
            Text = "Spectrum Look - " + GetAppVersion();

            KeyDown += MainForm_KeyDown;
            KeyPreview = true; // set this true so we can get all the key events for child controls
        }

        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private static string GetAppVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version + " (" + PROGRAM_DATE + ")";
        }

        /// <summary>
        /// Handles resizing the control in the panel.
        /// </summary>
        private void panel_Resize(object sender, EventArgs e)
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
            mManager.HandleKeyboardShortcuts(e);
        }

        private void detachPlotFromMainForm()
        {
            if (!mCurrentOptions.IsPlotInMainForm)
            {
                if (panelPlot.Controls.Contains(mManager.mPlot))
                {
                    var pointOnTheScreen = mManager.mPlot.PointToScreen(mManager.mPlot.Location);
                    panelPlot.Controls.Remove(mManager.mPlot);
                    mManager.mPlot.Hide();
                    mManager.mPlot.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                    mManager.mPlot.TopLevel = true;
                    mManager.mPlot.ShowInTaskbar = true;// false;
                    mManager.mPlot.ShowIcon = false;
                    mManager.mPlot.ControlBox = true;// false;
                    mManager.mPlot.FormClosing += PlotForm_Closing;
                    mManager.mPlot.Location = pointOnTheScreen;
                }
                MainForm_Resize(this, null);
                mManager.mPlot.Show();
            }
            else if (mCurrentOptions.IsPlotInMainForm && !panelPlot.Controls.Contains(mManager.mPlot))
            {
                mManager.mPlot.TopLevel = false;
                mManager.mPlot.FormBorderStyle = FormBorderStyle.None;
                mManager.mPlot.Location = new Point(0, 0); // Sets it to the corner of the screen... May need to save the original location though.
                mManager.mPlot.Hide();
                panelPlot.Controls.Add(mManager.mPlot);
                panelPlot.Controls[mManager.mPlot.Name].Dock = DockStyle.Fill;
                MainForm_Resize(this, null);
                mManager.mPlot.Show();
            }
        }

        private void MoveCollapseButton()
        {
            // cmdShowHideFragmentIons.Height = splitContainer2.Panel1.Height / 2;
            // cmdShowHideFragmentIons.Location = new Point(splitContainer2.Panel1.Left + 4, splitContainer2.Panel1.Top + cmdShowHideFragmentIons.Height / 4);
        }

        private void PlotForm_Closing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            mCurrentOptions.IsPlotInMainForm = true;
            // detachPlotFromMainForm();
        }

        private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            MoveCollapseButton();
        }

        private void SplitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            MoveCollapseButton();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // if (cmdShowHideFragmentIons.Text == "<<")
            //{
            //    if (mCurrentOptions != null && mCurrentOptions.IsPlotInMainForm)
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
            //    // mManager.mFragmentLadder.Width = cmdShowHideFragmentIons.Location.X;
            //}
            // if (cmdShowHideFragmentIons.Text == ">>")
            //{
            //    cmdShowHideFragmentIons.Location = new Point(0, 2);
            //    splitContainer2.SplitterDistance = cmdShowHideFragmentIons.Width;
            //}

            // panelFragmentLadder.Height = splitContainer2.Panel1.Height;
            // mManager.mFragmentLadder.Height = splitContainer2.Panel1.Height;

            // MoveCollapseButton();

            // panelDataView.Height = splitContainer1.Panel1.Height;
            // panelDataView.Width = splitContainer1.Panel1.Width;

            // if (mCurrentOptions != null && mCurrentOptions.IsPlotInMainForm) // This is so that the resize doesn't try to resize the plot window when it is detached.
            //{
            //    panelPlot.Height = splitContainer2.Panel2.Height;
            //    panelPlot.Width = splitContainer2.Panel2.Width;
            //}
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            /*
            if (cmdShowHideFragmentIons.Text == "<<")
            {
                splitContainer2.SplitterDistance = cmdShowHideFragmentIons.Width;
                panelFragmentLadder.Visible = false;
                cmdShowHideFragmentIons.Location = new Point(0, 2);
                cmdShowHideFragmentIons.Text = ">>";
                if (mCurrentOptions.IsPlotInMainForm)
                {
                    panelPlot.Height = splitContainer2.Panel2.Height;
                    panelPlot.Width = splitContainer2.Panel2.Width;
                    mManager.mPlot.Height = splitContainer2.Panel2.Height;
                    mManager.mPlot.Width = splitContainer2.Panel2.Width;
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

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mManager.RunOpenDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm_FormClosing(sender, null);
            Environment.Exit(0);
        }

        private void PlottingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mManager.OpenOptionsMenu("Plot Options");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mManager.HandleSaveWorkFile("");
        }

        private void OptionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mManager.OpenOptionsMenu("General Options");
        }

        private void FragmentLadderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mManager.OpenOptionsMenu("Fragment Ladder Options");
        }

        private void DataViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            mManager.OpenOptionsMenu("Data View Options");
        }

        private void GlobalOptionsHiding(object sender, EventArgs e)
        {
            // if (((MainFormOptions)sender).Visible == false)
            //{
            //    detachPlotFromMainForm(); // This uses the boolean value isPlotDetached.
            //}
        }

        private void SaveWorkStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveWorkFileDialog = new SaveFileDialog
            {
                Filter = "SpectrumLook Work File (*.slwf)|*.slwf|All Files (*.*)|*.*", Title = "Save Work File"
            };

            try
            {
                if (saveWorkFileDialog.ShowDialog() == DialogResult.OK)
                {
                    mManager.HandleSaveWorkFile(saveWorkFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void openWorkStripMenuItem_Click(object sender, EventArgs e)
        {
            var openWorkFileDialog = new OpenFileDialog
            {
                Filter = "SpectrumLook Work File (*.slwf)|*.slwf|All Files (*.*)|*.*",
                Title = "Open Work File"
            };

            try
            {
                if (openWorkFileDialog.ShowDialog() == DialogResult.OK)
                {
                   mManager.HandleOpenWorkFile(openWorkFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavePlotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mManager.SynopsisLoaded)
            {
                var batchSaveForm = new BatchSaveForm(mManager);
                batchSaveForm.Show();
            }
            else
            {
                MessageBox.Show("Please open a File before attempting to batch save.", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void UpdateObserver()
        {
            detachPlotFromMainForm();

            mManager.mFragmentationLadder.RegenerateLadderFromSelection();
        }
    }
}
