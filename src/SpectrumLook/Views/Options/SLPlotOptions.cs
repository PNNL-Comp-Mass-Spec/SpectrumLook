using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpectrumLook.Views
{
    /// <summary>
    /// Holds all the various option information needed for the plot, this is passed inbetween the plot and the menu in order to communicate
    /// </summary>
    [Serializable]
    public class PlotOptions : Subject
    {
        public bool showSnappingCursor
        {
            get => m_showSnappingCursor;
            set
            {
                m_showSnappingCursor = value;
                Invoke();
            }
        }
        public bool showLegend
        {
            get => m_showLegend;
            set
            {
                m_showLegend = value;
                Invoke();
            }
        }
        public int annotationPercent
        {
            get => m_annotationPercent;
            set
            {
                m_annotationPercent = value;
                Invoke();
            }
        }
        public int annotationTextSize
        {
            get => m_annotationTextSize;
            set
            {
                m_annotationTextSize = value;
                Invoke();
            }
        }
        public Color annotationColor
        {
            get => m_annotationColor;
            set
            {
                m_annotationColor = value;
                Invoke();
            }
        }
        public Color matchedColor
        {
            get => m_matchedColor;
            set
            {
                m_matchedColor = value;
                Invoke();
            }
        }
        public Color unmatchedColor
        {
            get => m_unmatchedColor;
            set
            {
                m_unmatchedColor = value;
                Invoke();
            }
        }
        public bool zoomHorizontal
        {
            get => m_zoomHorizontal;
            set
            {
                m_zoomHorizontal = value;
                Invoke();
            }
        }
        public bool hideUnmatched
        {
            get => m_hideUnmatched;
            set
            {
                m_hideUnmatched = value;
                Invoke();
            }
        }
        public Keys unzoomKey
        {
            get => m_unzoomKey;
            set
            {
                m_unzoomKey = value;
                Invoke();
            }
        }
        public int focusOffset
        {
            get => m_focusOffset;
            set
            {
                m_focusOffset = value;
                Invoke();
            }
        }
        public bool rightClickUnzoom
        {
            get => m_rightClickUnzoom;
            set
            {
                m_rightClickUnzoom = value;
                Invoke();
            }
        }
        public bool hidePlotTools
        {
            get => m_hidePlotTools;
            set
            {
                m_hidePlotTools = value;
                Invoke();
            }
        }
        public int numberOfPlots
        {
            get
            {
                if (m_numberOfPlots < 1 || m_numberOfPlots > 5)
                {
                    return 1;
                }
                return m_numberOfPlots;
            }
            set
            {
                m_numberOfPlots = value;
                Invoke();
            }
        }

        /// <summary>
        /// a flag we will use to tell if we need to replot during an update
        /// </summary>
        public bool replot = false;

        private bool m_showSnappingCursor;
        private bool m_showLegend;
        private int m_annotationPercent;
        private int m_annotationTextSize;
        private Color m_annotationColor;
        private Color m_matchedColor;
        private Color m_unmatchedColor;
        private bool m_zoomHorizontal;
        private bool m_hideUnmatched;
        private Keys m_unzoomKey;
        private int m_focusOffset;
        private bool m_rightClickUnzoom;
        private bool m_hidePlotTools;
        private int m_numberOfPlots;

        /// <summary>
        /// constructor, sets values to initial settings
        /// </summary>
        public PlotOptions()
        {
            m_showSnappingCursor = true;
            m_showLegend = true;
            m_annotationPercent = 30;
            m_annotationTextSize = 8;
            m_annotationColor = Color.Black;
            m_matchedColor = Color.Red;
            m_unmatchedColor = Color.Black;
            m_hideUnmatched = false;
            m_unzoomKey = Keys.Back;
            m_focusOffset = 100;
            m_rightClickUnzoom = true;
            m_hidePlotTools = false;
            m_numberOfPlots = 1;
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
