using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.EfCore.SqlServer;
using Benday.Common;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public partial interface IUserClaimService : IServiceLayer<UserClaim>
    {
        IList<UserClaim> Search(
            SearchMethod searchTypeUsername = SearchMethod.Contains,
            string searchValueUsername = null,
            SearchMethod searchTypeClaimName = SearchMethod.Contains,
            string searchValueClaimName = null,
            SearchMethod searchTypeClaimValue = SearchMethod.Contains,
            string searchValueClaimValue = null,
            SearchMethod searchTypeClaimLogicType = SearchMethod.Contains,
            string searchValueClaimLogicType = null,
            SearchMethod searchTypeStatus = SearchMethod.Contains,
            string searchValueStatus = null,
            SearchMethod searchTypeCreatedBy = SearchMethod.Contains,
            string searchValueCreatedBy = null,
            SearchMethod searchTypeLastModifiedBy = SearchMethod.Contains,
            string searchValueLastModifiedBy = null,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        IList<Benday.YamlDemoApp.Api.DomainModels.UserClaim> SimpleSearch(
            string searchValue,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        
    }
}