using System;
using System.Collections.Generic;
using System.Linq;

namespace SpectrumLook.Builders
{
    public class MolecularWeightCalculator : ITheoryCalculator
    {
        public MolecularWeightCalculator(Dictionary<char, double> modificationList)
        {
            m_modificationList = modificationList;
        }

        /// <summary>
        /// This is an instantiation of the Molecular Weight Calculator.
        /// </summary>
        private MwtWinDll.MolecularWeightCalculator m_mMwtWin = new MwtWinDll.MolecularWeightCalculator();

        private Dictionary<char, double> m_modificationList;

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
        /// </summary>
        /// <param name="peptide">This is the peptide sequence.</param>
        /// <param name="fragmentationModeETD">True when the fragmentation mode is CID</param>
        /// <returns>List of theoretical ions as key/value pairs (key is ion abbreviation, value is m/z value)</returns>
        public List<KeyValuePair<string, double>> GetTheoreticalDataByPeptideSequence(string peptide, bool fragmentationModeETD)
        {

            // Set the element mode
            m_mMwtWin.SetElementMode(MwtWinDll.MWElementAndMassRoutines.emElementModeConstants.emIsotopicMass);

            // Initialize the options
            var udtFragSpectrumOptions = default(MwtWinDll.MWPeptideClass.udtFragmentationSpectrumOptionsType);
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
            if (fragmentationModeETD)
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

            // MolecularWeightTool allowed modification symbols.
            // 33	!
            // 35	#
            // 36	$
            // 37	%
            // 38	&
            // 39	'
            // 42	*
            // 43	+
            // 63	?
            // 64	@
            // 94	^
            // 95	_
            // 96	`
            // 126	~
            var allowedSymbols = "!#$%&'*+?@^_`~";
            var usedSymbols = new string(m_modificationList.Keys.ToArray());
            var availableSymbols = "";
            foreach (var c in allowedSymbols)
            {
                if (!usedSymbols.Contains(c))
                {
                    availableSymbols += c;
                }
            }
            var badSymbolMap = new Dictionary<char, char>();

            //Add the modifications if needed.
            if (m_modificationList != null)
            {
                m_mMwtWin.Peptide.RemoveAllModificationSymbols(); // Remove the default MWT modification symbols
                foreach (var modPair in m_modificationList)
                {
                    // Key is symbol, value is mzValue
                    var modComment = string.Empty;
                    var indicatesPhospho = Math.Abs(modPair.Value - 79.9663326) < 0.005;
                    var modResult = m_mMwtWin.Peptide.SetModificationSymbol(modPair.Key.ToString(), modPair.Value, indicatesPhospho,
                        modComment);

                    if (modResult != 0) // A symbol that is not allowed, most likely
                    {
                        modResult = m_mMwtWin.Peptide.SetModificationSymbol(availableSymbols[0].ToString(), modPair.Value, indicatesPhospho,
                        modComment);
                        badSymbolMap.Add(modPair.Key, availableSymbols[0]); // Add it to the dictionary
                        availableSymbols = availableSymbols.Substring(1); // Remove it from the available symbols.
                    }

                    //if modresult = 0 symbol add is successful, useful spot for breakpoint
                    //modResult = modResult + 0;
                }
            }

            var peptideFix = peptide;
            foreach (var symfix in badSymbolMap)
            {
                peptideFix = peptideFix.Replace(symfix.Key, symfix.Value);
            }

            // Obtain the fragmentation spectrum for a peptide
            // First define the peptide sequence
            // Need to pass "false" to parameter blnIs3LetterCode since "peptide" is in one-letter notation
            m_mMwtWin.Peptide.SetSequence(peptideFix,
                                        MwtWinDll.MWPeptideClass.ntgNTerminusGroupConstants.ntgHydrogen,
                                        MwtWinDll.MWPeptideClass.ctgCTerminusGroupConstants.ctgHydroxyl,
                                        false);

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
            var theoryList = GetTheoryList(cleanPeptide, fragmentationModeETD, modeString1, udtFragSpectrum);

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
                var ionDescription = modeString1 + "1";
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
            for (var i = 0; i < udtFragSpectrum.Length; i++)
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
                            var ionDescription = modeString1 + peptideResidueCount.ToString();
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
