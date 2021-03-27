using SpectrumLook.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SpectrumLookTests
{
    /// <summary>
    ///This is a test class for ActualListBuilderTest and is intended
    ///to contain all ActualListBuilderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ActualListBuilderTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get => testContextInstance;
            set => testContextInstance = value;
        }

        /// <summary>
        ///A test for ActualListBuilder Constructor
        ///</summary>
        [TestMethod()]
        public void ActualListBuilderConstructorTest()
        {
            var scanNumber = 0; // TODO: Initialize to an appropriate value
            IExperimentParser experimentParser = new MzParser("..\\..\\..\\TestData\\QC_Standards_Excerpt.mzXML"); // TODO: Initialize to an appropriate value
            var target = new ActualListBuilder(scanNumber, experimentParser);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for BuildList
        ///</summary>
        [TestMethod()]
        public void BuildListTest()
        {
            var scanNumber = 0; // TODO: Initialize to an appropriate value
            IExperimentParser experimentParser = null; // TODO: Initialize to an appropriate value
            var target = new ActualListBuilder(scanNumber, experimentParser); // TODO: Initialize to an appropriate value
            target.BuildList();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
