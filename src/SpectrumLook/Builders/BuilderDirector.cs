using System;
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
        
        public BuilderDirector()
        {
        }

        #region ActualList

        public List<Element> BuildActualList(int scanNumber, string fileLocation)
        {
            m_actualBuilder = new ActualListBuilder(scanNumber, new MzParser(fileLocation));

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

        public List<Element> BuildComparedList(double possibleError, List<Element> actualElementList, ref List<Element> theoryElementList)
        {

            List<Element> copyOfActualElementList = new List<Element>(actualElementList);

            for (int i = 0; i < copyOfActualElementList.Count; ++i)
            {
                copyOfActualElementList[i].annotation = "";
                copyOfActualElementList[i].matched = false;
            }


                m_comparedBuilder = new ComparedListBuilder(possibleError, copyOfActualElementList, ref theoryElementList);

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

        public List<Element> BuildTheoryList(string peptide, bool fragmentationModeCID, List<string> modificationList)
        {
            m_theoryBuilder = new TheoryListBuilder(peptide, fragmentationModeCID, new MolecularWeightCalculator(modificationList));
            
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
