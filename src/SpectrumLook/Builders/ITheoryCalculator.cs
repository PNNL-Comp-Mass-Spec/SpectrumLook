using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.Builders
{
    interface ITheoryCalculator
    {
        /// <summary>
        /// This will take the input of a peptide sequence and a bool for the fragmentation mode (false = CID, true = ETD).
        /// The output is a string array that should be structured such that annotations are odd index values (starting from 1)
        /// and mzValues are even index values (starting from 0).
        /// </summary>
        /// <param name="peptide">This is the peptide sequence.</param>
        /// <param name="fragmentationModeCID">True when the fragmentation mode is CID</param>
        /// <returns>List of theoretical ions as key/value pairs (key is ion abbreviation, value is m/z value)</returns>
        List<KeyValuePair<string, double>> GetTheoreticalDataByPeptideSequence(string peptide, bool fragmentationModeCID);
    }
}
