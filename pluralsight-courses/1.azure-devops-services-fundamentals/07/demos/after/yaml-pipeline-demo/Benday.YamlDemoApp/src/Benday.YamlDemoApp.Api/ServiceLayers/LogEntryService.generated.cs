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
    public partial class LogEntryService :
        ServiceLayerBase<Benday.YamlDemoApp.Api.DomainModels.LogEntry>,
        ILogEntryService
    {
        private ILogEntryRepository _Repository;
        private LogEntryAdapter _Adapter;
        private IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.LogEntry> _ValidatorInstance;
        private ISearchStringParserStrategy _SearchStringParser;
        
        public LogEntryService(
            ILogEntryRepository repository,
            IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.LogEntry> validator,
            IUsernameProvider usernameProvider, ISearchStringParserStrategy searchStringParser) :
            base(usernameProvider)
        {
            _Repository = repository;
            _ValidatorInstance = validator;
            _SearchStringParser = searchStringParser;
            
            _Adapter = new LogEntryAdapter();
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.LogEntry> GetAll(
            int maxNumberOfResults = 100)
        {
            var entityResults = _Repository.GetAll(maxNumberOfResults);
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.LogEntry>();
            
            _Adapter.Adapt(entityResults, returnValues);
            
            BeforeReturnFromGet(returnValues);
            
            return returnValues;
        }
        
        public Benday.YamlDemoApp.Api.DomainModels.LogEntry GetById(int id)
        {
            var entityResults = _Repository.GetById(id);
            
            if (entityResults == null)
            {
                return null;
            }
            else
            {
                var returnValue = new Benday.YamlDemoApp.Api.DomainModels.LogEntry();
                
                _Adapter.Adapt(entityResults, returnValue);
                
                BeforeReturnFromGet(returnValue);
                
                return returnValue;
            }
        }
        
        public void Save(Benday.YamlDemoApp.Api.DomainModels.LogEntry saveThis)
        {
            if (saveThis == null)
            throw new ArgumentNullException("saveThis", "saveThis is null.");
            
            if (_ValidatorInstance.IsValid(saveThis) == false)
            {
                ApiUtilities.ThrowValidationException("Item is invalid.");
            }
            else
            {
                Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity toValue;
                
                if (saveThis.Id == 0)
                {
                    toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.LogEntryEntity();
                }
                else
                {
                    toValue = _Repository.GetById(saveThis.Id);
                    
                    if (toValue == null)
                    {
                        ApiUtilities.ThrowUnknownObjectException("LogEntry", saveThis.Id);
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
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.LogEntry> SimpleSearch(
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
            "LogLevel", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "LogText", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "ExceptionText", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "EventId", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "State", SearchMethod.Contains, searchValue, SearchOperator.Or);
            
            
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.LogEntry> Search(
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
            if (string.IsNullOrWhiteSpace(searchValueLogLevel) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "LogLevel", searchTypeLogLevel, searchValueLogLevel);
            }
            if (string.IsNullOrWhiteSpace(searchValueLogText) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "LogText", searchTypeLogText, searchValueLogText);
            }
            if (string.IsNullOrWhiteSpace(searchValueExceptionText) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "ExceptionText", searchTypeExceptionText, searchValueExceptionText);
            }
            if (string.IsNullOrWhiteSpace(searchValueEventId) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "EventId", searchTypeEventId, searchValueEventId);
            }
            if (string.IsNullOrWhiteSpace(searchValueState) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "State", searchTypeState, searchValueState);
            }
            
            
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.LogEntry>();
            
            if (foundOneNonNullSearchValue == true)
            {
                search.MaxNumberOfResults = maxNumberOfResults;
                
                var results = _Repository.Search(search);
                var entityResults = results.Results;
                
                _Adapter.Adapt(entityResults, returnValues);
            }
            
            return returnValues;
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.LogEntry> Search(Search search)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.LogEntry>();
            
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