using System.Collections.Generic;
using ThermoRawFileReader;
using System.IO;

namespace SpectrumLook.Builders
{
    public class ThermoRawParser : IExperimentParser
    {
        private readonly XRawFileIO mFileReader;

        private string mFilePath;

        private readonly bool mFileOpened;

        string IExperimentParser.FilePath
        {
            get => mFilePath;
            set => mFilePath = value;
        }

        /// <summary>
        /// The constructor of this parser will attempt to open any files that
        /// end in ".RAW".  If a null string is passed in or a
        /// a string without a ".raw" file extension then an exception
        /// will be thrown.
        /// </summary>
        /// <param name="filePath">This must be a file Location to a ".raw" file.</param>
        public ThermoRawParser(string filePath)
        {
            mFilePath = filePath;

            var extension = Path.GetExtension(filePath);
            extension = extension.ToLower();

            if (mFilePath != null)
            {
                if (extension == ".raw")
                {
                    mFileReader = new XRawFileIO();
                    mFileOpened = mFileReader.OpenRawFile(mFilePath);                // mFileOpened is staying false for some reason. ****
                }
                else
                {
                    throw new System.InvalidProgramException("Invalid File Type");
                }
            }
            else
            {
                throw new System.InvalidOperationException("The file location passed was equal to null");
            }
        }

        /// <summary>
        /// Get the mass spec data for the given scan number
        /// </summary>
        /// <param name="scanNumber"></param>
        List<Element> IExperimentParser.GetExperimentDataByScanNumber(int scanNumber)
        {
            // mFileOpened = true;        // TEST

            if (mFileOpened)
            {
                var values = new List<Element>();

                var dataPairCount = mFileReader.GetScanData2D(scanNumber, out var mzIntensityPairList, 0, true);

                // Step through mzList and intensityList and assign them.
                for (var i = 0; i < dataPairCount; ++i)
                {
                    values.Add(new Element(mzIntensityPairList[0, i], mzIntensityPairList[1, i]));
                }

                return values;
            }
            else
            {
                return null;
            }
        }
    }
}
