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
            get => testContextInstance;
            set => testContextInstance = value;
        }

        /// <summary>
        ///A test for SequestParser Constructor
        ///</summary>
        [TestMethod()]
        public void SequestParserConstructorTest()
        {
            var fileLocation = "..\\..\\..\\TestData\\QC_Standards_Excerpt_syn.txt";
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
        ///A test for SpectrumLook.Builders.ISynopsisParser.GetNextColumn
        ///</summary>
        [TestMethod()]
        [DeploymentItem("SpectrumLook.exe")]
        public void GetNextColumnTest()
        {
            var indexOfExpect = 0;

            var fileLocation = "..\\..\\..\\TestData\\QC_Standards_Excerpt_syn.txt";

            ISynopsisParser target = new SequestParser(fileLocation);

            var expected = "HitNum	ScanNum_s	ScanCount	ChargeState	MH	XCorr	DelCn	Sp	Reference	MultiProtein	Peptide_p	DelCn2	RankSp	RankXc	DelM	XcRatio	PassFilt	MScore	NumTrypticEnds".Split('\t');
                                //Copied straight from QC file.
            string[] actual;

            actual = target.GetNextRow();

            foreach (var actualTmp in actual)
            {
                Assert.AreNotSame(expected[indexOfExpect], actualTmp);
                ++indexOfExpect;
            }
        }
    }
}
