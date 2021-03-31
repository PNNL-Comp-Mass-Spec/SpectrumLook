using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using ZedGraph;
using System.Windows.Forms;
using SpectrumLook.Builders;

namespace SpectrumLook.Views
{
    /// <summary>
    /// Delegate used so that the plot can update the value of the snapping cursor position.
    /// </summary>
    /// <param name="newPosition"></param>
    public delegate void UpdatePointDelegate(PointF newPosition);

    public class MyZedGraph : ZedGraphControl
    {
        private readonly int mSnapBoxSize = 5;
        private Color mSnapBoxColor = Color.Black;
        private Point mSnapBoxPosition;
        private bool mSnapShowing;
        public bool mArrowShowing;
        public PointF mArrowPoint;
        private PointPairList mUnmatchedPoints;
        private PointPairList mMatchedPoints;
        private string mCurrentPeptide;
        private string mCurrentScanNumber;
        public PlotOptions mOptions;
        public UpdatePointDelegate mUpdateCursorCallback;
        public Manager mManager;

        private const string unmatchedCurveName = "Unmatched Peaks";
        private const string matchedCurveName = "Matched Peaks";

        /// <summary>
        /// The constructor for our custom ZedGraph
        /// </summary>
        public MyZedGraph()
        {
            mSnapBoxPosition.X = 0;
            mSnapBoxPosition.Y = 0;

            ZoomEvent += MyZedGraph_ZoomEvent;
            Resize += MyZedGraph_Resize;
            InitializeGraph();
        }

        /// <summary>
        /// Sets the Title, X, and Y axis text in the graph
        /// </summary>
        /// <param name="zgc"></param>
        private void InitializeGraph()
        {
            GraphPane.Title.Text = "Scan: ";
            GraphPane.XAxis.Title.Text = "m/z";
            GraphPane.YAxis.Title.Text = "Relative Intensity";
        }

        /// <summary>
        /// Sets the size for the graph in proportion to the Form that it is in
        /// </summary>
        public void SetSize(Point position, int width, int height)
        {
            // the top right corner location of the graph
            Location = position;

            Size = new Size(width, height);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            //
            // MyZedGraph
            //
            AutoScaleDimensions = new SizeF(6F, 13F);
            Name = "MyZedGraph";
            Size = new Size(151, 150);
            ResumeLayout(false);
        }

        /// <summary>
        /// ZedGraph event that fires when the mouse moves.  We use this to paint the snapping cursor over the graph.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (mOptions.ShowSnappingCursor)
            {
                var mousePosition = new PointF(e.X, e.Y);
                UpdateSnappingCursor(mousePosition);
            }
        }

        public void UpdateSnappingCursor(PointF mousePosition)
        {
            PointPair closestPoint;
            if (FindClosestPoint(mousePosition, out closestPoint, out var closestPane))
            {
                var graphPoint = new PointF((float)closestPoint.X, (float)closestPoint.Y);
                var boxPoint = closestPane.GeneralTransform(graphPoint, CoordType.AxisXYScale);
                mSnapBoxPosition.X = (int)boxPoint.X - mSnapBoxSize / 2;
                mSnapBoxPosition.Y = (int)boxPoint.Y - mSnapBoxSize / 2;

                if (mUpdateCursorCallback != null)
                {
                    mUpdateCursorCallback(graphPoint);
                }

                mSnapShowing = true;
                Invalidate();
            }
            else
            {
                if (mSnapShowing)
                {
                    mSnapShowing = false;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Finds the closest point in the plot to the mouse position and assigns it to closestPoint
        /// </summary>
        /// <param name="mousePosition">The current mouse position</param>
        /// <param name="closestPoint">will either be the closest point or an empty point if one is not found</param>
        /// <returns>True if a point is found, False otherwise</returns>
        public bool FindClosestPoint(PointF mousePosition, out PointPair closestPoint, out GraphPane closestPane)
        {
            closestPoint = null;

            var unmatchedClosest = new PointPair();
            var matchedClosest = new PointPair();

            closestPane = MasterPane.FindPane(mousePosition);

            if (closestPane == null)
            {  // if we couldn't get the pane then we can't find the point
                return false;
            }

            // reverseTransform converts the mouse position to the point on a graph
            closestPane.ReverseTransform(mousePosition, out var graphX, out var graphY);

            var mousePositionPP = new PointPair(graphX, graphY);

            matchedClosest = GetClosestPointInCurve(mousePositionPP, closestPane.CurveList[matchedCurveName]);
            if (!mOptions.HideUnmatched)
            {
                unmatchedClosest = GetClosestPointInCurve(mousePositionPP, closestPane.CurveList[unmatchedCurveName]);
            }

            // determine which of the points we found was closer
            if (CalculateDistance(mousePositionPP, matchedClosest) > CalculateDistance(mousePositionPP, unmatchedClosest))
            {
                // unmatched is closest
                closestPoint = unmatchedClosest;
                mSnapBoxColor = mOptions.UnmatchedColor;
            }
            else
            {
                // matched is closest
                closestPoint = matchedClosest;
                mSnapBoxColor = mOptions.MatchedColor;
            }

            var foundClosest = closestPoint.X != 0 || closestPoint.Y != 0;

            return foundClosest;
        }

        /// <summary>
        /// Retrieves the closest point to the mouse point in the curve
        /// </summary>
        private PointPair GetClosestPointInCurve(PointPair mousePoint, CurveItem curve)
        {
            var closestPoint = new PointPair();
            if (curve != null)
            {
                var toSearch = curve.Points;
                var closestDistance = GraphPane.XAxis.Scale.Max;

                if (toSearch != null && toSearch.Count > 0)
                {
                    for (var i = 0; i < toSearch.Count; i++)
                    {
                        var point = toSearch[i];
                        var tempDist = CalculateDistance(mousePoint, point);
                        if (tempDist < closestDistance)
                        {
                            closestDistance = tempDist;
                            closestPoint = point;
                        }
                    }
                }
            }

            return closestPoint;
        }

        /// <summary>
        /// Simply Calculates the distance between the two points on the XY plane
        /// </summary>
        private double CalculateDistance(PointPair pointA, PointPair pointB)
        {
            return Math.Sqrt(Math.Pow(pointB.X - pointA.X, 2) + Math.Pow(pointB.Y - pointA.Y, 2));
        }

        /// <summary>
        /// ZedGraph event that fires the moment when the mouse is no longer above the form.  We use this to know when to stop painting the snapping cursor
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mSnapShowing = false;
            Invalidate();
        }

        /// <summary>
        /// Draws the Snapping cursor at the given XY coordinate
        /// </summary>
        /// <param name="xcoord">The x coordinate on the form where the snapping point will go</param>
        /// <param name="ycoord">The y coordinate on the form where the snapping point will go</param>
        public void DrawSnapCursor(Point boxPosition, PaintEventArgs e)
        {
            var snapBrush = new SolidBrush(mSnapBoxColor);
            var snapRect = new Rectangle(boxPosition.X, boxPosition.Y, mSnapBoxSize, mSnapBoxSize);

            e.Graphics.FillRectangle(snapBrush, snapRect);
        }

        /// <summary>
        /// Returns the position of the snapping cursor for other methods to use
        /// </summary>
        public Point GetSnapCursorPosition()
        {
            return mSnapBoxPosition;
        }

        /// <summary>
        /// Adds annotations from the matchedPoints to the Graph Pane
        /// Plot Options specify how much of the top % of annotations to display, along with size and color.
        /// </summary>
        /// <param name="pointsList"></param>
        /// <param name="myPane"></param>
        private void AddAnnotations(IPointList pointsList, PaneBase myPane)
        {
            // add the annotations for the matched items
            double offset = 5;
            var minIntensityToDisplay = FindMinIntensityToDisplay(pointsList);
            var currentInstance = mManager.GetCurrentInstance();

            for (var i = 0; i < pointsList.Count; i++)
            {
                var usingCustomAnnotation = false;
                var text = new TextObj();

                // look for if the user has defined a custom annotation if they have we deal with that instead of making a new one
                for (var j = 0; j < currentInstance.Annotations.Count; j++)
                {
                    if (Math.Abs(currentInstance.Annotations[j].Point.X - pointsList[i].X) < 0.001 &&
                        Math.Abs(currentInstance.Annotations[j].Point.Y - pointsList[i].Y) < 0.001)
                    {
                        usingCustomAnnotation = true;
                        var customAnnotation = currentInstance.Annotations[j];
                        var pt = pointsList[i];

                        // Create a text label from the Y data value
                        text = new TextObj(customAnnotation.Text, pt.X, pt.Y + offset, CoordType.AxisXYScale, AlignH.Left, AlignV.Center)
                        {
                            Tag = (object)pt
                        };

                        // Store the point into the text object's tag

                        if (customAnnotation.ShowHideAuto > 0)
                        {
                            // Always show this annotation
                            text.IsVisible = true;
                        }
                        else if (customAnnotation.ShowHideAuto < 0)
                        {
                            // Always hide this annotation
                            text.IsVisible = false;
                        }
                        else if (pt.Y <= minIntensityToDisplay)
                        {
                            // Auto Determine if we are going to show the annotation for this point
                            text.IsVisible = false;
                        }

                        break;
                    }
                }

                if (!usingCustomAnnotation)
                {
                    // Get the pointpair
                    var pt = pointsList[i];

                    // Create a text label from the Y data value
                    string tagText;
                    if (pt.Tag != null)
                    {
                        tagText = pt.Tag as string;
                    }
                    else
                    {
                        tagText = string.Empty;
                    }

                    text = new TextObj(tagText, pt.X, pt.Y + offset, CoordType.AxisXYScale, AlignH.Left, AlignV.Center)
                    {
                        Tag = (object)pt
                    };

                    // Store the point into the text object's tag

                    // Determine if we are going to show the annotation for this point
                    if (pt.Y <= minIntensityToDisplay)
                    {
                        text.IsVisible = false;
                    }
                }

                text.IsClippedToChartRect = true; // set true because we want the annotations to hide when they go off the borders of the graph
                text.FontSpec.Size = mOptions.AnnotationTextSize;
                text.FontSpec.FontColor = mOptions.AnnotationColor;
                text.ZOrder = ZOrder.C_BehindChartBorder;

                // Hide the border and the fill
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;

                // Rotate the text to 90 degrees
                text.FontSpec.Angle = 90;

                myPane.GraphObjList.Add(text);
            }
        }

        /// <summary>
        /// Fires just after the form is done zooming.  Reevaluates the annotations for the new scale
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="oldState"></param>
        /// <param name="newState"></param>
        private void MyZedGraph_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            if (mManager.DataLoaded)
            {
                ReevaluateAnnotations();
            }
            // mArrowShowing = false;
        }

        /// <summary>
        /// Based on how the user zoomed in the plot, we will need to re-evaluate how to hide/show annotations.
        /// </summary>
        public void ReevaluateAnnotations()
        {
            foreach (var pane in MasterPane.PaneList)
            {
                var minY = pane.YAxis.Scale.Min;
                if (!mOptions.ZoomHorizontal) // For box zoom (non horizontal zoom) change the YAxis to 0 to keep the X axis
                {
                    pane.YAxis.Scale.Min = 0;
                    minY = 0;
                }
                var minX = pane.XAxis.Scale.Min;
                var maxX = pane.XAxis.Scale.Max;

                var maxY = pane.YAxis.Scale.Max;
                try
                {
                    var visibleMatchedPoints = GetVisiblePoints(pane.CurveList[matchedCurveName].Points, pane.CurveList[unmatchedCurveName].Points, minX, maxX, minY, maxY);
                    var minIntensityToDisplay = FindMinIntensityToDisplay(visibleMatchedPoints);
                    foreach (TextObj text in pane.GraphObjList)
                    {
                        var pt = (PointPair)text.Tag;
                        var customAnnotation = new Annotation();

                        foreach (var annotation in mManager.GetCurrentInstance().Annotations)
                        {
                            if (annotation.Point.X == pt.X &&
                             annotation.Point.Y == pt.Y)
                            {
                                customAnnotation = annotation;
                                break;
                            }
                        }

                        if (customAnnotation.ShowHideAuto > 0)
                        {
                            // Always show this annotation
                            text.IsVisible = true;
                        }
                        else if (customAnnotation.ShowHideAuto < 0)
                        {
                            // Always hide this annotation
                            text.IsVisible = false;
                        }
                        else if (pt.Y <= minIntensityToDisplay)
                        {
                            // Auto Determine if we are going to show the annotation for this point
                            text.IsVisible = false;
                        }
                        else
                        {
                            text.IsVisible = true;
                        }
                    }
                }
                catch
                {
                    // Do nothing upon catch
                }
            }
        }

        /// <summary>
        /// Returns a list of the points that are visible in the current window
        /// </summary>
        private PointPairList GetVisiblePoints(IPointList unmatchedPoints, IPointList matchedPoints, double minX, double maxX, double minY, double maxY)
        {
            var visiblePointsList = GetVisiblePoints(matchedPoints, minX, maxX, minY, maxY);
            visiblePointsList.AddRange(GetVisiblePoints(unmatchedPoints, minX, maxX, minY, maxY));

            return visiblePointsList;
        }

        /// <summary>
        /// Returns a list of the points that are visible in the given points list
        /// </summary>
        private PointPairList GetVisiblePoints(IPointList points, double minX, double maxX, double minY, double maxY)
        {
            var visibleList = new PointPairList();

            if (points != null)
            {
                for (var i = 0; i < points.Count; i++)
                {
                    var point = points[i];

                    if (point.X < maxX && point.X > minX && point.Y < maxY && point.Y > minY)
                    {
                        visibleList.Add(point);
                    }
                }
            }

            return visibleList;
        }

        /// <summary>
        /// From a list of matched points, finds the lowest value of relative intensity that will satisfy the top percentage requirement
        ///
        /// Example, if our set was { 1, 5, 7, 3} and we wanted the top 50% of values, this method would return 5.
        /// </summary>
        /// <param name="pointsList"></param>
        /// <returns></returns>
        private double FindMinIntensityToDisplay(IPointList pointsList)
        {
            var minIntensity = 0.0;

            var numAnnotationsToHide = Convert.ToInt32(pointsList.Count * ((double)mOptions.AnnotationPercent / 100.0));
            var values = new List<double>();

            for (var i = 0; i < pointsList.Count; i++)
            {
                values.Add(pointsList[i].Y);
            }
            values.Sort();

            var selectIndex = values.Count - numAnnotationsToHide - 1;
            if (selectIndex >= 0)
            {
                minIntensity = values[selectIndex];
            }

            return minIntensity;
        }

        private const float mStandardBaseDemension = 7.0F;

        /// <summary>
        /// Creates a plot and places it into ZedGraph
        /// </summary>
        public void PlotGraph(string peptide, string scanNumber, List<Element> unmatchedPointsList, List<Element> matchedPointsList)
        {
            var unmatchedPoints = MakePointPairList(unmatchedPointsList);
            var matchedPoints = MakePointPairList(matchedPointsList);

            PlotGraph(peptide, scanNumber, unmatchedPoints, matchedPoints);
        }

        /// <summary>
        /// Creates a new plot in the zedGraph Control
        /// </summary>
        /// <param name="peptide"></param>
        /// <param name="scanNumber"></param>
        /// <param name="unmatchedPointsList"></param>
        /// <param name="matchedPointsList"></param>
        public void PlotGraph(string peptide, string scanNumber, PointPairList unmatchedPointsList, PointPairList matchedPointsList)
        {
            // save the data
            mCurrentPeptide = peptide;
            mCurrentScanNumber = scanNumber;
            mUnmatchedPoints = unmatchedPointsList;
            mMatchedPoints = matchedPointsList;

            // clear the masterPane
            var master = MasterPane;
            master.GraphObjList.Clear();
            master.PaneList.Clear();

            // split the points into groups

            // Divides the points into sections, this is used when we create more than one plot
            DividePointsIntoSections(mOptions.NumberOfPlots, mMatchedPoints, mUnmatchedPoints, out var matchedPointsSection, out var unmatchedPointsSection);

            // Show the master pane title
            master.Title.IsVisible = true;
            master.Title.Text = "Peptide: " + peptide + " Scan: " + scanNumber;

            // Leave a margin around the master pane, but only a small gap between panes
            master.Margin.All = 10;
            master.InnerPaneGap = 5;

            for (var j = 0; j < mOptions.NumberOfPlots; j++)
            {
                // Create a new graph -- dimensions to be set later by MasterPane Layout
                var myPaneT = new GraphPane(new Rectangle(10, 10, 10, 10),
                   "",
                   "m/z",
                   "Relative Intensity");

                // Set the BaseDimension, so fonts are scale a little bigger
                myPaneT.BaseDimension = mStandardBaseDemension / mOptions.NumberOfPlots;

                // Hide the XAxis scale and title
                myPaneT.XAxis.Title.IsVisible = false;
                myPaneT.XAxis.Scale.IsVisible = false;
                // Hide the legend, border, and GraphPane title
                myPaneT.Legend.IsVisible = false;
                myPaneT.Border.IsVisible = false;
                myPaneT.Title.IsVisible = false;

                // Restrict the scale to go right up to the last data point
                double matchedMax = 0;
                double unmatchedMax = 0;
                var matchedMin = double.MaxValue;
                var unmatchedMin = double.MaxValue;

                if (matchedPointsSection[j].Count > 0)
                {
                    matchedMax = matchedPointsSection[j][matchedPointsSection[j].Count - 1].X;
                    matchedMin = matchedPointsSection[j][0].X;
                }
                if (unmatchedPointsSection[j].Count > 0)
                {
                    unmatchedMax = unmatchedPointsSection[j][unmatchedPointsSection[j].Count - 1].X;
                    unmatchedMin = unmatchedPointsSection[j][0].X;
                }

                myPaneT.XAxis.Scale.Max = matchedMax > unmatchedMax ? matchedMax : unmatchedMax;
                myPaneT.XAxis.Scale.Min = matchedMin < unmatchedMin ? matchedMin : unmatchedMin;

                // Remove all margins
                myPaneT.Margin.All = 0;
                // Except, leave some top margin on the first GraphPane
                if (j == 0)
                {
                    myPaneT.XAxis.Scale.Min = myPaneT.XAxis.Scale.Min - 100;
                    myPaneT.Margin.Top = 20;
                }
                // And some bottom margin on the last GraphPane
                // Also, show the X title and scale on the last GraphPane only
                if (j == mOptions.NumberOfPlots - 1)
                {
                    myPaneT.XAxis.Scale.Max = myPaneT.XAxis.Scale.Max + 100;
                    myPaneT.XAxis.Title.IsVisible = true;
                    myPaneT.Legend.IsVisible = mOptions.ShowLegend;
                    myPaneT.Legend.Position = LegendPos.BottomCenter;
                }
                myPaneT.XAxis.Scale.IsVisible = true;
                // myPaneT.Margin.Bottom = 10;
                if (j > 0)
                {
                    myPaneT.YAxis.Scale.IsSkipLastLabel = true;
                }
                // This sets the minimum amount of space for the left and right side, respectively
                // The reason for this is so that the Chart Rect's all end up being the same size.
                myPaneT.YAxis.MinSpace = 80;
                myPaneT.Y2Axis.MinSpace = 20;

                // generate the lines
                // Keep the matched points in front by drawing them first.
                var matchedCurve = myPaneT.AddOHLCBar(matchedCurveName, matchedPointsSection[j], mOptions.MatchedColor);
                matchedCurve.Bar.Width = 2;
                AddAnnotations(matchedCurve.Points, myPaneT);
                if (!mOptions.HideUnmatched)
                {
                    var unmatchedCurve = myPaneT.AddOHLCBar(unmatchedCurveName, unmatchedPointsSection[j], mOptions.UnmatchedColor);
                    AddAnnotations(unmatchedCurve.Points, myPaneT);
                }

                // Add the GraphPane to the MasterPane.PaneList
                master.Add(myPaneT);
            }

            // Tell ZedGraph to re layout the axes since the data has changed
            using (var g = CreateGraphics())
            {
                // Align the GraphPanes vertically
                if (mOptions.NumberOfPlots >= 4)
                {
                    master.SetLayout(g, PaneLayout.SquareColPreferred);
                }
                else
                {
                    master.SetLayout(g, PaneLayout.SingleColumn);
                }
                master.AxisChange(g);
                PerformAutoScale();
            }
        }

        /// <summary>
        /// Handles reassigning all of the options in case they have been changed
        /// </summary>
        public void UpdateOptions()
        {
            if (mUnmatchedPoints != null && mMatchedPoints != null && mUnmatchedPoints.Count != 0 && mMatchedPoints.Count != 0)
            {
                SuspendLayout();

                if (mOptions.Replot)
                {
                    PlotGraph(mCurrentPeptide, mCurrentScanNumber, mUnmatchedPoints, mMatchedPoints);
                    mOptions.Replot = false;
                }
                else
                {
                    foreach (var myPane in MasterPane.PaneList)
                    {
                        // PointPairList oldUnmatchedPoints = (PointPairList)myPane.CurveList["Unmatched Peaks"].Points;
                        // PointPairList oldMatchedPoints = (PointPairList)myPane.CurveList["Matched Peaks"].Points;

                        myPane.CurveList.Clear();
                        myPane.GraphObjList.Clear();

                        if (!mOptions.HideUnmatched)
                        {
                            var unmatchedCurve = myPane.AddOHLCBar("Unmatched Peaks", mUnmatchedPoints, mOptions.UnmatchedColor);
                            AddAnnotations(unmatchedCurve.Points, myPane);
                        }
                        var matchedCurve = myPane.AddOHLCBar("Matched Peaks", mMatchedPoints, mOptions.MatchedColor);
                        AddAnnotations(matchedCurve.Points, myPane);
                    }
                }

                ResumeLayout();
            }
        }

        private void DividePointsIntoSections(int numberSections, PointPairList originalMatched, PointPairList originalUnmatched, out List<PointPairList> matchedSections, out List<PointPairList> unmatchedSections)
        {
            // for now just to see how this looks, we are going to just duplicate the matched + unmatched lists into the sections
            unmatchedSections = new List<PointPairList>();
            matchedSections = new List<PointPairList>();

            // since this will be very common, we will make a quick getaway with it
            if (numberSections <= 1)
            {
                unmatchedSections.Add(originalUnmatched);
                matchedSections.Add(originalMatched);
                return;
            }

            var totNumOfPoints = originalMatched.Count + originalUnmatched.Count;
            var pointsPerSection = totNumOfPoints / numberSections;
            int unmatchedIndex = 0, matchedIndex = 0;

            for (var i = 0; i < numberSections; i++)
            {
                var tempUnmatched = new PointPairList();
                var tempMatched = new PointPairList();

                // here goes hoping that the lists are sorted...
                for (var j = 0; j < pointsPerSection; j++)
                {
                    var nextMatched = matchedIndex < originalMatched.Count ? originalMatched[matchedIndex].X : originalUnmatched[originalUnmatched.Count - 1].X + 1;
                    var nextUnmatched = unmatchedIndex < originalUnmatched.Count ? originalUnmatched[unmatchedIndex].X : originalMatched[originalMatched.Count - 1].X + 1;

                    if (nextUnmatched < nextMatched)
                    {
                        tempUnmatched.Add(originalUnmatched[unmatchedIndex]);
                        unmatchedIndex++;
                    }
                    else
                    {
                        tempMatched.Add(originalMatched[matchedIndex]);
                        matchedIndex++;
                    }
                }

                unmatchedSections.Add(tempUnmatched);
                matchedSections.Add(tempMatched);
            }

            // add points to the last section that may have been rounded off
            while (unmatchedIndex < originalUnmatched.Count)
            {
                unmatchedSections[numberSections - 1].Add(originalUnmatched[unmatchedIndex]);
                unmatchedIndex++;
            }
            while (matchedIndex < originalMatched.Count)
            {
                matchedSections[numberSections - 1].Add(originalMatched[matchedIndex]);
                matchedIndex++;
            }
        }

        /// <summary>
        /// Converts a List of Point to the PointPairList that zedGraphUses
        /// </summary>
        /// <param name="points"></param>
        private PointPairList MakePointPairList(List<Element> points)
        {
            var newList = new PointPairList();

            foreach (var point in points)
            {
                if (string.IsNullOrEmpty(point.Annotation))
                {
                    newList.Add(point.Mz, point.Intensity);
                }
                else
                {
                    newList.Add(point.Mz, point.Intensity, point.Annotation);
                }
            }

            return newList;
        }

        /// <summary>
        /// Sets the drawArrow flag to true so that the plot knows to draw the arrow, also sets the point the arrow is drawn at
        /// </summary>
        /// <param name="drawPoint">the graph coordinates of where to draw the arrow</param>
        public void PaintArrow(PointF graphPoint)
        {
            mArrowShowing = true;
            mArrowPoint.X = graphPoint.X;
            mArrowPoint.Y = graphPoint.Y;
        }

        /// <summary>
        /// Handles drawing a vertical arrow pointing up on the screen
        /// </summary>
        /// <param name="arrowPoint">the location of the tip of the arrow</param>
        public void DrawArrow(PointF arrowPoint, PaintEventArgs e)
        {
            const int penThickness = 7;
            var Draw = true;

            if (arrowPoint.X > GraphPane.XAxis.Scale.Max || arrowPoint.X < GraphPane.XAxis.Scale.Min ||
                arrowPoint.Y > GraphPane.YAxis.Scale.Max || arrowPoint.Y < GraphPane.YAxis.Scale.Min)
            {
                Draw = false;
            }

            if (Draw)
            {
                var g = e.Graphics;
                var drawPoint = GraphPane.GeneralTransform(arrowPoint, CoordType.AxisXYScale);

                g.SmoothingMode = SmoothingMode.AntiAlias;

                var p = new Pen(Color.Black, penThickness);
                p.StartCap = LineCap.Square;
                p.EndCap = LineCap.ArrowAnchor;

                // These are the coordinates on the screen of where the arrow will start and End
                var pointStart = new Point((int)(drawPoint.X + .5), (int)(drawPoint.Y + 20.5));
                var pointEnd = new Point((int)(drawPoint.X + .5), (int)(drawPoint.Y + .5));

                g.DrawLine(p, pointStart, pointEnd);

                g.DrawString(arrowPoint.X.ToString("0.0"), new Font(FontFamily.GenericSerif, 8), Brushes.Black, new PointF(pointStart.X - penThickness, pointStart.Y + 3));
                // p.Dispose();
            }
        }

        /// <summary>
        /// ZedGraph paint override to paint what we want on top of the form
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (mSnapShowing && mOptions.ShowSnappingCursor)
            {
                DrawSnapCursor(mSnapBoxPosition, e);
            }
            if (mArrowShowing)
            {
                DrawArrow(mArrowPoint, e);
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (!mOptions.RightClickUnzoom)
            {
                base.OnMouseClick(e);
            }
            else
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (mManager.DataLoaded)
                    {
                        HandleZoomOut();
                    }
                }
                else
                {
                    base.OnMouseClick(e);
                }
            }
        }

        /// <summary>
        /// Event that fires when the control is resized
        /// </summary>
        private void MyZedGraph_Resize(object sender, EventArgs e)
        {
            // mArrowShowing = false;
        }

        /// <summary>
        /// event that fires when the mouse enters the control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mArrowShowing = false;
        }

        /// <summary>
        /// Handles zooming out the plot when the user hits the zoom out button while the mouse is over the plot
        /// </summary>
        public void HandleZoomOut()
        {
            var cursorPos = Cursor.Position;
            var cursorPosF = new PointF(cursorPos.X, cursorPos.Y);
            GraphPane closestPane;

            closestPane = FindNearestPane(cursorPosF);
            if (closestPane != null)
            {
                if (closestPane.ZoomStack.IsEmpty)
                {
                    // for some reason, the stack can be empty when it should have a value... so we will just re-plot
                    PlotGraph(mCurrentPeptide, mCurrentScanNumber, mUnmatchedPoints, mMatchedPoints);
                }
                else
                {
                    ZoomOut(closestPane);
                }
                UpdateSnappingCursor(cursorPosF);
            }
        }

        /// <summary>
        /// Locates the nearest pane to the mouse position
        /// </summary>
        /// <param name="mousePt"></param>
        /// <returns></returns>
        private GraphPane FindNearestPane(PointF mousePt)
        {
            var closestDistance = double.MaxValue;
            GraphPane closest = null;

            foreach (var pane in MasterPane.PaneList)
            {
                var paneCenter = new PointPair(pane.Rect.Location.X + .5 * pane.Rect.Width, pane.Rect.Location.Y + .5 * pane.Rect.Height);
                var paneDistance = CalculateDistance(paneCenter, new PointPair(mousePt));
                if (paneDistance < closestDistance)
                {
                    closest = pane;
                }
            }

            return closest;
        }
    }
}
