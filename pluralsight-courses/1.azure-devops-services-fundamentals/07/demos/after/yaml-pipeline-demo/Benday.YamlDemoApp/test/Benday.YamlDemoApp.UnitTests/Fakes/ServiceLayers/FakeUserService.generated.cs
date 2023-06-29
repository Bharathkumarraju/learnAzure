using Benday.YamlDemoApp.Api.ServiceLayers;
using Benday.EfCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using Benday.Common;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers
{
    public partial class FakeUserService :
        FakeServiceLayer<Benday.YamlDemoApp.Api.DomainModels.User>, IUserService
    {
        public IList<Benday.YamlDemoApp.Api.DomainModels.User> SearchUsingParametersReturnValue { get; set; }
        public bool WasSearchUsingParametersCalled { get; set; }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.User> Search(
            SearchMethod searchTypeUsername = SearchMethod.Contains,
            string searchValueUsername = null,
            SearchMethod searchTypeSource = SearchMethod.Contains,
            string searchValueSource = null,
            SearchMethod searchTypeEmailAddress = SearchMethod.Contains,
            string searchValueEmailAddress = null,
            SearchMethod searchTypeFirstName = SearchMethod.Contains,
            string searchValueFirstName = null,
            SearchMethod searchTypeLastName = SearchMethod.Contains,
            string searchValueLastName = null,
            SearchMethod searchTypePhoneNumber = SearchMethod.Contains,
            string searchValuePhoneNumber = null,
            SearchMethod searchTypeStatus = SearchMethod.Contains,
            string searchValueStatus = null,
            SearchMethod searchTypeCreatedBy = SearchMethod.Contains,
            string searchValueCreatedBy = null,
            SearchMethod searchTypeLastModifiedBy = SearchMethod.Contains,
            string searchValueLastModifiedBy = null,
            
            
            string sortBy = null, string sortByDirection = null,
            int maxNumberOfResults = 100)
        {
            WasSearchUsingParametersCalled = true;
            
            return SearchUsingParametersReturnValue;
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.User> SimpleSearchReturnValue { get; set; }
        public bool WasSimpleSearchCalled { get; set; }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.User> SimpleSearch(
            string searchValue, string sortBy = null, string sortByDirection = null, int maxNumberOfResults = 100)
        {
            WasSimpleSearchCalled = true;
            
            return SimpleSearchReturnValue;
        }
        
        public User GetByUsernameReturnValue { get; set; }
        public bool WasGetByUsernameCalled { get; set; }
        
        public User GetByUsername(string username)
        {
            WasGetByUsernameCalled = true;
            return GetByUsernameReturnValue;
        }
    }
}