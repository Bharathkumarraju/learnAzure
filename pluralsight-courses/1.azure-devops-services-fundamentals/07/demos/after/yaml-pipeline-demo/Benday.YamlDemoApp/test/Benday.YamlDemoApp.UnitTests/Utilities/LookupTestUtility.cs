using System;
using System.Collections.Generic;
using Benday.YamlDemoApp.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Utilities
{
    public static class LookupTestUtility
    {
        public static List<Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity> CreateEntities(
            bool createAsUnsaved = true)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity>();

            for (var i = 0; i < 10; i++)
            {
                var temp = CreateEntity();

                returnValues.Add(temp);

                if (createAsUnsaved == false)
                {
                    temp.Id = i + 1;
                }
            }

            return returnValues;
        }

        public static Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity CreateEntity()
        {
            var fromValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity
            {
                Id = UnitTestUtility.GetFakeValueForInt("Id"),
                DisplayOrder = UnitTestUtility.GetFakeValueForInt("DisplayOrder"),
                LookupType = UnitTestUtility.GetFakeValueForString("LookupType"),
                LookupKey = UnitTestUtility.GetFakeValueForString("LookupKey"),
                LookupValue = UnitTestUtility.GetFakeValueForString("LookupValue"),
                Status = UnitTestUtility.GetFakeValueForString("Status"),
                CreatedBy = UnitTestUtility.GetFakeValueForString("CreatedBy"),
                CreatedDate = UnitTestUtility.GetFakeValueForDateTime("CreatedDate"),
                LastModifiedBy = UnitTestUtility.GetFakeValueForString("LastModifiedBy"),
                LastModifiedDate = UnitTestUtility.GetFakeValueForDateTime("LastModifiedDate"),
                Timestamp = UnitTestUtility.GetFakeValueForByteArray("Timestamp")
            };
            return fromValue;
        }

        public static Benday.YamlDemoApp.Api.DomainModels.Lookup CreateModel(
            bool createAsUnsaved = true)
        {
            var fromValue = new Benday.YamlDemoApp.Api.DomainModels.Lookup
            {
                Id = UnitTestUtility.GetFakeValueForInt("Id"),
                DisplayOrder = UnitTestUtility.GetFakeValueForInt("DisplayOrder"),
                LookupType = UnitTestUtility.GetFakeValueForString("LookupType"),
                LookupKey = UnitTestUtility.GetFakeValueForString("LookupKey"),
                LookupValue = UnitTestUtility.GetFakeValueForString("LookupValue"),
                Status = UnitTestUtility.GetFakeValueForString("Status"),
                CreatedBy = UnitTestUtility.GetFakeValueForString("CreatedBy"),
                CreatedDate = UnitTestUtility.GetFakeValueForDateTime("CreatedDate"),
                LastModifiedBy = UnitTestUtility.GetFakeValueForString("LastModifiedBy"),
                LastModifiedDate = UnitTestUtility.GetFakeValueForDateTime("LastModifiedDate"),
                Timestamp = UnitTestUtility.GetFakeValueForByteArray("Timestamp")
            };
            if (createAsUnsaved == true)
            {
                fromValue.Id = 0;
                fromValue.CreatedDate = default;
                fromValue.LastModifiedDate = default;
                fromValue.CreatedBy = null;
                fromValue.LastModifiedBy = null;
            }

            return fromValue;
        }

        public static List<Benday.YamlDemoApp.Api.DomainModels.Lookup> CreateModels(
            bool createAsUnsaved = true, int numberOfRecords = 10)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.Lookup>();

            for (var i = 0; i < numberOfRecords; i++)
            {
                var temp = CreateModel(createAsUnsaved);

                returnValues.Add(temp);

                if (createAsUnsaved == false)
                {
                    temp.Id = i + 1;
                }
                else
                {
                    temp.Id = ApiConstants.UnsavedId;
                    temp.CreatedDate = default;
                    temp.LastModifiedDate = default;
                    temp.CreatedBy = null;
                    temp.LastModifiedBy = null;
                }
            }

            return returnValues;
        }

        public static void ModifyModel(
            Benday.YamlDemoApp.Api.DomainModels.Lookup fromValue)
        {
            if (fromValue == null)
            {
                throw new ArgumentNullException(nameof(fromValue), $"{nameof(fromValue)} is null.");
            }

            fromValue.DisplayOrder = UnitTestUtility.GetFakeValueForInt("Modified DisplayOrder");
            fromValue.LookupType = UnitTestUtility.GetFakeValueForString("Modified LookupType");
            fromValue.LookupKey = UnitTestUtility.GetFakeValueForString("Modified LookupKey");
            fromValue.LookupValue = UnitTestUtility.GetFakeValueForString("Modified LookupValue");
            fromValue.Status = UnitTestUtility.GetFakeValueForString("Modified Status");
            fromValue.CreatedBy = UnitTestUtility.GetFakeValueForString("Modified CreatedBy");
            fromValue.CreatedDate = UnitTestUtility.GetFakeValueForDateTime("Modified CreatedDate");
            fromValue.LastModifiedBy = UnitTestUtility.GetFakeValueForString("Modified LastModifiedBy");
            fromValue.LastModifiedDate = UnitTestUtility.GetFakeValueForDateTime("Modified LastModifiedDate");
            fromValue.Timestamp = UnitTestUtility.GetFakeValueForByteArray("Modified Timestamp");

        }

        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.Api.DomainModels.Lookup> expected,
            IList<Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity> actual)
        {
            Assert.IsNotNull(expected, "Expected was null.");
            Assert.IsNotNull(actual, "Actual was null.");
            Assert.AreEqual<int>(expected.Count, actual.Count, "Item count should match.");

            for (var i = 0; i < expected.Count; i++)
            {
                AssertAreEqual(expected[i], actual[i]);
            }
        }

        public static void AssertAreEqual(
            Benday.YamlDemoApp.Api.DomainModels.Lookup expected,
            Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<int>(expected.DisplayOrder, actual.DisplayOrder, "DisplayOrder");
            Assert.AreEqual<string>(expected.LookupType, actual.LookupType, "LookupType");
            Assert.AreEqual<string>(expected.LookupKey, actual.LookupKey, "LookupKey");
            Assert.AreEqual<string>(expected.LookupValue, actual.LookupValue, "LookupValue");
            Assert.AreEqual<string>(expected.Status, actual.Status, "Status");
            Assert.AreEqual<string>(expected.CreatedBy, actual.CreatedBy, "CreatedBy");
            Assert.AreEqual<DateTime>(expected.CreatedDate, actual.CreatedDate, "CreatedDate");
            Assert.AreEqual<string>(expected.LastModifiedBy, actual.LastModifiedBy, "LastModifiedBy");
            Assert.AreEqual<DateTime>(expected.LastModifiedDate, actual.LastModifiedDate, "LastModifiedDate");
            Assert.AreEqual<byte[]>(expected.Timestamp, actual.Timestamp, "Timestamp");


        }

        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity> expected,
            IList<Benday.YamlDemoApp.Api.DomainModels.Lookup> actual)
        {
            Assert.IsNotNull(expected, "Expected was null.");
            Assert.IsNotNull(actual, "Actual was null.");
            Assert.AreEqual<int>(expected.Count, actual.Count, "Item count should match.");

            for (var i = 0; i < expected.Count; i++)
            {
                AssertAreEqual(expected[i], actual[i]);
            }
        }

        public static void AssertAreEqual(
            Benday.YamlDemoApp.Api.DataAccess.Entities.LookupEntity expected,
            Benday.YamlDemoApp.Api.DomainModels.Lookup actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<int>(expected.DisplayOrder, actual.DisplayOrder, "DisplayOrder");
            Assert.AreEqual<string>(expected.LookupType, actual.LookupType, "LookupType");
            Assert.AreEqual<string>(expected.LookupKey, actual.LookupKey, "LookupKey");
            Assert.AreEqual<string>(expected.LookupValue, actual.LookupValue, "LookupValue");
            Assert.AreEqual<string>(expected.Status, actual.Status, "Status");
            Assert.AreEqual<string>(expected.CreatedBy, actual.CreatedBy, "CreatedBy");
            Assert.AreEqual<DateTime>(expected.CreatedDate, actual.CreatedDate, "CreatedDate");
            Assert.AreEqual<string>(expected.LastModifiedBy, actual.LastModifiedBy, "LastModifiedBy");
            Assert.AreEqual<DateTime>(expected.LastModifiedDate, actual.LastModifiedDate, "LastModifiedDate");
            Assert.AreEqual<byte[]>(expected.Timestamp, actual.Timestamp, "Timestamp");


        }
    }
}
