using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace SpectrumLook
{
    /// <summary>
    /// This object is used to Serialize the Hashtable that stores the LadderInstances to a file.
    /// By Patrick Tobin
    /// </summary>
    public class HashtableXmlSerializer
    {
        #region MEMEBERS

        #region PRIVATE
        private string m_fileSaved;
        #endregion

        #endregion

        #region PROPERTIES
        #region PUBLIC
        public string fileSaved
        {
            get
            {
                return m_fileSaved;
            }
        }
        #endregion
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// This object just set the m_fileSaved path to null.
        /// WriteHashTable(string location, Hashtable tableToWrite) must first be called.
        /// </summary>
        public HashtableXmlSerializer()
        {
            this.m_fileSaved = null;
        }
        #endregion

        #region FUNCTIONS

        #region PUBLIC

        /// <summary>
        /// This a function that just rewrites the file, if a file location has already been given to the HashtableXmlSerializer.
        /// </summary>
        /// <param name="tableToWrite">The hashtable that contains a List of LadderInstances.</param>
        public void WriteHashTable(Hashtable tableToWrite)
        {
            if(m_fileSaved != null)
                this.WriteHashTable(m_fileSaved, tableToWrite);
        }

        /// <summary>
        /// This function is used to write a Xml file of the Hashtable that stores List of LadderInstances.
        /// </summary>
        /// <param name="location">The location in which the XML file will be written to.</param>
        /// <param name="tableToWrite">The hashtable that contains a List of LadderInstances.</param>
        public void WriteHashTable(string location, Hashtable tableToWrite)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<List<LadderInstance>>));
            TextWriter textWriter = new StreamWriter(location);

            m_fileSaved = location;

            ICollection keys = tableToWrite.Keys;
            List<List<LadderInstance>> savingSchema = new List<List<LadderInstance>>();

            foreach (string currentKey in keys)
            {
                savingSchema.Add((List<LadderInstance>)tableToWrite[currentKey]);
            }

            serializer.Serialize(textWriter, savingSchema);

            textWriter.Close();
        }

        /// <summary>
        /// This function is used to read a XML file and output a Hashtable filled with a List of LadderInstances
        /// </summary>
        /// <param name="location">The stored location of the XML file.</param>
        /// <returns></returns>
        public Hashtable ReadXmlWorkFile(string location)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<List<LadderInstance>>));
            TextReader textReader = new StreamReader(location);
            Hashtable outputedHashTable = new Hashtable();
            List<List<LadderInstance>> savedSchema;

            m_fileSaved = location;

            savedSchema = (List<List<LadderInstance>>)serializer.Deserialize(textReader);
            textReader.Close();

            foreach (List<LadderInstance> currentLadderList in savedSchema)
            {
                outputedHashTable.Add((currentLadderList[0].scanNumberString + currentLadderList[0].PeptideString), currentLadderList);
            }

            return outputedHashTable;
        }
        #endregion

        #endregion
    }
}
