using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.Builders
{
    class BuilderDirector
    {
        ActualListBuilder m_actualBuilder;
        ComparedListBuilder m_comparedBuilder;
        TheoryListBuilder m_theoryBuilder;
        IExperimentParser m_parser;
        
        public BuilderDirector()
        {
        }

        #region ActualList

        public List<Element> BuildActualList(int scanNumber, string fileLocation)       //Idea is to check the extension, call the right parser based on extension of file name.
        {
            string mzXML = ".mzxml";
            string raw = ".raw";
            string extension = Path.GetExtension(fileLocation);
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
                if(m_parser.Filename != fileLocation)
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

                m_actualBuilder.SetScanNumber(scanNumber);

            }


            m_actualBuilder.BuildList();

            return m_actualBuilder.ElementList;
        }

        public List<Element> GetActualList()
        {
            List<Element> actualList = new List<Element>();

            if (m_actualBuilder != null)
            {
                actualList = m_actualBuilder.ElementList;
            }

            return actualList;
        }

        #endregion

        #region ComparedList

        public List<Element> BuildComparedList(double possibleError, double lowerBoundPossibleError, List<Element> actualElementList, ref List<Element> theoryElementList)
        {

            List<Element> copyOfActualElementList = new List<Element>(actualElementList);

            for (int i = 0; i < copyOfActualElementList.Count; ++i)
            {
                copyOfActualElementList[i].annotation = "";
                copyOfActualElementList[i].matched = false;
            }

            ///Need to add the lowerBoundPossibleError into the function call below.
            m_comparedBuilder = new ComparedListBuilder(possibleError, lowerBoundPossibleError, copyOfActualElementList, ref theoryElementList);

            m_comparedBuilder.BuildList();

            return m_comparedBuilder.ElementList;
        }

        public List<Element> GetComparedList()
        {
            List<Element> comparedList = new List<Element>();

            if (m_comparedBuilder != null)
            {
                comparedList = m_comparedBuilder.ElementList;
            }

            return comparedList;
        }

        #endregion

        #region TheoryList

        public List<Element> BuildTheoryList(string peptide, bool fragmentationModeETD, Dictionary<char, double> modificationList)
        {
            m_theoryBuilder = new TheoryListBuilder(peptide, fragmentationModeETD, new MolecularWeightCalculator(modificationList));
            
            m_theoryBuilder.BuildList();

            return m_theoryBuilder.ElementList;
        }

        public List<Element> GetTheoryList()
        {
            List<Element> theoryList = new List<Element>();

            if (m_theoryBuilder != null)
            {
                theoryList = m_theoryBuilder.ElementList;
            }

            return theoryList;
        }

        #endregion

    }
}
