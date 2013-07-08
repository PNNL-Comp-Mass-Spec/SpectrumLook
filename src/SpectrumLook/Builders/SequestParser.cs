using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.Builders
{
    /// <summary>
    /// The class derives from the ISynopsysParser interface class.
    /// This class defines how to read from a Sequest file.
    /// By Patrick Tobin
    /// </summary>
    public class SequestParser : ISynopsysParser
    {
        #region MEMBERS

        #region PUBLIC
        #endregion

        #region PRIVATE

        /// <summary>
        /// This stores the current location of the file that
        /// is passed into the constructor.
        /// </summary>
        private string m_fileLocation;

        /// <summary>
        /// This is used to read from the inputed file.
        /// </summary>
        private TextReader m_fileReader;

        /// <summary>
        /// This is used to store the current row count when reading from the file.
        /// This starts are zero and goes to N-1. Where N is the number of rows in
        /// the current sequest file.
        /// </summary>
        private int m_currentRowCount;

        #endregion

        #endregion

        #region PROPERTIES

        #region PUBLIC
        public string fileLocation
        {
            get
            {
                return fileLocation;
            }
            internal set
            {
                fileLocation = value;
            }
        }

        public int currentRowCount
        {
            get
            {
                return m_currentRowCount;
            }
            internal set
            {
                m_currentRowCount = value;
            }
        }
        #endregion

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// The constructor will attempt to open the inputed file location
        /// that was passed into the constructor.  If the string passed in
        /// is null or is an unreadable file location then an error will 
        /// be thrown.
        /// </summary>
        /// <param name="fileLocation">The location of the sequest file. (.txt format)</param>
        public SequestParser(string fileLocation)
        {
            this.m_fileLocation = fileLocation;

            this.m_currentRowCount = 0;

            if (m_fileLocation != null)
            {
                try
                {
                    m_fileReader = File.OpenText(fileLocation);
                }
                catch(Exception e)
                {
                    throw e; //If the File can not be opened for some reason.
                }
            }
            else
            {
                throw new System.InvalidProgramException("Null fileLocation Value.");
                //Throw Exception.
            }
        }

        #endregion

        #region FUNCTIONS

        #region PUBLIC

        /// <summary>
        /// This function will needed to be called multiple times in order to read
        /// all data from the file.  If this is the first time this function is called
        /// then the headers of the columns that are in the opened sequest file are returned
        /// in the form of a string array.  The Peptide column has a concatinated "_p" at the end
        /// of the header and a "_s" is cancatinated to the end of the scan number to represent the
        /// connection between the synopsis file and the experiment file.
        /// </summary>
        /// <returns></returns>
        string[] ISynopsysParser.GetNextColumn()
        {
            string currentLine = m_fileReader.ReadLine();
            if ((currentLine != null) && (currentLine != ""))
            {
                string[] splicedRow = currentLine.Split('\t');
                //This is where add "_p" and "_s" to the row that signifies the peptide sequence and connection to experiment data.
                /*if (m_currentRowCount == 0) //if zero then we are currently looking at the headers of the columns
                {
                    splicedRow[1] = splicedRow[1] + "_s";
                    splicedRow[10] = splicedRow[10] + "_p";
                }*/
                ++m_currentRowCount;
                return splicedRow;
            }
            return null;
        }

        #endregion

        #region PRIVATE
        #endregion

        #endregion
    }
}
