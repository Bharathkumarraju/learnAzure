using System.Collections.Generic;
using Benday.YamlDemoApp.Api.Adapters;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Adapters
{
    [TestClass]
    public class ConfigurationItemAdapterFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private ConfigurationItemAdapter _systemUnderTest;
        public ConfigurationItemAdapter SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new ConfigurationItemAdapter();
                }

                return _systemUnderTest;
            }
        }

        [TestMethod]
        public void AdaptConfigurationItemFromEntityToModel()
        {
            // arrange
            var fromValue = ConfigurationItemTestUtility.CreateEntity();
            var toValue = new Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            ConfigurationItemTestUtility.AssertAreEqual(fromValue, toValue);
            Assert.IsFalse(toValue.HasChanges(), "Should not have changes after adapt.");
        }

        [TestMethod]
        public void AdaptConfigurationItemFromEntitiesToModels_ToEmpty()
        {
            // arrange
            var fromValues = ConfigurationItemTestUtility.CreateEntities();
            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            ConfigurationItemTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptConfigurationItemFromEntitiesToModels_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = ConfigurationItemTestUtility.CreateEntities(false);

            foreach (var fromValue in fromValues)
            {
                Assert.AreNotEqual<int>(0, fromValue.Id, "Value wasn't 'saved' before start of test.");
            }

            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>();

            // adapt first time
            SystemUnderTest.Adapt(fromValues, toValues);

            var originalValuesById = GetOriginalValuesById(toValues);

            // act
            // call adapt again
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            AssertValuesStillExistAndIdDidNotChange(toValues, originalValuesById);
            ConfigurationItemTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }

        [TestMethod]
        public void AdaptConfigurationItemFromModelToEntity()
        {
            // arrange
            var fromValue = ConfigurationItemTestUtility.CreateModel();
            var toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            ConfigurationItemTestUtility.AssertAreEqual(fromValue, toValue);
        }

        [TestMethod]
        public void AdaptConfigurationItemFromModelsToEntities_ToEmpty()
        {
            // arrange
            var fromValues = ConfigurationItemTestUtility.CreateModels();
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            ConfigurationItemTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptConfigurationItemFromModelsToEntities_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = ConfigurationItemTestUtility.CreateModels(false);
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity>();

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
            ConfigurationItemTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }
    }
}
