using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.Builders
{
    /// <summary>
    /// This class is used to generate a list of elements based on two lists that are passed into the constructor
    /// for if there is a match or unmatched value in these two lists then it will be added to the output list of elements.
    /// To generate this list you must call the BuildList function and the use the elementList
    /// Property to retrieve the said list.
    /// By Patrick Tobin
    /// </summary>
    public class ComparedListBuilder : ElementListBuilder
    {
        /// <summary>
        /// This is a reference to the actualElementList that was passed into the constructor.
        /// </summary>
        private List<Element> m_actualElementList;

        /// <summary>
        /// This is a reference to the theoryElementList that was passed into the constructor.
        /// </summary>
        private List<Element> m_theoryElementList;

        /// <summary>
        /// This is a value that stores the possible error between the theoretical m/z values and the actual m/z values
        /// </summary>
        private double m_upperBoundTolerance;

        /// <summary>
        /// This is a value that stores the lower possible error between the theoretical m/z values and the actual m/z values.
        /// </summary>
        private double m_lowerBoundTolerance;

        private double m_precursor;

        public double possibleError
        {
            get
            {
                return m_upperBoundTolerance;
            }
            set
            {
                if (value >= 0.0)
                {
                    m_upperBoundTolerance = value;
                }
            }
        }

        public double lowerBoundPossibleError
        {
            get
            {
                return m_lowerBoundTolerance;
            }
            set
            {
                if (value >= 0.0)
                {
                    m_lowerBoundTolerance = value;
                }
            }
        }

        #region CONSTRUCTOR
        /// <summary>
        /// This constructor sets the initial value for the BuildList Function.
        /// </summary>
        /// <param name="possibleError">This is a number greater than zero where it is the possible error between m/z value in the theory List and the Actual List</param>
        /// <param name="actualElementList">This should be a reference to a list generated from the ActualListBuilder</param>
        /// <param name="precursor">The precursor, to label the precursor if present </param>
        /// <param name="theoryElementList">This should be a reference to a list generated from the TheoryListBuilder </param>
        public ComparedListBuilder(double possibleError, double lowerBoundPossibleError, List<Element> actualElementList, double precursor, ref List<Element> theoryElementList)
        {
            this.m_actualElementList    = actualElementList;
            this.m_theoryElementList    = theoryElementList;
            this.ElementList            = new List<Element>();
            this.m_precursor            = precursor;
            m_upperBoundTolerance       = possibleError;
            m_lowerBoundTolerance       = lowerBoundPossibleError;
        }
        #endregion

        #region FUNCTIONS

        #region PUBLIC

        /// <summary>
        /// BuildElement builds a List of elements based on the actualElementList, theoryElementList
        /// and the possibleError passed into the constructor.
        /// </summary>
        //TODO : Make an option to do a method to do highest Intensity or closest value in that "range" of errored values.
        //public override void BuildList()
        //{
        //    int currentActualIndex = 0;
        //    int currentTheoryIndex = 0;
        //    Element elementForCopying;
        //    int highestIntensityIndex = 0;
        //    int absoluteHighestIntenistyIndex = 0;

        //    while (currentTheoryIndex < this.m_theoryElementList.Count)
        //    {
        //        currentActualIndex = 0;
        //        highestIntensityIndex = -1;

        //        while (currentActualIndex < this.m_actualElementList.Count)
        //        {

        //            if ((((m_theoryElementList[currentTheoryIndex].mzValue) <= (m_actualElementList[currentActualIndex].mzValue * (1.00 + (possibleError / 100.0)))) &&
        //                ((m_theoryElementList[currentTheoryIndex].mzValue) >= (m_actualElementList[currentActualIndex].mzValue * (1.00 - (lowerBoundPossibleError / 100.0))))))
        //            {
        //                if (highestIntensityIndex == -1)
        //                {
        //                    highestIntensityIndex = currentActualIndex;

        //                }
        //                else if (Math.Abs(m_actualElementList[highestIntensityIndex].mzValue - m_theoryElementList[currentTheoryIndex].mzValue) > Math.Abs(m_actualElementList[currentActualIndex].mzValue - m_theoryElementList[currentTheoryIndex].mzValue))
        //                {
        //                    highestIntensityIndex = currentActualIndex;
        //                }
        //                else if (Math.Abs(m_actualElementList[highestIntensityIndex].mzValue - m_theoryElementList[currentTheoryIndex].mzValue) == Math.Abs(m_actualElementList[currentActualIndex].mzValue - m_theoryElementList[currentTheoryIndex].mzValue))
        //                {
        //                    if (m_actualElementList[highestIntensityIndex].intensity < m_actualElementList[currentActualIndex].intensity)
        //                    {
        //                        highestIntensityIndex = currentActualIndex;
        //                    }
        //                }
        //                if (m_actualElementList[highestIntensityIndex].intensity > m_actualElementList[absoluteHighestIntenistyIndex].intensity)
        //                {
        //                    absoluteHighestIntenistyIndex = highestIntensityIndex;
        //                }
        //            }
        //            ++currentActualIndex;
        //        }
        //        if (highestIntensityIndex != -1)
        //        {
        //            m_actualElementList[highestIntensityIndex].matched = true;
        //            if (m_actualElementList[highestIntensityIndex].annotation != "")
        //            {
        //                m_actualElementList[highestIntensityIndex].annotation = m_actualElementList[highestIntensityIndex].annotation + "," + m_theoryElementList[currentTheoryIndex].annotation;
        //            }
        //            else
        //            {
        //                m_actualElementList[highestIntensityIndex].annotation = m_theoryElementList[currentTheoryIndex].annotation;
        //            }
        //            m_theoryElementList[currentTheoryIndex].matched = true;
        //        }
        //        ++currentTheoryIndex;
        //    }

        //    // Tag a precursor onto the hightes Intensity Index
        //    m_actualElementList[absoluteHighestIntenistyIndex].annotation = m_actualElementList[absoluteHighestIntenistyIndex].annotation + " - PRECURSOR";

        //    //When all is said and done the compared list will just be a copy of the actual list.
        //    for (int i = 0; i < m_actualElementList.Count; ++i )
        //    {
        //        elementForCopying               = new Element();
        //        Element currentElement          = m_actualElementList[i];
        //        elementForCopying.annotation    = currentElement.annotation;
        //        elementForCopying.matched       = currentElement.matched;
        //        elementForCopying.intensity     = currentElement.intensity;
        //        elementForCopying.mzValue       = currentElement.mzValue;
        //        ElementList.Add(elementForCopying);
        //    }
        //}

       // public override void BuildList()
        //{
        //    int currentActualIndex = 0;
        //    int currentTheoryIndex = 0;

        //    Element elementForCopying;
        //    int highestIntensityIndex           = 0;
        //    int absoluteHighestIntenistyIndex   = 0;

        //    while (currentTheoryIndex < this.m_theoryElementList.Count)
        //    {
        //        currentActualIndex      = 0;
        //        highestIntensityIndex   = -1;
        //        double theoryMz         = m_theoryElementList[currentTheoryIndex].mzValue;

        //        while (currentActualIndex < this.m_actualElementList.Count)
        //        {
        //            double actualMz         = m_actualElementList[currentActualIndex].mzValue;
        //            double actualIntensity  = m_actualElementList[currentActualIndex].intensity;
        //            double upperActualMz    = actualMz + possibleError;
        //            double lowerActualMz    = actualMz - possibleError;

        //            if ((theoryMz <= upperActualMz) && (theoryMz >= lowerActualMz))
        //            {
        //                double diffMz = Math.Abs(actualMz - theoryMz);

        //                // If highestIntensityIndex == -1, then this is the first peak we have seen, select it
        //                // else
        //                //     look at other things that are close...select the biggest peak
        //                //
        //                if (highestIntensityIndex == -1)
        //                {
        //                    highestIntensityIndex = currentActualIndex;
        //                }
        //                else if (Math.Abs(m_actualElementList[highestIntensityIndex].mzValue - theoryMz) > diffMz)
        //                {
        //                    highestIntensityIndex = currentActualIndex;
        //                }
        //                else if (Math.Abs(m_actualElementList[highestIntensityIndex].mzValue - theoryMz) == diffMz)
        //                {
        //                    if (m_actualElementList[highestIntensityIndex].intensity < actualIntensity)
        //                    {
        //                        highestIntensityIndex = currentActualIndex;
        //                    }
        //                }

        //                // Find the pre-cursor

        //                                if (m_actualElementList[highestIntensityIndex].intensity > m_actualElementList[absoluteHighestIntenistyIndex].intensity)
        //                                {
        //                                    absoluteHighestIntenistyIndex = highestIntensityIndex;
        //                                }
        //            }
        //            ++currentActualIndex;
        //        }
        //        if (highestIntensityIndex != -1)
        //        {
        //            m_actualElementList[highestIntensityIndex].matched = true;
        //            if (m_actualElementList[highestIntensityIndex].annotation != "")
        //            {
        //                m_actualElementList[highestIntensityIndex].annotation = m_actualElementList[highestIntensityIndex].annotation + "," + m_theoryElementList[currentTheoryIndex].annotation;
        //            }
        //            else
        //            {
        //                m_actualElementList[highestIntensityIndex].annotation = m_theoryElementList[currentTheoryIndex].annotation;
        //            }
        //            m_theoryElementList[currentTheoryIndex].matched = true;
        //        }
        //        ++currentTheoryIndex;
        //    }

        //    // Tag a precursor onto the hightes Intensity Index
        //    m_actualElementList[absoluteHighestIntenistyIndex].annotation = m_actualElementList[absoluteHighestIntenistyIndex].annotation + " - PRECURSOR";

        //    //When all is said and done the compared list will just be a copy of the actual list.
        //    for (int i = 0; i < m_actualElementList.Count; ++i)
        //    {
        //        elementForCopying = new Element();
        //        Element currentElement = m_actualElementList[i];
        //        elementForCopying.annotation = currentElement.annotation;
        //        elementForCopying.matched = currentElement.matched;
        //        elementForCopying.intensity = currentElement.intensity;
        //        elementForCopying.mzValue = currentElement.mzValue;
        //        ElementList.Add(elementForCopying);
        //    }
        //}

        public override void BuildList()
        {
            foreach(var theoryElement in m_theoryElementList)
            {
                var pairs = from actualElement in m_actualElementList
                            where
                                (Math.Abs(theoryElement.Mz - actualElement.Mz) <= m_upperBoundTolerance) && (Math.Abs(theoryElement.Mz  - actualElement.Mz) >= m_lowerBoundTolerance)

                            select new { actualElement };

                // Should make this a LINQ query...
                Element maxElement = null;
                foreach (var item in pairs)
                {
                    if (maxElement == null)
                    {
                        maxElement = item.actualElement;
                    }
                    else
                    {
                        var theoryDiff = Math.Abs(item.actualElement.Mz - theoryElement.Mz);
                        var maxDiff    = Math.Abs(item.actualElement.Mz - maxElement.Mz);
                        if (theoryDiff < maxDiff)
                        {
                            maxElement = item.actualElement;
                        }
                    }
                }

                if (maxElement != null)
                {
                    theoryElement.Matched   = true;
                    maxElement.Matched      = true;

                    if (maxElement.Annotation != "")
                    {
                        maxElement.Annotation = string.Format("{0},{1}", maxElement.Annotation, theoryElement.Annotation);
                    }
                    else
                    {
                        maxElement.Annotation = theoryElement.Annotation;
                    }
                }
            }

            Element maxIntensity = null;
            Element precursor = null;
            foreach (var item in m_actualElementList)
            {
                if (Math.Abs(item.Mz - m_precursor) <= m_upperBoundTolerance &&
                    Math.Abs(item.Mz - m_precursor) >= m_lowerBoundTolerance)
                {
                    if (precursor == null || item.Intensity > precursor.Intensity)
                    {
                        precursor = item;
                    }
                }
                if (maxIntensity == null)
                {
                    maxIntensity = item;
                }
                else if (maxIntensity.Intensity < item.Intensity)
                {
                    maxIntensity = item;
                }
            }

            if (precursor != null)
            {
                if (maxIntensity == null || precursor.Intensity >= (maxIntensity.Intensity/10.0))
                {
                    precursor.Annotation += " - PRECURSOR";
                }
            }
            //// Tag a precursor onto the highest Intensity Index
            //if (maxIntensity != null)
            //{
            //    maxIntensity.Annotation += " - PRECURSOR";
            //}

            //When all is said and done the compared list will just be a copy of the actual list.
            foreach (var currentElement in m_actualElementList)
            {
                var elementForCopying       = new Element();
                elementForCopying.Annotation    = currentElement.Annotation;
                elementForCopying.Matched       = currentElement.Matched;
                elementForCopying.Intensity     = currentElement.Intensity;
                elementForCopying.Mz       = currentElement.Mz;

                ElementList.Add(elementForCopying);
            }
        }

        #endregion

        #region PRIVATE
        #endregion

        #endregion
    }
}
