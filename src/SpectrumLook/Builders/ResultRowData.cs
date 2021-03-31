namespace SpectrumLook.Builders
{
    public class ResultRowData
    {
        public string DatasetName;
        public string ScanNumber;
        public string Peptide;
        public string PrecursorMZ;

        public double DblPrecursorMZ => PrecursorMZ == null ? 0 : double.Parse(PrecursorMZ);

        public ResultRowData(string datasetName, string scanNumber, string peptide, string precursorMZ)
        {
            DatasetName = datasetName;
            ScanNumber = scanNumber;
            Peptide = peptide;
            PrecursorMZ = precursorMZ;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", ScanNumber, Peptide);
        }
    }
}
