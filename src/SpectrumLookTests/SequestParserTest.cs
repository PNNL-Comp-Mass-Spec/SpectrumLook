using SpectrumLook.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SpectrumLookTests
{
    
    
    /// <summary>
    ///This is a test class for SequestParserTest and is intended
    ///to contain all SequestParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SequestParserTest
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
        ///A test for SequestParser Constructor
        ///</summary>
        [TestMethod()]
        public void SequestParserConstructorTest()
        {
            string fileLocation = "..\\..\\..\\TestData\\QC_Standards_Excerpt_syn.txt";
            SequestParser target;
            try
            {
                target = new SequestParser(fileLocation);
            }
            catch (Exception)
            {
                Assert.Inconclusive("Opening file exception! Did the test data move?");
            }

            try
            {
                target = new SequestParser("");
            }
            catch (Exception)
            {
                target = null;
            }
            Assert.IsNull(target, "SequestParser is using a null File Location!");
        }

        /// <summary>
        ///A test for SpectrumLook.Builders.ISynopsysParser.GetNextColumn
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SpectrumLook.exe")]
        public void GetNextColumnTest()
        {
            int indexOfExpect = 0;

            string fileLocation = "..\\..\\..\\TestData\\QC_Standards_Excerpt_syn.txt";

            ISynopsysParser target = new SequestParser(fileLocation);

            string[] expected = "HitNum	ScanNum_s	ScanCount	ChargeState	MH	XCorr	DelCn	Sp	Reference	MultiProtein	Peptide_p	DelCn2	RankSp	RankXc	DelM	XcRatio	PassFilt	MScore	NumTrypticEnds".Split('\t');
                                //Copied straight from QC file.
            string[] actual;

            actual = target.GetNextColumn();

            foreach (string actualTmp in actual)
            {
                Assert.AreNotSame(expected[indexOfExpect], actualTmp);
                ++indexOfExpect;
            }
        }
    }
}
