using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.Builders
{
    class TheoryListBuilder : ElementListBuilder
    {
        #region MEMBERS
        /// <summary>
        /// the peptide that was passed into this builder that the theory set is calculated from
        /// </summary>
        private string m_peptide;
        
        /// <summary>
        /// A flag representing the fragmentation mode for the calculations (false = CID, true = ETD)
        /// </summary>
        private bool m_fragmentationModeCID;

        /// <summary>
        /// The theory calculator that handles the brunt of calculating peptides
        /// </summary>
        private ITheoryCalculator m_theoryCalculator;
        
        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Constructor for the TheoryListBuilder
        /// </summary>
        /// <param name="peptide"></param>
        /// <param name="fragmentationModeCID"></param>
        public TheoryListBuilder(string peptide, bool fragmentationModeCID, ITheoryCalculator theoryCalculator)
        {
            m_peptide = peptide;
            m_fragmentationModeCID = fragmentationModeCID;
            m_theoryCalculator = theoryCalculator;
        }
        
        #endregion

        #region FUNCTIONS

        /// <summary>
        /// Builds the theory List from the data given in the constructor for this class
        /// </summary>
        public override void BuildList()
        {
            string[] theoreticalData = this.m_theoryCalculator.GetTheoreticalDataByPeptideSequence(m_peptide, m_fragmentationModeCID);

            ElementList = new List<Element>();

            for(int i = 0; i < theoreticalData.Count(); i += 2)
            {
                Element newElement = new Element();

                /// The output is a string array that should be structured such that annotations are odd index values (starting from 1)
                /// and mzValues are even index values (starting from 0).
                newElement.annotation = theoreticalData[i + 1];
                newElement.mzValue = Convert.ToDouble(theoreticalData[i]);
                newElement.intensity = 0;
                newElement.matched = false;

                ElementList.Add(newElement);
            }
        }

        #endregion
    }
}
