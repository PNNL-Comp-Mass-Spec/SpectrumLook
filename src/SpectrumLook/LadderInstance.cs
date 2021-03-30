using System.Collections.Generic;
using ZedGraph;

namespace SpectrumLook
{
    /// <summary>
    /// A struct for the information needed for an annotation
    /// </summary>
    public struct Annotation
    {
        public string Text;
        public int ShowHideAuto;
        public PointPair Point;
    }

    /// <summary>
    /// The Ladder Instance Object is used to store already calculated fragment ladder.
    /// This is the object that will be saved to a file.
    /// By Patrick Tobin
    /// </summary>
    public class LadderInstance
    {
        public List<string[]> MzValue { get; set; }

        public List<string> MzValueHeaders { get; set; }

        public string ScanAndPeptide { get; set; }

        public string ScanNumberString
        {
            get
            {
                var outString = "";
                var i = 0;
                while (ScanAndPeptide[i] != '|')
                {
                    outString += ScanAndPeptide[i].ToString();
                    ++i;
                }

                return outString;
            }
        }

        public string PeptideString
        {
            get
            {
                var barIndex = ScanAndPeptide.IndexOf('|');
                if (barIndex < 0 || barIndex == ScanAndPeptide.Length - 1)
                    return string.Empty;

                return ScanAndPeptide.Substring(barIndex + 1);
            }
            set
            {
                var stringParts = ScanAndPeptide.Split('|');
                ScanAndPeptide = stringParts[0] + "|" + value;
            }
        }

        public string CurrentMode { get; set; }

        public List<Annotation> Annotations { get; set; }

        public LadderInstance()
        {
            CurrentMode = string.Empty;
            ScanAndPeptide = string.Empty;
            MzValueHeaders = new List<string>();
            MzValue = new List<string[]>();
            Annotations = new List<Annotation>();
        }
    }
}
