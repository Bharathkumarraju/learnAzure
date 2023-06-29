using Benday.YamlDemoApp.Api.ServiceLayers;
using Benday.EfCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using Benday.Common;
using Benday.YamlDemoApp.Api.DomainModels;

namespace Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers
{
    public partial class FakeFeedbackService :
        FakeServiceLayer<Benday.YamlDemoApp.Api.DomainModels.Feedback>, IFeedbackService
    {
        public IList<Benday.YamlDemoApp.Api.DomainModels.Feedback> SearchUsingParametersReturnValue { get; set; }
        public bool WasSearchUsingParametersCalled { get; set; }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.Feedback> Search(
            SearchMethod searchTypeFeedbackType = SearchMethod.Contains,
            string searchValueFeedbackType = null,
            SearchMethod searchTypeSentiment = SearchMethod.Contains,
            string searchValueSentiment = null,
            SearchMethod searchTypeSubject = SearchMethod.Contains,
            string searchValueSubject = null,
            SearchMethod searchTypeFeedbackText = SearchMethod.Contains,
            string searchValueFeedbackText = null,
            SearchMethod searchTypeUsername = SearchMethod.Contains,
            string searchValueUsername = null,
            SearchMethod searchTypeFirstName = SearchMethod.Contains,
            string searchValueFirstName = null,
            SearchMethod searchTypeLastName = SearchMethod.Contains,
            string searchValueLastName = null,
            SearchMethod searchTypeReferer = SearchMethod.Contains,
            string searchValueReferer = null,
            SearchMethod searchTypeUserAgent = SearchMethod.Contains,
            string searchValueUserAgent = null,
            SearchMethod searchTypeIpAddress = SearchMethod.Contains,
            string searchValueIpAddress = null,
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
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.Feedback> SimpleSearchReturnValue { get; set; }
        public bool WasSimpleSearchCalled { get; set; }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.Feedback> SimpleSearch(
            string searchValue, string sortBy = null, string sortByDirection = null, int maxNumberOfResults = 100)
        {
            WasSimpleSearchCalled = true;
            
            return SimpleSearchReturnValue;
        }
        
        
    }
}