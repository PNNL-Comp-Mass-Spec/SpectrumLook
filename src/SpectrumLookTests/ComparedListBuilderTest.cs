﻿using SpectrumLook.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace SpectrumLookTests
{
    /// <summary>
    ///This is a test class for ComparedListBuilderTest and is intended
    ///to contain all ComparedListBuilderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ComparedListBuilderTest
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
        ///A test for BuildList
        ///</summary>
        [TestMethod()]
        public void BuildListTest()
        {
            double possibleError = 0F; // TODO: Initialize to an appropriate value
            double lowerPossibleError = 0F; // TODO: Initialize to an appropriate value
            List<Element> actualElementList = null; // TODO: Initialize to an appropriate value
            List<Element> actualElementListExpected = null; // TODO: Initialize to an appropriate value
            List<Element> theoryElementList = null; // TODO: Initialize to an appropriate value
            List<Element> theoryElementListExpected = null; // TODO: Initialize to an appropriate value
            ComparedListBuilder target = new ComparedListBuilder(possibleError, lowerPossibleError, actualElementList, 0, ref theoryElementList); // TODO: Initialize to an appropriate value
            target.BuildList();
            Assert.AreEqual(actualElementListExpected, actualElementList);
            Assert.AreEqual(theoryElementListExpected, theoryElementList);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ComparedListBuilder Constructor
        ///</summary>
        [TestMethod()]
        public void ComparedListBuilderConstructorTest()
        {
            double possibleError = 0F; // TODO: Initialize to an appropriate value
            double lowerBoundPossibleError = 0F; // TODO: Initialize to an appropriate value
            List<Element> actualElementList = null; // TODO: Initialize to an appropriate value
            List<Element> actualElementListExpected = null; // TODO: Initialize to an appropriate value
            List<Element> theoryElementList = null; // TODO: Initialize to an appropriate value
            List<Element> theoryElementListExpected = null; // TODO: Initialize to an appropriate value
            ComparedListBuilder target = new ComparedListBuilder(possibleError, lowerBoundPossibleError, actualElementList, 0, ref theoryElementList);
            Assert.AreEqual(actualElementListExpected, actualElementList);
            Assert.AreEqual(theoryElementListExpected, theoryElementList);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
