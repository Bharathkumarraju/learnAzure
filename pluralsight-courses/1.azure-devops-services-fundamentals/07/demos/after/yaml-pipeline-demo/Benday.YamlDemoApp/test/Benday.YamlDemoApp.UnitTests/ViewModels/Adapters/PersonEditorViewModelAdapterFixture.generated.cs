using Benday.YamlDemoApp.Api.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Benday.YamlDemoApp.WebUi.Models;
using Benday.YamlDemoApp.WebUi.Models.Adapters;

namespace Benday.YamlDemoApp.UnitTests.ViewModels.Adapters
{
    [TestClass]
    public class PersonAdapterFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }
        
        private PersonEditorViewModelAdapter _systemUnderTest;
        public PersonEditorViewModelAdapter SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new PersonEditorViewModelAdapter();
                }
                
                return _systemUnderTest;
            }
        }
        
        [TestMethod]
        public void AdaptPersonFromViewModelsToModels()
        {
            // arrange
            var fromValues = PersonViewModelTestUtility.CreateEditorViewModels();
            
            var allValuesCount = fromValues.Count;
            
            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.Person>();
            
            // act
            SystemUnderTest.Adapt(fromValues, toValues);
            
            // assert
            Assert.AreEqual<int>(allValuesCount, toValues.Count, "Count was wrong.");
        }
        
        [TestMethod]
        public void AdaptPersonFromViewModelToModel()
        {
            // arrange
            var fromValue = PersonViewModelTestUtility.CreateEditorViewModel();
            var toValue = new Benday.YamlDemoApp.Api.DomainModels.Person();
            
            // act
            SystemUnderTest.Adapt(fromValue, toValue);
            
            // assert
            PersonViewModelTestUtility.AssertAreEqual(fromValue, toValue);
        }
        
        [TestMethod]
        public void AdaptPersonFromModelToViewModel()
        {
            // arrange
            var fromValue = PersonTestUtility.CreateModel();
            var toValue = new Benday.YamlDemoApp.WebUi.Models.PersonEditorViewModel();
            
            // act
            SystemUnderTest.Adapt(fromValue, toValue);
            
            // assert
            PersonViewModelTestUtility.AssertAreEqual(fromValue, toValue);
        }
    }
}