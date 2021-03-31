using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MolecularWeightCalculator;
using MolecularWeightCalculator.Formula;
using MolecularWeightCalculator.Sequence;

namespace SpectrumLook.Builders
{
    public class MolecularWeightUtility : ITheoryCalculator
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="modificationList"></param>
        public MolecularWeightUtility(Dictionary<char, double> modificationList)
        {
            mModificationList = modificationList;
        }

        /// <summary>
        /// This is an instantiation of the Molecular Weight Calculator.
        /// </summary>
        private readonly MolecularWeightTool mMolecularWeightTool = new MolecularWeightTool();

        private Dictionary<char, double> mModificationList;

        /// <summary>
        /// The de novo Table values for b ions
        /// </summary>
        private Dictionary<char, double> mDeNovoTableB = new Dictionary<char, double>()
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
        /// <param name="fragmentationMode">Fragmentation mode</param>
        /// <returns>List of theoretical ions as key/value pairs (key is ion abbreviation, value is m/z value)</returns>
        public List<KeyValuePair<string, double>> GetTheoreticalDataByPeptideSequence(string peptide, FragmentationMode fragmentationMode)
        {
            // Set the element mode
            mMolecularWeightTool.SetElementMode(ElementMassMode.Isotopic);

            // Get default fragmentation spectrum options
            var fragSpectrumOptions = mMolecularWeightTool.Peptide.GetFragmentationSpectrumOptions();

            // The entire list of value will be retrieved without any filtering
            fragSpectrumOptions.DoubleChargeIonsShow = true;
            fragSpectrumOptions.TripleChargeIonsShow = true;
            fragSpectrumOptions.DoubleChargeIonsThreshold = 1;
            fragSpectrumOptions.TripleChargeIonsThreshold = 1;

            // Each label begins with "b", "y", "c", or "z"
            char modeString1;
            if (fragmentationMode == FragmentationMode.ETD)
            {
                modeString1 = 'c';
                fragSpectrumOptions.IonTypeOptions[(int)IonType.BIon].ShowIon = false;
                fragSpectrumOptions.IonTypeOptions[(int)IonType.YIon].ShowIon = false;
                fragSpectrumOptions.IonTypeOptions[(int)IonType.CIon].ShowIon = true;
                fragSpectrumOptions.IonTypeOptions[(int)IonType.ZIon].ShowIon = true;
                fragSpectrumOptions.IonTypeOptions[(int)IonType.AIon].ShowIon = false;
            }
            else if (fragmentationMode ==FragmentationMode.CID)
            {
                modeString1 = 'b';
                fragSpectrumOptions.IonTypeOptions[(int)IonType.BIon].ShowIon = true;
                fragSpectrumOptions.IonTypeOptions[(int)IonType.YIon].ShowIon = true;
                fragSpectrumOptions.IonTypeOptions[(int)IonType.CIon].ShowIon = false;
                fragSpectrumOptions.IonTypeOptions[(int)IonType.ZIon].ShowIon = false;
                fragSpectrumOptions.IonTypeOptions[(int)IonType.AIon].ShowIon = false;
            }
            else
            {
                throw new InvalidEnumArgumentException(nameof(fragmentationMode));
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
            const string allowedSymbols = "!#$%&'*+?@^_`~";
            var usedSymbols = new string(mModificationList.Keys.ToArray());

            var availableSymbols = new Queue<char>();
            foreach (var c in allowedSymbols)
            {
                if (!usedSymbols.Contains(c))
                {
                    availableSymbols.Enqueue(c);
                }
            }

            var badSymbolMap = new Dictionary<char, char>();

            // Add the modifications if needed.
            if (mModificationList != null)
            {
                mMolecularWeightTool.Peptide.RemoveAllModificationSymbols(); // Remove the default MWT modification symbols
                foreach (var modPair in mModificationList)
                {
                    // Key is symbol, value is mzValue
                    var symbolToUse = modPair.Key;
                    var modificationMass = modPair.Value;
                    var modComment = string.Empty;
                    var indicatesPhospho = Math.Abs(modificationMass - 79.9663326) < 0.005;

                    var modResult = mMolecularWeightTool.Peptide.SetModificationSymbol(symbolToUse.ToString(), modificationMass, indicatesPhospho, modComment);

                    if (modResult != 0)
                    {
                        // modPair.Key is a character that is not allowed for a dynamic modification symbol
                        // In particular, a dash is not allowed
                        // Auto-switch to a different mod symbol

                        symbolToUse = availableSymbols.Dequeue();
                        modResult = mMolecularWeightTool.Peptide.SetModificationSymbol(symbolToUse.ToString(), modificationMass, indicatesPhospho, modComment);
                        badSymbolMap.Add(modPair.Key, symbolToUse); // Add it to the dictionary
                    }

                    // If modResult = 0 symbol add is successful
                    // This is a useful spot for a breakpoint
                    if (modResult != 0)
                    {
                        Console.WriteLine("Error defining modification with symbol {0} and mass{1}", symbolToUse, modPair.Value);
                    }
                }
            }

            var peptideFix = peptide;
            foreach (var item in badSymbolMap)
            {
                peptideFix = peptideFix.Replace(item.Key, item.Value);
            }

            // Obtain the fragmentation spectrum for a peptide
            // First define the peptide sequence
            // Need to pass "false" to parameter is3LetterCode since "peptide" is in one-letter notation
            mMolecularWeightTool.Peptide.SetSequence(peptideFix, NTerminusGroupType.Hydrogen, CTerminusGroupType.Hydroxyl, false);

            // Update the options
            mMolecularWeightTool.Peptide.SetFragmentationSpectrumOptions(fragSpectrumOptions);

            // Get the fragmentation masses = null;
            mMolecularWeightTool.Peptide.GetFragmentationMasses(out var fragSpectrum);

            var cleanPeptide = mMolecularWeightTool.Peptide.GetSequence(false, false, false, false, false);

            if (string.IsNullOrWhiteSpace(cleanPeptide))
            {
                // No valid residues
                return new List<KeyValuePair<string, double>>();
            }

            // Obtain the list of ions
            var theoryList = GetTheoryList(cleanPeptide, fragmentationMode, modeString1, fragSpectrum);

            return theoryList;
        }

        private List<KeyValuePair<string, double>> GetTheoryList(
            string peptide,
            FragmentationMode fragmentationMode,
            char modeString1,
            IReadOnlyList<FragmentationSpectrumData> fragSpectrum)
        {
            var theoryList = new List<KeyValuePair<string, double>>();

            double nTerminusResidueMass = 0;
            if (mDeNovoTableB.ContainsKey(peptide[0]))
                nTerminusResidueMass = mDeNovoTableB[peptide[0]];

            // Generate the first b or c ion, as 1+, 2+, and 3+
            for (var charge = 1; charge <= 3; charge++)
            {
                var ionDescription = modeString1 + "1";
                if (charge > 1)
                    ionDescription += new string('+', charge);

                if (fragmentationMode == FragmentationMode.ETD)
                    theoryList.Add(new KeyValuePair<string, double>(ionDescription, nTerminusResidueMass / charge));
                else
                    theoryList.Add(new KeyValuePair<string, double>(ionDescription, (nTerminusResidueMass + 18) / charge));
            }

            var peptideResidueCount = peptide.Length;
            var cTermModeSymbolFlag = modeString1 + (peptideResidueCount - 1).ToString();
            var cTermMassesAdded = false;

            double cTerminusResidueMass = 0;
            if (mDeNovoTableB.ContainsKey(peptide.Last()))
                cTerminusResidueMass = mDeNovoTableB[peptide.Last()];

            // Generate every other ion except the last of the b or c ion series
            for (var i = 0; i < fragSpectrum.Count; i++)
            {
                try
                {
                    if (fragSpectrum[i].Symbol.StartsWith("Shoulder"))
                    {
                        // Shoulder ion; ignore it
                        continue;
                    }
                    else
                    {
                        theoryList.Add(new KeyValuePair<string, double>(fragSpectrum[i].Symbol, fragSpectrum[i].Mass));

                        // Generate the last b or c ion, as 1+, 2+, and 3+
                        // This code only works if peptide contains all capital letters

                        if (fragSpectrum[i].Symbol != cTermModeSymbolFlag || cTermMassesAdded)
                        {
                            continue;
                        }

                        cTermMassesAdded = true;
                        for (var charge = 1; charge <= 3; charge++)
                        {
                            var ionDescription = modeString1 + peptideResidueCount.ToString();
                            if (charge > 1)
                                ionDescription += new string('+', charge);

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
