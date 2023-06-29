using System.Collections.Generic;
using Benday.YamlDemoApp.Api.Adapters;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Adapters
{
    [TestClass]
    public class FeedbackAdapterFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private FeedbackAdapter _systemUnderTest;
        public FeedbackAdapter SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new FeedbackAdapter();
                }

                return _systemUnderTest;
            }
        }

        [TestMethod]
        public void AdaptFeedbackFromEntityToModel()
        {
            // arrange
            var fromValue = FeedbackTestUtility.CreateEntity();
            var toValue = new Benday.YamlDemoApp.Api.DomainModels.Feedback();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            FeedbackTestUtility.AssertAreEqual(fromValue, toValue);
            Assert.IsFalse(toValue.HasChanges(), "Should not have changes after adapt.");
        }

        [TestMethod]
        public void AdaptFeedbackFromEntitiesToModels_ToEmpty()
        {
            // arrange
            var fromValues = FeedbackTestUtility.CreateEntities();
            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.Feedback>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            FeedbackTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptFeedbackFromEntitiesToModels_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = FeedbackTestUtility.CreateEntities(false);

            foreach (var fromValue in fromValues)
            {
                Assert.AreNotEqual<int>(0, fromValue.Id, "Value wasn't 'saved' before start of test.");
            }

            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.Feedback>();

            // adapt first time
            SystemUnderTest.Adapt(fromValues, toValues);

            var originalValuesById = GetOriginalValuesById(toValues);

            // act
            // call adapt again
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            AssertValuesStillExistAndIdDidNotChange(toValues, originalValuesById);
            FeedbackTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DomainModels.Feedback> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.Feedback> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DomainModels.Feedback expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.Feedback> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DomainModels.Feedback> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.Feedback>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }

        [TestMethod]
        public void AdaptFeedbackFromModelToEntity()
        {
            // arrange
            var fromValue = FeedbackTestUtility.CreateModel();
            var toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            FeedbackTestUtility.AssertAreEqual(fromValue, toValue);
        }

        [TestMethod]
        public void AdaptFeedbackFromModelsToEntities_ToEmpty()
        {
            // arrange
            var fromValues = FeedbackTestUtility.CreateModels();
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            FeedbackTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptFeedbackFromModelsToEntities_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = FeedbackTestUtility.CreateModels(false);
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity>();

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
            FeedbackTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }
    }
}
