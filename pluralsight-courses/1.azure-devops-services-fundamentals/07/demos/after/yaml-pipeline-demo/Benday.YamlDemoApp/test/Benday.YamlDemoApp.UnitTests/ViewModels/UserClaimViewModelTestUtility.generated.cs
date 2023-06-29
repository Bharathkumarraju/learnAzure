using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Benday.YamlDemoApp.Api;
using Benday.YamlDemoApp.UnitTests.Utilities;

namespace Benday.YamlDemoApp.UnitTests.ViewModels
{
    public static class UserClaimViewModelTestUtility
    {
        public static List<Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel> CreateEditorViewModels(
            bool createAsUnsaved = true)
        {
            var returnValues = new List<Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel>();
            
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
        
        public static Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel CreateEditorViewModel(
            bool createAsUnsaved = true)
        {
            var fromValue = new Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel
            {
                Id = UnitTestUtility.GetFakeValueForInt("Id"),
                Username = UnitTestUtility.GetFakeValueForString("Username"),
                ClaimName = UnitTestUtility.GetFakeValueForString("ClaimName"),
                ClaimValue = UnitTestUtility.GetFakeValueForString("ClaimValue"),
                UserId = UnitTestUtility.GetFakeValueForInt("UserId"),
                ClaimLogicType = UnitTestUtility.GetFakeValueForString("ClaimLogicType"),
                StartDate = UnitTestUtility.GetFakeValueForDateTime("StartDate"),
                EndDate = UnitTestUtility.GetFakeValueForDateTime("EndDate"),
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
            IList<Benday.YamlDemoApp.Api.DomainModels.UserClaim> expected,
            IList<Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel> actual)
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
            Benday.YamlDemoApp.Api.DomainModels.UserClaim expected,
            Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<string>(expected.Username, actual.Username, "Username");
            Assert.AreEqual<string>(expected.ClaimName, actual.ClaimName, "ClaimName");
            Assert.AreEqual<string>(expected.ClaimValue, actual.ClaimValue, "ClaimValue");
            Assert.AreEqual<int>(expected.UserId, actual.UserId, "UserId");
            Assert.AreEqual<string>(expected.ClaimLogicType, actual.ClaimLogicType, "ClaimLogicType");
            Assert.AreEqual<DateTime>(expected.StartDate, actual.StartDate, "StartDate");
            Assert.AreEqual<DateTime>(expected.EndDate, actual.EndDate, "EndDate");
            Assert.AreEqual<string>(expected.Status, actual.Status, "Status");
            Assert.AreEqual<string>(expected.CreatedBy, actual.CreatedBy, "CreatedBy");
            Assert.AreEqual<DateTime>(expected.CreatedDate, actual.CreatedDate, "CreatedDate");
            Assert.AreEqual<string>(expected.LastModifiedBy, actual.LastModifiedBy, "LastModifiedBy");
            Assert.AreEqual<DateTime>(expected.LastModifiedDate, actual.LastModifiedDate, "LastModifiedDate");
            Assert.AreEqual<byte[]>(expected.Timestamp, actual.Timestamp, "Timestamp");
            
            
        }
        
        public static void AssertAreEqual(
            IList<Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel> expected,
            IList<Benday.YamlDemoApp.Api.DomainModels.UserClaim> actual)
        {
            Assert.IsNotNull(expected, "Expected was null.");
            Assert.IsNotNull(actual, "Actual was null.");
            Assert.AreEqual<int>(expected.Count, actual.Count,
            "{0}.AssertAreEqual(): Item count should match.",
            nameof(UserClaimViewModelTestUtility));
            
            for (int i = 0; i < expected.Count; i++)
            {
                AssertAreEqual(expected[i], actual[i]);
            }
        }
        
        public static void AssertAreNotEqual(
            Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel expected,
            Benday.YamlDemoApp.Api.DomainModels.UserClaim actual)
        {
            Assert.AreNotEqual<int>(expected.Id, actual.Id,
            "{0}.AssertAreNotEqual(): Id should not match.",
            nameof(UserClaimViewModelTestUtility));
        }
        
        public static void AssertAreEqual(
            Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel expected,
            Benday.YamlDemoApp.Api.DomainModels.UserClaim actual)
        {
            Assert.AreEqual<int>(expected.Id, actual.Id, "Id");
            Assert.AreEqual<string>(expected.Username, actual.Username, "Username");
            Assert.AreEqual<string>(expected.ClaimName, actual.ClaimName, "ClaimName");
            Assert.AreEqual<string>(expected.ClaimValue, actual.ClaimValue, "ClaimValue");
            Assert.AreEqual<int>(expected.UserId, actual.UserId, "UserId");
            Assert.AreEqual<string>(expected.ClaimLogicType, actual.ClaimLogicType, "ClaimLogicType");
            Assert.AreEqual<DateTime>(expected.StartDate, actual.StartDate, "StartDate");
            Assert.AreEqual<DateTime>(expected.EndDate, actual.EndDate, "EndDate");
            Assert.AreEqual<string>(expected.Status, actual.Status, "Status");
            Assert.AreEqual<string>(expected.CreatedBy, actual.CreatedBy, "CreatedBy");
            Assert.AreEqual<DateTime>(expected.CreatedDate, actual.CreatedDate, "CreatedDate");
            Assert.AreEqual<string>(expected.LastModifiedBy, actual.LastModifiedBy, "LastModifiedBy");
            Assert.AreEqual<DateTime>(expected.LastModifiedDate, actual.LastModifiedDate, "LastModifiedDate");
            Assert.AreEqual<byte[]>(expected.Timestamp, actual.Timestamp, "Timestamp");
            
            
        }
    }
}