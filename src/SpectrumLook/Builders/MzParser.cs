using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        #region MEMBERS

        #region PUBLIC
        #endregion

        #region PRIVATE

        private clsMSDataFileReaderBaseClass m_fileToRead;
        //private clsMzXMLFileAccessor m_fileToRead;

        private string m_fileLocation;

        private bool m_fileOpened;

        string IExperimentParser.Filename
        {
            get
            {
                return m_fileLocation;
            }
            set
            {
                m_fileLocation = value;
            }
        }

        #endregion

        #endregion

        #region PROPERTIES

        public string currentFileLocation
        {
            get
            {
                return m_fileLocation;
            }
            internal set
            {
                m_fileLocation = value;
            }
        }

        public bool isFileOpened
        {
            get
            {
                return m_fileOpened;
            }
            internal set
            {
                m_fileOpened = value;
            }
        }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// The constructor of this parser will attempt to open any files that
        /// end in ".mzXML" or ".mzData".  If a null string is passed in or a
        /// a string without a ".mzXML" or ".mzData" file extension then an exception
        /// will be thrown.
        /// </summary>
        /// <param name="fileLocation">This must be a file Location to a ".mzXML" or ".mzData" file.</param>
        public MzParser(string fileLocation)
        {
            this.m_fileLocation = fileLocation;
            if (m_fileLocation != null)
            {
                if (m_fileLocation.Contains(".mzXML"))
                {
                    m_fileToRead = new clsMzXMLFileAccessor();
                    m_fileOpened = m_fileToRead.OpenFile(m_fileLocation);                    
                }
                else if (m_fileLocation.Contains(".mzData"))
                {
                    m_fileToRead = new clsMzXMLFileAccessor();
                    m_fileOpened = m_fileToRead.OpenFile(m_fileLocation);
                }
                else
                {
                    throw new System.InvalidProgramException("Invalid File Type");
                }
                m_fileToRead.ReadAndCacheEntireFile();
            }
            else
            {
                throw new System.InvalidOperationException("The file location passed was equal to null");
            }
        }

        #endregion

        #region FUCTIONS

        #region PUBLIC
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
            List<string> outputValues = new List<string>();
            clsSpectrumInfo currentSpectrum;

            if (this.m_fileOpened)
            {
                //Load the entire file into memory.                                
                //m_fileToRead.ReadAndCacheEntireFile();
            
                currentSpectrum         = new clsSpectrumInfo();            
                m_fileToRead.GetSpectrumByScanNumber(scanNum, ref currentSpectrum);

                List<Element> elements  = new List<Element>();
                foreach (double currentMzValue in currentSpectrum.MZList)
                {
                    Element element     = new Element();
                    element.mzValue     = currentMzValue;
                    element.intensity   = Convert.ToDouble(currentSpectrum.LookupIonIntensityByMZ(currentMzValue, (float)0.0, (float)0.04));
                    element.matched     = false;
                    elements.Add(element);
                }
                return elements;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region PRIVATE
        #endregion

        #endregion

    }
}
