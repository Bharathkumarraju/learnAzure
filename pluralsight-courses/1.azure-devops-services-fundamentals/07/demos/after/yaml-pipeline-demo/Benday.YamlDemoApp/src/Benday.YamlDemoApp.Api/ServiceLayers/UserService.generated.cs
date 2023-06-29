using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Benday.YamlDemoApp.Api.Adapters;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.Api.DataAccess.Entities;
using Benday.YamlDemoApp.Api.DataAccess.SqlServer;
using Benday.EfCore.SqlServer;
using Benday.Common;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public partial class UserService :
        CoreFieldsServiceLayerBase<Benday.YamlDemoApp.Api.DomainModels.User>,
        IUserService
    {
        private IUserRepository _Repository;
        private UserAdapter _Adapter;
        private IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.User> _ValidatorInstance;
        private ISearchStringParserStrategy _SearchStringParser;
        
        public UserService(
            IUserRepository repository,
            IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.User> validator,
            IUsernameProvider usernameProvider, ISearchStringParserStrategy searchStringParser) :
            base(usernameProvider)
        {
            _Repository = repository;
            _ValidatorInstance = validator;
            _SearchStringParser = searchStringParser;
            
            _Adapter = new UserAdapter();
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.User> GetAll(
            int maxNumberOfResults = 100)
        {
            var entityResults = _Repository.GetAll(maxNumberOfResults);
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.User>();
            
            _Adapter.Adapt(entityResults, returnValues);
            
            BeforeReturnFromGet(returnValues);
            
            return returnValues;
        }
        
        public Benday.YamlDemoApp.Api.DomainModels.User GetById(int id)
        {
            var entityResults = _Repository.GetById(id);
            
            if (entityResults == null)
            {
                return null;
            }
            else
            {
                var returnValue = new Benday.YamlDemoApp.Api.DomainModels.User();
                
                _Adapter.Adapt(entityResults, returnValue);
                
                BeforeReturnFromGet(returnValue);
                
                return returnValue;
            }
        }
        
        public void Save(Benday.YamlDemoApp.Api.DomainModels.User saveThis)
        {
            if (saveThis == null)
            throw new ArgumentNullException("saveThis", "saveThis is null.");
            
            if (_ValidatorInstance.IsValid(saveThis) == false)
            {
                ApiUtilities.ThrowValidationException("Item is invalid.");
            }
            else
            {
                Benday.YamlDemoApp.Api.DataAccess.Entities.UserEntity toValue;
                
                if (saveThis.Id == 0)
                {
                    toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.UserEntity();
                }
                else
                {
                    toValue = _Repository.GetById(saveThis.Id);
                    
                    if (toValue == null)
                    {
                        ApiUtilities.ThrowUnknownObjectException("User", saveThis.Id);
                    }
                }
                
                PopulateAuditFieldsBeforeSave(saveThis);
                
                var modelClaimsValues = new List<CoreFieldsDomainModelBase>(saveThis.Claims);
                PopulateAuditFieldsBeforeSave(modelClaimsValues);
                
                
                _Adapter.Adapt(saveThis, toValue);
                
                _Repository.Save(toValue);
                
                PopulateFieldsFromEntityAfterSave(toValue, saveThis);
                
                var entityClaimsValues = new List<CoreFieldsEntityBase>(toValue.Claims);
                PopulateFieldsFromEntityAfterSave(entityClaimsValues, modelClaimsValues);
                
            }
        }
        
        public void DeleteById(int id)
        {
            var match = _Repository.GetById(id);
            
            if (match == null)
            {
                throw new InvalidOperationException(
                $"Could not locate an item with an id of '{id}'."
                );
            }
            else
            {
                _Repository.Delete(match);
            }
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.User> SimpleSearch(
            string searchValue,
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100)
        {
            Search search = GetSimpleSearch(searchValue, maxNumberOfResults);
            
            if (sortBy != null)
            {
                search.AddSort(sortBy, sortByDirection);
            }
            
            return Search(search);
        }
        
        private Search GetSimpleSearch(string searchValue, int maxNumberOfResults)
        {
            var search = new Search();
            
            search.MaxNumberOfResults = maxNumberOfResults;
            
            var searchTokens = _SearchStringParser.Parse(searchValue);
            
            foreach (var searchToken in searchTokens)
            {
                AddSimpleSearchForValue(search, searchToken);
            }
            
            return search;
        }
        
        private void AddSimpleSearchForValue(Search search, string searchValue)
        {
            search.AddArgument(
            "Username", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Source", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "EmailAddress", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "FirstName", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "LastName", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "PhoneNumber", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Status", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "CreatedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "LastModifiedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            
            
        }
        
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
            
            
            string sortBy = null,
            string sortByDirection = null,
            int maxNumberOfResults = 100)
        {
            var search = new Search();
            
            if (sortBy != null)
            {
                search.AddSort(sortBy, sortByDirection);
            }
            
            bool foundOneNonNullSearchValue = false;
            
            if (string.IsNullOrWhiteSpace(searchValueUsername) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Username", searchTypeUsername, searchValueUsername);
            }
            if (string.IsNullOrWhiteSpace(searchValueSource) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Source", searchTypeSource, searchValueSource);
            }
            if (string.IsNullOrWhiteSpace(searchValueEmailAddress) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "EmailAddress", searchTypeEmailAddress, searchValueEmailAddress);
            }
            if (string.IsNullOrWhiteSpace(searchValueFirstName) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "FirstName", searchTypeFirstName, searchValueFirstName);
            }
            if (string.IsNullOrWhiteSpace(searchValueLastName) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "LastName", searchTypeLastName, searchValueLastName);
            }
            if (string.IsNullOrWhiteSpace(searchValuePhoneNumber) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "PhoneNumber", searchTypePhoneNumber, searchValuePhoneNumber);
            }
            if (string.IsNullOrWhiteSpace(searchValueStatus) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Status", searchTypeStatus, searchValueStatus);
            }
            if (string.IsNullOrWhiteSpace(searchValueCreatedBy) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "CreatedBy", searchTypeCreatedBy, searchValueCreatedBy);
            }
            if (string.IsNullOrWhiteSpace(searchValueLastModifiedBy) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "LastModifiedBy", searchTypeLastModifiedBy, searchValueLastModifiedBy);
            }
            
            
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.User>();
            
            if (foundOneNonNullSearchValue == true)
            {
                search.MaxNumberOfResults = maxNumberOfResults;
                
                var results = _Repository.Search(search);
                var entityResults = results.Results;
                
                _Adapter.Adapt(entityResults, returnValues);
            }
            
            return returnValues;
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.User> Search(Search search)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.User>();
            
            if (search == null ||
            search.Arguments == null ||
            search.MaxNumberOfResults == 0)
            {
                return returnValues;
            }
            else
            {
                var results = _Repository.Search(search);
                var entityResults = results.Results;
                
                _Adapter.Adapt(entityResults, returnValues);
            }
            
            return returnValues;
        }
        
        public User GetByUsername(string username)
        {
            var result = this.Search(searchValueUsername: username).FirstOrDefault();
            
            if (result != null)
            {
                AddClaimsFromGlobalUser(result);
            }
            
            return result;
        }
        
        private void AddClaimsFromGlobalUser(User user)
        {
            var globalUser = this.Search(
            searchValueUsername: ApiConstants.Username_GlobalUser).FirstOrDefault();
            
            if (globalUser != null && globalUser.Claims != null &&
            globalUser.Claims.Count != 0)
            {
                foreach (var item in globalUser.Claims)
                {
                    user.Claims.Add(item);
                }
            }
        }
    }
}