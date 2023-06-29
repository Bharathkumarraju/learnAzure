using System;
using System.Collections.Generic;
using Benday.YamlDemoApp.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Benday.YamlDemoApp.UnitTests.Utilities
{
    public static class LogEntryTestUtility
    {
        public static List<Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity> CreateEntities(
            bool createAsUnsaved = true)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity>();

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

        public static Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity CreateEntity()
        {
            var fromValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity
            {
                Id = UnitTestUtility.GetFakeValueForInt("Id"),
                Category = UnitTestUtility.GetFakeValueForString("Category"),
                LogLevel = UnitTestUtility.GetFakeValueForString("LogLevel"),
                LogText = UnitTestUtility.GetFakeValueForString("LogText"),
                ExceptionText = UnitTestUtility.GetFakeValueForString("ExceptionText"),
                EventId = UnitTestUtility.GetFakeValueForString("EventId"),
                State = UnitTestUtility.GetFakeValueForString("State"),
                LogDate = UnitTestUtility.GetFakeValueForDateTime("LogDate")
            };
            return fromValue;
        }

        public static Benday.YamlDemoApp.Api.DomainModels.LogEntry CreateModel(
            bool createAsUnsaved = true)
        {
            var fromValue = new Benday.YamlDemoApp.Api.DomainModels.LogEntry
            {
                Id = UnitTestUtility.GetFakeValueForInt("Id"),
                Category = UnitTestUtility.GetFakeValueForString("Category"),
                LogLevel = UnitTestUtility.GetFakeValueForString("LogLevel"),
                LogText = UnitTestUtility.GetFakeValueForString("LogText"),
                ExceptionText = UnitTestUtility.GetFakeValueForString("ExceptionText"),
                EventId = UnitTestUtility.GetFakeValueForString("EventId"),
                State = UnitTestUtility.GetFakeValueForString("State"),
                LogDate = UnitTestUtility.GetFakeValueForDateTime("LogDate")
            };
            if (createAsUnsaved == true)
            {
                fromValue.Id = 0;
            }

            return fromValue;
        }

        public static List<Benday.YamlDemoApp.Api.DomainModels.LogEntry> CreateModels(
            bool createAsUnsaved = true, int numberOfRecords = 10)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.LogEntry>();

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

                }
            }

            return returnValues;
        }

        public static void ModifyModel(
            Benday.YamlDemoApp.Api.DomainModels.LogEntry fromValue)
        {
            if (fromValue == null)
            {
                throw new ArgumentNullException(nameof(fromValue), $"{nameof(fromValue)} is null.");
            }

            fromValue.Category = UnitTestUtility.GetFakeValueForString("Modified Category");
            fromValue.LogLevel = UnitTestUtility.GetFakeValueForString("Modified LogLevel");
            fromValue.LogText = UnitTestUtility.GetFakeValueForString("Modified LogText");
            fromValue.ExceptionText = UnitTestUtility.GetFakeValueForString("Modified ExceptionText");
            fromValue.EventId = UnitTestUtility.GetFakeValueForString("Modified EventId");
            fromValue.State = UnitTestUtility.GetFakeValueForString("Modified State");
            fromValue.LogDate = UnitTestUtility.GetFakeValueForDateTime("Modified LogDate");

        }

        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.Api.DomainModels.LogEntry> expected,
            IList<Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity> actual)
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
            Benday.YamlDemoApp.Api.DomainModels.LogEntry expected,
            Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<string>(expected.Category, actual.Category, "Category");
            Assert.AreEqual<string>(expected.LogLevel, actual.LogLevel, "LogLevel");
            Assert.AreEqual<string>(expected.LogText, actual.LogText, "LogText");
            Assert.AreEqual<string>(expected.ExceptionText, actual.ExceptionText, "ExceptionText");
            Assert.AreEqual<string>(expected.EventId, actual.EventId, "EventId");
            Assert.AreEqual<string>(expected.State, actual.State, "State");
            Assert.AreEqual<DateTime>(expected.LogDate, actual.LogDate, "LogDate");


        }

        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity> expected,
            IList<Benday.YamlDemoApp.Api.DomainModels.LogEntry> actual)
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
            Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity expected,
            Benday.YamlDemoApp.Api.DomainModels.LogEntry actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<string>(expected.Category, actual.Category, "Category");
            Assert.AreEqual<string>(expected.LogLevel, actual.LogLevel, "LogLevel");
            Assert.AreEqual<string>(expected.LogText, actual.LogText, "LogText");
            Assert.AreEqual<string>(expected.ExceptionText, actual.ExceptionText, "ExceptionText");
            Assert.AreEqual<string>(expected.EventId, actual.EventId, "EventId");
            Assert.AreEqual<string>(expected.State, actual.State, "State");
            Assert.AreEqual<DateTime>(expected.LogDate, actual.LogDate, "LogDate");


        }
    }
}
