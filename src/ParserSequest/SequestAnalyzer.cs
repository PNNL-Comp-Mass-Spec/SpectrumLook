using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.ParserSequest
{
    /// <summary>
    /// Name:        PeptideTuple
    /// Description: This struct is used to handle information that is within the Sequest file.  Each piece of data represents a Column in the sequest file.
    /// </summary>
    public struct PeptideTuple
    {

        // TODO: Add Comments to each member
        public int HitNumber;
        public int ScanNumber;
        public int ScanCount;
        public int ChargeState;
        public double MH;
        public double XCorr;
        public double DelCn;
        public double Sp;
        public string Reference;
        public int MultiProtein;
        public string Peptide;
        public double DelCn2;
        public double RankSp;
        public double RankXc;
        public double DelM;
        public double XcRatio;
        public double PassFilt;
        public double MScore;
        public int NumberTrypicEnds;
    }

    /// <summary>
    /// Name:        SequestAnalyzer
    /// Description: This Sequest Analyzer is a simple tab spaced analyzer.  It reads a .txt file that is formated into a sequest file.
    ///              It takes in the name of a file location and outputs a Data Table.
    /// Updated:     1/26/2011
    /// By:          Patrick Tobin
    /// Group:       WSU Senior Design
    /// </summary>
    public class SequestAnalyzer
    {
        // TODO: Add Comments to fields
        public DataTable FileStructure
        {
            get
            {
                return mFileStructure;
            }
            internal set
            {
                mFileStructure= value;
            }
        }
        private DataTable mFileStructure;
        private string mFileLocation;
        private double mToloranceValue;
        private double mMassOfProton;
        private List<PeptideTuple> mSequestListData;
        private string[] mColumnHeaders;

        // TODO: Make it so this can ready any type of File format, as long as it has Scan numbers.
        /// <summary>
        /// Name:        SequestAnalyzer
        /// Description: This takes in a file location and loads the sequest file into memory, through the use of a data table.
        /// </summary>
        /// <param name="fileLocation">
        /// Input:       fileLocation is a string that represents a valid location to a sequest file.
        /// </param>
        public SequestAnalyzer(string fileLocation)
        {
            FileStructure = new DataTable();

            TextReader fileReader;
            mFileLocation = fileLocation;
            fileReader = File.OpenText(fileLocation);
            mMassOfProton = 1.00727638;

            // This is where the column headers are defined from the file.
            mColumnHeaders = fileReader.ReadLine().Split('\t');
            string currentLine; //<--- First line is telling what the Columns are
            DataRow RowtoAdd;
            PeptideTuple CurrentPeptide;

            this.mSequestListData = new List<PeptideTuple>();

            while ((currentLine = fileReader.ReadLine()) != null)
            {
                string[] TextColumn = currentLine.Split('\t');
                if (FileStructure.Columns.Count == 0)
                {
                    for (int i = 0; i < mColumnHeaders.Length; ++i)
                    {
                        FileStructure.Columns.Add(mColumnHeaders[i]);
                    }
                }
                RowtoAdd = FileStructure.NewRow();
                CurrentPeptide = new PeptideTuple();

                RowtoAdd[mColumnHeaders[0]] = Convert.ToInt32(TextColumn[0]);    // Hit Number
                CurrentPeptide.HitNumber = Convert.ToInt32(TextColumn[0]);

                RowtoAdd[mColumnHeaders[1]] = Convert.ToInt32(TextColumn[1]);    // ScanNumber
                CurrentPeptide.ScanNumber = Convert.ToInt32(TextColumn[1]);

                RowtoAdd[mColumnHeaders[2]] = Convert.ToInt32(TextColumn[2]);    // ScanCount
                CurrentPeptide.ScanCount = Convert.ToInt32(TextColumn[2]);

                RowtoAdd[mColumnHeaders[3]] = Convert.ToInt32(TextColumn[3]);    // ChargeState
                CurrentPeptide.ChargeState = Convert.ToInt32(TextColumn[3]);

                RowtoAdd[mColumnHeaders[4]] = Convert.ToDouble(TextColumn[4]);   // MH
                CurrentPeptide.MH = Convert.ToDouble(TextColumn[4]);

                RowtoAdd[mColumnHeaders[5]] = Convert.ToDouble(TextColumn[5]);   // XCorr
                CurrentPeptide.XCorr = Convert.ToDouble(TextColumn[5]);

                RowtoAdd[mColumnHeaders[6]] = Convert.ToDouble(TextColumn[6]);   // DelCn
                CurrentPeptide.DelCn = Convert.ToDouble(TextColumn[6]);

                RowtoAdd[mColumnHeaders[7]] = Convert.ToDouble(TextColumn[7]);   // Sp
                CurrentPeptide.Sp = Convert.ToDouble(TextColumn[7]);

                RowtoAdd[mColumnHeaders[8]] = TextColumn[8];                     // Reference
                CurrentPeptide.Reference = TextColumn[8];

                RowtoAdd[mColumnHeaders[9]] = Convert.ToInt32(TextColumn[9]);    // Multiprotein
                CurrentPeptide.MultiProtein = Convert.ToInt32(TextColumn[9]);

                RowtoAdd[mColumnHeaders[10]] = TextColumn[10];                   // Peptide
                CurrentPeptide.Peptide = TextColumn[10];

                RowtoAdd[mColumnHeaders[11]] = Convert.ToDouble(TextColumn[11]); // DelCn2
                CurrentPeptide.DelCn2 = Convert.ToDouble(TextColumn[11]);

                RowtoAdd[mColumnHeaders[12]] = Convert.ToDouble(TextColumn[12]); // RankSp
                CurrentPeptide.RankSp = Convert.ToDouble(TextColumn[12]);

                RowtoAdd[mColumnHeaders[13]] = Convert.ToDouble(TextColumn[13]); // RankXc
                CurrentPeptide.RankXc = Convert.ToDouble(TextColumn[13]);

                RowtoAdd[mColumnHeaders[14]] = Convert.ToDouble(TextColumn[14]); // DelM
                CurrentPeptide.DelM = Convert.ToDouble(TextColumn[14]);

                RowtoAdd[mColumnHeaders[15]] = Convert.ToDouble(TextColumn[15]); // XcRatio
                CurrentPeptide.XcRatio = Convert.ToDouble(TextColumn[15]);

                RowtoAdd[mColumnHeaders[16]] = Convert.ToDouble(TextColumn[16]); // PassFilt
                CurrentPeptide.PassFilt = Convert.ToDouble(TextColumn[16]);

                RowtoAdd[mColumnHeaders[17]] = Convert.ToDouble(TextColumn[17]); // MScore
                CurrentPeptide.MScore = Convert.ToDouble(TextColumn[17]);

                RowtoAdd[mColumnHeaders[18]] = Convert.ToInt32(TextColumn[18]);  // NumberTrypicEnds
                CurrentPeptide.NumberTrypicEnds = Convert.ToInt32(TextColumn[18]);

                this.mSequestListData.Add(CurrentPeptide);

                FileStructure.Rows.Add(RowtoAdd);
            }

            fileReader.Close();
        }

        // TODO: Add Comments to this
        // TODO: MAKE THIS A PROPERTY?
        /// <summary>
        /// Name:        GetSequestList
        /// Description: This function fills a list of peptide tuples with the corisponding values that are currently in the data table.
        /// </summary>
        /// <returns>
        /// Output:      A list of peptide tuples that corispond with current data table.
        /// </returns>
        public List<PeptideTuple> GetSequestList()
        {
            return mSequestListData;
        }

        /// <summary>
        /// Name:        ConverToDataTable
        /// Description: This converts the inputed List of peptide tuples and outputs a Data Table.
        /// </summary>
        /// <param name="source">
        /// Input:     This is an inputed list of peptide tuples.
        /// </param>
        /// <returns>
        /// Output     This is a data table, where each column is a data member within The peptide Tuple.
        /// </returns>
        public DataTable ConvertToDataTable(List<PeptideTuple> source)
        {
            DataTable ReturnedDataTable = new DataTable();
            DataRow CurrentDataRow;

            foreach (PeptideTuple CurrentTuple in source)
            {
                if (ReturnedDataTable.Columns.Count == 0)
                {
                    for (int i = 0; i < mColumnHeaders.Length; ++i)
                    {
                        ReturnedDataTable.Columns.Add(mColumnHeaders[i]);
                    }
                }
                CurrentDataRow = ReturnedDataTable.NewRow();

                // Filling the current row from the inputed list.
                CurrentDataRow[mColumnHeaders[(int)DataColumn.HitNumber]] = CurrentTuple.HitNumber;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.ScanNumber]] = CurrentTuple.ScanNumber;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.ScanCount]] = CurrentTuple.ScanCount;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.ChargeState]] = CurrentTuple.ChargeState;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.MH]] = CurrentTuple.MH;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.XCorr]] = CurrentTuple.XCorr;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.DelCn]] = CurrentTuple.DelCn;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.Sp]] = CurrentTuple.Sp;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.Reference]] = CurrentTuple.Reference;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.MultiProtein]] = CurrentTuple.MultiProtein;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.Peptide]] = CurrentTuple.Peptide;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.DelCn2]] = CurrentTuple.DelCn2;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.RankSp]] = CurrentTuple.RankSp;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.RankXc]] = CurrentTuple.RankXc;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.DelM]] = CurrentTuple.DelM;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.XcRatio]] = CurrentTuple.XcRatio;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.PassFilt]] = CurrentTuple.PassFilt;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.MScore]] = CurrentTuple.MScore;
                CurrentDataRow[mColumnHeaders[(int)DataColumn.NumberTrypicEnds]] = CurrentTuple.NumberTrypicEnds;

                ReturnedDataTable.Rows.Add(CurrentDataRow);
            }

            return ReturnedDataTable;
        }

        /// <summary>
        /// Name: DataColumn
        /// Description: These are defined numbers for looking through the data table.
        /// </summary>
        public enum DataColumn
        {
            HitNumber = 0,
            ScanNumber = 1,
            ScanCount = 2,
            ChargeState = 3,
            MH = 4,
            XCorr = 5,
            DelCn = 6,
            Sp = 7,
            Reference = 8,
            MultiProtein = 9,
            Peptide = 10,
            DelCn2 = 11,
            RankSp = 12,
            RankXc = 13,
            DelM = 14,
            XcRatio = 15,
            PassFilt = 16,
            MScore = 17,
            NumberTrypicEnds = 18
        }
    }
}