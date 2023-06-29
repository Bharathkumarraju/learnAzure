using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.EfCore.SqlServer;
using Benday.Common;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public partial interface ILookupService : IServiceLayer<Lookup>
    {
        IList<Lookup> Search(
            SearchMethod searchTypeLookupType = SearchMethod.Contains,
            string searchValueLookupType = null,
            SearchMethod searchTypeLookupKey = SearchMethod.Contains,
            string searchValueLookupKey = null,
            SearchMethod searchTypeLookupValue = SearchMethod.Contains,
            string searchValueLookupValue = null,
            SearchMethod searchTypeStatus = SearchMethod.Contains,
            string searchValueStatus = null,
            SearchMethod searchTypeCreatedBy = SearchMethod.Contains,
            string searchValueCreatedBy = null,
            SearchMethod searchTypeLastModifiedBy = SearchMethod.Contains,
            string searchValueLastModifiedBy = null,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        IList<Benday.YamlDemoApp.Api.DomainModels.Lookup> SimpleSearch(
            string searchValue,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        IList<Lookup> GetAllByType(string lookupType);
    }
}