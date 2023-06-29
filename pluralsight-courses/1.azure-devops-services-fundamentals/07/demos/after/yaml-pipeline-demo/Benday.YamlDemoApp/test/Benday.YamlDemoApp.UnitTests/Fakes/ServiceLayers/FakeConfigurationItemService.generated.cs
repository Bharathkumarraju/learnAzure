using Benday.YamlDemoApp.Api.ServiceLayers;
using Benday.EfCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using Benday.Common;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers
{
    public partial class FakeConfigurationItemService :
        FakeServiceLayer<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>, IConfigurationItemService
    {
        public IList<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> SearchUsingParametersReturnValue { get; set; }
        public bool WasSearchUsingParametersCalled { get; set; }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> Search(
            SearchMethod searchTypeCategory = SearchMethod.Contains,
            string searchValueCategory = null,
            SearchMethod searchTypeConfigurationKey = SearchMethod.Contains,
            string searchValueConfigurationKey = null,
            SearchMethod searchTypeDescription = SearchMethod.Contains,
            string searchValueDescription = null,
            SearchMethod searchTypeConfigurationValue = SearchMethod.Contains,
            string searchValueConfigurationValue = null,
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
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> SimpleSearchReturnValue { get; set; }
        public bool WasSimpleSearchCalled { get; set; }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> SimpleSearch(
            string searchValue, string sortBy = null, string sortByDirection = null, int maxNumberOfResults = 100)
        {
            WasSimpleSearchCalled = true;
            
            return SimpleSearchReturnValue;
        }
        
        
    }
}