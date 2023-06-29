using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Benday.YamlDemoApp.Api;
using Benday.YamlDemoApp.UnitTests.Utilities;

namespace Benday.YamlDemoApp.UnitTests.ViewModels
{
    public static class LogEntryViewModelTestUtility
    {
        public static List<Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel> CreateEditorViewModels(
            bool createAsUnsaved = true)
        {
            var returnValues = new List<Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel>();
            
            for (int i = 0; i < 10; i++)
            {
                var temp = CreateEditorViewModel();
                
                returnValues.Add(temp);
                
                if (createAsUnsaved == false)
                {
                    temp.Id = i + 1;
                }
            }
            
            return returnValues;
        }
        
        public static Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel CreateEditorViewModel(
            bool createAsUnsaved = true)
        {
            var fromValue = new Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel
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
        
        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.Api.DomainModels.LogEntry> expected,
            IList<Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel> actual)
        {
            Assert.IsNotNull(expected, "Expected was null.");
            Assert.IsNotNull(actual, "Actual was null.");
            Assert.AreEqual<int>(expected.Count, actual.Count, "Item count should match.");
            
            for (int i = 0; i < expected.Count; i++)
            {
                AssertAreEqual(expected[i], actual[i]);
            }
        }
        
        public static void AssertAreEqual(
            Benday.YamlDemoApp.Api.DomainModels.LogEntry expected,
            Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel actual)
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
            IList<Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel> expected,
            IList<Benday.YamlDemoApp.Api.DomainModels.LogEntry> actual)
        {
            Assert.IsNotNull(expected, "Expected was null.");
            Assert.IsNotNull(actual, "Actual was null.");
            Assert.AreEqual<int>(expected.Count, actual.Count,
            "{0}.AssertAreEqual(): Item count should match.",
            nameof(LogEntryViewModelTestUtility));
            
            for (int i = 0; i < expected.Count; i++)
            {
                AssertAreEqual(expected[i], actual[i]);
            }
        }
        
        public static void AssertAreNotEqual(
            Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel expected,
            Benday.YamlDemoApp.Api.DomainModels.LogEntry actual)
        {
            Assert.AreNotEqual<int>(expected.Id, actual.Id,
            "{0}.AssertAreNotEqual(): Id should not match.",
            nameof(LogEntryViewModelTestUtility));
        }
        
        public static void AssertAreEqual(
            Benday.YamlDemoApp.WebUi.Models.LogEntryEditorViewModel expected,
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