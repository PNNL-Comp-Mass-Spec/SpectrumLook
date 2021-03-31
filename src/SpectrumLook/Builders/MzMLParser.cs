using System;
using System.Collections.Generic;
using MSDataFileReader;

namespace SpectrumLook.Builders
{
    public class MzMLParser : IExperimentParser
    {
        private readonly PSI_Interface.MSData.SimpleMzMLReader mFileReader;

        private readonly SortedSet<int> mScanNumbers;

        string IExperimentParser.FilePath
        {
            get => CurrentFilePath;
            set => CurrentFilePath = value;
        }

        public string CurrentFilePath { get; internal set; }

        public bool IsFileOpened { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath">This must be a file Location to a ".mzML" file.</param>
        public MzMLParser(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new InvalidOperationException("Empty file path sent to the MzMLParser constructor");
            }

            CurrentFilePath = filePath;
            mScanNumbers = new SortedSet<int>();

            if (CurrentFilePath.EndsWith(".mzML", StringComparison.OrdinalIgnoreCase))
            {
                mFileReader = new PSI_Interface.MSData.SimpleMzMLReader(filePath, true);
                CacheScanNumbers();
                IsFileOpened = true;
            }
            else
            {
                throw new InvalidProgramException("Invalid File Type, must be .mzML");
            }
        }

        private void CacheScanNumbers()
        {
            try
            {
                foreach (var spectrum in mFileReader.ReadAllSpectra(false))
                {
                    mScanNumbers.Add(spectrum.ScanNumber);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error caching scan numbers in the .mzML file: " + ex.Message);
            }
        }

        /// <summary>
        /// Get the mass spec data for the given scan number
        /// </summary>
        /// <param name="scanNumber"></param>
        List<Element> IExperimentParser.GetExperimentDataByScanNumber(int scanNumber)
        {
            if (!IsFileOpened)
            {
                return null;
            }

            if (!mScanNumbers.Contains(scanNumber))
            {
                return null;
            }

            var spectrum = mFileReader.ReadMassSpectrum(scanNumber, true);

            var elements = new List<Element>();
            foreach (var peak in spectrum.Peaks)
            {
                var element = new Element
                {
                    Mz = peak.Mz,
                    Intensity = peak.Intensity
                };
                elements.Add(element);
            }

            return elements;
        }
    }
}
