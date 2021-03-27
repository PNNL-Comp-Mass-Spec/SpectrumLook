using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;

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
            get => this.m_showSnappingCursor;
            set
            {
                this.m_showSnappingCursor = value;
                this.Invoke();
            }
        }
        public bool showLegend
        {
            get => this.m_showLegend;
            set
            {
                this.m_showLegend = value;
                this.Invoke();
            }
        }
        public int annotationPercent
        {
            get => this.m_annotationPercent;
            set
            {
                this.m_annotationPercent = value;
                this.Invoke();
            }
        }
        public int annotationTextSize
        {
            get => this.m_annotationTextSize;
            set
            {
                this.m_annotationTextSize = value;
                this.Invoke();
            }
        }
        public Color annotationColor
        {
            get => this.m_annotationColor;
            set
            {
                this.m_annotationColor = value;
                this.Invoke();
            }
        }
        public Color matchedColor
        {
            get => this.m_matchedColor;
            set
            {
                this.m_matchedColor = value;
                this.Invoke();
            }
        }
        public Color unmatchedColor
        {
            get => this.m_unmatchedColor;
            set
            {
                this.m_unmatchedColor = value;
                this.Invoke();
            }
        }
        public bool zoomHorizontal
        {
            get => this.m_zoomHorizontal;
            set
            {
                this.m_zoomHorizontal = value;
                this.Invoke();
            }
        }
        public bool hideUnmatched
        {
            get => this.m_hideUnmatched;
            set
            {
                this.m_hideUnmatched = value;
                this.Invoke();
            }
        }
        public Keys unzoomKey
        {
            get => this.m_unzoomKey;
            set
            {
                this.m_unzoomKey = value;
                this.Invoke();
            }
        }
        public int focusOffset
        {
            get => this.m_focusOffset;
            set
            {
                this.m_focusOffset = value;
                this.Invoke();
            }
        }
        public bool rightClickUnzoom
        {
            get => this.m_rightClickUnzoom;
            set
            {
                this.m_rightClickUnzoom = value;
                this.Invoke();
            }
        }
        public bool hidePlotTools
        {
            get => this.m_hidePlotTools;
            set
            {
                this.m_hidePlotTools = value;
                this.Invoke();
            }
        }
        public int numberOfPlots
        {
            get
            {
                if (this.m_numberOfPlots < 1 || this.m_numberOfPlots > 5)
                {
                    return 1;
                }
                return this.m_numberOfPlots;
            }
            set
            {
                this.m_numberOfPlots = value;
                this.Invoke();
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
