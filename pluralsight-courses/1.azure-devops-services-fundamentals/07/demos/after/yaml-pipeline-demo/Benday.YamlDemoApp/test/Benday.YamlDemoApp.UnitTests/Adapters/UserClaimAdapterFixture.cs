using System.Collections.Generic;
using Benday.YamlDemoApp.Api.Adapters;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Adapters
{
    [TestClass]
    public class UserClaimAdapterFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }

        private UserClaimAdapter _systemUnderTest;
        public UserClaimAdapter SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new UserClaimAdapter();
                }

                return _systemUnderTest;
            }
        }

        [TestMethod]
        public void AdaptUserClaimFromEntityToModel()
        {
            // arrange
            var fromValue = UserClaimTestUtility.CreateEntity();
            var toValue = new Benday.YamlDemoApp.Api.DomainModels.UserClaim();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            UserClaimTestUtility.AssertAreEqual(fromValue, toValue);
            Assert.IsFalse(toValue.HasChanges(), "Should not have changes after adapt.");
        }

        [TestMethod]
        public void AdaptUserClaimFromEntitiesToModels_ToEmpty()
        {
            // arrange
            var fromValues = UserClaimTestUtility.CreateEntities();
            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.UserClaim>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            UserClaimTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptUserClaimFromEntitiesToModels_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = UserClaimTestUtility.CreateEntities(false);

            foreach (var fromValue in fromValues)
            {
                Assert.AreNotEqual<int>(0, fromValue.Id, "Value wasn't 'saved' before start of test.");
            }

            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.UserClaim>();

            // adapt first time
            SystemUnderTest.Adapt(fromValues, toValues);

            var originalValuesById = GetOriginalValuesById(toValues);

            // act
            // call adapt again
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            AssertValuesStillExistAndIdDidNotChange(toValues, originalValuesById);
            UserClaimTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DomainModels.UserClaim> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.UserClaim> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DomainModels.UserClaim expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.UserClaim> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DomainModels.UserClaim> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DomainModels.UserClaim>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }

        [TestMethod]
        public void AdaptUserClaimFromModelToEntity()
        {
            // arrange
            var fromValue = UserClaimTestUtility.CreateModel();
            var toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity();

            // act
            SystemUnderTest.Adapt(fromValue, toValue);

            // assert
            UserClaimTestUtility.AssertAreEqual(fromValue, toValue);
        }

        [TestMethod]
        public void AdaptUserClaimFromModelsToEntities_ToEmpty()
        {
            // arrange
            var fromValues = UserClaimTestUtility.CreateModels();
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity>();

            // act
            SystemUnderTest.Adapt(fromValues, toValues);

            // assert
            Assert.AreNotEqual<int>(0, toValues.Count, "There should be values.");
            UserClaimTestUtility.AssertAreEqual(fromValues, toValues);
        }

        [TestMethod]
        public void AdaptUserClaimFromModelsToEntities_MergesByIdForExistingValues()
        {
            // arrange
            var fromValues = UserClaimTestUtility.CreateModels(false);
            var toValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity>();

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
            UserClaimTestUtility.AssertAreEqual(fromValues, toValues);
        }

        private static void AssertValuesStillExistAndIdDidNotChange(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity> actualValues,
            Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity> expectedValuesById)
        {
            Assert.AreEqual<int>(expectedValuesById.Count, actualValues.Count, "Item count changed.");

            Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity expected;

            foreach (var expectedId in expectedValuesById.Keys)
            {
                expected = expectedValuesById.GetValueOrDefault(expectedId);

                Assert.IsNotNull(expected, "Expected value should not be null.");

                Assert.AreEqual<int>(expectedId, expected.Id, "Id value should not have changed.");

                Assert.IsTrue(actualValues.Contains(expected), "Value should exist in actual values.");
            }
        }

        private static Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity> GetOriginalValuesById(
            List<Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity> values)
        {
            var originalValuesById =
            new Dictionary<int, Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity>();

            foreach (var item in values)
            {
                originalValuesById.Add(item.Id, item);
            }

            return originalValuesById;
        }
    }
}
