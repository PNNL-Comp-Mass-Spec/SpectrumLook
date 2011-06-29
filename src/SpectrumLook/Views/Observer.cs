using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpectrumLook.Views
{
    //TODO : I'm starting to wonder if I should make this an interface. Since this class only has one function
    //          and that function should not be the same for all classes.
    /// <summary>
    /// This is the class that all the different views will derive from.
    /// Each view must define what there UpdateObserver function will do.
    /// Patrick Tobin
    /// </summary>
    //TODO : update model as such.
    //TODO : has to be an interface because multiple inheritance only works using interfaces and since all the view inherit
        //      from forms as well, we need to make this an interface.
    public interface IObserver
    {
        /// <summary>
        /// This is the function that all the view must override and define themselves.
        /// </summary>
        void UpdateObserver();
    }
}
