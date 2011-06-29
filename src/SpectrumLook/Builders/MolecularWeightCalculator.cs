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
        /// <returns></returns>
        public string[] GetTheoreticalDataByPeptideSequence(string peptide, bool fragmentationModeCID)
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
            string modeString1;
            if (fragmentationModeCID)
            {
                modeString1 = "c";
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itBIon].ShowIon = false;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itYIon].ShowIon = false;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itCIon].ShowIon = true;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itZIon].ShowIon = true;
                udtFragSpectrumOptions.IonTypeOptions[(int)MwtWinDll.MWPeptideClass.itIonTypeConstants.itAIon].ShowIon = false;
            }
            else
            {
                modeString1 = "b";
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
                int stringIndex = 0;

                foreach (string stringToFind in m_modificationList)
                {
                    string frontPart = "";
                    string backPart = "";
                    double outValue = 0.0;
                    bool foundspace = false;

                    for (stringIndex = 0; stringIndex < stringToFind.Length; ++stringIndex)
                    {
                        if (stringToFind[stringIndex] == '|')
                        {
                            foundspace = true;
                            continue;
                        }
                        if (!foundspace)
                            frontPart += stringToFind[stringIndex];
                        else
                            backPart += stringToFind[stringIndex];
                    }


                    bool randomBool = true;
                    string randomComment = "";

                    double.TryParse(backPart,out outValue);

                    m_mMwtWin.Peptide.SetModificationSymbol("[" + frontPart + "]", outValue, ref randomBool, ref randomComment);
                }
            }

            // Update the options
            m_mMwtWin.Peptide.SetFragmentationSpectrumOptions(ref udtFragSpectrumOptions);

            // Get the fragmentation masses
            MwtWinDll.MWPeptideClass.udtFragmentationSpectrumDataType[] udtFragSpectrum = null;
            m_mMwtWin.Peptide.GetFragmentationMasses(ref udtFragSpectrum);

            // Create a new theoretical list
            System.Collections.ArrayList theoryList = new System.Collections.ArrayList();

            // Generate the first b or c ion
            if (!fragmentationModeCID)
            {
                theoryList.Add((m_deNovoTableB[peptide[0]]).ToString());
                theoryList.Add(modeString1 + "1");
            }
            else
            {
                theoryList.Add((m_deNovoTableB[peptide[0]] + 18).ToString());
                theoryList.Add(modeString1 + "1");
            }

            // Generate the first b++ or c++ ion
            if (!fragmentationModeCID)
            {
                theoryList.Add((m_deNovoTableB[peptide[0]] / 2).ToString());
                theoryList.Add(modeString1 + "1++");
            }
            else
            {
                theoryList.Add(((m_deNovoTableB[peptide[0]] + 18) / 2).ToString());
                theoryList.Add(modeString1 + "1++");
            }

            // Generate the first b+++ or c+++ ion
            if (!fragmentationModeCID)
            {
                theoryList.Add((m_deNovoTableB[peptide[0]] / 3).ToString());
                theoryList.Add(modeString1 + "1+++");
            }
            else
            {
                theoryList.Add(((m_deNovoTableB[peptide[0]] + 18) / 3).ToString());
                theoryList.Add(modeString1 + "1+++");
            }

            // Generate every other ion except the last of the b or c ion series
            for (int i = 0; i < udtFragSpectrum.Length; i++)
            {
                try
                {
                    if ((udtFragSpectrum[i].Symbol[0] == 'S') && (udtFragSpectrum[i].Symbol[1] == 'h'))
                    {}
                    else
                    {
                        theoryList.Add((udtFragSpectrum[i].Mass).ToString());
                        theoryList.Add(udtFragSpectrum[i].Symbol);

                        // Generate the last b or c ion
                        if( (udtFragSpectrum[i].Symbol) == (modeString1 + (peptide.Length - 1).ToString()) )
                        {
                            theoryList.Add((udtFragSpectrum[i].Mass + m_deNovoTableB[peptide[peptide.Length - 1]]).ToString());
                            theoryList.Add(modeString1 + (peptide.Length).ToString());
                        }

                        // Generate the last b++ or c++ ion
                        if( (udtFragSpectrum[i].Symbol) == (modeString1 + (peptide.Length - 1).ToString()) + "++")
                        {
                            theoryList.Add((udtFragSpectrum[i].Mass + (m_deNovoTableB[peptide[peptide.Length - 1]] / 2)).ToString() );
                            theoryList.Add(modeString1 + (peptide.Length).ToString() + "++");
                        }

                        // Generate the last b+++ or c+++ ion
                        if( (udtFragSpectrum[i].Symbol) == (modeString1 + (peptide.Length - 1).ToString()) + "+++")
                        {
                            theoryList.Add((udtFragSpectrum[i].Mass + (m_deNovoTableB[peptide[peptide.Length - 1]] / 3)).ToString());
                            theoryList.Add(modeString1 + (peptide.Length).ToString() + "+++");
                        }
                    }
                }
                catch{}
            }

            string[] theoreticalList = (string[])theoryList.ToArray(typeof(string));
            return theoreticalList;
        }
    }
}
