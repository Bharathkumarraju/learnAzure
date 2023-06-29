using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.Api.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Benday.YamlDemoApp.Api.ServiceLayers;
using Benday.YamlDemoApp.UnitTests.Fakes;
using Benday.YamlDemoApp.UnitTests.Fakes.Repositories;
using Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Benday.YamlDemoApp.Api;
using System.Threading.Tasks;
using Benday.YamlDemoApp.UnitTests.Fakes.Validation;
using Benday.YamlDemoApp.UnitTests.Fakes.Security;

namespace Benday.YamlDemoApp.UnitTests.ServiceLayers
{
    [TestClass]
    public partial class FeedbackServiceFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
            _FeedbackRepositoryInstance = null;
            _ValidatorStrategy = null;
            _UsernameProvider = null;
        }
        
        private FeedbackService _systemUnderTest;
        
        private FeedbackService SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new FeedbackService(
                    FeedbackRepositoryInstance,
                    ValidatorStrategy,
                    UsernameProvider,
                    new DefaultSearchStringParserStrategy());
                }
                
                return _systemUnderTest;
            }
        }
        
        private InMemoryFeedbackRepository _FeedbackRepositoryInstance;
        public InMemoryFeedbackRepository FeedbackRepositoryInstance
        {
            get
            {
                if (_FeedbackRepositoryInstance == null)
                {
                    _FeedbackRepositoryInstance = new InMemoryFeedbackRepository();
                }
                
                return _FeedbackRepositoryInstance;
            }
        }
        
        private FakeValidatorStrategy<Feedback> _ValidatorStrategy;
        public FakeValidatorStrategy<Feedback> ValidatorStrategy
        {
            get
            {
                if (_ValidatorStrategy == null)
                {
                    _ValidatorStrategy = new FakeValidatorStrategy<Feedback>();
                }
                
                return _ValidatorStrategy;
            }
        }
        
        private FakeUsernameProvider _UsernameProvider;
        public FakeUsernameProvider UsernameProvider
        {
            get
            {
                if (_UsernameProvider == null)
                {
                    _UsernameProvider = new FakeUsernameProvider();
                    
                    _UsernameProvider.GetUsernameReturnValue =
                    UnitTestConstants.FakeUsername1;
                }
                
                return _UsernameProvider;
            }
        }
        
        private void PopulateRepositoryWithTestData()
        {
            var items = FeedbackTestUtility.CreateEntities();
            
            foreach (var item in items)
            {
                FeedbackRepositoryInstance.Save(item);
            }
        }
        
        [TestMethod]
        public void GetAll_WithDataInRepository_ReturnsData()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var expectedCount = FeedbackRepositoryInstance.Items.Count();
            
            // act
            var actual = SystemUnderTest.GetAll();
            
            // assert
            Assert.IsTrue(FeedbackRepositoryInstance.WasGetAllCalled, "Repository's GetAll() method was not called.");
            Assert.IsNotNull(actual);
            Assert.AreEqual<int>(expectedCount, actual.Count, "Wrong number of items returned.");
        }
        
        [TestMethod]
        public void GetAll_WithNoDataInRepository_ReturnsEmptyCollection()
        {
            // arrange
            var expectedCount = 0;
            
            // act
            var actual = SystemUnderTest.GetAll();
            
            // assert
            Assert.IsTrue(FeedbackRepositoryInstance.WasGetAllCalled, "Repository's GetAll() method was not called.");
            Assert.IsNotNull(actual);
            Assert.AreEqual<int>(expectedCount, actual.Count, "Wrong number of items returned.");
        }
        
        [TestMethod]
        public void GetById_ForKnownId_ReturnsResult()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var expected = FeedbackRepositoryInstance.Items[2];
            
            // act
            var actual = SystemUnderTest.GetById(expected.Id);
            
            // assert
            Assert.IsTrue(FeedbackRepositoryInstance.WasGetByIdCalled, "Repository's GetById() method was not called.");
            Assert.IsNotNull(actual);
        }
        
        [TestMethod]
        public void GetById_ForUnknownId_ReturnsNull()
        {
            // arrange
            
            // act
            var actual = SystemUnderTest.GetById(1234);
            
            // assert
            Assert.IsTrue(FeedbackRepositoryInstance.WasGetByIdCalled, "Repository's GetById() method was not called.");
            Assert.IsNull(actual);
        }
        
        [TestMethod]
        public void Save_NewValidItem_IdIsPopulatedAndIsInRepository()
        {
            // arrange
            var saveThis = FeedbackTestUtility.CreateModel(true);
            ValidatorStrategy.IsValidReturnValue = true;
            
            // act
            SystemUnderTest.Save(saveThis);
            
            // assert
            Assert.AreNotEqual<int>(0, saveThis.Id, "Id should not be zero after save.");
            Assert.IsTrue(FeedbackRepositoryInstance.WasSaveCalled, "Save was not called.");
            var actual = FeedbackRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(actual, "Item wasn't saved to repository.");
            
            var entity = FeedbackRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(entity, "Entity wasn't saved to repository.");
            FeedbackTestUtility.AssertAreEqual(saveThis, entity);
        }
        
        [TestMethod]
        public void Save_NewValidItem_CreatedAndLastUpdatedFieldsArePopulated()
        {
            // arrange
            var saveThis = FeedbackTestUtility.CreateModel(true);
            ValidatorStrategy.IsValidReturnValue = true;
            Assert.IsFalse(
            string.IsNullOrWhiteSpace(UsernameProvider.GetUsernameReturnValue),
            "Username provider was not initialized properly.");
            
            // act
            SystemUnderTest.Save(saveThis);
            
            // assert
            UnitTestUtility.AssertDomainModelBaseAuditFieldsArePopulated(
            saveThis, UsernameProvider.GetUsernameReturnValue, "saveThis");
            
            
            
            var entity = FeedbackRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(entity, "Entity wasn't saved to repository.");
            FeedbackTestUtility.AssertAreEqual(saveThis, entity);
        }
        
        [TestMethod]
        public void Save_ModifiedValidItem_CreatedAndLastUpdatedFieldsArePopulated()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var savedEntity = FeedbackRepositoryInstance.Items[2];
            var saveThis = SystemUnderTest.GetById(savedEntity.Id);
            Assert.IsNotNull(saveThis, "Item to modify is null");
            Assert.AreNotEqual<int>(0, saveThis.Id,
            "Item to modify has an Id of 0 indicating that it was not saved.");
            var expectedId = saveThis.Id;
            ValidatorStrategy.IsValidReturnValue = true;
            
            FeedbackTestUtility.ModifyModel(saveThis);
            
            FeedbackRepositoryInstance.ResetMethodCallTrackers();
            
            UsernameProvider.GetUsernameReturnValue = UnitTestConstants.FakeUsername2;
            
            var originalCreatedBy = saveThis.CreatedBy;
            var originalLastModifiedBy = saveThis.LastModifiedBy;
            
            var originalCreatedDate = saveThis.CreatedDate;
            var originalLastModified = saveThis.LastModifiedDate;
            
            UnitTestUtility.Pause(UnitTestConstants.NumberOfMillisecondsForRecentDateTimeAssert * 2);
            
            // act
            SystemUnderTest.Save(saveThis);
            
            // assert
            Assert.AreEqual<string>(originalCreatedBy,
            saveThis.CreatedBy,
            "CreatedBy was wrong.");
            
            Assert.AreEqual<DateTime>(originalCreatedDate, saveThis.CreatedDate,
            "CreatedDate was wrong.");
            
            Assert.AreEqual<string>(UnitTestConstants.FakeUsername2,
            saveThis.LastModifiedBy,
            "LastModifiedBy was wrong.");
            
            UnitTestUtility.AssertDateTimeIsRecent(
            saveThis.LastModifiedDate, "LastModifiedDate");
            
            var entity = FeedbackRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(entity, "Entity wasn't saved to repository.");
            FeedbackTestUtility.AssertAreEqual(saveThis, entity);
        }
        
        [TestMethod]
        public void Save_NewInvalidItem_DoesNotGetSavedAndThrowsValidationException()
        {
            // arrange
            var saveThis = FeedbackTestUtility.CreateModel(true);
            ValidatorStrategy.IsValidReturnValue = false;
            bool gotException = false;
            
            // act
            try
            {
                SystemUnderTest.Save(saveThis);
            }
            catch (InvalidObjectException)
            {
                gotException = true;
            }
            catch (Exception ex)
            {
                Assert.Fail($"Got wrong kind of exception. {ex}");
            }
            
            // assert
            Assert.IsTrue(gotException, "Did not get an invalid object exception.");
            Assert.IsFalse(FeedbackRepositoryInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        public void Save_ModifiedValidItem_SavesChangesToRepository()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var savedEntity = FeedbackRepositoryInstance.Items[2];
            var saveThis = SystemUnderTest.GetById(savedEntity.Id);
            Assert.IsNotNull(saveThis, "Item to modify is null");
            Assert.AreNotEqual<int>(0, saveThis.Id,
            "Item to modify has an Id of 0 indicating that it was not saved.");
            var expectedId = saveThis.Id;
            ValidatorStrategy.IsValidReturnValue = true;
            
            FeedbackTestUtility.ModifyModel(saveThis);
            
            FeedbackRepositoryInstance.ResetMethodCallTrackers();
            
            // act
            SystemUnderTest.Save(saveThis);
            
            // assert
            Assert.AreEqual<int>(expectedId, saveThis.Id,
            "Id should not change when modified.");
            Assert.IsTrue(FeedbackRepositoryInstance.WasSaveCalled, "Save was not called.");
            
            var entity = FeedbackRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(entity, "Entity wasn't saved to repository.");
            FeedbackTestUtility.AssertAreEqual(saveThis, entity);
        }
        
        [TestMethod]
        public void Save_ModifiedInvalidItem_DoesNotGetSavedAndThrowsValidationException()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var savedEntity = FeedbackRepositoryInstance.Items[2];
            var saveThis = SystemUnderTest.GetById(savedEntity.Id);
            Assert.IsNotNull(saveThis, "Item to modify is null");
            Assert.AreNotEqual<int>(0, saveThis.Id,
            "Item to modify has an Id of 0 indicating that it was not saved.");
            var expectedId = saveThis.Id;
            
            ValidatorStrategy.IsValidReturnValue = false;
            bool gotException = false;
            
            FeedbackTestUtility.ModifyModel(saveThis);
            
            FeedbackRepositoryInstance.ResetMethodCallTrackers();
            
            // act
            try
            {
                SystemUnderTest.Save(saveThis);
            }
            catch (InvalidObjectException)
            {
                gotException = true;
            }
            catch (Exception ex)
            {
                Assert.Fail($"Got wrong kind of exception. {ex}");
            }
            
            // assert
            Assert.IsTrue(gotException, "Did not get an invalid object exception.");
            Assert.AreNotEqual<int>(0, saveThis.Id, "Id should not be zero after save.");
            Assert.IsFalse(FeedbackRepositoryInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        [ExpectedException(typeof(UnknownObjectException))]
        public void Save_ExistingItemThatNoLongerIsInTheDatabase_DoesNotGetSavedAndThrowsException()
        {
            // arrange
            var idValueThatIsNotAlreadySavedToTheRepository = 1312341234;
            var saveThis = FeedbackTestUtility.CreateModel();
            saveThis.Id = idValueThatIsNotAlreadySavedToTheRepository;
            ValidatorStrategy.IsValidReturnValue = true;
            
            // act
            SystemUnderTest.Save(saveThis);
            
            // assert
        }
        
        [TestMethod]
        public void Delete_RemovesItemFromRepository()
        {
            // arrange
            
            PopulateRepositoryWithTestData();
            var savedEntity = FeedbackRepositoryInstance.Items[2];
            var saveThis = SystemUnderTest.GetById(savedEntity.Id);
            Assert.IsNotNull(saveThis, "Item to modify is null");
            Assert.AreNotEqual<int>(0, saveThis.Id,
            "Item to modify has an Id of 0 indicating that it was not saved.");
            var expectedId = saveThis.Id;
            
            // act
            SystemUnderTest.DeleteById(saveThis.Id);
            
            // assert
            Assert.IsTrue(FeedbackRepositoryInstance.WasDeleteByIdCalled, "DeleteById was not called.");
            
            var entity = FeedbackRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNull(entity, "Entity should have been deleted from repository.");
        }
    }
}