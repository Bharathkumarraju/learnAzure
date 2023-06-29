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
    public class UserClaimAdapterFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }
        
        private UserClaimEditorViewModelAdapter _systemUnderTest;
        public UserClaimEditorViewModelAdapter SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new UserClaimEditorViewModelAdapter();
                }
                
                return _systemUnderTest;
            }
        }
        
        [TestMethod]
        public void AdaptUserClaimFromViewModelsToModels()
        {
            // arrange
            var fromValues = UserClaimViewModelTestUtility.CreateEditorViewModels();
            
            var allValuesCount = fromValues.Count;
            
            var toValues = new List<Benday.YamlDemoApp.Api.DomainModels.UserClaim>();
            
            // act
            SystemUnderTest.Adapt(fromValues, toValues);
            
            // assert
            Assert.AreEqual<int>(allValuesCount, toValues.Count, "Count was wrong.");
        }
        
        [TestMethod]
        public void AdaptUserClaimFromViewModelToModel()
        {
            // arrange
            var fromValue = UserClaimViewModelTestUtility.CreateEditorViewModel();
            var toValue = new Benday.YamlDemoApp.Api.DomainModels.UserClaim();
            
            // act
            SystemUnderTest.Adapt(fromValue, toValue);
            
            // assert
            UserClaimViewModelTestUtility.AssertAreEqual(fromValue, toValue);
        }
        
        [TestMethod]
        public void AdaptUserClaimFromModelToViewModel()
        {
            // arrange
            var fromValue = UserClaimTestUtility.CreateModel();
            var toValue = new Benday.YamlDemoApp.WebUi.Models.UserClaimEditorViewModel();
            
            // act
            SystemUnderTest.Adapt(fromValue, toValue);
            
            // assert
            UserClaimViewModelTestUtility.AssertAreEqual(fromValue, toValue);
        }
    }
}