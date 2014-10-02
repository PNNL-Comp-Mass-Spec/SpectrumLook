using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.Builders
{
    public class MolecularWeightCalculator : ITheoryCalculator
    {

        public MolecularWeightCalculator(List<string> modificationList)
        {
            m_modificationList = modificationList;
        }

        /// <summary>
        /// This is an instantiation of the Molecular Weight Calculator.
        /// </summary>
        private MwtWinDll.MolecularWeightCalculator m_mMwtWin = new MwtWinDll.MolecularWeightCalculator();

        private List<string> m_modificationList;

        /// <summary>
        /// The deNovo Table values for b ions
        /// </summary>
        private Dictionary<char, double> m_deNovoTableB = new Dictionary<char, double>()
        {
            {'G', 57.021464},
            {'A', 71.037114},
            {'S', 87.032029},
            {'P', 97.052764},
            {'V', 99.068414},
            {'T', 101.04768},
            {'C', 103.00919},
            {'L', 113.08406},
            {'I', 113.08406},
            {'N', 114.04293},
            {'D', 115.02694},
            {'Q', 128.05858},
            {'K', 128.09496},
            {'E', 129.04259},
            {'M', 131.04048},
            {'H', 137.05891},
            {'F', 147.06841},
            {'R', 156.10111},
            {'Y', 163.06333},
            {'W', 186.07931}
        };

        /// <summary>
        /// This will take the input of a peptide sequence and a bool for the fragmentation mode (false = CID, true = ETD).
        /// The output is a string array that should be structured such that annotations are odd index values (starting from 1)
        /// and mzValues are even index values (starting from 0).
        /// </summary>
        /// <param name="peptide">This is the peptide sequence.</param>
        /// <param name="fragmentationModeCID">True when the fragmentation mode is CID</param>
        /// <returns>List of theoretical ions as key/value pairs (key is ion abbreviation, value is m/z value)</returns>
        public List<KeyValuePair<string, double>> GetTheoreticalDataByPeptideSequence(string peptide, bool fragmentationModeCID)
        {

            // Set the element mode
            m_mMwtWin.SetElementMode(MwtWinDll.MWElementAndMassRoutines.emElementModeConstants.emIsotopicMass);

            // Initialize the options
            MwtWinDll.MWPeptideClass.udtFragmentationSpectrumOptionsType udtFragSpectrumOptions = default(MwtWinDll.MWPeptideClass.udtFragmentationSpectrumOptionsType);
            udtFragSpectrumOptions.Initialize();

            // Initialize udtFragSpectrumOptions with the defaults
            udtFragSpectrumOptions = m_mMwtWin.Peptide.GetFragmentationSpectrumOptions();

            // The entire list of value will be retirved without any filtering
            udtFragSpectrumOptions.DoubleChargeIonsShow = true;
            udtFragSpectrumOptions.TripleChargeIonsShow = true;
            udtFragSpectrumOptions.DoubleChargeIonsThreshold = 1;
            udtFragSpectrumOptions.TripleChargeIonsThreshold = 1;

            // Each label begins with "b", "y", "c", or "z"
            char modeString1;
            if (fragmentationModeCID)
            {
                modeString1 = 'c';
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itBIon].ShowIon = false;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itYIon].ShowIon = false;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itCIon].ShowIon = true;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itZIon].ShowIon = true;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itAIon].ShowIon = false;
            }
            else
            {
                modeString1 = 'b';
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itBIon].ShowIon = true;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itYIon].ShowIon = true;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itCIon].ShowIon = false;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itZIon].ShowIon = false;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itAIon].ShowIon = false;
            }

            // Obtain the fragmentation spectrum for a peptide
            // First define the peptide sequence
            // Need to pass "false" to parameter blnIs3LetterCode since "peptide" is in one-letter notation
            m_mMwtWin.Peptide.SetSequence(peptide,
                                        MwtWinDll.MWPeptideClass.ntgNTerminusGroupConstants.ntgHydrogen,
                                        MwtWinDll.MWPeptideClass.ctgCTerminusGroupConstants.ctgHydroxyl,
                                        false);

            //Add the modifications if needed.
            if (m_modificationList != null)
            {
                var sepChars = new char[] { '|' };

                foreach (string modDef in m_modificationList)
                {
                    // Modifications are of the form Symbol|ModMass
                    // Split on |

                    var splitVals = modDef.Split(sepChars);
                    if (splitVals.Length > 1)
                    {
                        double modMass;
                        string modComment = string.Empty;

                        if (double.TryParse(splitVals[1], out modMass))
                        {
                            bool indicatesPhospho = Math.Abs(modMass - 79.9663326) < 0.005;

                            int modResult = m_mMwtWin.Peptide.SetModificationSymbol(splitVals[0], modMass, indicatesPhospho, modComment);

                            //if modresult = 0 symbol add is successful, useful spot for breakpoint
                            //modResult = modResult + 0;
                        }
                    }
                }
            }

            // Update the options
            m_mMwtWin.Peptide.SetFragmentationSpectrumOptions(udtFragSpectrumOptions);

            // Get the fragmentation masses
            MwtWinDll.MWPeptideClass.udtFragmentationSpectrumDataType[] udtFragSpectrum = null;
            m_mMwtWin.Peptide.GetFragmentationMasses(ref udtFragSpectrum);


            var cleanPeptide = m_mMwtWin.Peptide.GetSequence(false, false, false, false, false);

            if (string.IsNullOrWhiteSpace(cleanPeptide))
            {
                // No valid residues
                return new List<KeyValuePair<string, double>>();
            }

            // Obtain the list of ions
            List<KeyValuePair<string, double>> theoryList = GetTheoryList(cleanPeptide, fragmentationModeCID, modeString1, udtFragSpectrum);

            return theoryList;
        }

        private List<KeyValuePair<string, double>> GetTheoryList(
            string peptide,
            bool fragmentationModeCID,
            char modeString1,
            MwtWinDll.MWPeptideClass.udtFragmentationSpectrumDataType[] udtFragSpectrum)
        {
            var theoryList = new List<KeyValuePair<string, double>>();

            double nTerminusResidueMass = 0;
            if (m_deNovoTableB.ContainsKey(peptide[0]))
                nTerminusResidueMass = m_deNovoTableB[peptide[0]];

            // Generate the first b or c ion, as 1+, 2+, and 3+
            for (var charge = 1; charge <= 3; charge++)
            {
                string ionDescription = modeString1 + "1";
                if (charge > 1)
                    ionDescription += new String('+', charge);

                if (!fragmentationModeCID)
                    theoryList.Add(new KeyValuePair<string, double>(ionDescription, nTerminusResidueMass / charge));
                else
                    theoryList.Add(new KeyValuePair<string, double>(ionDescription, (nTerminusResidueMass + 18) / charge));
            }

            var peptideResidueCount = peptide.Length;
            var cTermModeSymbolFlag = modeString1 + (peptideResidueCount - 1).ToString();
            var cTermMassesAdded = false;

            double cTerminusResidueMass = 0;
            if (m_deNovoTableB.ContainsKey(peptide.Last()))
                cTerminusResidueMass = m_deNovoTableB[peptide.Last()];

            // Generate every other ion except the last of the b or c ion series
            for (int i = 0; i < udtFragSpectrum.Length; i++)
            {
                try
                {
                    if ((udtFragSpectrum[i].Symbol.StartsWith("Shoulder")))
                    {
                        // Shoulder ion; ignore it
                        continue;
                    }
                    else
                    {
                        theoryList.Add(new KeyValuePair<string, double>(udtFragSpectrum[i].Symbol, udtFragSpectrum[i].Mass));

                        // Generate the last b or c ion, as 1+, 2+, and 3+
                        // This code only works if peptide contains all capital letters

                        if (udtFragSpectrum[i].Symbol != cTermModeSymbolFlag || cTermMassesAdded)
                        {
                            continue;
                        }

                        cTermMassesAdded = true;
                        for (var charge = 1; charge <= 3; charge++)
                        {
                            string ionDescription = modeString1 + peptideResidueCount.ToString();
                            if (charge > 1)
                                ionDescription += new String('+', charge);

                            theoryList.Add(new KeyValuePair<string, double>(ionDescription, cTerminusResidueMass / charge));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ignore errors here
                }
            }

            return theoryList;
        }
    }
}
