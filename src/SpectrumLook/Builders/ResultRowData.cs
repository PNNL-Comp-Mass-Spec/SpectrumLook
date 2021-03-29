namespace SpectrumLook.Builders
{
    public class ResultRowData
    {
        public string DatasetName;
        public string ScanNumber;
        public string Peptide;
        public string PrecursorMZ;

        public int IntScanNumber => int.Parse(ScanNumber);
        public double DblPrecursorMZ => double.Parse(PrecursorMZ);

        public ResultRowData(string datasetName, string scanNumber, string peptide, string precursorMZ)
        {
            DatasetName = datasetName;
            ScanNumber = scanNumber;
            Peptide = peptide;
            PrecursorMZ = precursorMZ;
        }
    }
}
