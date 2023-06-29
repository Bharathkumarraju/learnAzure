using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Benday.YamlDemoApp.Api;
using Benday.YamlDemoApp.UnitTests.Utilities;

namespace Benday.YamlDemoApp.UnitTests.ViewModels
{
    public static class ConfigurationItemViewModelTestUtility
    {
        public static List<Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel> CreateEditorViewModels(
            bool createAsUnsaved = true)
        {
            var returnValues = new List<Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel>();
            
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
        
        public static Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel CreateEditorViewModel(
            bool createAsUnsaved = true)
        {
            var fromValue = new Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel
            {
                Id = UnitTestUtility.GetFakeValueForInt("Id"),
                Category = UnitTestUtility.GetFakeValueForString("Category"),
                ConfigurationKey = UnitTestUtility.GetFakeValueForString("ConfigurationKey"),
                Description = UnitTestUtility.GetFakeValueForString("Description"),
                ConfigurationValue = UnitTestUtility.GetFakeValueForString("ConfigurationValue"),
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
                fromValue.CreatedDate = default(DateTime);
                fromValue.LastModifiedDate = default(DateTime);
                fromValue.CreatedBy = null;
                fromValue.LastModifiedBy = null;
            }
            
            return fromValue;
        }
        
        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> expected,
            IList<Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel> actual)
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
            Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem expected,
            Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<string>(expected.Category, actual.Category, "Category");
            Assert.AreEqual<string>(expected.ConfigurationKey, actual.ConfigurationKey, "ConfigurationKey");
            Assert.AreEqual<string>(expected.Description, actual.Description, "Description");
            Assert.AreEqual<string>(expected.ConfigurationValue, actual.ConfigurationValue, "ConfigurationValue");
            Assert.AreEqual<string>(expected.Status, actual.Status, "Status");
            Assert.AreEqual<string>(expected.CreatedBy, actual.CreatedBy, "CreatedBy");
            Assert.AreEqual<DateTime>(expected.CreatedDate, actual.CreatedDate, "CreatedDate");
            Assert.AreEqual<string>(expected.LastModifiedBy, actual.LastModifiedBy, "LastModifiedBy");
            Assert.AreEqual<DateTime>(expected.LastModifiedDate, actual.LastModifiedDate, "LastModifiedDate");
            Assert.AreEqual<byte[]>(expected.Timestamp, actual.Timestamp, "Timestamp");
            
            
        }
        
        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel> expected,
            IList<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> actual)
        {
            Assert.IsNotNull(expected, "Expected was null.");
            Assert.IsNotNull(actual, "Actual was null.");
            Assert.AreEqual<int>(expected.Count, actual.Count,
            "{0}.AssertAreEqual(): Item count should match.",
            nameof(ConfigurationItemViewModelTestUtility));
            
            for (int i = 0; i < expected.Count; i++)
            {
                AssertAreEqual(expected[i], actual[i]);
            }
        }
        
        public static void AssertAreNotEqual(
            Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel expected,
            Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem actual)
        {
            Assert.AreNotEqual<int>(expected.Id, actual.Id,
            "{0}.AssertAreNotEqual(): Id should not match.",
            nameof(ConfigurationItemViewModelTestUtility));
        }
        
        public static void AssertAreEqual(
            Benday.YamlDemoApp.WebUi.Models.ConfigurationItemEditorViewModel expected,
            Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<string>(expected.Category, actual.Category, "Category");
            Assert.AreEqual<string>(expected.ConfigurationKey, actual.ConfigurationKey, "ConfigurationKey");
            Assert.AreEqual<string>(expected.Description, actual.Description, "Description");
            Assert.AreEqual<string>(expected.ConfigurationValue, actual.ConfigurationValue, "ConfigurationValue");
            Assert.AreEqual<string>(expected.Status, actual.Status, "Status");
            Assert.AreEqual<string>(expected.CreatedBy, actual.CreatedBy, "CreatedBy");
            Assert.AreEqual<DateTime>(expected.CreatedDate, actual.CreatedDate, "CreatedDate");
            Assert.AreEqual<string>(expected.LastModifiedBy, actual.LastModifiedBy, "LastModifiedBy");
            Assert.AreEqual<DateTime>(expected.LastModifiedDate, actual.LastModifiedDate, "LastModifiedDate");
            Assert.AreEqual<byte[]>(expected.Timestamp, actual.Timestamp, "Timestamp");
            
            
        }
    }
}