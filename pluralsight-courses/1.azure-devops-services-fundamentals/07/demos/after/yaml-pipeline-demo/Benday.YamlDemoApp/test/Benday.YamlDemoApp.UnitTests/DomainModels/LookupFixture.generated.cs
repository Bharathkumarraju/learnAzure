using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.UnitTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benday.YamlDemoApp.UnitTests.DomainModels
{
    [TestClass]
    public partial class LookupFixture
    {
        [TestInitialize]
        public void OnTestInitialize()
        {
            _systemUnderTest = null;
        }
        
        private Lookup _systemUnderTest;
        public Lookup SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = new Lookup();
                }
                
                return _systemUnderTest;
            }
        }
        
        [TestMethod]
        public void Lookup_VerifyDomainModelBaseOperations()
        {
            var instance = LookupTestUtility.CreateModel(false);
            
            instance.AcceptChanges();
            
            var tester = new DomainModelFieldTester<Lookup>(instance);
            
            tester.RunChangeTrackingTestsForValueTypeProperties();
        }
        
        
    }
}