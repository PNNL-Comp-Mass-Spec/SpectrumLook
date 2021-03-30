using System.Collections.Generic;
using ZedGraph;

namespace SpectrumLook
{
    /// <summary>
    /// A struct for the information needed for an annotation
    /// </summary>
    public struct Annotation
    {
        public string mText;
        public int mShowHideAuto;
        public PointPair mPoint;
    }

    /// <summary>
    /// The Ladder Instance Object is used to store already calculated fragment ladder.
    /// This is the object that will be saved to a file.
    /// By Patrick Tobin
    /// </summary>
    public class LadderInstance
    {
        public List<string[]> mzValue { get; set; }

        public List<string> mzValueHeaders { get; set; }

        public string scanAndPeptide { get; set; }

        public string scanNumberString
        {
            get
            {
                var outString = "";
                var i = 0;
                while (scanAndPeptide[i] != '|')
                {
                    outString += scanAndPeptide[i].ToString();
                    ++i;
                }

                return outString;
            }
        }
        public string PeptideString
        {
            get
            {
                var outString = "";
                var startIndex = scanAndPeptide.IndexOf('|');
                ++startIndex;

                while (startIndex < scanAndPeptide.Length)
                {
                    outString += scanAndPeptide[startIndex].ToString();
                    ++startIndex;
                }

                return outString;
            }
            set
            {
                var splittedString = scanAndPeptide.Split('|');
                scanAndPeptide = splittedString[0] + "|" + value;
            }
        }
        public string currentMode { get; set; }

        public List<Annotation> annotations { get; set; }

        public LadderInstance()
        {
            currentMode = "";
            scanAndPeptide = "";
            mzValueHeaders = new List<string>();
            mzValue = new List<string[]>();
            annotations = new List<Annotation>();
        }
    }
}
