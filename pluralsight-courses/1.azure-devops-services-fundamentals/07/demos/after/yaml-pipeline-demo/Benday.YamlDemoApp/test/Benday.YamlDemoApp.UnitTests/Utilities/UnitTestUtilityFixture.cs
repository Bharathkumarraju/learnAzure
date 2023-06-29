using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Utilities
{
    [TestClass]
    public class UnitTestUtilityFixture
    {
        [TestMethod]
        public void IsDateTimeRecent_LessThan1SecondBefore_ReturnsTrue()
        {
            var value1 = new DateTime(1900, 1, 1);
            var value2 = value1.AddMilliseconds(-999);

            Assert.IsTrue(
            UnitTestUtility.IsDateTimeRecent(value1, value2),
            "Expected value to be recent.");
        }

        [TestMethod]
        public void IsDateTimeRecent_LessThan1SecondAfter_ReturnsTrue()
        {

            var value1 = new DateTime(1900, 1, 1);
            var value2 = value1.AddMilliseconds(999);

            Assert.IsTrue(
            UnitTestUtility.IsDateTimeRecent(value1, value2),
            "Expected value to be recent.");
        }

        [TestMethod]
        public void IsDateTimeRecent_Equals_ReturnsTrue()
        {
            var value1 = new DateTime(1900, 1, 1);
            var value2 = value1;

            Assert.IsTrue(
            UnitTestUtility.IsDateTimeRecent(value1, value2),
            "Expected value to be recent.");
        }

        [TestMethod]
        public void IsDateTimeRecent_GreaterThan1SecondBefore_ReturnsFalse()
        {
            var value1 = new DateTime(1900, 1, 1);
            var value2 = value1.AddMilliseconds(-1001);

            Assert.IsFalse(
            UnitTestUtility.IsDateTimeRecent(value1, value2),
            "Expected value to not be recent.");
        }

        [TestMethod]
        public void IsDateTimeRecent_GreaterThan1SecondAfter_ReturnsFalse()
        {
            var value1 = new DateTime(1900, 1, 1);
            var value2 = value1.AddMilliseconds(1001);

            Assert.IsFalse(
            UnitTestUtility.IsDateTimeRecent(value1, value2),
            "Expected value to not be recent.");
        }
    }
}
