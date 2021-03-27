using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.Builders
{
    /// <summary>
    /// This class will be used as the base class for the TheoryListBuilder, ActualListBuilder and the Compared ListBuilder.
    /// It is required that all derived classes override the BuildList method.  And the constructor be the input parameters for
    /// building there corresponding list.
    /// By Patrick Tobin
    /// </summary>
    public abstract class ElementListBuilder
    {
        #region MEMBERS

        #region PUBLIC
        #endregion

        #region PRIVATE
        /// <summary>
        /// This is the List that each derived class will generate.
        /// </summary>
        private List<Element> m_elementList;
        #endregion

        #endregion

        #region PROPERTIES
        public List<Element> ElementList
        {
            get
            {
                return m_elementList;
            }
            internal set//Should not be setable by outside objects except the derived class.
            {
                m_elementList = value;
            }
        }
        #endregion

        #region CONSTRUCTOR
        public ElementListBuilder()
        {
            this.m_elementList = new List<Element>();
        }
        #endregion

        #region FUNCTIONS

        #region PUBLIC
        /// <summary>
        /// This function must be overridden to define how the list must be built.
        /// </summary>
        public abstract void BuildList();

        #endregion

        #region PRIVATE
        #endregion

        #endregion
    }
}
