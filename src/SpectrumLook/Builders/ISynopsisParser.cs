namespace SpectrumLook.Builders
{
    /// <summary>
    /// This defines the functions of what the parser should return and in what format they should be returned to.
    /// By Patrick Tobin
    /// </summary>
    public interface ISynopsisParser
    {
        /// <summary>
        /// Each time this function is called is should return a string array corresponding to the current row of data.
        /// The first time this function is called it should return the headers for each column.
        /// The headers if concatenated with "_s" means that this is the connection between the synopsis file and the
        /// experiment file if concatenated with "_p"  this mean that this is the Peptide column.
        /// If there are no more rows to read the parser should return an empty string array.
        /// </summary>
        /// <returns></returns>
        string[] GetNextRow();
    }
}
