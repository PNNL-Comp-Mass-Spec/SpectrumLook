using System.Collections.Generic;

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
        /// <summary>
        /// This is the List that each derived class will generate.
        /// </summary>
        public List<Element> ElementList
        {
            get; internal set;
        }

        protected ElementListBuilder()
        {
            this.ElementList = new List<Element>();
        }

        /// <summary>
        /// This function must be overridden to define how the list must be built.
        /// </summary>
        public abstract void BuildList();
    }
}
