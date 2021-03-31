using System.Collections.Generic;

namespace SpectrumLook.Builders
{
    internal class TheoryListBuilder : ElementListBuilder
    {
        /// <summary>
        /// the peptide that was passed into this builder that the theory set is calculated from
        /// </summary>
        private readonly string mPeptide;

        /// <summary>
        /// A flag representing the fragmentation mode for the calculations
        /// </summary>
        private readonly Builders.FragmentationMode mFragmentationMode;

        /// <summary>
        /// The theory calculator that handles the brunt of calculating peptides
        /// </summary>
        private readonly ITheoryCalculator mTheoryCalculator;

        /// <summary>
        /// Constructor for the TheoryListBuilder
        /// </summary>
        /// <param name="peptide"></param>
        /// <param name="fragmentationMode"></param>
        /// <param name="theoryCalculator"></param>
        public TheoryListBuilder(string peptide, Builders.FragmentationMode fragmentationMode, ITheoryCalculator theoryCalculator)
        {
            mPeptide = peptide;
            mFragmentationMode = fragmentationMode;
            mTheoryCalculator = theoryCalculator;
        }

        /// <summary>
        /// Builds the theory List from the data given in the constructor for this class
        /// </summary>
        public override void BuildList()
        {
            var theoreticalData = mTheoryCalculator.GetTheoreticalDataByPeptideSequence(mPeptide, mFragmentationMode);

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
