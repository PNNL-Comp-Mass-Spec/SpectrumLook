using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    /// <summary>
    /// Holds all the various option information needed for the plot, this is passed between the plot and the menu in order to communicate
    /// </summary>
    [Serializable]
    public class PlotOptions : Subject
    {
        public bool ShowSnappingCursor
        {
            get => mShowSnappingCursor;
            set
            {
                mShowSnappingCursor = value;
                Invoke();
            }
        }
        public bool ShowLegend
        {
            get => mShowLegend;
            set
            {
                mShowLegend = value;
                Invoke();
            }
        }
        public int AnnotationPercent
        {
            get => mAnnotationPercent;
            set
            {
                mAnnotationPercent = value;
                Invoke();
            }
        }
        public int AnnotationTextSize
        {
            get => mAnnotationTextSize;
            set
            {
                mAnnotationTextSize = value;
                Invoke();
            }
        }
        public Color AnnotationColor
        {
            get => mAnnotationColor;
            set
            {
                mAnnotationColor = value;
                Invoke();
            }
        }
        public Color MatchedColor
        {
            get => mMatchedColor;
            set
            {
                mMatchedColor = value;
                Invoke();
            }
        }
        public Color UnmatchedColor
        {
            get => mUnmatchedColor;
            set
            {
                mUnmatchedColor = value;
                Invoke();
            }
        }
        public bool ZoomHorizontal
        {
            get => mZoomHorizontal;
            set
            {
                mZoomHorizontal = value;
                Invoke();
            }
        }
        public bool HideUnmatched
        {
            get => mHideUnmatched;
            set
            {
                mHideUnmatched = value;
                Invoke();
            }
        }
        public Keys UnzoomKey
        {
            get => mUnzoomKey;
            set
            {
                mUnzoomKey = value;
                Invoke();
            }
        }
        public int FocusOffset
        {
            get => mFocusOffset;
            set
            {
                mFocusOffset = value;
                Invoke();
            }
        }
        public bool RightClickUnzoom
        {
            get => mRightClickUnzoom;
            set
            {
                mRightClickUnzoom = value;
                Invoke();
            }
        }
        public bool HidePlotTools
        {
            get => mHidePlotTools;
            set
            {
                mHidePlotTools = value;
                Invoke();
            }
        }
        public int NumberOfPlots
        {
            get
            {
                if (mNumberOfPlots < 1 || mNumberOfPlots > 5)
                {
                    return 1;
                }
                return mNumberOfPlots;
            }
            set
            {
                mNumberOfPlots = value;
                Invoke();
            }
        }

        /// <summary>
        /// a flag we will use to tell if we need to re0plot during an update
        /// </summary>
        public bool Replot = false;

        private bool mShowSnappingCursor;
        private bool mShowLegend;
        private int mAnnotationPercent;
        private int mAnnotationTextSize;
        private Color mAnnotationColor;
        private Color mMatchedColor;
        private Color mUnmatchedColor;
        private bool mZoomHorizontal;
        private bool mHideUnmatched;
        private Keys mUnzoomKey;
        private int mFocusOffset;
        private bool mRightClickUnzoom;
        private bool mHidePlotTools;
        private int mNumberOfPlots;

        /// <summary>
        /// Constructor, sets values to initial settings
        /// </summary>
        public PlotOptions()
        {
            mShowSnappingCursor = true;
            mShowLegend = true;
            mAnnotationPercent = 30;
            mAnnotationTextSize = 8;
            mAnnotationColor = Color.Black;
            mMatchedColor = Color.Red;
            mUnmatchedColor = Color.Black;
            mHideUnmatched = false;
            mUnzoomKey = Keys.Back;
            mFocusOffset = 100;
            mRightClickUnzoom = true;
            mHidePlotTools = false;
            mNumberOfPlots = 1;
        }

        /// <summary>
        /// copy constructor, sets the values to the same values in the options that are passed in
        /// </summary>
        /// <param name="options"></param>
        public PlotOptions(PlotOptions options)
        {
            showSnappingCursor = options.showSnappingCursor;
            showLegend = options.showLegend;
            annotationPercent = options.annotationPercent;
            annotationTextSize = options.annotationTextSize;
            annotationColor = options.annotationColor;
            zoomHorizontal = options.zoomHorizontal;
            hideUnmatched = options.hideUnmatched;
            matchedColor = options.matchedColor;
            unmatchedColor = options.unmatchedColor;
            unzoomKey = options.unzoomKey;
            focusOffset = options.focusOffset;
            rightClickUnzoom = options.rightClickUnzoom;
            hidePlotTools = options.hidePlotTools;
            numberOfPlots = options.numberOfPlots;
        }

        public void CopyOptions(PlotOptions options)
        {
            showSnappingCursor = options.showSnappingCursor;
            showLegend = options.showLegend;
            annotationPercent = options.annotationPercent;
            annotationTextSize = options.annotationTextSize;
            annotationColor = options.annotationColor;
            zoomHorizontal = options.zoomHorizontal;
            hideUnmatched = options.hideUnmatched;
            matchedColor = options.matchedColor;
            unmatchedColor = options.unmatchedColor;
            unzoomKey = options.unzoomKey;
            focusOffset = options.focusOffset;
            rightClickUnzoom = options.rightClickUnzoom;
            hidePlotTools = options.hidePlotTools;
            numberOfPlots = options.numberOfPlots;
        }
    }
}
