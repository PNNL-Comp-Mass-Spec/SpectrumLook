using System.Collections.Generic;
using System.IO;
using System.Text;
using PHRPReader;
using PHRPReader.Data;
using SpectrumLook.Views.Options;

namespace SpectrumLook.Builders
{
    public class PHRPReaderParser : ISynopsysParser
    {
        private ReaderFactory m_reader;
        private FragmentLadderOptions m_fragLadderOptions;
        private bool m_firstRead = true;
        private int m_peptideColumnIndex;
        private string[] m_firstLine;

        public PHRPReaderParser(string synopsisFilePath, FragmentLadderOptions fragLadderOptions)
        {
            m_fragLadderOptions = fragLadderOptions;
            m_fragLadderOptions.modificationList.Clear();
            using (var reader = new StreamReader(synopsisFilePath))
            {
                m_firstLine = reader.ReadLine().Split('\t');
                for (int i = 0; i < m_firstLine.Length; i++)
                {
                    if (m_firstLine[i] == "Peptide")
                    {
                        m_peptideColumnIndex = i;
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
            m_reader = new ReaderFactory(synopsisFilePath, startupOptions);
            //m_reader.FastReadMode = true;
            m_reader.SkipDuplicatePSMs = true;
        }

        public string[] GetNextRow()
        {
            if (m_firstRead)
            {
                m_firstRead = false;
                return m_firstLine;
            }
            if (m_reader.MoveNext())
            {
                string line = m_reader.CurrentPSM.DataLineText;
                var residues = m_reader.CurrentPSM.ModifiedResidues;
                string cleanSequence = m_reader.CurrentPSM.PeptideCleanSequence;
                string[] splitLine = line.Split('\t');

                string modPeptide = PeptideWithAllMods(cleanSequence, residues);
                string prefix;
                string suffix;
                string sequence;
                PeptideCleavageStateCalculator.SplitPrefixAndSuffixFromSequence(
                    m_reader.CurrentPSM.Peptide, out sequence, out prefix, out suffix);

                splitLine[m_peptideColumnIndex] = prefix + "." + modPeptide + "." + suffix;

                return splitLine;
            }
            return null;
        }

        private string PeptideWithAllMods(string cleanSequence, List<AminoAcidModInfo> residues)
        {
            StringBuilder modPeptide = new StringBuilder();

            // Add any new mods to the modification list
            foreach (var modInfo in residues)
            {
                var modSym = modInfo.ModDefinition.ModificationSymbol;
                if (!m_fragLadderOptions.modificationList.ContainsKey(modSym))
                {
                    var modMass = modInfo.ModDefinition.ModificationMass;
                    m_fragLadderOptions.modificationList.Add(modSym, modMass);
                }
            }

            for (int i = 0; i < cleanSequence.Length; i++)
            {
                modPeptide.Append(cleanSequence[i]);

                // Add any mods for the specified location
                foreach (var modInfo in residues)
                {
                    var modSym = modInfo.ModDefinition.ModificationSymbol;
                    int loc = modInfo.ResidueLocInPeptide;
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
