using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;

namespace SpectrumLook.Builders
{
    static class DataBuilder
    {
        public static DataTable GetDataTable(
            ISynopsisParser synopsisParser,
            out Manager.SynFileColumnIndices synFileColumns)
        {
            synFileColumns = new Manager.SynFileColumnIndices();

            var dataTable = new DataTable("DataViewTable");
            var headerRow = synopsisParser.GetNextRow();

            if (headerRow == null)
            {
                throw new System.InvalidProgramException("The synopsis file is empty");
            }

            //First GetNextColumn actually gets First Row. Getting Peptide/Scan indices based off first row
            synFileColumns.Peptide = FindColumnIndex(headerRow, "Peptide", "Peptide_p");

            synFileColumns.Scan = FindColumnIndex(headerRow, "Scan", "ScanNum", "ScanNum_s", "Scan_s");

            synFileColumns.PrecursorMz = FindColumnIndex(headerRow, "PrecursorMZ");

            if (synFileColumns.PrecursorMz < 0)
            {
                synFileColumns.ParentMH = FindColumnIndex(headerRow, "MH");
                synFileColumns.Charge = FindColumnIndex(headerRow, "ChargeState", "Charge");
            }

            synFileColumns.Dataset = FindColumnIndex(headerRow, "Dataset");

            foreach (var headerName in headerRow)
            {
                dataTable.Columns.Add(new DataColumn(headerName, typeof(string)));
                dataTable.Columns[headerName].ReadOnly = headerName.IndexOf("peptide_p", StringComparison.OrdinalIgnoreCase) <= 0;
            }

            while (true)
            {
                var dataLine = synopsisParser.GetNextRow();
                if (dataLine == null)
                    break;

                var row = dataTable.NewRow();

                for (var i = 0; i < dataLine.Length; i++)
                    row[i] = dataLine[i];

                dataTable.Rows.Add(row);
            }

            //ProgressWindow.Close();
            return dataTable;
        }

        /// <summary>
        /// Determine the index of the column
        /// </summary>
        /// <param name="headerColumns">Header row columns</param>
        /// <param name="columnNameSynonyms">Column names to find</param>
        /// <returns>The index of the column if found, otherwise -1</returns>
        private static int FindColumnIndex(IReadOnlyList<string> headerColumns, params string[] columnNameSynonyms)
        {
            foreach (var synonym in columnNameSynonyms)
            {
                for (var i = 0; i < headerColumns.Count; i++)
                {
                    if (headerColumns[i].Equals(synonym))
                        return i;
                }
            }

            return -1;
        }
    }
}
