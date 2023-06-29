using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.EfCore.SqlServer;
using Benday.Common;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public partial interface ILogEntryService : IServiceLayer<LogEntry>
    {
        IList<LogEntry> Search(
            SearchMethod searchTypeCategory = SearchMethod.Contains,
            string searchValueCategory = null,
            SearchMethod searchTypeLogLevel = SearchMethod.Contains,
            string searchValueLogLevel = null,
            SearchMethod searchTypeLogText = SearchMethod.Contains,
            string searchValueLogText = null,
            SearchMethod searchTypeExceptionText = SearchMethod.Contains,
            string searchValueExceptionText = null,
            SearchMethod searchTypeEventId = SearchMethod.Contains,
            string searchValueEventId = null,
            SearchMethod searchTypeState = SearchMethod.Contains,
            string searchValueState = null,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        IList<Benday.YamlDemoApp.Api.DomainModels.LogEntry> SimpleSearch(
            string searchValue,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        
    }
}