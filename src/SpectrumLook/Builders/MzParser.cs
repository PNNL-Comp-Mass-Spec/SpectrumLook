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
    public class MzXMLParser : IExperimentParser
    {
        private readonly clsMSDataFileReaderBaseClass mFileReader;

        string IExperimentParser.FilePath
        {
            get => CurrentFilePath;
            set => CurrentFilePath = value;
        }

        public string CurrentFilePath { get; internal set; }

        public bool IsFileOpened { get; internal set; }

        /// <summary>
        /// The constructor of this parser will attempt to open any files that
        /// end in ".mzXML" or ".mzData".  If a null string is passed in or a
        /// a string without a ".mzXML" or ".mzData" file extension then an exception
        /// will be thrown.
        /// </summary>
        /// <param name="filePath">This must be a file Location to a ".mzXML" or ".mzData" file.</param>
        public MzXMLParser(string filePath)
        {
            CurrentFilePath = filePath;
            if (CurrentFilePath != null)
            {
                if (CurrentFilePath.EndsWith(".mzXML", StringComparison.OrdinalIgnoreCase) ||
                    CurrentFilePath.EndsWith(".mzData", StringComparison.OrdinalIgnoreCase))
                {
                    mFileReader = new clsMzXMLFileAccessor();
                    IsFileOpened = mFileReader.OpenFile(CurrentFilePath);
                }
                else
                {
                    throw new InvalidProgramException("Invalid File Type, must be .mzXML or .mzData");
                }
                mFileReader.ReadAndCacheEntireFile();
            }
            else
            {
                throw new InvalidOperationException("The file location passed was equal to null");
            }
        }

        /// <summary>
        /// Get the mass spec data for the given scan number
        /// </summary>
        /// <param name="scanNumber"></param>
        List<Element> IExperimentParser.GetExperimentDataByScanNumber(int scanNumber)
        {
            if (IsFileOpened)
            {
                // Load the entire file into memory.
                // mFileReader.ReadAndCacheEntireFile();

                mFileReader.GetSpectrumByScanNumber(scanNumber, out var currentSpectrum);

                if (currentSpectrum == null)
                    return null;

                var elements = new List<Element>();
                foreach (var currentMzValue in currentSpectrum.MZList)
                {
                    var element = new Element
                    {
                        Mz = currentMzValue,
                        Intensity = Convert.ToDouble(currentSpectrum.LookupIonIntensityByMZ(currentMzValue, (float)0.0, (float)0.04))
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
