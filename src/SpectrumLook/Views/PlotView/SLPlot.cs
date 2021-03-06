﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using SpectrumLook.Builders;

namespace SpectrumLook.Views
{
    public partial class SLPlot : Form, IObserver
    {
        private readonly Manager mManager;

        public PlotOptions Options
        {
            get => msPlot.mOptions;
            set => msPlot.mOptions = value;
        }

        /// <summary>
        /// The constructor for the form
        /// </summary>
        public SLPlot(Manager manager)
        {
            InitializeComponent();
            mManager = manager;
            msPlot.mManager = manager;

            Options = new PlotOptions();

            // Setup the parts of the form not involving ZedGraph
            msPlot.mUpdateCursorCallback = UpdateSnapPoint;
            trackBarAnnotationPercent.Value = msPlot.mOptions.AnnotationPercent;

            // set this true so that all keyboard events in any child control get processed by SLPlot first
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

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (!e.Handled)
            {
                mManager.HandleKeyboardShortcuts(e);
            }
        }

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

            // check what can we show given the space we have

            // check horizontal size constraints
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
            // check vertical size constraints
            if (this.Size.Height < 30 + (4 * (groupBoxClosestPoint.Size.Height)))
            {
                setAnnotationSliderVisible = false;
                setSnapBoxVisible = false;
                setButtonDetachPlot = false;
                setButtonPlotOptionsVisible = false;
                setNumPlotsGroupVisible = false;
            }

            // Check the user options for if we need to hide something we have room for
            if (mOptions != null && msPlot.mOptions.hidePlotTools)
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

            // now resize the form
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

        private void ButtonHidePlotOptions_Click(object sender, EventArgs e)
        {
            ToggleHidePlotTools();
        }

        private void ToggleHidePlotTools()
        {
            if (Options.HidePlotTools)
            {
                Options.HidePlotTools = false;
            }
            else
            {
                Options.HidePlotTools = true;
            }
        }

        /// <summary>
        /// Handles selecting the nearest peptide to edit the annotation for, and showing the editAnnotation form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MsPlot_DoubleClick(object sender, EventArgs e)
        {
            var usingCustomAnnotation = false;
            var mouseArgs = (MouseEventArgs)e;
            var g = msPlot.CreateGraphics();
            var mousePt = new PointF(mouseArgs.X, mouseArgs.Y);
            TextObj selectedTextObj = null;

            // retrieve the text object that the user may have selected
            if (msPlot.FindClosestPoint(mousePt, out var closestPoint, out var closestPane))
            {
                foreach (var textObject in closestPane.GraphObjList)
                {
                    if (textObject.Tag == closestPoint)
                    {
                        // zed graph found the annotation object, use that text object
                        selectedTextObj = textObject as TextObj;
                        break;
                    }
                }
            }
            else
            {
                // couldn't find the annotation from where the user clicked
                return;
            }

            if (selectedTextObj == null)
                return; // no obj, so don't do anything

            // see if we have a custom annotation for this annotation we selected
            var currentInstance = mManager.GetCurrentInstance();
            var selectedAnnotation = new Annotation();
            foreach (var annotation in currentInstance.Annotations)
            {
                if (annotation.Point == (PointPair)selectedTextObj.Tag)
                {
                    usingCustomAnnotation = true;
                    selectedAnnotation = annotation;
                    currentInstance.Annotations.Remove(annotation);
                    break;
                }
            }
            if (selectedAnnotation.Point == null && selectedAnnotation.Text == null)
            {
                selectedAnnotation.ShowHideAuto = 0;
                selectedAnnotation.Text = selectedTextObj.Text;
                selectedAnnotation.Point = (PointPair)selectedTextObj.Tag;
            }

            // Open the AnnotationEdit form
            var editForm = new PlotView.AnnotationEdit(selectedAnnotation);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                if (editForm.Annotation.ShowHideAuto < 0)
                {
                    selectedTextObj.IsVisible = false;
                }
                else if (editForm.Annotation.ShowHideAuto > 0)
                {
                    selectedTextObj.IsVisible = true;
                }
                selectedTextObj.Text = editForm.Annotation.Text;

                currentInstance.Annotations.Add(editForm.Annotation);
            }
            else if (usingCustomAnnotation)
            {
                // recreate the annotation we thought we were replacing
                currentInstance.Annotations.Add(selectedAnnotation);
            }

            msPlot.ReevaluateAnnotations();
            msPlot.Invalidate();
            return;
        }

        // The following is copied from ZedGraph... use this to figure out how to save an image File
        public readonly List<string> SaveAsImageTypes = new() { ".emf", ".png", ".gif", ".jpg", ".tif", ".bmp" };

        /// <summary>
        /// Saves the Current Plot image as the file specified by the filename
        /// </summary>
        /// <param name="filePath">Output file path. If there is no Extension, it is assumed as .png</param>
        /// <param name="errorMessage">Output: error message</param>
        /// <returns>true if the save was successful, false otherwise</returns>
        public bool SavePlotImageAs(string filePath, out string errorMessage)
        {
            try
            {
                if (msPlot.MasterPane == null)
                {
                    errorMessage = "msPlot.MasterPane is null";
                    return false;
                }

                if (string.IsNullOrEmpty(filePath))
                {
                    errorMessage = "Plot file path is empty";
                    return false;
                }

                var ext = Path.GetExtension(filePath).ToLower();

                if (ext.EndsWith("emf"))
                {
                    return SaveEmfFile(filePath, out errorMessage);
                }

                using Stream myStream = new FileStream(filePath, FileMode.Create);

                if (myStream == null)
                {
                    errorMessage = "Unable to create a file stream for " + filePath;
                    return false;
                }

                ImageFormat format = ext switch
                {
                    "png" => ImageFormat.Png,
                    "gif" => ImageFormat.Gif,
                    "jpg" or "jpeg" => ImageFormat.Jpeg,
                    "tiff" or "tif" => ImageFormat.Tiff,
                    "bmp" => ImageFormat.Bmp,
                    _ => ImageFormat.Png,
                };

                msPlot.MasterPane.GetImage(msPlot.MasterPane.IsAntiAlias).Save(myStream, format);

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        // DLLs needed to save an EmfFile

        [DllImport("gdi32.dll")]
        private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, StringBuilder hNULL);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteEnhMetaFile(IntPtr hemf);

        /// <summary>
        /// Save the current Graph to the specified filename in EMF (vector) format.
        /// </summary>
        /// <param name="filePath">Plot file path</param>
        /// <param name="errorMessage">Output: error message</param>
        /// <remarks>
        /// Note that this handler saves as an Emf format only.
        /// </remarks>
        internal bool SaveEmfFile(string filePath, out string errorMessage)
        {
            try
            {
                using (var g = CreateGraphics())
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
                        var tempName = new StringBuilder(filePath);
                        CopyEnhMetaFile(hEMF, tempName);
                        DeleteEnhMetaFile(hEMF);
                    }

                    g.ReleaseHdc(hdc);
                }

                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Extra event for the closing of the options form so that we can update the plot to the user's new options
        /// </summary>
        public void UpdateObserver()
        {
            ResizeForm();
            msPlot.UpdateOptions();
            msPlot.IsEnableVZoom = !msPlot.mOptions.ZoomHorizontal;
            msPlot.IsShowContextMenu = !msPlot.mOptions.RightClickUnzoom;
            trackBarAnnotationPercent.Value = msPlot.mOptions.AnnotationPercent;
            if (!msPlot.mOptions.ShowSnappingCursor)
            {
                mzTextBox.Text = string.Empty;
                relativeIntensityTextBox.Text = string.Empty;
            }
            // msPlot.ReevaluateAnnotations();
            msPlot.Invalidate();
            Invalidate();
        }

        /// <summary>
        /// Fires when the user scrolls the Annotation Percent Slider
        /// This will fire an event in the plot to replot with a different level of annotations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrackBarAnnotationPercent_Scroll(object sender, EventArgs e)
        {
            msPlot.mOptions.AnnotationPercent = trackBarAnnotationPercent.Value;
            msPlot.ReevaluateAnnotations();
            msPlot.Invalidate();
        }

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
        /// Handles zooming out ZedGraph by calling the myZedGraph HandleZoomOut method
        /// </summary>
        public void HandleZoomOut()
        {
            msPlot.HandleZoomOut();
        }

        /// <summary>
        /// Focuses the plot on a specific point.  This is used for when the user clicks the fragment ladder and we want to highlight that point
        /// </summary>
        /// <param name="focusValue">The x value to focus on in the plot</param>
        public void FocusPlotOnPoint(double focusValue)
        {
            var arrowPoint = msPlot.mArrowPoint;

            if (msPlot.mArrowShowing && Math.Abs((float)focusValue - arrowPoint.X) < Single.Epsilon)
            {
                msPlot.mArrowShowing = false;
            }
            else
            {
                var offset = msPlot.mOptions.FocusOffset;

                // change the zoom of the graph
                var oldZoom = new ZoomState(msPlot.GraphPane, ZoomState.StateType.Zoom);
                msPlot.GraphPane.XAxis.Scale.Min = focusValue - offset;
                msPlot.GraphPane.XAxis.Scale.Max = focusValue + offset;
                msPlot.GraphPane.YAxis.Scale.MinAuto = true;
                msPlot.GraphPane.YAxis.Scale.MaxAuto = true;
                msPlot.GraphPane.ZoomStack.Add(oldZoom);

                // place a cursor below the axis
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
                if (point.Matched)
                {
                    matchedPoints.Add(point);
                }
                else
                {
                    unmatchedPoints.Add(point);
                }
            }

            msPlot.PlotGraph(peptide, scanNumber, unmatchedPoints, matchedPoints);
            msPlot.mArrowShowing = false;
            msPlot.ReevaluateAnnotations();
            msPlot.Invalidate();
            msPlot.Update();
        }

        public void ButtonDetachPlot_Click(object sender, EventArgs e)
        {
            if (mManager.mMainForm.mCurrentOptions.IsPlotInMainForm)             // If plot is attached.
            {
                mManager.mMainForm.mCurrentOptions.IsPlotInMainForm = false;     // Set to not be attached.
                buttonDetachPlot.Text = "Attach Plot";
            }
            else
            {
                mManager.mMainForm.mCurrentOptions.IsPlotInMainForm = true;
                buttonDetachPlot.Text = "Detach Plot";
            }
        }

        private void ButtonPlotOptions_Click(object sender, EventArgs e)
        {
            mManager.OpenOptionsMenu("Plot Options");
        }

        private void NumberOfPlots_TextChanged(object sender, EventArgs e)
        {
            var str = numberOfPlots.Text.Trim();
            int numPlots;
            if (int.TryParse(str, out numPlots))
            {
                // mManager.mMainForm.mCurrentOptions.numberOfPlotsChangedOnForm = numPlots;
            }
            msPlot.Invalidate();
            msPlot.Update();
        }
    }
}