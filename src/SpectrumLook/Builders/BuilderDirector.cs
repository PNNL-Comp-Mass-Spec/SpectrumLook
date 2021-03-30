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

        public List<Element> BuildActualList(int scanNumber, string fileLocation)       // Idea is to check the extension, call the right parser based on extension of file name.
        {
            var mzXML = ".mzxml";
            var raw = ".raw";
            var extension = Path.GetExtension(fileLocation);
            extension = extension.ToLower();
            if (m_parser == null)
            {
                if (extension.Equals(mzXML))
                {
                    m_parser = new MzParser(fileLocation);
                    m_actualBuilder = new ActualListBuilder(scanNumber, m_parser);
                }

                if (extension.Equals(raw))
                {
                    m_parser = new ThermoRawParser(fileLocation);
                    m_actualBuilder = new ActualListBuilder(scanNumber, m_parser);
                }
            }
            else
            {
                if (m_parser.Filename != fileLocation)
                {
                    if (extension.Equals(mzXML))
                    {
                        m_parser = new MzParser(fileLocation);
                        m_actualBuilder = new ActualListBuilder(scanNumber, m_parser);
                    }

                    if (extension.Equals(raw))
                    {
                        m_parser = new ThermoRawParser(fileLocation);
                        m_actualBuilder = new ActualListBuilder(scanNumber, m_parser);
                    }
                }

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
