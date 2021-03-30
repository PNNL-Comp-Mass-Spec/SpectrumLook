using System.Collections.Generic;

namespace SpectrumLook.Builders
{
    /// <summary>
    /// This interface class defined what functions will be called in spectrumLook to read
    /// The current experiment File (i.e. mzXML, mzML, etc.)
    /// ProteoWizard's reader will be derived from this class.
    /// </summary>
    public interface IExperimentParser
    {
        /// <summary>
        /// Get the mass spec data for the given scan number
        /// </summary>
        /// <param name="scanNumber"></param>
        List<Element> GetExperimentDataByScanNumber(int scanNumber);

        string FilePath
        {
            get;
            set;
        }
    }
}
