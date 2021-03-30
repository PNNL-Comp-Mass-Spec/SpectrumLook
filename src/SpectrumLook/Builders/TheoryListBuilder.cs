using System.Collections.Generic;

namespace SpectrumLook.Builders
{
    class TheoryListBuilder : ElementListBuilder
    {
        /// <summary>
        /// the peptide that was passed into this builder that the theory set is calculated from
        /// </summary>
        private readonly string mPeptide;

        /// <summary>
        /// A flag representing the fragmentation mode for the calculations (false = CID, true = ETD)
        /// </summary>
        private readonly bool mFragmentationModeETD;

        /// <summary>
        /// The theory calculator that handles the brunt of calculating peptides
        /// </summary>
        private readonly ITheoryCalculator mTheoryCalculator;

        /// <summary>
        /// Constructor for the TheoryListBuilder
        /// </summary>
        /// <param name="peptide"></param>
        /// <param name="fragmentationModeCID"></param>
        /// <param name="theoryCalculator"></param>
        public TheoryListBuilder(string peptide, bool fragmentationModeCID, ITheoryCalculator theoryCalculator)
        {
            mPeptide = peptide;
            mFragmentationModeETD = fragmentationModeCID;
            mTheoryCalculator = theoryCalculator;
        }

        /// <summary>
        /// Builds the theory List from the data given in the constructor for this class
        /// </summary>
        public override void BuildList()
        {
            var theoreticalData = mTheoryCalculator.GetTheoreticalDataByPeptideSequence(mPeptide, mFragmentationModeETD);

            ElementList = new List<Element>();

            foreach (var theoreticalIon in theoreticalData)
            {
                var newElement = new Element
                {
                    Annotation = theoreticalIon.Key,
                    Mz = theoreticalIon.Value
                };

                ElementList.Add(newElement);
            }
        }
    }
}
