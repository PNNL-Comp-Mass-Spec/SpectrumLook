using SpectrumLook.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SpectrumLookTests
{
    /// <summary>
    /// This is a test class for MzXMLParserTest and is intended
    /// to contain all MzXMLParserTest Unit Tests
    ///</summary>
    [TestClass]
    public class MzXMLParserTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test for MzXMLParser Constructor
        ///</summary>
        [TestMethod]
        public void MzXMLParserConstructorTest()
        {
            var fileLocation = "..\\..\\..\\TestData\\QC_Standards_Excerpt.mzXML";
            var target = new MzXMLParser(fileLocation);
            Assert.IsNotNull(target,"Target is null on an existing file.  Did that data move?");
            try
            {
                target = new MzXMLParser("..\\..\\..\\TestData\\QC_Standards_Excerpt_syn");
            }
            catch (Exception)
            {
                target = null;
            }
            Assert.IsNull(target, "Exception not thrown.  File should not open if there is not a matching file extention of \".mzXML\" or \".mzData\"");
            try
            {
                target = new MzXMLParser("");
            }
            catch (Exception)
            {
                target = null;
            }
            Assert.IsNull(target, "Exception not thrown.  MzXMLParser does should not accept an empty string value for the file location.");
            // TODO: NEED TO RUN A .mzData
        }

        /// <summary>
        /// A test for SpectrumLook.Builders.IExperimentParser.GetExperimentDataByScanNumber
        ///</summary>
        [TestMethod]
        [DeploymentItem("SpectrumLook.exe")]
        public void GetExperimentDataByScanNumberTest()
        {
            var fileLocation = "..\\..\\..\\TestData\\QC_Standards_Excerpt.mzXML";
            IExperimentParser target = new MzXMLParser(fileLocation);
            var scanNum = 7382;
            var actual = target.GetExperimentDataByScanNumber(scanNum);
            Assert.AreEqual<int>(528, actual.Count ,"Length of output is unexpected, is there new test data?");
        }
    }
}
