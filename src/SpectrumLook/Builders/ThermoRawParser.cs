using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThermoRawFileReader;
using System.IO;

namespace SpectrumLook.Builders
{
    public class ThermoRawParser : IExperimentParser
    {
        XRawFileIO m_fileToRead;

        private string m_fileLocation;

        private bool m_fileOpened;

        string IExperimentParser.Filename
        {
            get => m_fileLocation;
            set => m_fileLocation = value;
        }

        /// <summary>
        /// The constructor of this parser will attempt to open any files that
        /// end in ".RAW".  If a null string is passed in or a
        /// a string without a ".raw" file extension then an exception
        /// will be thrown.
        /// </summary>
        /// <param name="fileLocation">This must be a file Location to a ".raw" file.</param>
        public ThermoRawParser(string filelocation)
        {
            this.m_fileLocation = filelocation;

            var extension = Path.GetExtension(filelocation);
            extension = extension.ToLower();

            if (m_fileLocation != null)
            {
                if (extension == ".raw")
                {
                    m_fileToRead = new XRawFileIO();
                    m_fileOpened = m_fileToRead.OpenRawFile(m_fileLocation);                //m_fileOpened is staying false for some reason. ****
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
        /// This function returns an array of strings where the odd index's are intensities
        /// and even index's are mz Values.  The function takes advantage of the MSDataFileReader
        /// from PNNL.
        /// </summary>
        /// <param name="scanNum">The scan number that is used to reference the experiment Data
        /// in the currently opened File.</param>
        /// <returns>An array of strings where odd index (starting from 1) are the intensities
        /// and the even index's (starting from 0) are the mzValues.</returns>
        List<Element> IExperimentParser.GetExperimentDataByScanNumber(int scanNum)   //Have GetExperimentDataByScanNumberRaw commented out, checking without the raw...
        {
            //m_fileOpened = true;        //TEST

            if (this.m_fileOpened)
            {
                var values = new List<Element>();
                double[,] mzIntensityPairList;
                var dataPairCount = -1;

                dataPairCount = m_fileToRead.GetScanData2D(scanNum, out mzIntensityPairList, 0, true);

                //Step through mzList and intensityList and assign them.
                for (var i = 0; i < dataPairCount; ++i)
                {
                    values.Add(new Element(mzIntensityPairList[0,i], mzIntensityPairList[1,i]));
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
