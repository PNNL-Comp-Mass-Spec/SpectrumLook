﻿using System;
using System.Collections.Generic;
using MSDataFileReader;

namespace SpectrumLook.Builders
{
    public class MzMLParser : IExperimentParser
    {
        private readonly PSI_Interface.MSData.SimpleMzMLReader mFileReader;

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

            if (CurrentFilePath.EndsWith(".mzML", StringComparison.OrdinalIgnoreCase))
            {
                mFileReader = new PSI_Interface.MSData.SimpleMzMLReader(filePath, true);
                IsFileOpened = true;
            }
            else
            {
                throw new InvalidProgramException("Invalid File Type, must be .mzML");
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

            var spectrum = mFileReader.GetSpectrumForScan(scanNumber, true);

            if (spectrum == null)
                return new List<Element>();

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
