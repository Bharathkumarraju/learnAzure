using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benday.YamlDemoApp.UnitTests.DomainModels
{
    [TestClass]
    public partial class UserClaimFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }
        
        private UserClaim _systemUnderTest;
        public UserClaim SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new UserClaim();
                }
                
                return _systemUnderTest;
            }
        }
        
        [TestMethod]
        public void UserClaim_VerifyDomainModelBaseOperations()
        {
            var instance = UserClaimTestUtility.CreateModel(false);
            
            instance.AcceptChanges();
            
            var tester = new DomainModelFieldTester<UserClaim>(instance);
            
            tester.RunChangeTrackingTestsForValueTypeProperties();
        }
        
        
    }
}