using SpectrumLook.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SpectrumLookTests
{
    /// <summary>
    ///This is a test class for ElementTest and is intended
    ///to contain all ElementTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ElementTest
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

        /// <summary>
        ///A test for Element Constructor
        ///</summary>
        [TestMethod()]
        public void ElementConstructorTest()
        {
            var target = new Element();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for annotation
        ///</summary>
        [TestMethod()]
        public void annotationTest()
        {
            var target = new Element(); // TODO: Initialize to an appropriate value
            var expected = string.Empty; // TODO: Initialize to an appropriate value
            target.Annotation = expected;
            var actual = target.Annotation;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for intensity
        ///</summary>
        [TestMethod()]
        public void intensityTest()
        {
            var target = new Element(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            target.Intensity = expected;
            var actual = target.Intensity;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for matched
        ///</summary>
        [TestMethod()]
        public void matchedTest()
        {
            var target = new Element(); // TODO: Initialize to an appropriate value
            var expected = false; // TODO: Initialize to an appropriate value
            target.Matched = expected;
            var actual = target.Matched;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for mzValue
        ///</summary>
        [TestMethod()]
        public void mzValueTest()
        {
            var target = new Element(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            target.Mz = expected;
            var actual = target.Mz;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
