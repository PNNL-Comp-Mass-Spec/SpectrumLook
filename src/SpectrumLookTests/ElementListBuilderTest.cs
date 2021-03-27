using SpectrumLook.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SpectrumLookTests
{
    /// <summary>
    ///This is a test class for ElementListBuilderTest and is intended
    ///to contain all ElementListBuilderTest Unit Tests
    ///</summary>
    [TestClass]
    public class ElementListBuilderTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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

        internal virtual ElementListBuilder CreateElementListBuilder()
        {
            // TODO: Instantiate an appropriate concrete class.
            ElementListBuilder target = null;
            return target;
        }

        /// <summary>
        ///A test for BuildList
        ///</summary>
        [TestMethod]
        public void BuildListTest()
        {
            var target = CreateElementListBuilder(); // TODO: Initialize to an appropriate value
            target.BuildList();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        internal virtual ElementListBuilder CreateElementListBuilder_Accessor()
        {
            // TODO: Instantiate an appropriate concrete class.
            ElementListBuilder target = null;
            return target;
        }

        /// <summary>
        ///A test for ElementList
        ///</summary>
        [TestMethod]
        public void ElementListTest()
        {
            //PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            //ElementListBuilder target = new ElementListBuilder(param0); // TODO: Initialize to an appropriate value
            List<Element> expected = null; // TODO: Initialize to an appropriate value
            List<Element> actual = null;
            //target.ElementList = expected;
            //actual = target.ElementList;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
