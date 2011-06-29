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
        #region MEMBERS

        #region PRIVATE

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
        private double m_possibleError;

        #endregion

        #region PUBLIC
        public double possibleError
        {
            get
            {
                return m_possibleError;
            }
            set
            {
                if (value >= 0.0)
                {
                    m_possibleError = value;
                }
            }
        }
        #endregion

        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// This constructor sets the initial value for the BuildList Function.
        /// </summary>
        /// <param name="possibleError">This is a number greater than zero where it is the possible error between m/z value in the theory List and the Actual List</param>
        /// <param name="actualElementList">This should be a reference to a list generated from the ActualListBuilder</param>
        /// <param name="theoryElementList">This should be a reference to a list generated from the TheoryListBuilder </param>
        public ComparedListBuilder(double possibleError, List<Element> actualElementList, ref List<Element> theoryElementList)
        {
            this.m_actualElementList = actualElementList;
            this.m_theoryElementList = theoryElementList;
            this.ElementList = new List<Element>();
            m_possibleError = possibleError;
        }
        #endregion

        #region FUNCTIONS

        #region PUBLIC

        /// <summary>
        /// BuildElement builds a List of elements based on the actualElementList, theoryElementList
        /// and the possibleError passed into the constructor.
        /// </summary>
        //TODO : Make an option to do a method to do highest Intensity or closest value in that "range" of errored values.
        public override void BuildList()
        {
            int currentActualIndex = 0;
            int currentTheoryIndex = 0;
            Element elementForCopying;
            int highestIntensityIndex = 0;

            while (currentTheoryIndex < this.m_theoryElementList.Count)
            {
                currentActualIndex = 0;
                highestIntensityIndex = -1;

                while (currentActualIndex < this.m_actualElementList.Count)
                {
                    if (((m_theoryElementList[currentTheoryIndex].mzValue) <= (m_actualElementList[currentActualIndex].mzValue * (1.00 + (possibleError / 100.0)))) &&
                        ((m_theoryElementList[currentTheoryIndex].mzValue) >= (m_actualElementList[currentActualIndex].mzValue * (1.00 - (possibleError / 100.0)))))
                    {
                        if (highestIntensityIndex == -1)
                        {
                            highestIntensityIndex = currentActualIndex;
                        }
                        else if (Math.Abs(m_actualElementList[highestIntensityIndex].mzValue - m_theoryElementList[currentTheoryIndex].mzValue) > Math.Abs(m_actualElementList[currentActualIndex].mzValue - m_theoryElementList[currentTheoryIndex].mzValue))
                        {
                            highestIntensityIndex = currentActualIndex;
                        }
                            //REALLY RARE CASE!
                            //But will cover it anyways.
                        else if (Math.Abs(m_actualElementList[highestIntensityIndex].mzValue - m_theoryElementList[currentTheoryIndex].mzValue) == Math.Abs(m_actualElementList[currentActualIndex].mzValue - m_theoryElementList[currentTheoryIndex].mzValue))
                        {
                            if (m_actualElementList[highestIntensityIndex].intensity < m_actualElementList[currentActualIndex].intensity)
                            {
                                highestIntensityIndex = currentActualIndex;
                            }
                        }
                    }
                    ++currentActualIndex;
                }
                if (highestIntensityIndex != -1)
                {
                    m_actualElementList[highestIntensityIndex].matched = true;
                    if (m_actualElementList[highestIntensityIndex].annotation != "")
                    {
                        m_actualElementList[highestIntensityIndex].annotation = m_actualElementList[highestIntensityIndex].annotation + "," + m_theoryElementList[currentTheoryIndex].annotation;
                    }
                    else
                    {
                        m_actualElementList[highestIntensityIndex].annotation = m_theoryElementList[currentTheoryIndex].annotation;
                    }
                    m_theoryElementList[currentTheoryIndex].matched = true;
                }
                ++currentTheoryIndex;
            }
            //When all is said and done the compared list will just be a copy of the actual list.
            for (int i = 0; i < m_actualElementList.Count; ++i )
            {
                elementForCopying = new Element();
                Element currentElement = m_actualElementList[i];
                elementForCopying.annotation = currentElement.annotation;
                elementForCopying.matched = currentElement.matched;
                elementForCopying.intensity = currentElement.intensity;
                elementForCopying.mzValue = currentElement.mzValue;
                //Add the element to the list that will be outputed.
                ElementList.Add(elementForCopying);
            }
        }

        #endregion

        #region PRIVATE
        #endregion

        #endregion
    }
}
