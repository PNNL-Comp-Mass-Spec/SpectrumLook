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
        static public DataTable GetDataTable(ISynopsysParser synopsisParser, ref int PeptideColumnIndex, ref int ScanColumnIndex, ref int PrecursorMzColumnInt, ref int DatasetColumnIndex)
        {
            //DataProgress ProgressWindow = new DataProgress();
            ////ProgressWindow.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            ////ProgressWindow.progressBar1.MarqueeAnimationSpeed = 30;
            //ProgressWindow.Show();

            DataRow row = null;
            var dataTable = new DataTable("DataViewTable");
            string[] InLine;
            InLine = synopsisParser.GetNextRow();

            //First GetNextColumn actually gets First Row. Getting Peptide/Scan indices based off first row
            PeptideColumnIndex = GetPeptideStringColumnIndex(InLine);
            ScanColumnIndex = GetScanNumberColumnIndex(InLine);
            PrecursorMzColumnInt = GetPrecursorMzColumnIndex(InLine);
            DatasetColumnIndex = GetDatasetColumnIndex(InLine);

            if (InLine == null)
            {
                throw new System.InvalidProgramException("The synopsis file is empty");
            }
            else
            {
                foreach (var i in InLine)
                {
                    dataTable.Columns.Add(new DataColumn(i, typeof(string)));
                    if (i.ToLower().Contains("peptide_p"))
                    {
                        dataTable.Columns[i].ReadOnly = false;
                    }
                    else
                    {
                        dataTable.Columns[i].ReadOnly = true;
                    }
                }
                InLine = synopsisParser.GetNextRow();

                while (InLine != null)
                {
                    row = dataTable.NewRow();
                    row.ItemArray = InLine;
                    dataTable.Rows.Add(row);

                    InLine = synopsisParser.GetNextRow();
                }
            }

            //ProgressWindow.Close();
            return dataTable;
        }


        /// <summary>
        /// Returns the Column Index of the ScanNumber string column within the header row
        /// Returns -1 if nothing is found
        /// </summary>
        public static int GetScanNumberColumnIndex(string[] HeaderRow)
        {
            var PotentialScanNumberColumnNames = new List<string>();
            PotentialScanNumberColumnNames.Add("ScanNum");
            PotentialScanNumberColumnNames.Add("Scan");
            PotentialScanNumberColumnNames.Add("ScanNum_s");    //_s Appended in SequestParser/GetNextColumn
            PotentialScanNumberColumnNames.Add("Scan_s");

            var i = 0;
            foreach(var s in HeaderRow)
            {
                if (PotentialScanNumberColumnNames.Contains(s))
                    return i;
                i++;
            }

            return -1;
        }

        /// <summary>
        /// Returns the Column Index of the Precursor m/z string column within the header row
        /// Returns -1 if nothing is found
        /// </summary>
        public static int GetPrecursorMzColumnIndex(string[] HeaderRow)
        {
            var PotentialPrecursorMzColumnNames = new List<string>();
            PotentialPrecursorMzColumnNames.Add("PrecursorMZ");

            var i = 0;
            foreach (var s in HeaderRow)
            {
                if (PotentialPrecursorMzColumnNames.Contains(s))
                    return i;
                i++;
            }

            return -1;
        }

        /// <summary>
        /// Returns the Column Index of the Dataset string column within the header row
        /// Returns -1 if nothing is found
        /// </summary>
        public static int GetDatasetColumnIndex(string[] HeaderRow)
        {
            var PotentialDatasetNameColumnNames = new List<string>();
            PotentialDatasetNameColumnNames.Add("Dataset");

            var i = 0;
            foreach (var s in HeaderRow)
            {
                if (PotentialDatasetNameColumnNames.Contains(s))
                    return i;
                i++;
            }

            return -1;
        }

        /// <summary>
        /// Returns the Column Index of the Peptide string column within the header row
        /// Returns -1 if nothing is found
        /// </summary>
        public static int GetPeptideStringColumnIndex(string[] HeaderRow)
        {
            var PotentialPeptideColumnNames = new List<string>();
            PotentialPeptideColumnNames.Add("Peptide"); //_p Appended in SequestParser/GetNextColumn
            PotentialPeptideColumnNames.Add("Peptide_p");

            var i = 0;
            foreach(var s in HeaderRow)
            {
                if (PotentialPeptideColumnNames.Contains(s))
                    return i;
                i++;
            }
            return -1;
        }
    }
}
