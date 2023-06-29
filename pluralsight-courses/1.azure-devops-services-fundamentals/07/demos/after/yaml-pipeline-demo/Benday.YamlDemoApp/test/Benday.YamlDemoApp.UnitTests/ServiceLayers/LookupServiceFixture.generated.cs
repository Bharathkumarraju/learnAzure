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
    public partial class LookupServiceFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
            _LookupRepositoryInstance = null;
            _ValidatorStrategy = null;
            _UsernameProvider = null;
        }
        
        private LookupService _systemUnderTest;
        
        private LookupService SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest =
                    new LookupService(
                    LookupRepositoryInstance,
                    ValidatorStrategy,
                    UsernameProvider,
                    new DefaultSearchStringParserStrategy());
                }
                
                return _systemUnderTest;
            }
        }
        
        private InMemoryLookupRepository _LookupRepositoryInstance;
        public InMemoryLookupRepository LookupRepositoryInstance
        {
            get
            {
                if (_LookupRepositoryInstance == null)
                {
                    _LookupRepositoryInstance = new InMemoryLookupRepository();
                }
                
                return _LookupRepositoryInstance;
            }
        }
        
        private FakeValidatorStrategy<Lookup> _ValidatorStrategy;
        public FakeValidatorStrategy<Lookup> ValidatorStrategy
        {
            get
            {
                if (_ValidatorStrategy == null)
                {
                    _ValidatorStrategy = new FakeValidatorStrategy<Lookup>();
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
            var items = LookupTestUtility.CreateEntities();
            
            foreach (var item in items)
            {
                LookupRepositoryInstance.Save(item);
            }
        }
        
        [TestMethod]
        public void GetAll_WithDataInRepository_ReturnsData()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var expectedCount = LookupRepositoryInstance.Items.Count();
            
            // act
            var actual = SystemUnderTest.GetAll();
            
            // assert
            Assert.IsTrue(LookupRepositoryInstance.WasGetAllCalled, "Repository's GetAll() method was not called.");
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
            Assert.IsTrue(LookupRepositoryInstance.WasGetAllCalled, "Repository's GetAll() method was not called.");
            Assert.IsNotNull(actual);
            Assert.AreEqual<int>(expectedCount, actual.Count, "Wrong number of items returned.");
        }
        
        [TestMethod]
        public void GetById_ForKnownId_ReturnsResult()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var expected = LookupRepositoryInstance.Items[2];
            
            // act
            var actual = SystemUnderTest.GetById(expected.Id);
            
            // assert
            Assert.IsTrue(LookupRepositoryInstance.WasGetByIdCalled, "Repository's GetById() method was not called.");
            Assert.IsNotNull(actual);
        }
        
        [TestMethod]
        public void GetById_ForUnknownId_ReturnsNull()
        {
            // arrange
            
            // act
            var actual = SystemUnderTest.GetById(1234);
            
            // assert
            Assert.IsTrue(LookupRepositoryInstance.WasGetByIdCalled, "Repository's GetById() method was not called.");
            Assert.IsNull(actual);
        }
        
        [TestMethod]
        public void Save_NewValidItem_IdIsPopulatedAndIsInRepository()
        {
            // arrange
            var saveThis = LookupTestUtility.CreateModel(true);
            ValidatorStrategy.IsValidReturnValue = true;
            
            // act
            SystemUnderTest.Save(saveThis);
            
            // assert
            Assert.AreNotEqual<int>(0, saveThis.Id, "Id should not be zero after save.");
            Assert.IsTrue(LookupRepositoryInstance.WasSaveCalled, "Save was not called.");
            var actual = LookupRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(actual, "Item wasn't saved to repository.");
            
            var entity = LookupRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(entity, "Entity wasn't saved to repository.");
            LookupTestUtility.AssertAreEqual(saveThis, entity);
        }
        
        [TestMethod]
        public void Save_NewValidItem_CreatedAndLastUpdatedFieldsArePopulated()
        {
            // arrange
            var saveThis = LookupTestUtility.CreateModel(true);
            ValidatorStrategy.IsValidReturnValue = true;
            Assert.IsFalse(
            string.IsNullOrWhiteSpace(UsernameProvider.GetUsernameReturnValue),
            "Username provider was not initialized properly.");
            
            // act
            SystemUnderTest.Save(saveThis);
            
            // assert
            UnitTestUtility.AssertDomainModelBaseAuditFieldsArePopulated(
            saveThis, UsernameProvider.GetUsernameReturnValue, "saveThis");
            
            
            
            var entity = LookupRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(entity, "Entity wasn't saved to repository.");
            LookupTestUtility.AssertAreEqual(saveThis, entity);
        }
        
        [TestMethod]
        public void Save_ModifiedValidItem_CreatedAndLastUpdatedFieldsArePopulated()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var savedEntity = LookupRepositoryInstance.Items[2];
            var saveThis = SystemUnderTest.GetById(savedEntity.Id);
            Assert.IsNotNull(saveThis, "Item to modify is null");
            Assert.AreNotEqual<int>(0, saveThis.Id,
            "Item to modify has an Id of 0 indicating that it was not saved.");
            var expectedId = saveThis.Id;
            ValidatorStrategy.IsValidReturnValue = true;
            
            LookupTestUtility.ModifyModel(saveThis);
            
            LookupRepositoryInstance.ResetMethodCallTrackers();
            
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
            
            var entity = LookupRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(entity, "Entity wasn't saved to repository.");
            LookupTestUtility.AssertAreEqual(saveThis, entity);
        }
        
        [TestMethod]
        public void Save_NewInvalidItem_DoesNotGetSavedAndThrowsValidationException()
        {
            // arrange
            var saveThis = LookupTestUtility.CreateModel(true);
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
            Assert.IsFalse(LookupRepositoryInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        public void Save_ModifiedValidItem_SavesChangesToRepository()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var savedEntity = LookupRepositoryInstance.Items[2];
            var saveThis = SystemUnderTest.GetById(savedEntity.Id);
            Assert.IsNotNull(saveThis, "Item to modify is null");
            Assert.AreNotEqual<int>(0, saveThis.Id,
            "Item to modify has an Id of 0 indicating that it was not saved.");
            var expectedId = saveThis.Id;
            ValidatorStrategy.IsValidReturnValue = true;
            
            LookupTestUtility.ModifyModel(saveThis);
            
            LookupRepositoryInstance.ResetMethodCallTrackers();
            
            // act
            SystemUnderTest.Save(saveThis);
            
            // assert
            Assert.AreEqual<int>(expectedId, saveThis.Id,
            "Id should not change when modified.");
            Assert.IsTrue(LookupRepositoryInstance.WasSaveCalled, "Save was not called.");
            
            var entity = LookupRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNotNull(entity, "Entity wasn't saved to repository.");
            LookupTestUtility.AssertAreEqual(saveThis, entity);
        }
        
        [TestMethod]
        public void Save_ModifiedInvalidItem_DoesNotGetSavedAndThrowsValidationException()
        {
            // arrange
            PopulateRepositoryWithTestData();
            var savedEntity = LookupRepositoryInstance.Items[2];
            var saveThis = SystemUnderTest.GetById(savedEntity.Id);
            Assert.IsNotNull(saveThis, "Item to modify is null");
            Assert.AreNotEqual<int>(0, saveThis.Id,
            "Item to modify has an Id of 0 indicating that it was not saved.");
            var expectedId = saveThis.Id;
            
            ValidatorStrategy.IsValidReturnValue = false;
            bool gotException = false;
            
            LookupTestUtility.ModifyModel(saveThis);
            
            LookupRepositoryInstance.ResetMethodCallTrackers();
            
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
            Assert.IsFalse(LookupRepositoryInstance.WasSaveCalled, "Save should not be called.");
        }
        
        [TestMethod]
        [ExpectedException(typeof(UnknownObjectException))]
        public void Save_ExistingItemThatNoLongerIsInTheDatabase_DoesNotGetSavedAndThrowsException()
        {
            // arrange
            var idValueThatIsNotAlreadySavedToTheRepository = 1312341234;
            var saveThis = LookupTestUtility.CreateModel();
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
            var savedEntity = LookupRepositoryInstance.Items[2];
            var saveThis = SystemUnderTest.GetById(savedEntity.Id);
            Assert.IsNotNull(saveThis, "Item to modify is null");
            Assert.AreNotEqual<int>(0, saveThis.Id,
            "Item to modify has an Id of 0 indicating that it was not saved.");
            var expectedId = saveThis.Id;
            
            // act
            SystemUnderTest.DeleteById(saveThis.Id);
            
            // assert
            Assert.IsTrue(LookupRepositoryInstance.WasDeleteByIdCalled, "DeleteById was not called.");
            
            var entity = LookupRepositoryInstance.GetById(saveThis.Id);
            Assert.IsNull(entity, "Entity should have been deleted from repository.");
        }
    }
}