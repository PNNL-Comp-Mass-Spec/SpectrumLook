using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Xml;
using MSDataFileReader;
using System.Threading;

namespace SpectrumLook
{
    //TODO: Class name starts with lower case.  Use upper case for coding standard.
            //will need to check out whole project and rename all instances where used.
    //TODO: Class name doesnt match file name.
        //TODO: Rename this class to MzReader
    //TODO: Does this need to implement IDisposable
    //TODO: Is there a is-a relationship here?  You could read other binary formats.  Possibly use an interface!
    /// <summary>
    /// Name:        MzReader
    /// Description: The MzReader is a singleton based Wrapper for reading both mzData and mzXML.
    /// Currently the backend parser we are using is from PNNL.  We hope for have the proteo-wizard parser
    /// in place of the current PNNL parser.
    /// Updated:     1/26/2011
    /// By:          Patrick Tobin
    /// Group:       WSU Senior Design
    /// </summary>
    ///
    public class mzXMLreader
    {
        /// <summary>
        /// Name:        m_fileToRead
        /// Description: This is an object that holds and reads a given file and allows access to the experimental information.
        ///              This object is fully defined in the MSDataFileReader.
        /// </summary>
        private clsMSDataFileReaderBaseClass m_fileToRead;

        /// <summary>
        /// Name:        m_currentSpectrum
        /// Description: This object is one element within a given mz file.
        ///              This is fully defined in MSDataFileReader.
        /// </summary>
        private clsSpectrumInfo m_currentSpectrum;

        /// <summary>
        /// Name:        m_isFileOpened
        /// Description: This is boolean flag that should be true when a file has been opend and false when it has not been opened.
        /// </summary>
        private bool m_isFileOpened;

        /// <summary>
        /// Name:        m_fileLocation
        /// Description: This is a string that holds the location of the mzFile that is currently open.
        /// </summary>
        private string m_fileLocation;

        /// <summary>
        /// Name:        m_mZIndex
        /// Description: This is an counter to make sure not to grab more then the files contains, when it comes to experimental data.
        /// </summary>
        private int m_mZIndex;

        /// <summary>
        /// Name: m_experimentCode
        /// Description: This is the current experimental code that is used to find data within the given mz experiment file.
        /// </summary>
        private int m_experimentCode;


        /// <summary>
        /// Name:        GetsMotherMzReader
        /// Description: This is a static instance of the MzReader.  This is the only way that outside users will be able to interact with this class (singleton).
        ///              This property, upon getting, also sets itself to a new MzReader (only is Instance is equal to null).
        ///              The set of this property is internal.
        /// </summary>
        public static mzXMLreader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new mzXMLreader();
                }
                return _instance;
            }
            internal set
            {
                _instance = value;
            }
        }
        private static mzXMLreader _instance;

        //TODO: Rename this class to MzReader
        /// <summary>
        /// Name:        MzReader
        /// Description: This is the only constructor for the MzReader.  MzReader is a singleton implementation, this explains why the constructor is private.
        /// </summary>
        private mzXMLreader()
        {
            this.m_fileLocation = "";
            this.m_experimentCode = 0;
            m_mZIndex = 0;
        }

        /// <summary>
        /// Name:        OpenMZXMLFile
        /// Description: After giving a valid file location, if a file is not already loaded, is loaded into m_fileToRead and the
        ///              m_isFileOpened flag is set to true.
        /// </summary>
        /// <param name="fileLocation">
        /// Name:        fileLocation
        /// Description: The fileLocation is an input that represents the direct location of a mzXML or mzData file.
        /// </param>
        public void OpenMZXMLFile(string fileLocation)
        {
            m_fileLocation = fileLocation;

            if (m_fileLocation != null)
                if (m_fileLocation.Contains(".mzXML"))
                {
                    m_fileToRead = new clsMzXMLFileReader();
                    m_isFileOpened = m_fileToRead.OpenFile(m_fileLocation);
                }
                else if (m_fileLocation.Contains(".mzData"))
                {
                    m_fileToRead = new clsMzDataFileReader();
                    m_isFileOpened = m_fileToRead.OpenFile(m_fileLocation);
                }
                else
                {
                    // Todo: throw an error <--- invalid file input.
                }
        }

        /// <summary>
        /// Name:        GetParentMZ
        /// Description: This function returns the ParentIon of the current experiment (determined by m_experimentCode).
        /// </summary>
        /// <returns>
        /// Output:      This function outputs the Parent Ion in the form of a double.
        /// </returns>
        public double GetParentMZ()
        {
            clsSpectrumInfo tempInfo = new clsSpectrumInfo();
            m_fileToRead.GetSpectrumByScanNumber(this.m_experimentCode, ref tempInfo);
            return tempInfo.ParentIonMZ;
        }

        /// <summary>
        /// Name:        FindExperiment
        /// Description: This function will find the experimental data (in the form of a list) that matches the given experimental code.
        /// </summary>
        /// <param name="experimentCode">
        /// Input:       The experimentCode is a code that can reference a given experiment that is in the current opened mz data file.
        /// </param>
        /// <returns>
        /// Output:      The output is a List of ActualElement, where actual elements are containers with all possible data that can be in the current file.
        /// </returns>
        public List<ActualElement> FindExperiment(int experimentCode)
        {
            //Initialize a new experimentalData list to return.
            List<ActualElement> experimentData = new List<ActualElement>();
            //update the new experimentCode to the inputed experiment code, so we can get the right Parent Ion number.
            this.m_experimentCode = experimentCode;

            if (this.m_isFileOpened)
            {
                //We need to Cache the entire file into memory (which puts it into MZList) to look at the data in the file.
                m_fileToRead.ReadAndCacheEntireFile();

                //narrow the number of Spectrums down to all the data sets that match the experimentCode.
                bool result = m_fileToRead.GetSpectrumByScanNumber(m_experimentCode, ref m_currentSpectrum);
                this.m_mZIndex = 0;

                //We will run through each data that is in memory and convert that data into
                //  an ActualElement structure and then all the element to the experimentData List.
                while (m_mZIndex < this.m_currentSpectrum.MZList.Count())
                {
                    ActualElement inputForList = new ActualElement();
                    inputForList.MZValue = this.m_currentSpectrum.MZList[m_mZIndex];
                    inputForList.Intensity = (double)this.m_currentSpectrum.LookupIonIntensityByMZ(inputForList.MZValue, (float)0, (float)0.04);
                    experimentData.Add(inputForList);
                    ++m_mZIndex;
                }
            }
            return experimentData;
        }

        /// <summary>
        /// Name: CloseFile
        /// Description: This function Closes the file and sets the opened flag to false.
        /// </summary>
        public void CloseFile()
        {
            this.m_isFileOpened = false;
            this.m_fileToRead.CloseFile();
            this.m_currentSpectrum = null;
        }

    }
}