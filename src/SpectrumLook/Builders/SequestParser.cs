﻿using System;
using System.IO;

namespace SpectrumLook.Builders
{
    /// <summary>
    /// The class derives from the ISynopsisParser interface class.
    /// This class defines how to read from a SEQUEST file.
    /// By Patrick Tobin
    /// </summary>
    public class SequestParser : ISynopsisParser
    {
        /// <summary>
        /// This is used to read from the input file.
        /// </summary>
        private readonly TextReader mFileReader;

        /// <summary>
        /// This is used to store the current row count when reading from the file.
        /// This starts are zero and goes to N-1. Where N is the number of rows in
        /// the current sequest file.
        /// </summary>
        public int CurrentRowCount { get; internal set; }

        /// <summary>
        /// The constructor will attempt to open the given file location
        /// that was passed into the constructor.  If the string passed in
        /// is null or is an unreadable file location then an error will
        /// be thrown.
        /// </summary>
        /// <param name="filePath">The location of the sequest file. (.txt format)</param>
        public SequestParser(string filePath)
        {
            CurrentRowCount = 0;

            if (filePath != null)
            {
                try
                {
                    mFileReader = File.OpenText(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception opening the synopsis file: " + ex.Message);
                    throw; // If the File can not be opened for some reason.
                }
            }
            else
            {
                throw new InvalidProgramException("Null filePath Value.");
                // Throw Exception.
            }
        }

        /// <summary>
        /// This function will needed to be called multiple times in order to read
        /// all data from the file.  If this is the first time this function is called
        /// then the headers of the columns that are in the opened SEQUEST file are returned
        /// in the form of a string array.  The Peptide column has a concatenated "_p" at the end
        /// of the header and a "_s" is concatenated to the end of the scan number to represent the
        /// connection between the synopsis file and the experiment file.
        /// </summary>
        /// <returns></returns>
        string[] ISynopsisParser.GetNextRow()
        {
            var currentLine = mFileReader.ReadLine();
            if (!string.IsNullOrEmpty(currentLine))
            {
                var splicedRow = currentLine.Split('\t');
                // This is where add "_p" and "_s" to the row that signifies the peptide sequence and connection to experiment data.
                /*if (mCurrentRowCount == 0) // if zero then we are currently looking at the headers of the columns
                {
                    splicedRow[1] = splicedRow[1] + "_s";
                    splicedRow[10] = splicedRow[10] + "_p";
                }*/
                ++CurrentRowCount;
                return splicedRow;
            }
            return null;
        }
    }
}
