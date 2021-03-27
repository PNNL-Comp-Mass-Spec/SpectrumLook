using SpectrumLook.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SpectrumLookTests
{
    /// <summary>
    ///This is a test class for MzParserTest and is intended
    ///to contain all MzParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class MzParserTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        ///A test for MzParser Constructor
        ///</summary>
        [TestMethod()]
        public void MzParserConstructorTest()
        {
            string fileLocation = "..\\..\\..\\TestData\\QC_Standards_Excerpt.mzXML";
            MzParser target = new MzParser(fileLocation);
            Assert.IsNotNull(target,"Target is null on an existing file.  Did that data move?");
            try
            {
                target = new MzParser("..\\..\\..\\TestData\\QC_Standards_Excerpt_syn");
            }
            catch (Exception)
            {
                target = null;
            }
            Assert.IsNull(target, "Exception not thrown.  File should not open if there is not a matching file extention of \".mzXML\" or \".mzData\"");
            try
            {
                target = new MzParser("");
            }
            catch (Exception)
            {
                target = null;
            }
            Assert.IsNull(target, "Exception not thrown.  MzParser does should not accept an empty string value for the file location.");
            // TODO: NEED TO RUN A .mzData
        }

        /// <summary>
        ///A test for SpectrumLook.Builders.IExperimentParser.GetExperimentDataByScanNumber
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SpectrumLook.exe")]
        public void GetExperimentDataByScanNumberTest()
        {
            string fileLocation = "..\\..\\..\\TestData\\QC_Standards_Excerpt.mzXML";
            IExperimentParser target = new MzParser(fileLocation);
            int scanNum = 7382;
            List<Element> actual = target.GetExperimentDataByScanNumber(scanNum);
            Assert.AreEqual<int>(528, actual.Count ,"Length of output is unexpected, is there new test data?");
        }
    }
}
