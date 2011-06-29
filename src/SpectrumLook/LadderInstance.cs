using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZedGraph;

namespace SpectrumLook
{
    /// <summary>
    /// A struct for the information needed for an annotation
    /// </summary>
    public struct Annotation
    {
        public string m_text;
        public int m_showHideAuto;
        public PointPair m_point;
    }

    /// <summary>
    /// The Ladder Instance Object is used to store already calculated fragment ladder.
    /// This is the object that will be saved to a file.
    /// By Patrick Tobin
    /// </summary>
    public class LadderInstance
    {
        #region MEMBERS

        #region PRIVATE

        private string m_currentMode;
        private string m_scanAndPeptide;
        private List<string[]> m_mzValues;
        private List<string> m_mzValueHeaders;
        private List<Annotation> m_annotations;

        #endregion

        #endregion

        #region PROPERTIES

        public List<string[]> mzValue
        {
            get
            {
                return m_mzValues;
            }
            set
            {
                m_mzValues = value;
            }
        }
        public List<string> mzValueHeaders
        {
            get
            {
                return m_mzValueHeaders;
            }
            set
            {
                m_mzValueHeaders = value;
            }
        }
        public string scanAndPeptide
        {
            get
            {
                return m_scanAndPeptide;
            }
            set
            {
                m_scanAndPeptide = value;
            }
        }
        public string scanNumberString
        {
            get
            {
                string outString = "";
                int i = 0;
                while (m_scanAndPeptide[i] != '|')
                {
                    outString += m_scanAndPeptide[i].ToString();
                    ++i;
                }

                return outString;
            }
        }
        public string PeptideString
        {
            get
            {
                string outString = "";
                int startIndex = m_scanAndPeptide.IndexOf('|');
                ++startIndex;

                while (startIndex < m_scanAndPeptide.Length)
                {
                    outString += m_scanAndPeptide[startIndex].ToString();
                    ++startIndex;
                }

                return outString;
            }
            set
            {
                string[] splittedString = m_scanAndPeptide.Split('|');
                m_scanAndPeptide =  splittedString[0] + "|" + value;
            }
        }
        public string currentMode
        {
            get
            {
                return m_currentMode;
            }
            set
            {
                m_currentMode = value;
            }
        }
        public List<Annotation> annotations
        {
            get
            {
                return m_annotations;
            }
            set
            {
                m_annotations = value;
            }
        }
        #endregion

        #region CONSTRUCTOR

        public LadderInstance()
        {
            m_currentMode = "";
            m_scanAndPeptide = "";
            m_mzValueHeaders = new List<string>();
            m_mzValues = new List<string[]>();
            m_annotations = new List<Annotation>();
        }

        #endregion
        
    }
}
