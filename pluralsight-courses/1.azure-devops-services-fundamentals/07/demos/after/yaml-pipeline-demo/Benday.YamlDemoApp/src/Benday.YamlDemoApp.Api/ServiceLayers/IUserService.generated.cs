using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.EfCore.SqlServer;
using Benday.Common;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public partial interface IUserService : IServiceLayer<User>
    {
        IList<User> Search(
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
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        IList<Benday.YamlDemoApp.Api.DomainModels.User> SimpleSearch(
            string searchValue,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100);
        
        User GetByUsername(string username);
    }
}