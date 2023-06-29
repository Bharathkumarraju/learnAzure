using System.Collections.Generic;
using Benday.YamlDemoApp.Api.Adapters;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Adapters
{
    [TestClass]
    public class LookupAdapterFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private LookupAdapter _systemUnderTest;
        public LookupAdapter SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new LookupAdapter();
                }

                return _systemUnderTest;
            }
        }

        [TestMethod]
        public void AdaptLookupFromEntityToModel()
        {
            // arrange
            var fromValue = LookupTestUtility.CreateEntity();
            var toValue = new Benday.YamlDemoApp.Api.DomainModels.Lookup();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            LookupTestUtility.AssertAreEqual(fromValue, toValue);
            Assert.IsFalse(toValue.HasChanges(), "Should not have changes after adapt.");
        }

        [TestMethod]
        public void AdaptLookupFromEntitiesToModels_ToEmpty()
        {
            // arrange
            var fromValues = LookupTestUtility.CreateEntities();
            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.Lookup>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            LookupTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptLookupFromEntitiesToModels_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = LookupTestUtility.CreateEntities(false);

            foreach (var fromValue in fromValues)
            {
                Assert.AreNotEqual<int>(0, fromValue.Id, "Value wasn't 'saved' before start of test.");
            }

            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.Lookup>();

            // adapt first time
            SystemUnderTest.Adapt(fromValues, toValues);

            var originalValuesById = GetOriginalValuesById(toValues);

            // act
            // call adapt again
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            AssertValuesStillExistAndIdDidNotChange(toValues, originalValuesById);
            LookupTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DomainModels.Lookup> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.Lookup> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DomainModels.Lookup expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.Lookup> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DomainModels.Lookup> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.Lookup>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }

        [TestMethod]
        public void AdaptLookupFromModelToEntity()
        {
            // arrange
            var fromValue = LookupTestUtility.CreateModel();
            var toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            LookupTestUtility.AssertAreEqual(fromValue, toValue);
        }

        [TestMethod]
        public void AdaptLookupFromModelsToEntities_ToEmpty()
        {
            // arrange
            var fromValues = LookupTestUtility.CreateModels();
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            LookupTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptLookupFromModelsToEntities_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = LookupTestUtility.CreateModels(false);
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity>();

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
            LookupTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }
    }
}
