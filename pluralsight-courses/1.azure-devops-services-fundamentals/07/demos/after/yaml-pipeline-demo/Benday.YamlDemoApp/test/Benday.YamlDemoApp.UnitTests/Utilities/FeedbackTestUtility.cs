using System;
using System.Collections.Generic;
using Benday.YamlDemoApp.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Utilities
{
    public static class FeedbackTestUtility
    {
        public static List<Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity> CreateEntities(
            bool createAsUnsaved = true)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity>();

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

        public static Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity CreateEntity()
        {
            var fromValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity
            {
                Id = UnitTestUtility.GetFakeValueForInt("Id"),
                FeedbackType = UnitTestUtility.GetFakeValueForString("FeedbackType"),
                Sentiment = UnitTestUtility.GetFakeValueForString("Sentiment"),
                Subject = UnitTestUtility.GetFakeValueForString("Subject"),
                FeedbackText = UnitTestUtility.GetFakeValueForString("FeedbackText"),
                Username = UnitTestUtility.GetFakeValueForString("Username"),
                FirstName = UnitTestUtility.GetFakeValueForString("FirstName"),
                LastName = UnitTestUtility.GetFakeValueForString("LastName"),
                Referer = UnitTestUtility.GetFakeValueForString("Referer"),
                UserAgent = UnitTestUtility.GetFakeValueForString("UserAgent"),
                IpAddress = UnitTestUtility.GetFakeValueForString("IpAddress"),
                IsContactRequest = true,
                Status = UnitTestUtility.GetFakeValueForString("Status"),
                CreatedBy = UnitTestUtility.GetFakeValueForString("CreatedBy"),
                CreatedDate = UnitTestUtility.GetFakeValueForDateTime("CreatedDate"),
                LastModifiedBy = UnitTestUtility.GetFakeValueForString("LastModifiedBy"),
                LastModifiedDate = UnitTestUtility.GetFakeValueForDateTime("LastModifiedDate"),
                Timestamp = UnitTestUtility.GetFakeValueForByteArray("Timestamp")
            };
            return fromValue;
        }

        public static Benday.YamlDemoApp.Api.DomainModels.Feedback CreateModel(
            bool createAsUnsaved = true)
        {
            var fromValue = new Benday.YamlDemoApp.Api.DomainModels.Feedback
            {
                Id = UnitTestUtility.GetFakeValueForInt("Id"),
                FeedbackType = UnitTestUtility.GetFakeValueForString("FeedbackType"),
                Sentiment = UnitTestUtility.GetFakeValueForString("Sentiment"),
                Subject = UnitTestUtility.GetFakeValueForString("Subject"),
                FeedbackText = UnitTestUtility.GetFakeValueForString("FeedbackText"),
                Username = UnitTestUtility.GetFakeValueForString("Username"),
                FirstName = UnitTestUtility.GetFakeValueForString("FirstName"),
                LastName = UnitTestUtility.GetFakeValueForString("LastName"),
                Referer = UnitTestUtility.GetFakeValueForString("Referer"),
                UserAgent = UnitTestUtility.GetFakeValueForString("UserAgent"),
                IpAddress = UnitTestUtility.GetFakeValueForString("IpAddress"),
                IsContactRequest = true,
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

        public static List<Benday.YamlDemoApp.Api.DomainModels.Feedback> CreateModels(
            bool createAsUnsaved = true, int numberOfRecords = 10)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.Feedback>();

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
            Benday.YamlDemoApp.Api.DomainModels.Feedback fromValue)
        {
            if (fromValue == null)
            {
                throw new ArgumentNullException(nameof(fromValue), $"{nameof(fromValue)} is null.");
            }

            fromValue.FeedbackType = UnitTestUtility.GetFakeValueForString("Modified FeedbackType");
            fromValue.Sentiment = UnitTestUtility.GetFakeValueForString("Modified Sentiment");
            fromValue.Subject = UnitTestUtility.GetFakeValueForString("Modified Subject");
            fromValue.FeedbackText = UnitTestUtility.GetFakeValueForString("Modified FeedbackText");
            fromValue.Username = UnitTestUtility.GetFakeValueForString("Modified Username");
            fromValue.FirstName = UnitTestUtility.GetFakeValueForString("Modified FirstName");
            fromValue.LastName = UnitTestUtility.GetFakeValueForString("Modified LastName");
            fromValue.Referer = UnitTestUtility.GetFakeValueForString("Modified Referer");
            fromValue.UserAgent = UnitTestUtility.GetFakeValueForString("Modified UserAgent");
            fromValue.IpAddress = UnitTestUtility.GetFakeValueForString("Modified IpAddress");
            fromValue.IsContactRequest = !fromValue.IsContactRequest;
            fromValue.Status = UnitTestUtility.GetFakeValueForString("Modified Status");
            fromValue.CreatedBy = UnitTestUtility.GetFakeValueForString("Modified CreatedBy");
            fromValue.CreatedDate = UnitTestUtility.GetFakeValueForDateTime("Modified CreatedDate");
            fromValue.LastModifiedBy = UnitTestUtility.GetFakeValueForString("Modified LastModifiedBy");
            fromValue.LastModifiedDate = UnitTestUtility.GetFakeValueForDateTime("Modified LastModifiedDate");
            fromValue.Timestamp = UnitTestUtility.GetFakeValueForByteArray("Modified Timestamp");

        }

        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.Api.DomainModels.Feedback> expected,
            IList<Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity> actual)
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
            Benday.YamlDemoApp.Api.DomainModels.Feedback expected,
            Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<string>(expected.FeedbackType, actual.FeedbackType, "FeedbackType");
            Assert.AreEqual<string>(expected.Sentiment, actual.Sentiment, "Sentiment");
            Assert.AreEqual<string>(expected.Subject, actual.Subject, "Subject");
            Assert.AreEqual<string>(expected.FeedbackText, actual.FeedbackText, "FeedbackText");
            Assert.AreEqual<string>(expected.Username, actual.Username, "Username");
            Assert.AreEqual<string>(expected.FirstName, actual.FirstName, "FirstName");
            Assert.AreEqual<string>(expected.LastName, actual.LastName, "LastName");
            Assert.AreEqual<string>(expected.Referer, actual.Referer, "Referer");
            Assert.AreEqual<string>(expected.UserAgent, actual.UserAgent, "UserAgent");
            Assert.AreEqual<string>(expected.IpAddress, actual.IpAddress, "IpAddress");
            Assert.AreEqual<bool>(expected.IsContactRequest, actual.IsContactRequest, "IsContactRequest");
            Assert.AreEqual<string>(expected.Status, actual.Status, "Status");
            Assert.AreEqual<string>(expected.CreatedBy, actual.CreatedBy, "CreatedBy");
            Assert.AreEqual<DateTime>(expected.CreatedDate, actual.CreatedDate, "CreatedDate");
            Assert.AreEqual<string>(expected.LastModifiedBy, actual.LastModifiedBy, "LastModifiedBy");
            Assert.AreEqual<DateTime>(expected.LastModifiedDate, actual.LastModifiedDate, "LastModifiedDate");
            Assert.AreEqual<byte[]>(expected.Timestamp, actual.Timestamp, "Timestamp");


        }

        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity> expected,
            IList<Benday.YamlDemoApp.Api.DomainModels.Feedback> actual)
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
            Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity expected,
            Benday.YamlDemoApp.Api.DomainModels.Feedback actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<string>(expected.FeedbackType, actual.FeedbackType, "FeedbackType");
            Assert.AreEqual<string>(expected.Sentiment, actual.Sentiment, "Sentiment");
            Assert.AreEqual<string>(expected.Subject, actual.Subject, "Subject");
            Assert.AreEqual<string>(expected.FeedbackText, actual.FeedbackText, "FeedbackText");
            Assert.AreEqual<string>(expected.Username, actual.Username, "Username");
            Assert.AreEqual<string>(expected.FirstName, actual.FirstName, "FirstName");
            Assert.AreEqual<string>(expected.LastName, actual.LastName, "LastName");
            Assert.AreEqual<string>(expected.Referer, actual.Referer, "Referer");
            Assert.AreEqual<string>(expected.UserAgent, actual.UserAgent, "UserAgent");
            Assert.AreEqual<string>(expected.IpAddress, actual.IpAddress, "IpAddress");
            Assert.AreEqual<bool>(expected.IsContactRequest, actual.IsContactRequest, "IsContactRequest");
            Assert.AreEqual<string>(expected.Status, actual.Status, "Status");
            Assert.AreEqual<string>(expected.CreatedBy, actual.CreatedBy, "CreatedBy");
            Assert.AreEqual<DateTime>(expected.CreatedDate, actual.CreatedDate, "CreatedDate");
            Assert.AreEqual<string>(expected.LastModifiedBy, actual.LastModifiedBy, "LastModifiedBy");
            Assert.AreEqual<DateTime>(expected.LastModifiedDate, actual.LastModifiedDate, "LastModifiedDate");
            Assert.AreEqual<byte[]>(expected.Timestamp, actual.Timestamp, "Timestamp");


        }
    }
}
