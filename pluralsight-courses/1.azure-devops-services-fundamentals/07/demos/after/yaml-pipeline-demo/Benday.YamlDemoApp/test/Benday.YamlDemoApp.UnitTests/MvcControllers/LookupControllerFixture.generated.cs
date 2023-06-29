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
    public class LookupControllerFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
            _LookupServiceInstance = null;
            
            
        }
        
        private Benday.YamlDemoApp.WebUi.Controllers.LookupController _systemUnderTest;
        
        private Benday.YamlDemoApp.WebUi.Controllers.LookupController SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new Benday.YamlDemoApp.WebUi.Controllers.LookupController(
                    LookupServiceInstance,
                    new DefaultValidatorStrategy<LookupEditorViewModel>(),
                    new FakeLogger<LookupController>()
                    );
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
        [TestMethod]
        public void LookupController_Index_CallsServiceAndReturnsList()
        {
            // arrange
            var expected = LookupTestUtility.CreateModels();
            
            LookupServiceInstance.GetAllReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<IList<Lookup>>(
            SystemUnderTest.Index()) as List<Lookup>;
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            CollectionAssert.AreEquivalent(expected, actual, "Wrong order values.");
            Assert.IsTrue(LookupServiceInstance.WasGetAllCalled, "GetAll was not called.");
        }
        
        private void InitializeFakeLookups()
        {
            LookupServiceInstance.GetAllByTypeReturnValue =
            LookupTestUtility.CreateModels(false);
        }
        
        [TestMethod]
        public void LookupController_Details_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = LookupTestUtility.CreateModel();
            
            LookupServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<Lookup>(
            SystemUnderTest.Details(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(LookupServiceInstance.WasGetByIdCalled, "GetById was not called.");
            
        }
        
        [TestMethod]
        public void LookupController_Details_ForUnknownValueReturnsNotFound()
        {
            // arrange
            LookupServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Details(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(LookupServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void LookupController_Edit_ForNewValueDoesNotCallServiceAndReturnsValue()
        {
            // arrange
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<LookupEditorViewModel>(
            SystemUnderTest.Edit(ApiConstants.UnsavedId));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsFalse(LookupServiceInstance.WasGetByIdCalled, "GetById should not be called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        [TestMethod]
        public void LookupController_Edit_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = LookupTestUtility.CreateModel();
            
            LookupServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<LookupEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsTrue(LookupServiceInstance.WasGetByIdCalled, "GetById was not called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        private void AssertLookupValueListsArePopulated(LookupEditorViewModel actual)
        {
            Assert.IsNotNull(actual.LookupTypes, "LookupTypes");
            Assert.AreNotEqual<int>(0, actual.LookupTypes.Count,
            "actual.LookupTypes should have items");
            Assert.IsNotNull(actual.Statuses, "Statuses");
            Assert.AreNotEqual<int>(0, actual.Statuses.Count,
            "actual.Statuses should have items");
            
        }
        
        [TestMethod]
        public void LookupController_Edit_ForUnknownValueReturnsNotFound()
        {
            // arrange
            LookupServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(LookupServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void LookupController_Edit_NewItem_SavesAndReturnsCreatedAtActionResultWithNewId()
        {
            // arrange
            var saveThis = LookupViewModelTestUtility.CreateEditorViewModel(true);
            LookupServiceInstance.OnSaveUpdateId = true;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(LookupServiceInstance.WasSaveCalled, "Save was not called.");
            // Assert.AreSame(saveThis, LookupServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void LookupController_Edit_ExistingItem_SavesAndReturns()
        {
            // arrange
            var saveThis = LookupTestUtility.CreateModel(false);
            LookupServiceInstance.GetByIdReturnValue = saveThis;
            
            var viewModel =
            UnitTestUtility.GetModel<LookupEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // act
            var actual = SystemUnderTest.Edit(viewModel);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(LookupServiceInstance.WasSaveCalled, "Save was not called.");
            Assert.AreSame(saveThis, LookupServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void LookupController_Edit_ReturnsNotFoundWhenIdIsInvalid()
        {
            // arrange
            var saveThis = LookupViewModelTestUtility.CreateEditorViewModel(false);
            LookupServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsFalse(LookupServiceInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        public void LookupController_Delete_GetConfirmationPage_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = LookupTestUtility.CreateModel();
            
            LookupServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<Lookup>(
            SystemUnderTest.Delete(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(LookupServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(LookupServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void LookupController_Delete_GetConfirmationPage_ForUnknownValueReturnsNotFound()
        {
            // arrange
            LookupServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(LookupServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(LookupServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void LookupController_Delete_Confirmed_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = LookupTestUtility.CreateModel();
            LookupServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            Assert.IsTrue(LookupServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsTrue(LookupServiceInstance.WasDeleteByIdCalled, "DeleteById was not called.");
        }
        
        [TestMethod]
        public void LookupController_Delete_Confirmed_ForUnknownValueReturnsNotFound()
        {
            // arrange
            var expected = LookupTestUtility.CreateModel();
            LookupServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(LookupServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(LookupServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void LookupController_Search_LoadPage()
        {
            // arrange
            LookupServiceInstance.SearchUsingSimpleSearchReturnValue =
            LookupTestUtility.CreateModels(false, 100);
            
            // act
            var result = SystemUnderTest.Search();
            
            // assert
            var model = UnitTestUtility.GetModel<LookupSearchViewModel>(result);
            
            Assert.IsNotNull(model, "Model was null");
            
            Assert.IsTrue(model.IsSimpleSearch, "IsSimpleSearch");
            Assert.IsFalse(LookupServiceInstance.WasSearchUsingSimpleSearchCalled,
            "Search using simple search should not be called.");
            
        }
        
        [TestMethod]
        public void LookupController_Search_RunSimpleSearch_ShouldHaveRecords()
        {
            // arrange
            LookupServiceInstance.SimpleSearchReturnValue =
            LookupTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<LookupSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act
            var result = SystemUnderTest.Search(model, null, null);
            var actual = UnitTestUtility.GetModel<LookupSearchViewModel>(result);
            
            // assert
            Assert.IsNotNull(actual, "Model was null");
            Assert.IsNotNull(actual.Results, "Results was null");
            Assert.IsNotNull(actual.Results.PageValues, "Results.PageValues was null");
            Assert.AreNotEqual<int>(0, actual.Results.PageValues.Count, "No items on the current page");
            
            Assert.IsTrue(string.IsNullOrEmpty(actual.CurrentSortProperty), "CurrentSortProperty should be null or empty.");
            Assert.AreEqual<string>(SearchConstants.SortDirectionAscending, actual.CurrentSortDirection, "CurrentSortDirection was wrong");
        }
        
        [TestMethod]
        public void LookupController_Search_RunSimpleSearch_WithSort()
        {
            // arrange
            LookupServiceInstance.SimpleSearchReturnValue =
            LookupTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<LookupSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // first time sort by null to simulate the first search on the page
            var actual = UnitTestUtility.GetModel<LookupSearchViewModel>(
            SystemUnderTest.Search(model, null, null));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, null, SearchConstants.SortDirectionAscending);
            
            // sort by Lookup name to simulate sorting the search
            string sortBy = nameof(Lookup.Id);
            actual = UnitTestUtility.GetModel<LookupSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            // sort by Lookup name again to simulate flipping the sort direction
            sortBy = nameof(Lookup.Id);
            actual = UnitTestUtility.GetModel<LookupSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionDescending);
        }
        
        [TestMethod]
        public void LookupController_Search_RunSimpleSearch_WithSort_ChangePages()
        {
            // arrange
            LookupServiceInstance.SimpleSearchReturnValue =
            LookupTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<LookupSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // sort by Lookup name to simulate sorting the search
            string sortBy = nameof(Lookup.Id);
            var actual = UnitTestUtility.GetModel<LookupSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            Assert.IsTrue(actual.Results.PageCount > 2, "PageCount should be greater than 2 for this test");
            
            var page1Values = new List<Lookup>(actual.Results.PageValues);
            
            // change to page 2
            actual = UnitTestUtility.GetModel<LookupSearchViewModel>(
            SystemUnderTest.Search(model, "2", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(2, actual.Results.CurrentPage, "Results page number was wrong.");
            var page2Values = new List<Lookup>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page2Values, page1Values, "Page values didn't change");
            
            // change to page 3
            actual = UnitTestUtility.GetModel<LookupSearchViewModel>(
            SystemUnderTest.Search(model, "3", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(3, actual.Results.CurrentPage, "Results page number was wrong.");
            var page3Values = new List<Lookup>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page3Values, page2Values, "Page values didn't change");
        }
    }
}