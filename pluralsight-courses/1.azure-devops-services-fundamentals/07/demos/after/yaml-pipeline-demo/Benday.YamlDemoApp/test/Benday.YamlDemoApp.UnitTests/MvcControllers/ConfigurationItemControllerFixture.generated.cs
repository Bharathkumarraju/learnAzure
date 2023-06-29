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
    public class ConfigurationItemControllerFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
            _ConfigurationItemServiceInstance = null;
            _LookupServiceInstance = null;
            
        }
        
        private Benday.YamlDemoApp.WebUi.Controllers.ConfigurationItemController _systemUnderTest;
        
        private Benday.YamlDemoApp.WebUi.Controllers.ConfigurationItemController SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new Benday.YamlDemoApp.WebUi.Controllers.ConfigurationItemController(
                    ConfigurationItemServiceInstance,
                    new DefaultValidatorStrategy<ConfigurationItemEditorViewModel>(),
                    new FakeLogger<ConfigurationItemController>(),
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
        
        private FakeConfigurationItemService _ConfigurationItemServiceInstance;
        public FakeConfigurationItemService ConfigurationItemServiceInstance
        {
            get
            {
                if (_ConfigurationItemServiceInstance == null)
                {
                    _ConfigurationItemServiceInstance =
                    new FakeConfigurationItemService();
                }
                
                return _ConfigurationItemServiceInstance;
            }
        }
        [TestMethod]
        public void ConfigurationItemController_Index_CallsServiceAndReturnsList()
        {
            // arrange
            var expected = ConfigurationItemTestUtility.CreateModels();
            
            ConfigurationItemServiceInstance.GetAllReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<IList<ConfigurationItem>>(
            SystemUnderTest.Index()) as List<ConfigurationItem>;
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            CollectionAssert.AreEquivalent(expected, actual, "Wrong order values.");
            Assert.IsTrue(ConfigurationItemServiceInstance.WasGetAllCalled, "GetAll was not called.");
        }
        
        private void InitializeFakeLookups()
        {
            LookupServiceInstance.GetAllByTypeReturnValue =
            LookupTestUtility.CreateModels(false);
        }
        
        [TestMethod]
        public void ConfigurationItemController_Details_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = ConfigurationItemTestUtility.CreateModel();
            
            ConfigurationItemServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<ConfigurationItem>(
            SystemUnderTest.Details(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(ConfigurationItemServiceInstance.WasGetByIdCalled, "GetById was not called.");
            
        }
        
        [TestMethod]
        public void ConfigurationItemController_Details_ForUnknownValueReturnsNotFound()
        {
            // arrange
            ConfigurationItemServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Details(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(ConfigurationItemServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Edit_ForNewValueDoesNotCallServiceAndReturnsValue()
        {
            // arrange
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<ConfigurationItemEditorViewModel>(
            SystemUnderTest.Edit(ApiConstants.UnsavedId));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsFalse(ConfigurationItemServiceInstance.WasGetByIdCalled, "GetById should not be called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        [TestMethod]
        public void ConfigurationItemController_Edit_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = ConfigurationItemTestUtility.CreateModel();
            
            ConfigurationItemServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<ConfigurationItemEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsTrue(ConfigurationItemServiceInstance.WasGetByIdCalled, "GetById was not called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        private void AssertLookupValueListsArePopulated(ConfigurationItemEditorViewModel actual)
        {
            Assert.IsNotNull(actual.Statuses, "Statuses");
            Assert.AreNotEqual<int>(0, actual.Statuses.Count,
            "actual.Statuses should have items");
            
        }
        
        [TestMethod]
        public void ConfigurationItemController_Edit_ForUnknownValueReturnsNotFound()
        {
            // arrange
            ConfigurationItemServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(ConfigurationItemServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Edit_NewItem_SavesAndReturnsCreatedAtActionResultWithNewId()
        {
            // arrange
            var saveThis = ConfigurationItemViewModelTestUtility.CreateEditorViewModel(true);
            ConfigurationItemServiceInstance.OnSaveUpdateId = true;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(ConfigurationItemServiceInstance.WasSaveCalled, "Save was not called.");
            // Assert.AreSame(saveThis, ConfigurationItemServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Edit_ExistingItem_SavesAndReturns()
        {
            // arrange
            var saveThis = ConfigurationItemTestUtility.CreateModel(false);
            ConfigurationItemServiceInstance.GetByIdReturnValue = saveThis;
            
            var viewModel =
            UnitTestUtility.GetModel<ConfigurationItemEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // act
            var actual = SystemUnderTest.Edit(viewModel);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(ConfigurationItemServiceInstance.WasSaveCalled, "Save was not called.");
            Assert.AreSame(saveThis, ConfigurationItemServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Edit_ReturnsNotFoundWhenIdIsInvalid()
        {
            // arrange
            var saveThis = ConfigurationItemViewModelTestUtility.CreateEditorViewModel(false);
            ConfigurationItemServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsFalse(ConfigurationItemServiceInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Delete_GetConfirmationPage_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = ConfigurationItemTestUtility.CreateModel();
            
            ConfigurationItemServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<ConfigurationItem>(
            SystemUnderTest.Delete(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(ConfigurationItemServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(ConfigurationItemServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Delete_GetConfirmationPage_ForUnknownValueReturnsNotFound()
        {
            // arrange
            ConfigurationItemServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(ConfigurationItemServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(ConfigurationItemServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Delete_Confirmed_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = ConfigurationItemTestUtility.CreateModel();
            ConfigurationItemServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            Assert.IsTrue(ConfigurationItemServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsTrue(ConfigurationItemServiceInstance.WasDeleteByIdCalled, "DeleteById was not called.");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Delete_Confirmed_ForUnknownValueReturnsNotFound()
        {
            // arrange
            var expected = ConfigurationItemTestUtility.CreateModel();
            ConfigurationItemServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(ConfigurationItemServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(ConfigurationItemServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Search_LoadPage()
        {
            // arrange
            ConfigurationItemServiceInstance.SearchUsingSimpleSearchReturnValue =
            ConfigurationItemTestUtility.CreateModels(false, 100);
            
            // act
            var result = SystemUnderTest.Search();
            
            // assert
            var model = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(result);
            
            Assert.IsNotNull(model, "Model was null");
            
            Assert.IsTrue(model.IsSimpleSearch, "IsSimpleSearch");
            Assert.IsFalse(ConfigurationItemServiceInstance.WasSearchUsingSimpleSearchCalled,
            "Search using simple search should not be called.");
            
        }
        
        [TestMethod]
        public void ConfigurationItemController_Search_RunSimpleSearch_ShouldHaveRecords()
        {
            // arrange
            ConfigurationItemServiceInstance.SimpleSearchReturnValue =
            ConfigurationItemTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act
            var result = SystemUnderTest.Search(model, null, null);
            var actual = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(result);
            
            // assert
            Assert.IsNotNull(actual, "Model was null");
            Assert.IsNotNull(actual.Results, "Results was null");
            Assert.IsNotNull(actual.Results.PageValues, "Results.PageValues was null");
            Assert.AreNotEqual<int>(0, actual.Results.PageValues.Count, "No items on the current page");
            
            Assert.IsTrue(string.IsNullOrEmpty(actual.CurrentSortProperty), "CurrentSortProperty should be null or empty.");
            Assert.AreEqual<string>(SearchConstants.SortDirectionAscending, actual.CurrentSortDirection, "CurrentSortDirection was wrong");
        }
        
        [TestMethod]
        public void ConfigurationItemController_Search_RunSimpleSearch_WithSort()
        {
            // arrange
            ConfigurationItemServiceInstance.SimpleSearchReturnValue =
            ConfigurationItemTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // first time sort by null to simulate the first search on the page
            var actual = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(
            SystemUnderTest.Search(model, null, null));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, null, SearchConstants.SortDirectionAscending);
            
            // sort by ConfigurationItem name to simulate sorting the search
            string sortBy = nameof(ConfigurationItem.Id);
            actual = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            // sort by ConfigurationItem name again to simulate flipping the sort direction
            sortBy = nameof(ConfigurationItem.Id);
            actual = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionDescending);
        }
        
        [TestMethod]
        public void ConfigurationItemController_Search_RunSimpleSearch_WithSort_ChangePages()
        {
            // arrange
            ConfigurationItemServiceInstance.SimpleSearchReturnValue =
            ConfigurationItemTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // sort by ConfigurationItem name to simulate sorting the search
            string sortBy = nameof(ConfigurationItem.Id);
            var actual = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            Assert.IsTrue(actual.Results.PageCount > 2, "PageCount should be greater than 2 for this test");
            
            var page1Values = new List<ConfigurationItem>(actual.Results.PageValues);
            
            // change to page 2
            actual = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(
            SystemUnderTest.Search(model, "2", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(2, actual.Results.CurrentPage, "Results page number was wrong.");
            var page2Values = new List<ConfigurationItem>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page2Values, page1Values, "Page values didn't change");
            
            // change to page 3
            actual = UnitTestUtility.GetModel<ConfigurationItemSearchViewModel>(
            SystemUnderTest.Search(model, "3", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(3, actual.Results.CurrentPage, "Results page number was wrong.");
            var page3Values = new List<ConfigurationItem>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page3Values, page2Values, "Page values didn't change");
        }
    }
}