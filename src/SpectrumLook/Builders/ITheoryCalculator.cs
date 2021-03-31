using System.Collections.Generic;

namespace SpectrumLook.Builders
{
    public enum FragmentationMode
    {
        CID = 0,
        ETD = 1
    }

    internal interface ITheoryCalculator
    {
        /// <summary>
        /// Get the theoretical fragmentation ions for the given peptide
        /// </summary>
        /// <param name="peptide">This is the peptide sequence.</param>
        /// <param name="fragmentationMode">Fragmentation mode</param>
        /// <returns>List of theoretical ions as key/value pairs (key is ion abbreviation, value is m/z value)</returns>
        List<KeyValuePair<string, double>> GetTheoreticalDataByPeptideSequence(string peptide, FragmentationMode fragmentationMode);
    }
}
