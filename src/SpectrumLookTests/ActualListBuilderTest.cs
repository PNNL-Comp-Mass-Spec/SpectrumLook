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
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ActualListBuilder Constructor
        ///</summary>
        [TestMethod()]
        public void ActualListBuilderConstructorTest()
        {
            int scanNumber = 0; // TODO: Initialize to an appropriate value
            IExperimentParser experimentParser = new MzParser("..\\..\\..\\TestData\\QC_Standards_Excerpt.mzXML"); // TODO: Initialize to an appropriate value
            ActualListBuilder target = new ActualListBuilder(scanNumber, experimentParser);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for BuildList
        ///</summary>
        [TestMethod()]
        public void BuildListTest()
        {
            int scanNumber = 0; // TODO: Initialize to an appropriate value
            IExperimentParser experimentParser = null; // TODO: Initialize to an appropriate value
            ActualListBuilder target = new ActualListBuilder(scanNumber, experimentParser); // TODO: Initialize to an appropriate value
            target.BuildList();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
