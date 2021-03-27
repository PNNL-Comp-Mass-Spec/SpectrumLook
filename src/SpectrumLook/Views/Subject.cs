using System;
using System.Collections.Generic;

namespace SpectrumLook.Views
{
    /// <summary>
    /// This is the class that all the different options will derive from.
    /// Patrick Tobin
    /// </summary>
    [Serializable]
    public abstract class Subject
    {
        private List<IObserver> m_observerList = null;

        /// <summary>
        /// This function Adds an observer to the list of of observers
        /// that will be updated when the Invoke function is called.
        /// </summary>
        /// <param name="observerToAdd">The observer you wish to add.</param>
        public void Attach(ref IObserver observerToAdd)
        {
            if (m_observerList == null)
            {
                m_observerList = new List<IObserver>();
            }
            m_observerList.Add(observerToAdd);
        }

        /// <summary>
        /// This function removes the provided observer from the list of of observers
        /// that will be updated when the Invoke function is called.
        /// </summary>
        /// <param name="observerToRemove">The observer you wish to remove from the list.</param>
        public void Detach(IObserver observerToRemove)
        {
            m_observerList.Remove(observerToRemove);
        }

        /// <summary>
        /// This function goes through the list of observers and calls the update
        /// function of thoughs observers.
        /// </summary>
        public void Invoke()
        {
            if (m_observerList != null)
            {
                foreach (var observer in m_observerList)
                {
                    observer.UpdateObserver();
                }
            }
        }
    }
}
