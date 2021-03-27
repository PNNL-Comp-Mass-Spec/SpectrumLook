using System;
using System.Collections.Generic;
using MSDataFileReader;

namespace SpectrumLook.Builders
{
    /// <summary>
    /// This class uses the MSDataFileReader from PNNL to read experiment files
    /// (only ".mzXML" and ".mzData" supported) and also uses the IExperimentParser interface
    /// to define the GetExperimentDataByScanNumber method.
    /// By Patrick Tobin
    /// </summary>
    public class MzParser : IExperimentParser
    {
        private readonly clsMSDataFileReaderBaseClass m_fileToRead;

        string IExperimentParser.Filename
        {
            get => currentFileLocation;
            set => currentFileLocation = value;
        }

        public string currentFileLocation { get; internal set; }

        public bool isFileOpened { get; internal set; }

        /// <summary>
        /// The constructor of this parser will attempt to open any files that
        /// end in ".mzXML" or ".mzData".  If a null string is passed in or a
        /// a string without a ".mzXML" or ".mzData" file extension then an exception
        /// will be thrown.
        /// </summary>
        /// <param name="fileLocation">This must be a file Location to a ".mzXML" or ".mzData" file.</param>
        public MzParser(string fileLocation)
        {
            currentFileLocation = fileLocation;
            if (currentFileLocation != null)
            {
                if (currentFileLocation.EndsWith(".mzXML", StringComparison.OrdinalIgnoreCase) ||
                    currentFileLocation.EndsWith(".mzData", StringComparison.OrdinalIgnoreCase))
                {
                    m_fileToRead = new clsMzXMLFileAccessor();
                    isFileOpened = m_fileToRead.OpenFile(currentFileLocation);
                }
                else
                {
                    throw new InvalidProgramException("Invalid File Type, must be .mzXML or .mzData");
                }
                m_fileToRead.ReadAndCacheEntireFile();
            }
            else
            {
                throw new InvalidOperationException("The file location passed was equal to null");
            }
        }

        /// <summary>
        /// This function returns an array of strings where the odd index's are intensities
        /// and even index's are mz Values.  The function takes advantage of the MSDataFileReader
        /// from PNNL.
        /// </summary>
        /// <param name="scanNum">The scan number that is used to reference the experiment Data
        /// in the currently opened File.</param>
        /// <returns></returns>
        List<Element> IExperimentParser.GetExperimentDataByScanNumber(int scanNum)
        {
            var outputValues = new List<string>();

            if (isFileOpened)
            {
                // Load the entire file into memory.
                // m_fileToRead.ReadAndCacheEntireFile();

                m_fileToRead.GetSpectrumByScanNumber(scanNum, out var currentSpectrum);

                var elements = new List<Element>();
                foreach (var currentMzValue in currentSpectrum.MZList)
                {
                    var element = new Element
                    {
                        Mz = currentMzValue,
                        Intensity = Convert.ToDouble(currentSpectrum.LookupIonIntensityByMZ(currentMzValue, (float)0.0, (float)0.04)),
                        Matched = false
                    };
                    elements.Add(element);
                }
                return elements;
            }
            else
            {
                return null;
            }
        }
    }
}
