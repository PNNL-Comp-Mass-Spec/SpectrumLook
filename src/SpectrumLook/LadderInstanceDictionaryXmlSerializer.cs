using System.Collections.Generic;
using System.Collections;
using System.Xml.Serialization;
using System.IO;

namespace SpectrumLook
{
    /// <summary>
    /// This object is used to Serialize the Hashtable that stores the LadderInstances to a file.
    /// By Patrick Tobin
    /// </summary>
    public class LadderInstanceDictionaryXmlSerializer
    {
        public string fileSaved { get; private set; }

        /// <summary>
        /// This object just set the m_fileSaved path to null.
        /// WriteHashTable(string location, Hashtable tableToWrite) must first be called.
        /// </summary>
        public LadderInstanceDictionaryXmlSerializer()
        {
            fileSaved = null;
        }

        /// <summary>
        /// This a function that just rewrites the file, if a file location has already been given to the HashtableXmlSerializer.
        /// </summary>
        /// <param name="tableToWrite">The hashtable that contains a List of LadderInstances.</param>
        public void WriteLadderInstanceDictionary(Dictionary<string, List<LadderInstance>> tableToWrite)
        {
            if (fileSaved != null)
                WriteLadderInstanceDictionary(fileSaved, tableToWrite);
        }

        /// <summary>
        /// This function is used to write a Xml file of the Hashtable that stores List of LadderInstances.
        /// </summary>
        /// <param name="location">The location in which the XML file will be written to.</param>
        /// <param name="tableToWrite">The hashtable that contains a List of LadderInstances.</param>
        public void WriteLadderInstanceDictionary(string location, Dictionary<string, List<LadderInstance>> tableToWrite)
        {
            var serializer = new XmlSerializer(typeof(List<List<LadderInstance>>));
            TextWriter textWriter = new StreamWriter(location);

            fileSaved = location;

            ICollection keys = tableToWrite.Keys;
            var savingSchema = new List<List<LadderInstance>>();

            foreach (string currentKey in keys)
            {
                savingSchema.Add(tableToWrite[currentKey]);
            }

            serializer.Serialize(textWriter, savingSchema);

            textWriter.Close();
        }

        /// <summary>
        /// This function is used to read a XML file and output a Dictionary filled with a List of LadderInstances
        /// </summary>
        /// <param name="location">The stored location of the XML file.</param>
        /// <returns>Dictionary </returns>
        public Dictionary<string, List<LadderInstance>> ReadXmlWorkFile(string location)
        {
            var serializer = new XmlSerializer(typeof(List<List<LadderInstance>>));
            TextReader textReader = new StreamReader(location);
            var ladderInstances = new Dictionary<string, List<LadderInstance>>();

            fileSaved = location;

            var savedSchema = (List<List<LadderInstance>>)serializer.Deserialize(textReader);
            textReader.Close();

            foreach (var currentLadderList in savedSchema)
            {
                ladderInstances.Add(currentLadderList[0].ScanNumberString + currentLadderList[0].PeptideString, currentLadderList);
            }

            return ladderInstances;
        }
    }
}
