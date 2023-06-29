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
    public partial class UserClaimService :
        CoreFieldsServiceLayerBase<Benday.YamlDemoApp.Api.DomainModels.UserClaim>,
        IUserClaimService
    {
        private IUserClaimRepository _Repository;
        private UserClaimAdapter _Adapter;
        private IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.UserClaim> _ValidatorInstance;
        private ISearchStringParserStrategy _SearchStringParser;
        
        public UserClaimService(
            IUserClaimRepository repository,
            IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.UserClaim> validator,
            IUsernameProvider usernameProvider, ISearchStringParserStrategy searchStringParser) :
            base(usernameProvider)
        {
            _Repository = repository;
            _ValidatorInstance = validator;
            _SearchStringParser = searchStringParser;
            
            _Adapter = new UserClaimAdapter();
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.UserClaim> GetAll(
            int maxNumberOfResults = 100)
        {
            var entityResults = _Repository.GetAll(maxNumberOfResults);
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.UserClaim>();
            
            _Adapter.Adapt(entityResults, returnValues);
            
            BeforeReturnFromGet(returnValues);
            
            return returnValues;
        }
        
        public Benday.YamlDemoApp.Api.DomainModels.UserClaim GetById(int id)
        {
            var entityResults = _Repository.GetById(id);
            
            if (entityResults == null)
            {
                return null;
            }
            else
            {
                var returnValue = new Benday.YamlDemoApp.Api.DomainModels.UserClaim();
                
                _Adapter.Adapt(entityResults, returnValue);
                
                BeforeReturnFromGet(returnValue);
                
                return returnValue;
            }
        }
        
        public void Save(Benday.YamlDemoApp.Api.DomainModels.UserClaim saveThis)
        {
            if (saveThis == null)
            throw new ArgumentNullException("saveThis", "saveThis is null.");
            
            if (_ValidatorInstance.IsValid(saveThis) == false)
            {
                ApiUtilities.ThrowValidationException("Item is invalid.");
            }
            else
            {
                Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity toValue;
                
                if (saveThis.Id == 0)
                {
                    toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.UserClaimEntity();
                }
                else
                {
                    toValue = _Repository.GetById(saveThis.Id);
                    
                    if (toValue == null)
                    {
                        ApiUtilities.ThrowUnknownObjectException("UserClaim", saveThis.Id);
                    }
                }
                
                PopulateAuditFieldsBeforeSave(saveThis);
                
                
                
                _Adapter.Adapt(saveThis, toValue);
                
                _Repository.Save(toValue);
                
                PopulateFieldsFromEntityAfterSave(toValue, saveThis);
                
                
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
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.UserClaim> SimpleSearch(
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
            "ClaimName", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "ClaimLogicType", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Status", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "CreatedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "LastModifiedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            
            
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.UserClaim> Search(
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
            if (string.IsNullOrWhiteSpace(searchValueClaimName) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "ClaimName", searchTypeClaimName, searchValueClaimName);
            }
            if (string.IsNullOrWhiteSpace(searchValueClaimValue) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "ClaimValue", searchTypeClaimValue, searchValueClaimValue);
            }
            if (string.IsNullOrWhiteSpace(searchValueClaimLogicType) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "ClaimLogicType", searchTypeClaimLogicType, searchValueClaimLogicType);
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
            
            
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.UserClaim>();
            
            if (foundOneNonNullSearchValue == true)
            {
                search.MaxNumberOfResults = maxNumberOfResults;
                
                var results = _Repository.Search(search);
                var entityResults = results.Results;
                
                _Adapter.Adapt(entityResults, returnValues);
            }
            
            return returnValues;
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.UserClaim> Search(Search search)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.UserClaim>();
            
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
        
        
    }
}