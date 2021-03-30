using System.Collections.Generic;
using System.IO;
using System.Text;
using PHRPReader;
using PHRPReader.Data;
using SpectrumLook.Views.Options;

namespace SpectrumLook.Builders
{
    public class PHRPReaderParser : ISynopsisParser
    {
        private readonly ReaderFactory mReader;
        private readonly FragmentLadderOptions mFragmentationLadderOptions;
        private bool mFirstRead = true;
        private readonly int mPeptideColumnIndex;
        private readonly string[] mFirstLine;

        public PHRPReaderParser(string synopsisFilePath, FragmentLadderOptions fragLadderOptions)
        {
            mFragmentationLadderOptions = fragLadderOptions;
            mFragmentationLadderOptions.ModificationList.Clear();
            using (var reader = new StreamReader(synopsisFilePath))
            {
                mFirstLine = reader.ReadLine().Split('\t');
                for (var i = 0; i < mFirstLine.Length; i++)
                {
                    if (mFirstLine[i] == "Peptide")
                    {
                        mPeptideColumnIndex = i;
                        break;
                    }
                }
            }
            var startupOptions = new StartupOptions
            {
                LoadModsAndSeqInfo = true,
                LoadMSGFResults = true,
                LoadScanStatsData = false,
                MaxProteinsPerPSM = 100
            };
            mReader = new ReaderFactory(synopsisFilePath, startupOptions)
            {
                // mReader.FastReadMode = true;
                SkipDuplicatePSMs = true
            };
        }

        public string[] GetNextRow()
        {
            if (mFirstRead)
            {
                mFirstRead = false;
                return mFirstLine;
            }
            if (mReader.MoveNext())
            {
                var line = mReader.CurrentPSM.DataLineText;
                var residues = mReader.CurrentPSM.ModifiedResidues;
                var cleanSequence = mReader.CurrentPSM.PeptideCleanSequence;
                var splitLine = line.Split('\t');

                var modPeptide = PeptideWithAllMods(cleanSequence, residues);
                PeptideCleavageStateCalculator.SplitPrefixAndSuffixFromSequence(
                    mReader.CurrentPSM.Peptide, out var sequence, out var prefix, out var suffix);

                splitLine[mPeptideColumnIndex] = prefix + "." + modPeptide + "." + suffix;

                return splitLine;
            }
            return null;
        }

        private string PeptideWithAllMods(string cleanSequence, List<AminoAcidModInfo> residues)
        {
            var modPeptide = new StringBuilder();

            // Add any new mods to the modification list
            foreach (var modInfo in residues)
            {
                var modSym = modInfo.ModDefinition.ModificationSymbol;
                if (!mFragmentationLadderOptions.ModificationList.ContainsKey(modSym))
                {
                    var modMass = modInfo.ModDefinition.ModificationMass;
                    mFragmentationLadderOptions.ModificationList.Add(modSym, modMass);
                }
            }

            for (var i = 0; i < cleanSequence.Length; i++)
            {
                modPeptide.Append(cleanSequence[i]);

                // Add any mods for the specified location
                foreach (var modInfo in residues)
                {
                    var modSym = modInfo.ModDefinition.ModificationSymbol;
                    var loc = modInfo.ResidueLocInPeptide;
                    var term = modInfo.TerminusState;

                    if (i + 1 == loc)
                    {
                        modPeptide.Append(modSym);
                    }
                }
            }
            return modPeptide.ToString();
        }
    }
}
