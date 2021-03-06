﻿using System.Collections.Generic;

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
        /// <summary>
        /// This stores the experiment parser, so that this class can
        /// retrieve data from the experiment files.
        /// </summary>
        private readonly IExperimentParser mFileParser;

        /// <summary>
        /// The current scan number
        /// </summary>
        private int mScanNumber;

        /// <summary>
        /// This constructor takes in a scan number of the desired experiment data with a provided parser
        /// </summary>
        /// <param name="scanNumber">A scan number referencing data in an experiment file.</param>
        /// <param name="experimentParser">An interface for reading experiment files (i.e. ".mzXML"...).</param>
        public ActualListBuilder(int scanNumber, IExperimentParser experimentParser)
        {
            if (experimentParser != null)
            {
                mScanNumber = scanNumber;
                mFileParser = experimentParser;
                ElementList = new List<Element>();
            }
            else
            {
                // Throw Exception?
            }
        }

        /// <summary>
        /// This function is derived from the ElementListBuilder Class.
        /// This version of the function uses the given scan number and given experiment parser to
        /// look up the experiment data that relates to the scan number and puts that data into the
        /// derived List of Elements.
        /// </summary>
        public override void BuildList()
        {
            var experimentDataStringArray = mFileParser.GetExperimentDataByScanNumber(mScanNumber);

            foreach (var item in experimentDataStringArray)
            {
                item.Matched = false;
            }

            ElementList = experimentDataStringArray;
        }

        public bool SetScanNumber(int newScanNum)
        {
            var success = false;
            if (newScanNum >= 0)
            {
                mScanNumber = newScanNum;
                success = true;
            }
            return success;
        }
    }
}
