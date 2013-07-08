using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.Builders
{
    /// <summary>
    /// This class is used to generate a list of element from a given file type (depending on a given parser) and
    /// a given scan number.  To generate this list you must call the BuildList function and the use the elementList
    /// Property to retrieve the said list.
    /// By Patrick Tobin
    /// </summary>
    public class ActualListBuilder : ElementListBuilder
    {
        #region MEMBERS

        #region PUBLIC
        #endregion

        #region PRIVATE
        /// <summary>
        /// This stores the inputed experiment parser, so that this class can
        /// retive data from the experiment files.
        /// </summary>
        private IExperimentParser m_fileParser;

        /// <summary>
        /// This stores the inputed scan number in the constructor so that the BuildList function knows where
        /// to find the need experiment data.
        /// </summary>
        private int m_scanNumber;

        #endregion

        #endregion

        #region PROPERTIES
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// This constructor takes in a scan number of the desired experiment data with a provided parser that is not null.
        /// </summary>
        /// <param name="scanNumber">A scan number referenecing data in an experiment file.</param>
        /// <param name="experimentParser">An interface for reading experiment files (i.e. ".mzXML"...).</param>
        public ActualListBuilder(int scanNumber, IExperimentParser experimentParser)
        {
            if (experimentParser != null)
            {
                m_scanNumber = scanNumber;
                m_fileParser = experimentParser;
                ElementList = new List<Element>();
            }
            else
            {
                //Throw Exception?
            }
        }
        #endregion

        #region FUNCTIONS

        #region PUBLIC
        /// <summary>
        /// This function is derived from the ElementListBuilder Class.
        /// This version of the function uses the given scan number and given experiment parser to
        /// look up the experiment data that relates to the scan number and puts that data into the
        /// derived List of Elements.
        /// </summary>
        public override void BuildList()
        {            
            List<Element> listToStore       = new List<Element>();
            Element tempElement             = new Element();
            List<Element> experimentDataStringArray = m_fileParser.GetExperimentDataByScanNumber(m_scanNumber);
            experimentDataStringArray.ForEach(x => x.matched = false);            
            ElementList = experimentDataStringArray;
        }

        public bool SetScanNumber(int newScanNum)
        {
            bool success = false;
            if (newScanNum >= 0)
            {
                m_scanNumber = newScanNum;
                success = true;
            }
            return success;
        }
        #endregion

        #region PRIVATE
        #endregion

        #endregion
    }
}
