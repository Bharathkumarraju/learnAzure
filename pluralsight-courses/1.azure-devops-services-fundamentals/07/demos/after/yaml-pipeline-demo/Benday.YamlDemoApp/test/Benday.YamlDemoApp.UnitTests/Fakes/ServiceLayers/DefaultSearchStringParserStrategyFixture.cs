using Benday.YamlDemoApp.Api.ServiceLayers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers
{
    [TestClass]
    public class DefaultSearchStringParserStrategyFixture
    {
        private DefaultSearchStringParserStrategy _systemUnderTest;
        public DefaultSearchStringParserStrategy SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new DefaultSearchStringParserStrategy();
                }

                return _systemUnderTest;
            }
        }

        private static void AssertItem(string[] actualValues, int index, string expected)
        {
            Assert.IsNotNull(actualValues);
            Assert.AreNotEqual<int>(0, actualValues.Length, "Should not be zero length.");
            Assert.IsTrue(index < actualValues.Length, "No item at index '{0}'.  Index will be out of bounds.", index);
            Assert.AreEqual<string>(expected, actualValues[index], "Value at index '{0}' is wrong.", index);
        }

        private const string SemiColonDelimiter = ";";
        private const string SemiColonDelimiterPlusSpace = "; ";

        private const string CommaDelimiter = ",";
        private const string CommaDelimiterPlusSpace = ", ";

        [TestMethod]
        public void ParseNoValueSearchStringWithoutDelimiter()
        {
            var result = SystemUnderTest.Parse(string.Empty);

            Assert.AreEqual<int>(0, result.Length, "Result count was wrong.");
        }

        [TestMethod]
        public void ParseNoValueSearchStringWithSemiColonDelimiter()
        {
            var result = SystemUnderTest.Parse(SemiColonDelimiter);

            Assert.AreEqual<int>(0, result.Length, "Result count was wrong.");
        }

        [TestMethod]
        public void ParseNoValueSearchStringWithSemiColonDelimiterPlusSpace()
        {
            var result = SystemUnderTest.Parse(SemiColonDelimiterPlusSpace);

            Assert.AreEqual<int>(0, result.Length, "Result count was wrong.");
        }

        [TestMethod]
        public void ParseSingleValueSearchStringWithoutDelimiter()
        {
            var result = SystemUnderTest.Parse("mezcal");

            Assert.AreEqual<int>(1, result.Length, "Result count was wrong.");
            AssertItem(result, 0, "mezcal");
        }

        [TestMethod]
        public void ParseSingleValueSearchStringWithSemiColonDelimiter()
        {
            var result = SystemUnderTest.Parse("mezcal" + SemiColonDelimiter);

            Assert.AreEqual<int>(1, result.Length, "Result count was wrong.");
            AssertItem(result, 0, "mezcal");
        }

        [TestMethod]
        public void ParseSingleValueSearchStringWithSemiColonDelimiterPlusSpace()
        {
            var result = SystemUnderTest.Parse("mezcal" + SemiColonDelimiterPlusSpace);

            Assert.AreEqual<int>(1, result.Length, "Result count was wrong.");
            AssertItem(result, 0, "mezcal");
        }

        [TestMethod]
        public void ParseTwoValueSearchStringWithSemiColonDelimiterAndNoTrailingDelimiter()
        {
            var result = SystemUnderTest.Parse("mezcal" + SemiColonDelimiter + "stuff");

            Assert.AreEqual<int>(2, result.Length, "Result count was wrong.");
            AssertItem(result, 0, "mezcal");
            AssertItem(result, 1, "stuff");
        }

        [TestMethod]
        public void ParseTwoValueSearchStringWithSemiColonDelimiterPlusSpaceAndNoTrailingDelimiter()
        {
            var result = SystemUnderTest.Parse("mezcal" + SemiColonDelimiterPlusSpace + "stuff");

            Assert.AreEqual<int>(2, result.Length, "Result count was wrong.");
            AssertItem(result, 0, "mezcal");
            AssertItem(result, 1, "stuff");
        }

        [TestMethod]
        public void ParseTwoValueSearchStringWithSemiColonDelimiterPlusSpaceTrailingDelimiter()
        {
            var result = SystemUnderTest.Parse(
            "mezcal" + SemiColonDelimiterPlusSpace + "stuff" + SemiColonDelimiter);

            Assert.AreEqual<int>(2, result.Length, "Result count was wrong.");
            AssertItem(result, 0, "mezcal");
            AssertItem(result, 1, "stuff");
        }

        [TestMethod]
        public void ParseTwoValueSearchStringWithCommaDelimiterPlusSpaceTrailingDelimiter()
        {
            var result = SystemUnderTest.Parse(
            "mezcal" + CommaDelimiterPlusSpace + "stuff" + CommaDelimiter);

            Assert.AreEqual<int>(2, result.Length, "Result count was wrong.");
            AssertItem(result, 0, "mezcal");
            AssertItem(result, 1, "stuff");
        }
    }
}
