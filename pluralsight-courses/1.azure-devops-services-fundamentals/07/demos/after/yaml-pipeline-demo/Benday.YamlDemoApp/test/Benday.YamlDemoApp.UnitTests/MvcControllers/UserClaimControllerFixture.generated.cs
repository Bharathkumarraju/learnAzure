using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.Api.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benday.YamlDemoApp.UnitTests.Fakes;
using Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Benday.YamlDemoApp.WebUi.Controllers;
using Benday.YamlDemoApp.WebUi.Models;
using Benday.YamlDemoApp.UnitTests.ViewModels;
using Benday.YamlDemoApp.Api;
using Benday.Common;



namespace Benday.YamlDemoApp.UnitTests.MvcControllers
{
    [TestClass]
    public class UserClaimControllerFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
            _UserClaimServiceInstance = null;
            _LookupServiceInstance = null;
            
        }
        
        private Benday.YamlDemoApp.WebUi.Controllers.UserClaimController _systemUnderTest;
        
        private Benday.YamlDemoApp.WebUi.Controllers.UserClaimController SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new Benday.YamlDemoApp.WebUi.Controllers.UserClaimController(
                    UserClaimServiceInstance,
                    new DefaultValidatorStrategy<UserClaimEditorViewModel>(),
                    new FakeLogger<UserClaimController>(),
                    LookupServiceInstance);
                }
                
                return _systemUnderTest;
            }
        }
        
        
        
        private FakeLookupService _LookupServiceInstance;
        public FakeLookupService LookupServiceInstance
        {
            get
            {
                if (_LookupServiceInstance == null)
                {
                    _LookupServiceInstance =
                    new FakeLookupService();
                }
                
                return _LookupServiceInstance;
            }
        }
        
        private FakeUserClaimService _UserClaimServiceInstance;
        public FakeUserClaimService UserClaimServiceInstance
        {
            get
            {
                if (_UserClaimServiceInstance == null)
                {
                    _UserClaimServiceInstance =
                    new FakeUserClaimService();
                }
                
                return _UserClaimServiceInstance;
            }
        }
        [TestMethod]
        public void UserClaimController_Index_CallsServiceAndReturnsList()
        {
            // arrange
            var expected = UserClaimTestUtility.CreateModels();
            
            UserClaimServiceInstance.GetAllReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<IList<UserClaim>>(
            SystemUnderTest.Index()) as List<UserClaim>;
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            CollectionAssert.AreEquivalent(expected, actual, "Wrong order values.");
            Assert.IsTrue(UserClaimServiceInstance.WasGetAllCalled, "GetAll was not called.");
        }
        
        private void InitializeFakeLookups()
        {
            LookupServiceInstance.GetAllByTypeReturnValue =
            LookupTestUtility.CreateModels(false);
        }
        
        [TestMethod]
        public void UserClaimController_Details_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = UserClaimTestUtility.CreateModel();
            
            UserClaimServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<UserClaim>(
            SystemUnderTest.Details(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(UserClaimServiceInstance.WasGetByIdCalled, "GetById was not called.");
            
        }
        
        [TestMethod]
        public void UserClaimController_Details_ForUnknownValueReturnsNotFound()
        {
            // arrange
            UserClaimServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Details(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(UserClaimServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void UserClaimController_Edit_ForNewValueDoesNotCallServiceAndReturnsValue()
        {
            // arrange
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<UserClaimEditorViewModel>(
            SystemUnderTest.Edit(ApiConstants.UnsavedId));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsFalse(UserClaimServiceInstance.WasGetByIdCalled, "GetById should not be called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        [TestMethod]
        public void UserClaimController_Edit_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = UserClaimTestUtility.CreateModel();
            
            UserClaimServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<UserClaimEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsTrue(UserClaimServiceInstance.WasGetByIdCalled, "GetById was not called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        private void AssertLookupValueListsArePopulated(UserClaimEditorViewModel actual)
        {
            Assert.IsNotNull(actual.ClaimLogicTypes, "ClaimLogicTypes");
            Assert.AreNotEqual<int>(0, actual.ClaimLogicTypes.Count,
            "actual.ClaimLogicTypes should have items");
            Assert.IsNotNull(actual.Statuses, "Statuses");
            Assert.AreNotEqual<int>(0, actual.Statuses.Count,
            "actual.Statuses should have items");
            
        }
        
        [TestMethod]
        public void UserClaimController_Edit_ForUnknownValueReturnsNotFound()
        {
            // arrange
            UserClaimServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(UserClaimServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void UserClaimController_Edit_NewItem_SavesAndReturnsCreatedAtActionResultWithNewId()
        {
            // arrange
            var saveThis = UserClaimViewModelTestUtility.CreateEditorViewModel(true);
            UserClaimServiceInstance.OnSaveUpdateId = true;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(UserClaimServiceInstance.WasSaveCalled, "Save was not called.");
            // Assert.AreSame(saveThis, UserClaimServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void UserClaimController_Edit_ExistingItem_SavesAndReturns()
        {
            // arrange
            var saveThis = UserClaimTestUtility.CreateModel(false);
            UserClaimServiceInstance.GetByIdReturnValue = saveThis;
            
            var viewModel =
            UnitTestUtility.GetModel<UserClaimEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // act
            var actual = SystemUnderTest.Edit(viewModel);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(UserClaimServiceInstance.WasSaveCalled, "Save was not called.");
            Assert.AreSame(saveThis, UserClaimServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void UserClaimController_Edit_ReturnsNotFoundWhenIdIsInvalid()
        {
            // arrange
            var saveThis = UserClaimViewModelTestUtility.CreateEditorViewModel(false);
            UserClaimServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsFalse(UserClaimServiceInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        public void UserClaimController_Delete_GetConfirmationPage_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = UserClaimTestUtility.CreateModel();
            
            UserClaimServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<UserClaim>(
            SystemUnderTest.Delete(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(UserClaimServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(UserClaimServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void UserClaimController_Delete_GetConfirmationPage_ForUnknownValueReturnsNotFound()
        {
            // arrange
            UserClaimServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(UserClaimServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(UserClaimServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void UserClaimController_Delete_Confirmed_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = UserClaimTestUtility.CreateModel();
            UserClaimServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            Assert.IsTrue(UserClaimServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsTrue(UserClaimServiceInstance.WasDeleteByIdCalled, "DeleteById was not called.");
        }
        
        [TestMethod]
        public void UserClaimController_Delete_Confirmed_ForUnknownValueReturnsNotFound()
        {
            // arrange
            var expected = UserClaimTestUtility.CreateModel();
            UserClaimServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(UserClaimServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(UserClaimServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void UserClaimController_Search_LoadPage()
        {
            // arrange
            UserClaimServiceInstance.SearchUsingSimpleSearchReturnValue =
            UserClaimTestUtility.CreateModels(false, 100);
            
            // act
            var result = SystemUnderTest.Search();
            
            // assert
            var model = UnitTestUtility.GetModel<UserClaimSearchViewModel>(result);
            
            Assert.IsNotNull(model, "Model was null");
            
            Assert.IsTrue(model.IsSimpleSearch, "IsSimpleSearch");
            Assert.IsFalse(UserClaimServiceInstance.WasSearchUsingSimpleSearchCalled,
            "Search using simple search should not be called.");
            
        }
        
        [TestMethod]
        public void UserClaimController_Search_RunSimpleSearch_ShouldHaveRecords()
        {
            // arrange
            UserClaimServiceInstance.SimpleSearchReturnValue =
            UserClaimTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<UserClaimSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act
            var result = SystemUnderTest.Search(model, null, null);
            var actual = UnitTestUtility.GetModel<UserClaimSearchViewModel>(result);
            
            // assert
            Assert.IsNotNull(actual, "Model was null");
            Assert.IsNotNull(actual.Results, "Results was null");
            Assert.IsNotNull(actual.Results.PageValues, "Results.PageValues was null");
            Assert.AreNotEqual<int>(0, actual.Results.PageValues.Count, "No items on the current page");
            
            Assert.IsTrue(string.IsNullOrEmpty(actual.CurrentSortProperty), "CurrentSortProperty should be null or empty.");
            Assert.AreEqual<string>(SearchConstants.SortDirectionAscending, actual.CurrentSortDirection, "CurrentSortDirection was wrong");
        }
        
        [TestMethod]
        public void UserClaimController_Search_RunSimpleSearch_WithSort()
        {
            // arrange
            UserClaimServiceInstance.SimpleSearchReturnValue =
            UserClaimTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<UserClaimSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // first time sort by null to simulate the first search on the page
            var actual = UnitTestUtility.GetModel<UserClaimSearchViewModel>(
            SystemUnderTest.Search(model, null, null));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, null, SearchConstants.SortDirectionAscending);
            
            // sort by UserClaim name to simulate sorting the search
            string sortBy = nameof(UserClaim.Id);
            actual = UnitTestUtility.GetModel<UserClaimSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            // sort by UserClaim name again to simulate flipping the sort direction
            sortBy = nameof(UserClaim.Id);
            actual = UnitTestUtility.GetModel<UserClaimSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionDescending);
        }
        
        [TestMethod]
        public void UserClaimController_Search_RunSimpleSearch_WithSort_ChangePages()
        {
            // arrange
            UserClaimServiceInstance.SimpleSearchReturnValue =
            UserClaimTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<UserClaimSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // sort by UserClaim name to simulate sorting the search
            string sortBy = nameof(UserClaim.Id);
            var actual = UnitTestUtility.GetModel<UserClaimSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            Assert.IsTrue(actual.Results.PageCount > 2, "PageCount should be greater than 2 for this test");
            
            var page1Values = new List<UserClaim>(actual.Results.PageValues);
            
            // change to page 2
            actual = UnitTestUtility.GetModel<UserClaimSearchViewModel>(
            SystemUnderTest.Search(model, "2", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(2, actual.Results.CurrentPage, "Results page number was wrong.");
            var page2Values = new List<UserClaim>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page2Values, page1Values, "Page values didn't change");
            
            // change to page 3
            actual = UnitTestUtility.GetModel<UserClaimSearchViewModel>(
            SystemUnderTest.Search(model, "3", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(3, actual.Results.CurrentPage, "Results page number was wrong.");
            var page3Values = new List<UserClaim>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page3Values, page2Values, "Page values didn't change");
        }
    }
}