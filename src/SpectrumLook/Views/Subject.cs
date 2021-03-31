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
        private List<IObserver> mObserverList = null;

        /// <summary>
        /// This function Adds an observer to the list of of observers
        /// that will be updated when the Invoke function is called.
        /// </summary>
        /// <param name="observerToAdd">The observer you wish to add.</param>
        public void Attach(ref IObserver observerToAdd)
        {
            if (mObserverList == null)
            {
                mObserverList = new List<IObserver>();
            }
            mObserverList.Add(observerToAdd);
        }

        /// <summary>
        /// This function removes the provided observer from the list of of observers
        /// that will be updated when the Invoke function is called.
        /// </summary>
        /// <param name="observerToRemove">The observer you wish to remove from the list.</param>
        public void Detach(IObserver observerToRemove)
        {
            mObserverList.Remove(observerToRemove);
        }

        /// <summary>
        /// This function goes through the list of observers and calls the update function on each
        /// </summary>
        public void Invoke()
        {
            if (mObserverList != null)
            {
                foreach (var observer in mObserverList)
                {
                    observer.UpdateObserver();
                }
            }
        }
    }
}
