using System.IO;
using System.Collections.Generic;

namespace SpectrumLook.Builders
{
    internal class BuilderDirector
    {
        private ActualListBuilder mActualBuilder;
        private ComparedListBuilder mComparedBuilder;
        private TheoryListBuilder mTheoryBuilder;
        private IExperimentParser mParser;

        public List<Element> BuildActualList(int scanNumber, string filePath)
        {
            const string mzXML = ".mzxml";
            const string mzML = ".mzml";
            const string raw = ".raw";

            var extension = Path.GetExtension(filePath).ToLower();

            if (mParser == null || mParser.FilePath != filePath)
            {
                if (extension.Equals(mzXML))
                {
                    mParser = new MzXMLParser(filePath);
                    mActualBuilder = new ActualListBuilder(scanNumber, mParser);
                }
                else if (extension.Equals(mzML))
                {
                    mParser = new MzMLParser(filePath);
                    mActualBuilder = new ActualListBuilder(scanNumber, mParser);
                }
                else if (extension.Equals(raw))
                {
                    mParser = new ThermoRawParser(filePath);
                    mActualBuilder = new ActualListBuilder(scanNumber, mParser);
                }
            }
            else
            {
                mActualBuilder.SetScanNumber(scanNumber);
            }

            mActualBuilder.BuildList();

            return mActualBuilder.ElementList;
        }

        public List<Element> GetActualList()
        {
            var actualList = new List<Element>();

            if (mActualBuilder != null)
            {
                actualList = mActualBuilder.ElementList;
            }

            return actualList;
        }

        public List<Element> BuildComparedList(double possibleError, double lowerBoundPossibleError, List<Element> actualElementList, double precursor, ref List<Element> theoryElementList)
        {
            var copyOfActualElementList = new List<Element>(actualElementList);

            for (var i = 0; i < copyOfActualElementList.Count; ++i)
            {
                copyOfActualElementList[i].Annotation = string.Empty;
                copyOfActualElementList[i].Matched = false;
            }

            // Need to add the lowerBoundPossibleError into the function call below.
            mComparedBuilder = new ComparedListBuilder(possibleError, lowerBoundPossibleError, copyOfActualElementList, precursor, ref theoryElementList);

            mComparedBuilder.BuildList();

            return mComparedBuilder.ElementList;
        }

        public List<Element> GetComparedList()
        {
            var comparedList = new List<Element>();

            if (mComparedBuilder != null)
            {
                comparedList = mComparedBuilder.ElementList;
            }

            return comparedList;
        }

        public List<Element> BuildTheoryList(string peptide, bool fragmentationModeETD, Dictionary<char, double> modificationList)
        {
            mTheoryBuilder = new TheoryListBuilder(peptide, fragmentationModeETD, new MolecularWeightUtility(modificationList));

            mTheoryBuilder.BuildList();

            return mTheoryBuilder.ElementList;
        }

        public List<Element> GetTheoryList()
        {
            var theoryList = new List<Element>();

            if (mTheoryBuilder != null)
            {
                theoryList = mTheoryBuilder.ElementList;
            }

            return theoryList;
        }
    }
}
