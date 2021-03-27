using System.Collections.Generic;

namespace SpectrumLook.Builders
{
    // TODO : Add info on who wrote this. Patrick
    /// <summary>
    /// This interface class defined what functions will be called in spectrumLook to read
    /// The current experiment File (i.e. mzXML, mzML, etc.)
    /// ProteoWizards reader will be derived from this class.
    /// </summary>
    public interface IExperimentParser
    {
        /// <summary>
        /// The input of this function will always be an assumed int value where the
        /// integer value is the Scan Number that an outside object requested.
        /// The output is a string array that should be structured such that intensitys are odd index values (starting from 1)
        /// and mzValues are even index values (starting from 0).
        /// </summary>
        /// <param name="scanNum">This is Assumed to be the Scan Number that the data is referenced by in the file.</param>
        /// <returns></returns>
        List<Element> GetExperimentDataByScanNumber(int scanNum);

        string Filename
        {
            get;
            set;
        }
    }
}
