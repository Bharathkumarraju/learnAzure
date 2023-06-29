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
    public class LogEntryControllerFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
            _LogEntryServiceInstance = null;
            _LookupServiceInstance = null;
            
        }
        
        private Benday.YamlDemoApp.WebUi.Controllers.LogEntryController _systemUnderTest;
        
        private Benday.YamlDemoApp.WebUi.Controllers.LogEntryController SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new Benday.YamlDemoApp.WebUi.Controllers.LogEntryController(
                    LogEntryServiceInstance,
                    new DefaultValidatorStrategy<LogEntryEditorViewModel>(),
                    new FakeLogger<LogEntryController>(),
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
        
        private FakeLogEntryService _LogEntryServiceInstance;
        public FakeLogEntryService LogEntryServiceInstance
        {
            get
            {
                if (_LogEntryServiceInstance == null)
                {
                    _LogEntryServiceInstance =
                    new FakeLogEntryService();
                }
                
                return _LogEntryServiceInstance;
            }
        }
        [TestMethod]
        public void LogEntryController_Index_CallsServiceAndReturnsList()
        {
            // arrange
            var expected = LogEntryTestUtility.CreateModels();
            
            LogEntryServiceInstance.GetAllReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<IList<LogEntry>>(
            SystemUnderTest.Index()) as List<LogEntry>;
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            CollectionAssert.AreEquivalent(expected, actual, "Wrong order values.");
            Assert.IsTrue(LogEntryServiceInstance.WasGetAllCalled, "GetAll was not called.");
        }
        
        private void InitializeFakeLookups()
        {
            LookupServiceInstance.GetAllByTypeReturnValue =
            LookupTestUtility.CreateModels(false);
        }
        
        [TestMethod]
        public void LogEntryController_Details_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = LogEntryTestUtility.CreateModel();
            
            LogEntryServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<LogEntry>(
            SystemUnderTest.Details(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(LogEntryServiceInstance.WasGetByIdCalled, "GetById was not called.");
            
        }
        
        [TestMethod]
        public void LogEntryController_Details_ForUnknownValueReturnsNotFound()
        {
            // arrange
            LogEntryServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Details(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(LogEntryServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void LogEntryController_Edit_ForNewValueDoesNotCallServiceAndReturnsValue()
        {
            // arrange
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<LogEntryEditorViewModel>(
            SystemUnderTest.Edit(ApiConstants.UnsavedId));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsFalse(LogEntryServiceInstance.WasGetByIdCalled, "GetById should not be called.");
            
        }
        
        [TestMethod]
        public void LogEntryController_Edit_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = LogEntryTestUtility.CreateModel();
            
            LogEntryServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<LogEntryEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsTrue(LogEntryServiceInstance.WasGetByIdCalled, "GetById was not called.");
            
        }
        
        
        
        [TestMethod]
        public void LogEntryController_Edit_ForUnknownValueReturnsNotFound()
        {
            // arrange
            LogEntryServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(LogEntryServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void LogEntryController_Edit_NewItem_SavesAndReturnsCreatedAtActionResultWithNewId()
        {
            // arrange
            var saveThis = LogEntryViewModelTestUtility.CreateEditorViewModel(true);
            LogEntryServiceInstance.OnSaveUpdateId = true;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(LogEntryServiceInstance.WasSaveCalled, "Save was not called.");
            // Assert.AreSame(saveThis, LogEntryServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void LogEntryController_Edit_ExistingItem_SavesAndReturns()
        {
            // arrange
            var saveThis = LogEntryTestUtility.CreateModel(false);
            LogEntryServiceInstance.GetByIdReturnValue = saveThis;
            
            var viewModel =
            UnitTestUtility.GetModel<LogEntryEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // act
            var actual = SystemUnderTest.Edit(viewModel);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(LogEntryServiceInstance.WasSaveCalled, "Save was not called.");
            Assert.AreSame(saveThis, LogEntryServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void LogEntryController_Edit_ReturnsNotFoundWhenIdIsInvalid()
        {
            // arrange
            var saveThis = LogEntryViewModelTestUtility.CreateEditorViewModel(false);
            LogEntryServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsFalse(LogEntryServiceInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        public void LogEntryController_Delete_GetConfirmationPage_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = LogEntryTestUtility.CreateModel();
            
            LogEntryServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<LogEntry>(
            SystemUnderTest.Delete(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(LogEntryServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(LogEntryServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void LogEntryController_Delete_GetConfirmationPage_ForUnknownValueReturnsNotFound()
        {
            // arrange
            LogEntryServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(LogEntryServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(LogEntryServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void LogEntryController_Delete_Confirmed_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = LogEntryTestUtility.CreateModel();
            LogEntryServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            Assert.IsTrue(LogEntryServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsTrue(LogEntryServiceInstance.WasDeleteByIdCalled, "DeleteById was not called.");
        }
        
        [TestMethod]
        public void LogEntryController_Delete_Confirmed_ForUnknownValueReturnsNotFound()
        {
            // arrange
            var expected = LogEntryTestUtility.CreateModel();
            LogEntryServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(LogEntryServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(LogEntryServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void LogEntryController_Search_LoadPage()
        {
            // arrange
            LogEntryServiceInstance.SearchUsingSimpleSearchReturnValue =
            LogEntryTestUtility.CreateModels(false, 100);
            
            // act
            var result = SystemUnderTest.Search();
            
            // assert
            var model = UnitTestUtility.GetModel<LogEntrySearchViewModel>(result);
            
            Assert.IsNotNull(model, "Model was null");
            
            Assert.IsTrue(model.IsSimpleSearch, "IsSimpleSearch");
            Assert.IsFalse(LogEntryServiceInstance.WasSearchUsingSimpleSearchCalled,
            "Search using simple search should not be called.");
            
        }
        
        [TestMethod]
        public void LogEntryController_Search_RunSimpleSearch_ShouldHaveRecords()
        {
            // arrange
            LogEntryServiceInstance.SimpleSearchReturnValue =
            LogEntryTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<LogEntrySearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act
            var result = SystemUnderTest.Search(model, null, null);
            var actual = UnitTestUtility.GetModel<LogEntrySearchViewModel>(result);
            
            // assert
            Assert.IsNotNull(actual, "Model was null");
            Assert.IsNotNull(actual.Results, "Results was null");
            Assert.IsNotNull(actual.Results.PageValues, "Results.PageValues was null");
            Assert.AreNotEqual<int>(0, actual.Results.PageValues.Count, "No items on the current page");
            
            Assert.IsTrue(string.IsNullOrEmpty(actual.CurrentSortProperty), "CurrentSortProperty should be null or empty.");
            Assert.AreEqual<string>(SearchConstants.SortDirectionAscending, actual.CurrentSortDirection, "CurrentSortDirection was wrong");
        }
        
        [TestMethod]
        public void LogEntryController_Search_RunSimpleSearch_WithSort()
        {
            // arrange
            LogEntryServiceInstance.SimpleSearchReturnValue =
            LogEntryTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<LogEntrySearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // first time sort by null to simulate the first search on the page
            var actual = UnitTestUtility.GetModel<LogEntrySearchViewModel>(
            SystemUnderTest.Search(model, null, null));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, null, SearchConstants.SortDirectionAscending);
            
            // sort by LogEntry name to simulate sorting the search
            string sortBy = nameof(LogEntry.Id);
            actual = UnitTestUtility.GetModel<LogEntrySearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            // sort by LogEntry name again to simulate flipping the sort direction
            sortBy = nameof(LogEntry.Id);
            actual = UnitTestUtility.GetModel<LogEntrySearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionDescending);
        }
        
        [TestMethod]
        public void LogEntryController_Search_RunSimpleSearch_WithSort_ChangePages()
        {
            // arrange
            LogEntryServiceInstance.SimpleSearchReturnValue =
            LogEntryTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<LogEntrySearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // sort by LogEntry name to simulate sorting the search
            string sortBy = nameof(LogEntry.Id);
            var actual = UnitTestUtility.GetModel<LogEntrySearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            Assert.IsTrue(actual.Results.PageCount > 2, "PageCount should be greater than 2 for this test");
            
            var page1Values = new List<LogEntry>(actual.Results.PageValues);
            
            // change to page 2
            actual = UnitTestUtility.GetModel<LogEntrySearchViewModel>(
            SystemUnderTest.Search(model, "2", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(2, actual.Results.CurrentPage, "Results page number was wrong.");
            var page2Values = new List<LogEntry>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page2Values, page1Values, "Page values didn't change");
            
            // change to page 3
            actual = UnitTestUtility.GetModel<LogEntrySearchViewModel>(
            SystemUnderTest.Search(model, "3", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(3, actual.Results.CurrentPage, "Results page number was wrong.");
            var page3Values = new List<LogEntry>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page3Values, page2Values, "Page values didn't change");
        }
    }
}