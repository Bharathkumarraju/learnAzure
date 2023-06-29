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
    public class UserControllerFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
            _UserServiceInstance = null;
            _LookupServiceInstance = null;
            
        }
        
        private Benday.YamlDemoApp.WebUi.Controllers.UserController _systemUnderTest;
        
        private Benday.YamlDemoApp.WebUi.Controllers.UserController SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new Benday.YamlDemoApp.WebUi.Controllers.UserController(
                    UserServiceInstance,
                    new DefaultValidatorStrategy<UserEditorViewModel>(),
                    new FakeLogger<UserController>(),
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
        
        private FakeUserService _UserServiceInstance;
        public FakeUserService UserServiceInstance
        {
            get
            {
                if (_UserServiceInstance == null)
                {
                    _UserServiceInstance =
                    new FakeUserService();
                }
                
                return _UserServiceInstance;
            }
        }
        [TestMethod]
        public void UserController_Index_CallsServiceAndReturnsList()
        {
            // arrange
            var expected = UserTestUtility.CreateModels();
            
            UserServiceInstance.GetAllReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<IList<User>>(
            SystemUnderTest.Index()) as List<User>;
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            CollectionAssert.AreEquivalent(expected, actual, "Wrong order values.");
            Assert.IsTrue(UserServiceInstance.WasGetAllCalled, "GetAll was not called.");
        }
        
        private void InitializeFakeLookups()
        {
            LookupServiceInstance.GetAllByTypeReturnValue =
            LookupTestUtility.CreateModels(false);
        }
        
        [TestMethod]
        public void UserController_Details_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = UserTestUtility.CreateModel();
            
            UserServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<User>(
            SystemUnderTest.Details(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(UserServiceInstance.WasGetByIdCalled, "GetById was not called.");
            
        }
        
        [TestMethod]
        public void UserController_Details_ForUnknownValueReturnsNotFound()
        {
            // arrange
            UserServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Details(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(UserServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void UserController_Edit_ForNewValueDoesNotCallServiceAndReturnsValue()
        {
            // arrange
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<UserEditorViewModel>(
            SystemUnderTest.Edit(ApiConstants.UnsavedId));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsFalse(UserServiceInstance.WasGetByIdCalled, "GetById should not be called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        [TestMethod]
        public void UserController_Edit_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = UserTestUtility.CreateModel();
            
            UserServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<UserEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsTrue(UserServiceInstance.WasGetByIdCalled, "GetById was not called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        private void AssertLookupValueListsArePopulated(UserEditorViewModel actual)
        {
            Assert.IsNotNull(actual.Statuses, "Statuses");
            Assert.AreNotEqual<int>(0, actual.Statuses.Count,
            "actual.Statuses should have items");
            
        }
        
        [TestMethod]
        public void UserController_Edit_ForUnknownValueReturnsNotFound()
        {
            // arrange
            UserServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(UserServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void UserController_Edit_NewItem_SavesAndReturnsCreatedAtActionResultWithNewId()
        {
            // arrange
            var saveThis = UserViewModelTestUtility.CreateEditorViewModel(true);
            UserServiceInstance.OnSaveUpdateId = true;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(UserServiceInstance.WasSaveCalled, "Save was not called.");
            // Assert.AreSame(saveThis, UserServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void UserController_Edit_ExistingItem_SavesAndReturns()
        {
            // arrange
            var saveThis = UserTestUtility.CreateModel(false);
            UserServiceInstance.GetByIdReturnValue = saveThis;
            
            var viewModel =
            UnitTestUtility.GetModel<UserEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // act
            var actual = SystemUnderTest.Edit(viewModel);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(UserServiceInstance.WasSaveCalled, "Save was not called.");
            Assert.AreSame(saveThis, UserServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void UserController_Edit_ReturnsNotFoundWhenIdIsInvalid()
        {
            // arrange
            var saveThis = UserViewModelTestUtility.CreateEditorViewModel(false);
            UserServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsFalse(UserServiceInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        public void UserController_Delete_GetConfirmationPage_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = UserTestUtility.CreateModel();
            
            UserServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<User>(
            SystemUnderTest.Delete(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(UserServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(UserServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void UserController_Delete_GetConfirmationPage_ForUnknownValueReturnsNotFound()
        {
            // arrange
            UserServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(UserServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(UserServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void UserController_Delete_Confirmed_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = UserTestUtility.CreateModel();
            UserServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            Assert.IsTrue(UserServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsTrue(UserServiceInstance.WasDeleteByIdCalled, "DeleteById was not called.");
        }
        
        [TestMethod]
        public void UserController_Delete_Confirmed_ForUnknownValueReturnsNotFound()
        {
            // arrange
            var expected = UserTestUtility.CreateModel();
            UserServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(UserServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(UserServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void UserController_Search_LoadPage()
        {
            // arrange
            UserServiceInstance.SearchUsingSimpleSearchReturnValue =
            UserTestUtility.CreateModels(false, 100);
            
            // act
            var result = SystemUnderTest.Search();
            
            // assert
            var model = UnitTestUtility.GetModel<UserSearchViewModel>(result);
            
            Assert.IsNotNull(model, "Model was null");
            
            Assert.IsTrue(model.IsSimpleSearch, "IsSimpleSearch");
            Assert.IsFalse(UserServiceInstance.WasSearchUsingSimpleSearchCalled,
            "Search using simple search should not be called.");
            
        }
        
        [TestMethod]
        public void UserController_Search_RunSimpleSearch_ShouldHaveRecords()
        {
            // arrange
            UserServiceInstance.SimpleSearchReturnValue =
            UserTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<UserSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act
            var result = SystemUnderTest.Search(model, null, null);
            var actual = UnitTestUtility.GetModel<UserSearchViewModel>(result);
            
            // assert
            Assert.IsNotNull(actual, "Model was null");
            Assert.IsNotNull(actual.Results, "Results was null");
            Assert.IsNotNull(actual.Results.PageValues, "Results.PageValues was null");
            Assert.AreNotEqual<int>(0, actual.Results.PageValues.Count, "No items on the current page");
            
            Assert.IsTrue(string.IsNullOrEmpty(actual.CurrentSortProperty), "CurrentSortProperty should be null or empty.");
            Assert.AreEqual<string>(SearchConstants.SortDirectionAscending, actual.CurrentSortDirection, "CurrentSortDirection was wrong");
        }
        
        [TestMethod]
        public void UserController_Search_RunSimpleSearch_WithSort()
        {
            // arrange
            UserServiceInstance.SimpleSearchReturnValue =
            UserTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<UserSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // first time sort by null to simulate the first search on the page
            var actual = UnitTestUtility.GetModel<UserSearchViewModel>(
            SystemUnderTest.Search(model, null, null));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, null, SearchConstants.SortDirectionAscending);
            
            // sort by User name to simulate sorting the search
            string sortBy = nameof(User.Id);
            actual = UnitTestUtility.GetModel<UserSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            // sort by User name again to simulate flipping the sort direction
            sortBy = nameof(User.Id);
            actual = UnitTestUtility.GetModel<UserSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionDescending);
        }
        
        [TestMethod]
        public void UserController_Search_RunSimpleSearch_WithSort_ChangePages()
        {
            // arrange
            UserServiceInstance.SimpleSearchReturnValue =
            UserTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<UserSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // sort by User name to simulate sorting the search
            string sortBy = nameof(User.Id);
            var actual = UnitTestUtility.GetModel<UserSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            Assert.IsTrue(actual.Results.PageCount > 2, "PageCount should be greater than 2 for this test");
            
            var page1Values = new List<User>(actual.Results.PageValues);
            
            // change to page 2
            actual = UnitTestUtility.GetModel<UserSearchViewModel>(
            SystemUnderTest.Search(model, "2", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(2, actual.Results.CurrentPage, "Results page number was wrong.");
            var page2Values = new List<User>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page2Values, page1Values, "Page values didn't change");
            
            // change to page 3
            actual = UnitTestUtility.GetModel<UserSearchViewModel>(
            SystemUnderTest.Search(model, "3", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(3, actual.Results.CurrentPage, "Results page number was wrong.");
            var page3Values = new List<User>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page3Values, page2Values, "Page values didn't change");
        }
    }
}