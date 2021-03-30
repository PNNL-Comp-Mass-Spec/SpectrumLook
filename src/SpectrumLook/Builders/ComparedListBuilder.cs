using System;
using System.Collections.Generic;
using System.Linq;

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
        private readonly List<Element> mActualElementList;

        /// <summary>
        /// This is a reference to the theoryElementList that was passed into the constructor.
        /// </summary>
        private readonly List<Element> mTheoryElementList;

        /// <summary>
        /// This is a value that stores the possible error between the theoretical m/z values and the actual m/z values
        /// </summary>
        private double mUpperBoundTolerance;

        /// <summary>
        /// This is a value that stores the lower possible error between the theoretical m/z values and the actual m/z values.
        /// </summary>
        private double mLowerBoundTolerance;

        private readonly double mPrecursor;

        public double PossibleError
        {
            get => mUpperBoundTolerance;
            set
            {
                if (value >= 0.0)
                {
                    mUpperBoundTolerance = value;
                }
            }
        }

        public double LowerBoundPossibleError
        {
            get => mLowerBoundTolerance;
            set
            {
                if (value >= 0.0)
                {
                    mLowerBoundTolerance = value;
                }
            }
        }

        /// <summary>
        /// This constructor sets the initial value for the BuildList Function.
        /// </summary>
        /// <param name="possibleError">This is a number greater than zero where it is the possible error between m/z value in the theory List and the Actual List</param>
        /// <param name="actualElementList">This should be a reference to a list generated from the ActualListBuilder</param>
        /// <param name="precursor">The precursor, to label the precursor if present </param>
        /// <param name="theoryElementList">This should be a reference to a list generated from the TheoryListBuilder </param>
        public ComparedListBuilder(double possibleError, double lowerBoundPossibleError, List<Element> actualElementList, double precursor, ref List<Element> theoryElementList)
        {
            mActualElementList = actualElementList;
            mTheoryElementList = theoryElementList;
            ElementList = new List<Element>();
            mPrecursor = precursor;
            mUpperBoundTolerance = possibleError;
            mLowerBoundTolerance = lowerBoundPossibleError;
        }

        /// <summary>
        /// BuildElement builds a List of elements based on the actualElementList, theoryElementList
        /// and the possibleError passed into the constructor.
        /// </summary>
        // TODO : Make an option to do a method to do highest Intensity or closest value in that "range" of errored values.
        // public override void BuildList()
        //{
        //    int currentActualIndex = 0;
        //    int currentTheoryIndex = 0;
        //    Element elementForCopying;
        //    int highestIntensityIndex = 0;
        //    int absoluteHighestIntenistyIndex = 0;

        //    while (currentTheoryIndex < this.mTheoryElementList.Count)
        //    {
        //        currentActualIndex = 0;
        //        highestIntensityIndex = -1;

        //        while (currentActualIndex < this.mActualElementList.Count)
        //        {

        //            if ((((mTheoryElementList[currentTheoryIndex].MzValue) <= (mActualElementList[currentActualIndex].MzValue * (1.00 + (possibleError / 100.0)))) &&
        //                ((mTheoryElementList[currentTheoryIndex].MzValue) >= (mActualElementList[currentActualIndex].MzValue * (1.00 - (lowerBoundPossibleError / 100.0))))))
        //            {
        //                if (highestIntensityIndex == -1)
        //                {
        //                    highestIntensityIndex = currentActualIndex;

        //                }
        //                else if (Math.Abs(mActualElementList[highestIntensityIndex].MzValue - mTheoryElementList[currentTheoryIndex].MzValue) > Math.Abs(mActualElementList[currentActualIndex].MzValue - mTheoryElementList[currentTheoryIndex].MzValue))
        //                {
        //                    highestIntensityIndex = currentActualIndex;
        //                }
        //                else if (Math.Abs(mActualElementList[highestIntensityIndex].MzValue - mTheoryElementList[currentTheoryIndex].MzValue) == Math.Abs(mActualElementList[currentActualIndex].MzValue - mTheoryElementList[currentTheoryIndex].MzValue))
        //                {
        //                    if (mActualElementList[highestIntensityIndex].intensity < mActualElementList[currentActualIndex].intensity)
        //                    {
        //                        highestIntensityIndex = currentActualIndex;
        //                    }
        //                }
        //                if (mActualElementList[highestIntensityIndex].intensity > mActualElementList[absoluteHighestIntenistyIndex].intensity)
        //                {
        //                    absoluteHighestIntenistyIndex = highestIntensityIndex;
        //                }
        //            }
        //            ++currentActualIndex;
        //        }
        //        if (highestIntensityIndex != -1)
        //        {
        //            mActualElementList[highestIntensityIndex].matched = true;
        //            if (mActualElementList[highestIntensityIndex].annotation != "")
        //            {
        //                mActualElementList[highestIntensityIndex].annotation = mActualElementList[highestIntensityIndex].annotation + "," + mTheoryElementList[currentTheoryIndex].annotation;
        //            }
        //            else
        //            {
        //                mActualElementList[highestIntensityIndex].annotation = mTheoryElementList[currentTheoryIndex].annotation;
        //            }
        //            mTheoryElementList[currentTheoryIndex].matched = true;
        //        }
        //        ++currentTheoryIndex;
        //    }

        //    // Tag a precursor onto the hightes Intensity Index
        //    mActualElementList[absoluteHighestIntenistyIndex].annotation = mActualElementList[absoluteHighestIntenistyIndex].annotation + " - PRECURSOR";

        //    // When all is said and done the compared list will just be a copy of the actual list.
        //    for (int i = 0; i < mActualElementList.Count; ++i )
        //    {
        //        elementForCopying               = new Element();
        //        Element currentElement          = mActualElementList[i];
        //        elementForCopying.annotation    = currentElement.annotation;
        //        elementForCopying.matched       = currentElement.matched;
        //        elementForCopying.intensity     = currentElement.intensity;
        //        elementForCopying.MzValue       = currentElement.MzValue;
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

        //    while (currentTheoryIndex < this.mTheoryElementList.Count)
        //    {
        //        currentActualIndex      = 0;
        //        highestIntensityIndex   = -1;
        //        double theoryMz         = mTheoryElementList[currentTheoryIndex].MzValue;

        //        while (currentActualIndex < this.mActualElementList.Count)
        //        {
        //            double actualMz         = mActualElementList[currentActualIndex].MzValue;
        //            double actualIntensity  = mActualElementList[currentActualIndex].intensity;
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
        //                else if (Math.Abs(mActualElementList[highestIntensityIndex].MzValue - theoryMz) > diffMz)
        //                {
        //                    highestIntensityIndex = currentActualIndex;
        //                }
        //                else if (Math.Abs(mActualElementList[highestIntensityIndex].MzValue - theoryMz) == diffMz)
        //                {
        //                    if (mActualElementList[highestIntensityIndex].intensity < actualIntensity)
        //                    {
        //                        highestIntensityIndex = currentActualIndex;
        //                    }
        //                }

        //                // Find the pre-cursor

        //                                if (mActualElementList[highestIntensityIndex].intensity > mActualElementList[absoluteHighestIntenistyIndex].intensity)
        //                                {
        //                                    absoluteHighestIntenistyIndex = highestIntensityIndex;
        //                                }
        //            }
        //            ++currentActualIndex;
        //        }
        //        if (highestIntensityIndex != -1)
        //        {
        //            mActualElementList[highestIntensityIndex].matched = true;
        //            if (mActualElementList[highestIntensityIndex].annotation != "")
        //            {
        //                mActualElementList[highestIntensityIndex].annotation = mActualElementList[highestIntensityIndex].annotation + "," + mTheoryElementList[currentTheoryIndex].annotation;
        //            }
        //            else
        //            {
        //                mActualElementList[highestIntensityIndex].annotation = mTheoryElementList[currentTheoryIndex].annotation;
        //            }
        //            mTheoryElementList[currentTheoryIndex].matched = true;
        //        }
        //        ++currentTheoryIndex;
        //    }

        //    // Tag a precursor onto the hightes Intensity Index
        //    mActualElementList[absoluteHighestIntenistyIndex].annotation = mActualElementList[absoluteHighestIntenistyIndex].annotation + " - PRECURSOR";

        //    // When all is said and done the compared list will just be a copy of the actual list.
        //    for (int i = 0; i < mActualElementList.Count; ++i)
        //    {
        //        elementForCopying = new Element();
        //        Element currentElement = mActualElementList[i];
        //        elementForCopying.annotation = currentElement.annotation;
        //        elementForCopying.matched = currentElement.matched;
        //        elementForCopying.intensity = currentElement.intensity;
        //        elementForCopying.MzValue = currentElement.MzValue;
        //        ElementList.Add(elementForCopying);
        //    }
        //}

        public override void BuildList()
        {
            foreach (var theoryElement in mTheoryElementList)
            {
                var pairs = from actualElement in mActualElementList
                            where
                                Math.Abs(theoryElement.Mz - actualElement.Mz) <= mUpperBoundTolerance && Math.Abs(theoryElement.Mz - actualElement.Mz) >= mLowerBoundTolerance

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
                        var maxDiff = Math.Abs(item.actualElement.Mz - maxElement.Mz);
                        if (theoryDiff < maxDiff)
                        {
                            maxElement = item.actualElement;
                        }
                    }
                }

                if (maxElement != null)
                {
                    theoryElement.Matched = true;
                    maxElement.Matched = true;

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
            foreach (var item in mActualElementList)
            {
                if (Math.Abs(item.Mz - mPrecursor) <= mUpperBoundTolerance &&
                    Math.Abs(item.Mz - mPrecursor) >= mLowerBoundTolerance)
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
                if (maxIntensity == null || precursor.Intensity >= maxIntensity.Intensity / 10.0)
                {
                    precursor.Annotation += " - PRECURSOR";
                }
            }
            // Tag a precursor onto the highest Intensity Index
            // if (maxIntensity != null)
            //{
            //    maxIntensity.Annotation += " - PRECURSOR";
            //}

            // When all is said and done the compared list will just be a copy of the actual list.
            foreach (var currentElement in mActualElementList)
            {
                var elementForCopying = new Element
                {
                    Annotation = currentElement.Annotation,
                    Matched = currentElement.Matched,
                    Intensity = currentElement.Intensity,
                    Mz = currentElement.Mz
                };

                ElementList.Add(elementForCopying);
            }
        }
    }
}
