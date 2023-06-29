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
    public class FeedbackControllerFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
            _FeedbackServiceInstance = null;
            _LookupServiceInstance = null;
            
        }
        
        private Benday.YamlDemoApp.WebUi.Controllers.FeedbackController _systemUnderTest;
        
        private Benday.YamlDemoApp.WebUi.Controllers.FeedbackController SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new Benday.YamlDemoApp.WebUi.Controllers.FeedbackController(
                    FeedbackServiceInstance,
                    new DefaultValidatorStrategy<FeedbackEditorViewModel>(),
                    new FakeLogger<FeedbackController>(),
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
        
        private FakeFeedbackService _FeedbackServiceInstance;
        public FakeFeedbackService FeedbackServiceInstance
        {
            get
            {
                if (_FeedbackServiceInstance == null)
                {
                    _FeedbackServiceInstance =
                    new FakeFeedbackService();
                }
                
                return _FeedbackServiceInstance;
            }
        }
        [TestMethod]
        public void FeedbackController_Index_CallsServiceAndReturnsList()
        {
            // arrange
            var expected = FeedbackTestUtility.CreateModels();
            
            FeedbackServiceInstance.GetAllReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<IList<Feedback>>(
            SystemUnderTest.Index()) as List<Feedback>;
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            CollectionAssert.AreEquivalent(expected, actual, "Wrong order values.");
            Assert.IsTrue(FeedbackServiceInstance.WasGetAllCalled, "GetAll was not called.");
        }
        
        private void InitializeFakeLookups()
        {
            LookupServiceInstance.GetAllByTypeReturnValue =
            LookupTestUtility.CreateModels(false);
        }
        
        [TestMethod]
        public void FeedbackController_Details_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = FeedbackTestUtility.CreateModel();
            
            FeedbackServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<Feedback>(
            SystemUnderTest.Details(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(FeedbackServiceInstance.WasGetByIdCalled, "GetById was not called.");
            
        }
        
        [TestMethod]
        public void FeedbackController_Details_ForUnknownValueReturnsNotFound()
        {
            // arrange
            FeedbackServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Details(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(FeedbackServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void FeedbackController_Edit_ForNewValueDoesNotCallServiceAndReturnsValue()
        {
            // arrange
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<FeedbackEditorViewModel>(
            SystemUnderTest.Edit(ApiConstants.UnsavedId));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsFalse(FeedbackServiceInstance.WasGetByIdCalled, "GetById should not be called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        [TestMethod]
        public void FeedbackController_Edit_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = FeedbackTestUtility.CreateModel();
            
            FeedbackServiceInstance.GetByIdReturnValue = expected;
            
            InitializeFakeLookups();
            
            // act
            var actual =
            UnitTestUtility.GetModel<FeedbackEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.IsTrue(FeedbackServiceInstance.WasGetByIdCalled, "GetById was not called.");
            AssertLookupValueListsArePopulated(actual);
        }
        
        private void AssertLookupValueListsArePopulated(FeedbackEditorViewModel actual)
        {
            Assert.IsNotNull(actual.FeedbackTypes, "FeedbackTypes");
            Assert.AreNotEqual<int>(0, actual.FeedbackTypes.Count,
            "actual.FeedbackTypes should have items");
            Assert.IsNotNull(actual.Sentiments, "Sentiments");
            Assert.AreNotEqual<int>(0, actual.Sentiments.Count,
            "actual.Sentiments should have items");
            Assert.IsNotNull(actual.Statuses, "Statuses");
            Assert.AreNotEqual<int>(0, actual.Statuses.Count,
            "actual.Statuses should have items");
            
        }
        
        [TestMethod]
        public void FeedbackController_Edit_ForUnknownValueReturnsNotFound()
        {
            // arrange
            FeedbackServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(FeedbackServiceInstance.WasGetByIdCalled, "GetById was not called.");
        }
        
        [TestMethod]
        public void FeedbackController_Edit_NewItem_SavesAndReturnsCreatedAtActionResultWithNewId()
        {
            // arrange
            var saveThis = FeedbackViewModelTestUtility.CreateEditorViewModel(true);
            FeedbackServiceInstance.OnSaveUpdateId = true;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(FeedbackServiceInstance.WasSaveCalled, "Save was not called.");
            // Assert.AreSame(saveThis, FeedbackServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void FeedbackController_Edit_ExistingItem_SavesAndReturns()
        {
            // arrange
            var saveThis = FeedbackTestUtility.CreateModel(false);
            FeedbackServiceInstance.GetByIdReturnValue = saveThis;
            
            var viewModel =
            UnitTestUtility.GetModel<FeedbackEditorViewModel>(
            SystemUnderTest.Edit(1234));
            
            // act
            var actual = SystemUnderTest.Edit(viewModel);
            
            // assert
            UnitTestUtility.AssertIsRedirectToActionResult(actual);
            Assert.IsTrue(FeedbackServiceInstance.WasSaveCalled, "Save was not called.");
            Assert.AreSame(saveThis, FeedbackServiceInstance.SaveArgumentValue, "Wrong value was saved.");
        }
        
        [TestMethod]
        public void FeedbackController_Edit_ReturnsNotFoundWhenIdIsInvalid()
        {
            // arrange
            var saveThis = FeedbackViewModelTestUtility.CreateEditorViewModel(false);
            FeedbackServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Edit(saveThis);
            
            // assert
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsFalse(FeedbackServiceInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        public void FeedbackController_Delete_GetConfirmationPage_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = FeedbackTestUtility.CreateModel();
            
            FeedbackServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual =
            UnitTestUtility.GetModel<Feedback>(
            SystemUnderTest.Delete(1234));
            
            // assert
            Assert.IsNotNull(actual, "Model was null.");
            Assert.AreSame(expected, actual, "Did not return the expected instance.");
            Assert.IsTrue(FeedbackServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(FeedbackServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void FeedbackController_Delete_GetConfirmationPage_ForUnknownValueReturnsNotFound()
        {
            // arrange
            FeedbackServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(1234);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(FeedbackServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(FeedbackServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void FeedbackController_Delete_Confirmed_ForKnownValueCallsServiceAndReturnsValue()
        {
            // arrange
            var expected = FeedbackTestUtility.CreateModel();
            FeedbackServiceInstance.GetByIdReturnValue = expected;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            Assert.IsTrue(FeedbackServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsTrue(FeedbackServiceInstance.WasDeleteByIdCalled, "DeleteById was not called.");
        }
        
        [TestMethod]
        public void FeedbackController_Delete_Confirmed_ForUnknownValueReturnsNotFound()
        {
            // arrange
            var expected = FeedbackTestUtility.CreateModel();
            FeedbackServiceInstance.GetByIdReturnValue = null;
            
            // act
            var actual = SystemUnderTest.Delete(expected);
            
            // assert
            Assert.IsNotNull(actual, "Return value was null.");
            UnitTestUtility.AssertIsHttpNotFound(actual);
            Assert.IsTrue(FeedbackServiceInstance.WasGetByIdCalled, "GetById was not called.");
            Assert.IsFalse(FeedbackServiceInstance.WasDeleteByIdCalled,
            "DeleteById should not be called.");
        }
        
        [TestMethod]
        public void FeedbackController_Search_LoadPage()
        {
            // arrange
            FeedbackServiceInstance.SearchUsingSimpleSearchReturnValue =
            FeedbackTestUtility.CreateModels(false, 100);
            
            // act
            var result = SystemUnderTest.Search();
            
            // assert
            var model = UnitTestUtility.GetModel<FeedbackSearchViewModel>(result);
            
            Assert.IsNotNull(model, "Model was null");
            
            Assert.IsTrue(model.IsSimpleSearch, "IsSimpleSearch");
            Assert.IsFalse(FeedbackServiceInstance.WasSearchUsingSimpleSearchCalled,
            "Search using simple search should not be called.");
            
        }
        
        [TestMethod]
        public void FeedbackController_Search_RunSimpleSearch_ShouldHaveRecords()
        {
            // arrange
            FeedbackServiceInstance.SimpleSearchReturnValue =
            FeedbackTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<FeedbackSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act
            var result = SystemUnderTest.Search(model, null, null);
            var actual = UnitTestUtility.GetModel<FeedbackSearchViewModel>(result);
            
            // assert
            Assert.IsNotNull(actual, "Model was null");
            Assert.IsNotNull(actual.Results, "Results was null");
            Assert.IsNotNull(actual.Results.PageValues, "Results.PageValues was null");
            Assert.AreNotEqual<int>(0, actual.Results.PageValues.Count, "No items on the current page");
            
            Assert.IsTrue(string.IsNullOrEmpty(actual.CurrentSortProperty), "CurrentSortProperty should be null or empty.");
            Assert.AreEqual<string>(SearchConstants.SortDirectionAscending, actual.CurrentSortDirection, "CurrentSortDirection was wrong");
        }
        
        [TestMethod]
        public void FeedbackController_Search_RunSimpleSearch_WithSort()
        {
            // arrange
            FeedbackServiceInstance.SimpleSearchReturnValue =
            FeedbackTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<FeedbackSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // first time sort by null to simulate the first search on the page
            var actual = UnitTestUtility.GetModel<FeedbackSearchViewModel>(
            SystemUnderTest.Search(model, null, null));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, null, SearchConstants.SortDirectionAscending);
            
            // sort by Feedback name to simulate sorting the search
            string sortBy = nameof(Feedback.Id);
            actual = UnitTestUtility.GetModel<FeedbackSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            // sort by Feedback name again to simulate flipping the sort direction
            sortBy = nameof(Feedback.Id);
            actual = UnitTestUtility.GetModel<FeedbackSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionDescending);
        }
        
        [TestMethod]
        public void FeedbackController_Search_RunSimpleSearch_WithSort_ChangePages()
        {
            // arrange
            FeedbackServiceInstance.SimpleSearchReturnValue =
            FeedbackTestUtility.CreateModels(false, 100);
            
            var model = UnitTestUtility.GetModel<FeedbackSearchViewModel>(
            SystemUnderTest.Search());
            
            model.IsSimpleSearch = true;
            model.SimpleSearchValue = "searchval";
            
            // act & assert
            
            // sort by Feedback name to simulate sorting the search
            string sortBy = nameof(Feedback.Id);
            var actual = UnitTestUtility.GetModel<FeedbackSearchViewModel>(
            SystemUnderTest.Search(model, null, sortBy));
            
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            
            Assert.IsTrue(actual.Results.PageCount > 2, "PageCount should be greater than 2 for this test");
            
            var page1Values = new List<Feedback>(actual.Results.PageValues);
            
            // change to page 2
            actual = UnitTestUtility.GetModel<FeedbackSearchViewModel>(
            SystemUnderTest.Search(model, "2", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(2, actual.Results.CurrentPage, "Results page number was wrong.");
            var page2Values = new List<Feedback>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page2Values, page1Values, "Page values didn't change");
            
            // change to page 3
            actual = UnitTestUtility.GetModel<FeedbackSearchViewModel>(
            SystemUnderTest.Search(model, "3", null));
            SearchAndSortTestUtility.AssertSearchResultsAndSortDirection(actual, sortBy, SearchConstants.SortDirectionAscending);
            Assert.AreEqual<int>(3, actual.Results.CurrentPage, "Results page number was wrong.");
            var page3Values = new List<Feedback>(actual.Results.PageValues);
            CollectionAssert.AreNotEquivalent(page3Values, page2Values, "Page values didn't change");
        }
    }
}