using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benday.YamlDemoApp.UnitTests.DomainModels
{
    [TestClass]
    public partial class FeedbackFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }
        
        private Feedback _systemUnderTest;
        public Feedback SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new Feedback();
                }
                
                return _systemUnderTest;
            }
        }
        
        [TestMethod]
        public void Feedback_VerifyDomainModelBaseOperations()
        {
            var instance = FeedbackTestUtility.CreateModel(false);
            
            instance.AcceptChanges();
            
            var tester = new DomainModelFieldTester<Feedback>(instance);
            
            tester.RunChangeTrackingTestsForValueTypeProperties();
        }
        
        
    }
}