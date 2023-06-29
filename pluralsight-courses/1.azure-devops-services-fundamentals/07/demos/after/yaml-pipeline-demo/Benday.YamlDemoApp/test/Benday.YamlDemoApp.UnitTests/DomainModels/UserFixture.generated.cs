using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benday.YamlDemoApp.UnitTests.DomainModels
{
    [TestClass]
    public partial class UserFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }
        
        private User _systemUnderTest;
        public User SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new User();
                }
                
                return _systemUnderTest;
            }
        }
        
        [TestMethod]
        public void User_VerifyDomainModelBaseOperations()
        {
            var instance = UserTestUtility.CreateModel(false);
            
            instance.AcceptChanges();
            
            var tester = new DomainModelFieldTester<User>(instance);
            
            tester.RunChangeTrackingTestsForValueTypeProperties();
        }
        
        
    }
}