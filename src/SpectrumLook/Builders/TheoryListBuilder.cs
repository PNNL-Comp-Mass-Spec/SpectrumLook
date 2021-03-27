using System.Collections.Generic;

namespace SpectrumLook.Builders
{
    class TheoryListBuilder : ElementListBuilder
    {
        /// <summary>
        /// the peptide that was passed into this builder that the theory set is calculated from
        /// </summary>
        private string m_peptide;

        /// <summary>
        /// A flag representing the fragmentation mode for the calculations (false = CID, true = ETD)
        /// </summary>
        private bool m_fragmentationModeETD;

        /// <summary>
        /// The theory calculator that handles the brunt of calculating peptides
        /// </summary>
        private ITheoryCalculator m_theoryCalculator;

        /// <summary>
        /// Constructor for the TheoryListBuilder
        /// </summary>
        /// <param name="peptide"></param>
        /// <param name="fragmentationModeCID"></param>
        public TheoryListBuilder(string peptide, bool fragmentationModeCID, ITheoryCalculator theoryCalculator)
        {
            m_peptide = peptide;
            m_fragmentationModeETD = fragmentationModeCID;
            m_theoryCalculator = theoryCalculator;
        }

        /// <summary>
        /// Builds the theory List from the data given in the constructor for this class
        /// </summary>
        public override void BuildList()
        {
            var theoreticalData = m_theoryCalculator.GetTheoreticalDataByPeptideSequence(m_peptide, m_fragmentationModeETD);

            ElementList = new List<Element>();

            foreach (var theoreticalIon in theoreticalData)
            {
                var newElement = new Element
                {
                    Annotation = theoreticalIon.Key,
                    Mz = theoreticalIon.Value,
                    Intensity = 0,
                    Matched = false
                };

                ElementList.Add(newElement);
            }
        }
    }
}
