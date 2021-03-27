using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using SpectrumLook;
using SpectrumLook.Builders;

namespace SpectrumLook.Views
{
    public partial class SLPlot : Form, IObserver
    {
        private Manager m_manager;
        public PlotOptions m_options
        {
            get
            {
                return msPlot.m_options;
            }
            set
            {
                msPlot.m_options = value;
            }
        }

        #region Initialization
        /// <summary>
        /// The constructor for the form
        /// </summary>
        public SLPlot(Manager manager)
        {
            InitializeComponent();
            m_manager = manager;
            msPlot.m_manager = manager;

            m_options = new PlotOptions();

            //Setup the parts of the form not involving zedgraph
            msPlot.m_updateCursorCallback = UpdateSnapPoint;
            trackBarAnnotationPercent.Value = msPlot.m_options.annotationPercent;

            //set this true so that all keyboard events in any child control get processed by SLPlot first
            KeyPreview = true;
        }

        /// <summary>
        /// The form Load event, initializes the graph and sets it's initial size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // msPlot.SetSize(new Point(10, 10), ClientRectangle.Width - 20, ClientRectangle.Height - 20);
        }
        #endregion

        #region Keyboard Shortcuts
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!e.Handled)
            {
                m_manager.HandleKeyboardShortcuts(e);
            }
        }
        #endregion

        #region Form Sizing
        /// <summary>
        /// The resize event for the form, resizes the graph at the same time
        /// </summary>
        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeForm();
        }

        /// <summary>
        /// The resize event for the form, resizes the graph at the same time
        /// </summary>
        private void ResizeForm()
        {
            /*
            bool setSnapBoxVisible = true;
            bool setAnnotationSliderVisible = true;
            bool setButtonDetachPlot = true;
            bool setButtonPlotOptionsVisible = true;
            bool setNumPlotsGroupVisible = true;

            ////check what can we show given the space we have

            //check horizontal size constraints
            if (this.Size.Width < (20 + groupBoxAnnotationCoverage.Size.Width + groupBoxClosestPoint.Size.Width))
            {
                setAnnotationSliderVisible = false;
                setButtonDetachPlot = false;
                setButtonPlotOptionsVisible = false;
                setNumPlotsGroupVisible = false;
                if (this.Size.Width < (12 + groupBoxClosestPoint.Width + (.5 * groupBoxAnnotationCoverage.Size.Width)))
                {
                    setSnapBoxVisible = false;
                }
            }
            //check vertical size constraints
            if (this.Size.Height < 30 + (4 * (groupBoxClosestPoint.Size.Height)))
            {
                setAnnotationSliderVisible = false;
                setSnapBoxVisible = false;
                setButtonDetachPlot = false;
                setButtonPlotOptionsVisible = false;
                setNumPlotsGroupVisible = false;
            }

            ////Check the user options for if we need to hide something we have room for
            if (m_options != null && msPlot.m_options.hidePlotTools)
            {
                setSnapBoxVisible = false;
                setAnnotationSliderVisible = false;
                setButtonDetachPlot = false;
                setButtonPlotOptionsVisible = false;
                setNumPlotsGroupVisible = false;
                buttonHidePlotOptions.Text = "▲";
            }
            else
            {
                buttonHidePlotOptions.Text = "▼";
            }

            //now resize the form
            groupBoxClosestPoint.Visible = setSnapBoxVisible;
            groupBoxAnnotationCoverage.Visible = setAnnotationSliderVisible;
            buttonDetachPlot.Visible = setButtonDetachPlot;
            buttonPlotOptions.Visible = setButtonPlotOptionsVisible;
            numberOfPlots.Visible = setNumPlotsGroupVisible;
            label1.Visible = setNumPlotsGroupVisible;
            if (setSnapBoxVisible || setAnnotationSliderVisible)
            {
                msPlot.SetSize(new Point(10, 10), ClientRectangle.Width - 20, ClientRectangle.Height - (30 + groupBoxClosestPoint.Height));
            }
            else
            {
                msPlot.SetSize(new Point(10, 10), ClientRectangle.Width - 20, ClientRectangle.Height - (30 + buttonHidePlotOptions.Height));
            }
            */
        }

        private void buttonHidePlotOptions_Click(object sender, EventArgs e)
        {
            ToggleHidePlotTools();
        }

        private void ToggleHidePlotTools()
        {
            if (m_options.hidePlotTools)
            {
                m_options.hidePlotTools = false;
            }
            else
            {
                m_options.hidePlotTools = true;
            }
        }
        #endregion

        #region Annotation Editing

        /// <summary>
        /// Handles selecting the nearest peptide to edit the annotation for, and showing the editAnnotation form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void msPlot_DoubleClick(object sender, EventArgs e)
        {
            var usingCustomAnnotation = false;
            var mouseArgs = (MouseEventArgs)e;
            var g = msPlot.CreateGraphics();
            GraphPane closestPane;
            PointPair closestPoint;
            var mousePt = new PointF(mouseArgs.X, mouseArgs.Y);
            TextObj selectedTextObj = null;

            //retrieve the text object that the user may have selected
            if (msPlot.FindClosestPoint(mousePt, out closestPoint, out closestPane))
            {
                foreach (var textObject in closestPane.GraphObjList)
                {
                    if (textObject.Tag == closestPoint)
                    {
                        //zed graph found the annotation object, use that text object
                        selectedTextObj = textObject as TextObj;
                        break;
                    }
                }
            }
            else
            {
                //couldn't find the annotation from where the user clicked
                return;
            }

            if (selectedTextObj == null)
                return; //no obj, so don't do anything

            //see if we have a custom annotation for this annotation we selected
            var currentInstance = m_manager.GetCurrentInstance();
            var selectedAnnotation = new Annotation();
            foreach (var annotation in currentInstance.annotations)
            {
                if (annotation.m_point == (PointPair)selectedTextObj.Tag)
                {
                    usingCustomAnnotation = true;
                    selectedAnnotation = annotation;
                    currentInstance.annotations.Remove(annotation);
                    break;
                }
            }
            if (selectedAnnotation.m_point == null && selectedAnnotation.m_text == null)
            {
                selectedAnnotation.m_showHideAuto = 0;
                selectedAnnotation.m_text = selectedTextObj.Text;
                selectedAnnotation.m_point = (PointPair)selectedTextObj.Tag;
            }

            // Open the AnnotationEdit form
            var editForm = new PlotView.AnnotationEdit(selectedAnnotation);
            if (editForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (editForm.m_annotation.m_showHideAuto < 0)
                {
                    selectedTextObj.IsVisible = false;
                }
                else if (editForm.m_annotation.m_showHideAuto > 0)
                {
                    selectedTextObj.IsVisible = true;
                }
                selectedTextObj.Text = editForm.m_annotation.m_text;

                currentInstance.annotations.Add(editForm.m_annotation);
            }
            else if (usingCustomAnnotation)
            {
                //recreate the annotation we thought we were replacing
                currentInstance.annotations.Add(selectedAnnotation);
            }

            msPlot.ReevaluateAnnotations();
            msPlot.Invalidate();
            return;
        }
        #endregion

        #region Batch Save

        ///The following is copied from ZedGraph... use this to figure out how to save an image File

        public readonly List<string> SaveAsImageTypes = new List<string>() { ".emf", ".png", ".gif", ".jpg", ".tif", ".bmp" };

        /// <summary>
        /// Saves the Current Plot image as the file specified by the filename
        /// </summary>
        /// <param name="fileName">The name of the output file that we will make.  If there is no Extension then it will be assumed as .png</param>
        /// <returns>true if the save was successful, false otherwise</returns>
        public bool SavePlotImageAs(string fileName)
        {
            var success = true;

            try
            {
                if (msPlot.MasterPane != null && !string.IsNullOrEmpty(fileName))
                {
                    var ext = System.IO.Path.GetExtension(fileName).ToLower();

                    using (Stream myStream = new FileStream(fileName, FileMode.Create))
                    {
                        if (myStream != null)
                        {
                            if (ext.EndsWith("emf"))
                            {
                                myStream.Close();
                                SaveEmfFile(fileName);
                            }
                            else
                            {
                                var format = ImageFormat.Png;
                                switch (ext)
                                {
                                    case ("png"): format = ImageFormat.Png; break;
                                    case ("gif"): format = ImageFormat.Gif; break;
                                    case ("jpg"):
                                    case ("jpeg"): format = ImageFormat.Jpeg; break;
                                    case ("tiff"):
                                    case ("tif"): format = ImageFormat.Tiff; break;
                                    case ("bmp"): format = ImageFormat.Bmp; break;
                                    default: format = ImageFormat.Png; break;
                                }

                                msPlot.MasterPane.GetImage(msPlot.MasterPane.IsAntiAlias).Save(myStream, format);
                                myStream.Close();
                            }
                        }
                    }
                }
            }
            catch
            {
                success = false;
            }

            return success;
        }

        /// Dlls needed to save an EmfFile
        [DllImport("gdi32.dll")]
        static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, System.Text.StringBuilder hNULL);
        [DllImport("gdi32.dll")]
        static extern bool DeleteEnhMetaFile(IntPtr hemf);

        /// <summary>
        /// Save the current Graph to the specified filename in EMF (vector) format.
        /// </summary>
        /// <remarks>
        /// Note that this handler saves as an Emf format only.  The default handler is
        /// <see cref="SaveAs()" />, which allows for Bitmap or EMF formats.
        /// </remarks>
        internal void SaveEmfFile(string fileName)
        {
            using (var g = this.CreateGraphics())
            {
                var hdc = g.GetHdc();
                var metaFile = new Metafile(hdc, EmfType.EmfPlusOnly);
                using (var gMeta = Graphics.FromImage(metaFile))
                {
                    msPlot.MasterPane.Draw(gMeta);
                }

                IntPtr hEMF;
                hEMF = metaFile.GetHenhmetafile(); // invalidates mf
                if (!hEMF.Equals(new IntPtr(0)))
                {
                    var tempName = new StringBuilder(fileName);
                    CopyEnhMetaFile(hEMF, tempName);
                    DeleteEnhMetaFile(hEMF);
                }

                g.ReleaseHdc(hdc);
            }

        }


        #endregion

        #region Options
        /// <summary>
        /// Extra event for the closing of the options form so that we can update the plot to the user's new options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateObserver()
        {
            ResizeForm();
            msPlot.UpdateOptions();
            msPlot.IsEnableVZoom = !msPlot.m_options.zoomHorizontal;
            msPlot.IsShowContextMenu = !msPlot.m_options.rightClickUnzoom;
            trackBarAnnotationPercent.Value = msPlot.m_options.annotationPercent;
            if (!msPlot.m_options.showSnappingCursor)
            {
                mzTextBox.Text = string.Empty;
                relativeIntensityTextBox.Text = string.Empty;
            }
            //msPlot.ReevaluateAnnotations();
            msPlot.Invalidate();
            this.Invalidate();
        }

        /// <summary>
        /// Fires when the user scrolls the Annotation Percent Slider
        /// This will fire an event in the plot to replot with a different level of annotations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBarAnnotationPercent_Scroll(object sender, EventArgs e)
        {

            msPlot.m_options.annotationPercent = trackBarAnnotationPercent.Value;
            msPlot.ReevaluateAnnotations();
            msPlot.Invalidate();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Updates information on the form about the Snapping Point's Position.  This is used as a callback in the myZedGraph plotter.
        /// </summary>
        /// <param name="newPosition"></param>
        public void UpdateSnapPoint(PointF newPosition)
        {
            mzTextBox.Text = newPosition.X.ToString();
            relativeIntensityTextBox.Text = newPosition.Y.ToString();
        }

        /// <summary>
        /// Handles unzooming zedgraph by calling the myZedGraph HandleZoomOut method
        /// </summary>
        public void HandleZoomOut()
        {
            msPlot.HandleZoomOut();
        }

        /// <summary>
        /// Focuses the plot on a specific point.  This is used for when the user clicks the fragment ladder and we want to hilight that point
        /// </summary>
        /// <param name="focusValue">The x value to focus on in the plot</param>
        public void FocusPlotOnPoint(double focusValue)
        {
            var arrowPoint = msPlot.m_arrowPoint;

            if (msPlot.m_arrowShowing && Math.Abs((float)focusValue - arrowPoint.X) < Single.Epsilon)
            {
                msPlot.m_arrowShowing = false;
            }
            else
            {
                var offset = msPlot.m_options.focusOffset;

                //change the zoom of the graph
                var oldZoom = new ZoomState(msPlot.GraphPane, ZoomState.StateType.Zoom);
                msPlot.GraphPane.XAxis.Scale.Min = focusValue - offset;
                msPlot.GraphPane.XAxis.Scale.Max = focusValue + offset;
                msPlot.GraphPane.YAxis.Scale.MinAuto = true;
                msPlot.GraphPane.YAxis.Scale.MaxAuto = true;
                msPlot.GraphPane.ZoomStack.Add(oldZoom);

                //place a cursor below the axis
                var graphPoint = new PointF((float)focusValue, (float)msPlot.GraphPane.YAxis.Scale.Min);
                msPlot.PaintArrow(graphPoint);
            }
            msPlot.Invalidate();
        }

        /// <summary>
        /// Takes a list of compared elements and splits them into matched and unmatched elements,
        /// then passes the information to msPlot to plot the actual graph
        /// </summary>
        /// <param name="Points"></param>
        /// <param name="scanNumber"></param>
        public void PlotData(List<Element> Points, string scanNumber, string peptide)
        {
            var matchedPoints = new List<Element>();
            var unmatchedPoints = new List<Element>();

            foreach (var point in Points)
            {
                if (point.Matched == true)
                {
                    matchedPoints.Add(point);
                }
                else
                {
                    unmatchedPoints.Add(point);
                }
            }

            msPlot.PlotGraph(peptide, scanNumber, unmatchedPoints, matchedPoints);
            msPlot.m_arrowShowing = false;
            msPlot.ReevaluateAnnotations();
            msPlot.Invalidate();
            msPlot.Update();
        }

        public void buttonDetachPlot_Click(object sender, EventArgs e)
        {
            if (m_manager.m_mainForm.m_currentOptions.isPlotInMainForm)             //If plot is attached.
            {
                m_manager.m_mainForm.m_currentOptions.isPlotInMainForm = false;     //Set to not be attached.
                buttonDetachPlot.Text = "Attach Plot";
            }
            else
            {
                m_manager.m_mainForm.m_currentOptions.isPlotInMainForm = true;
                buttonDetachPlot.Text = "Detach Plot";
            }
        }

        #endregion

        private void buttonPlotOptions_Click(object sender, EventArgs e)
        {
            m_manager.OpenOptionsMenu("Plot Options");
        }

        private void numberOfPlots_TextChanged(object sender, EventArgs e)
        {
            var numPlots = 0;
            var str = numberOfPlots.Text.Trim();
            if (int.TryParse(str, out numPlots))
            {
                //m_manager.m_mainForm.m_currentOptions.numberOfPlotsChangedOnForm = numPlots;
            }
            msPlot.Invalidate();
            msPlot.Update();
        }
    }
}