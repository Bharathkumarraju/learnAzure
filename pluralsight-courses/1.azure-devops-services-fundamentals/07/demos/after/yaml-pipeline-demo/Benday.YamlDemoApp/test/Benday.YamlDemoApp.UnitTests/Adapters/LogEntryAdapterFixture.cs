using System.Collections.Generic;
using Benday.YamlDemoApp.Api.Adapters;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Adapters
{
    [TestClass]
    public class LogEntryAdapterFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private LogEntryAdapter _systemUnderTest;
        public LogEntryAdapter SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new LogEntryAdapter();
                }

                return _systemUnderTest;
            }
        }

        [TestMethod]
        public void AdaptLogEntryFromEntityToModel()
        {
            // arrange
            var fromValue = LogEntryTestUtility.CreateEntity();
            var toValue = new Benday.YamlDemoApp.Api.DomainModels.LogEntry();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            LogEntryTestUtility.AssertAreEqual(fromValue, toValue);
            Assert.IsFalse(toValue.HasChanges(), "Should not have changes after adapt.");
        }

        [TestMethod]
        public void AdaptLogEntryFromEntitiesToModels_ToEmpty()
        {
            // arrange
            var fromValues = LogEntryTestUtility.CreateEntities();
            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.LogEntry>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            LogEntryTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptLogEntryFromEntitiesToModels_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = LogEntryTestUtility.CreateEntities(false);

            foreach (var fromValue in fromValues)
            {
                Assert.AreNotEqual<int>(0, fromValue.Id, "Value wasn't 'saved' before start of test.");
            }

            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.LogEntry>();

            // adapt first time
            SystemUnderTest.Adapt(fromValues, toValues);

            var originalValuesById = GetOriginalValuesById(toValues);

            // act
            // call adapt again
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            AssertValuesStillExistAndIdDidNotChange(toValues, originalValuesById);
            LogEntryTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DomainModels.LogEntry> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.LogEntry> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DomainModels.LogEntry expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.LogEntry> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DomainModels.LogEntry> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.LogEntry>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }

        [TestMethod]
        public void AdaptLogEntryFromModelToEntity()
        {
            // arrange
            var fromValue = LogEntryTestUtility.CreateModel();
            var toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            LogEntryTestUtility.AssertAreEqual(fromValue, toValue);
        }

        [TestMethod]
        public void AdaptLogEntryFromModelsToEntities_ToEmpty()
        {
            // arrange
            var fromValues = LogEntryTestUtility.CreateModels();
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            LogEntryTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptLogEntryFromModelsToEntities_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = LogEntryTestUtility.CreateModels(false);
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity>();

            foreach (var fromValue in fromValues)
            {
                Assert.AreNotEqual<int>(0, fromValue.Id, "Value wasn't 'saved' before start of test.");
            }

            // adapt first time
            SystemUnderTest.Adapt(fromValues, toValues);

            var originalValuesById = GetOriginalValuesById(toValues);

            // act
            // call adapt again
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            AssertValuesStillExistAndIdDidNotChange(toValues, originalValuesById);
            LogEntryTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }
    }
}
