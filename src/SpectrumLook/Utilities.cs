using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpectrumLook
{
    internal static class Utilities
    {
        public static int BatchSaveCounter { get; set; }

        private static readonly SortedSet<char> mInvalidFilenameChars = new();

        /// <summary>
        /// Generates a new plot file name, based on the user's options and the information used in the plot
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="usePeptideAndScanName"></param>
        /// <param name="peptide"></param>
        /// <param name="scanNumber"></param>
        /// <returns>Auto-generated filename</returns>
        public static string CreateNextPlotFileName(string baseName, bool usePeptideAndScanName, string peptide, string scanNumber)
        {
            BatchSaveCounter++;

            if (!usePeptideAndScanName)
            {
                // Attach a unique number to the saved file, since we are not guaranteed uniqueness from peptide or scan number alone.
                return string.Format("{0}_{1:0000}", baseName, BatchSaveCounter);
            }

            // Replace asterisks with underscores
            peptide = peptide.Replace("*", "_");

            // Check for any other invalid characters

            if (mInvalidFilenameChars.Count == 0)
            {
                foreach (var character in Path.GetInvalidPathChars())
                {
                    mInvalidFilenameChars.Add(character);
                }

                foreach (var character in Path.GetInvalidFileNameChars())
                {
                    if (!mInvalidFilenameChars.Contains(character))
                        mInvalidFilenameChars.Add(character);
                }
            }

            var updateRequired = mInvalidFilenameChars.Any(invalidChar => peptide.Contains(invalidChar));

            string cleanPeptide;
            if (updateRequired)
            {
                var residues = new StringBuilder();

                foreach (var residue in peptide)
                {
                    if (mInvalidFilenameChars.Contains(residue))
                        residues.Append("_");
                    else
                        residues.Append(residue);
                }

                cleanPeptide = residues.ToString();
            }
            else
            {
                cleanPeptide = peptide;
            }

            return string.Format("{0}_{1}_{2}", baseName, cleanPeptide, scanNumber);
        }
    }
}
