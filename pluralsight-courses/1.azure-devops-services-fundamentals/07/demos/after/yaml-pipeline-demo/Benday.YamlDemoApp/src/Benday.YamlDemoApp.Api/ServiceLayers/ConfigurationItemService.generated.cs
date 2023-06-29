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
    public partial class ConfigurationItemService :
        CoreFieldsServiceLayerBase<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>,
        IConfigurationItemService
    {
        private IConfigurationItemRepository _Repository;
        private ConfigurationItemAdapter _Adapter;
        private IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> _ValidatorInstance;
        private ISearchStringParserStrategy _SearchStringParser;
        
        public ConfigurationItemService(
            IConfigurationItemRepository repository,
            IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> validator,
            IUsernameProvider usernameProvider, ISearchStringParserStrategy searchStringParser) :
            base(usernameProvider)
        {
            _Repository = repository;
            _ValidatorInstance = validator;
            _SearchStringParser = searchStringParser;
            
            _Adapter = new ConfigurationItemAdapter();
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> GetAll(
            int maxNumberOfResults = 100)
        {
            var entityResults = _Repository.GetAll(maxNumberOfResults);
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>();
            
            _Adapter.Adapt(entityResults, returnValues);
            
            BeforeReturnFromGet(returnValues);
            
            return returnValues;
        }
        
        public Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem GetById(int id)
        {
            var entityResults = _Repository.GetById(id);
            
            if (entityResults == null)
            {
                return null;
            }
            else
            {
                var returnValue = new Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem();
                
                _Adapter.Adapt(entityResults, returnValue);
                
                BeforeReturnFromGet(returnValue);
                
                return returnValue;
            }
        }
        
        public void Save(Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem saveThis)
        {
            if (saveThis == null)
            throw new ArgumentNullException("saveThis", "saveThis is null.");
            
            if (_ValidatorInstance.IsValid(saveThis) == false)
            {
                ApiUtilities.ThrowValidationException("Item is invalid.");
            }
            else
            {
                Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity toValue;
                
                if (saveThis.Id == 0)
                {
                    toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.ConfigurationItemEntity();
                }
                else
                {
                    toValue = _Repository.GetById(saveThis.Id);
                    
                    if (toValue == null)
                    {
                        ApiUtilities.ThrowUnknownObjectException("ConfigurationItem", saveThis.Id);
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
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> SimpleSearch(
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
            "Category", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "ConfigurationKey", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Description", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "ConfigurationValue", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Status", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "CreatedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "LastModifiedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            
            
        }
        
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
            
            if (string.IsNullOrWhiteSpace(searchValueCategory) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Category", searchTypeCategory, searchValueCategory);
            }
            if (string.IsNullOrWhiteSpace(searchValueConfigurationKey) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "ConfigurationKey", searchTypeConfigurationKey, searchValueConfigurationKey);
            }
            if (string.IsNullOrWhiteSpace(searchValueDescription) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Description", searchTypeDescription, searchValueDescription);
            }
            if (string.IsNullOrWhiteSpace(searchValueConfigurationValue) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "ConfigurationValue", searchTypeConfigurationValue, searchValueConfigurationValue);
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
            
            
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>();
            
            if (foundOneNonNullSearchValue == true)
            {
                search.MaxNumberOfResults = maxNumberOfResults;
                
                var results = _Repository.Search(search);
                var entityResults = results.Results;
                
                _Adapter.Adapt(entityResults, returnValues);
            }
            
            return returnValues;
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem> Search(Search search)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>();
            
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