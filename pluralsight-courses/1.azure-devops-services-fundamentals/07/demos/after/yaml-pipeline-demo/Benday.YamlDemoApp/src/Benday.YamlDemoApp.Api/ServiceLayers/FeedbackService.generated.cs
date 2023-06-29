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
    public partial class FeedbackService :
        CoreFieldsServiceLayerBase<Benday.YamlDemoApp.Api.DomainModels.Feedback>,
        IFeedbackService
    {
        private IFeedbackRepository _Repository;
        private FeedbackAdapter _Adapter;
        private IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.Feedback> _ValidatorInstance;
        private ISearchStringParserStrategy _SearchStringParser;
        
        public FeedbackService(
            IFeedbackRepository repository,
            IValidatorStrategy<Benday.YamlDemoApp.Api.DomainModels.Feedback> validator,
            IUsernameProvider usernameProvider, ISearchStringParserStrategy searchStringParser) :
            base(usernameProvider)
        {
            _Repository = repository;
            _ValidatorInstance = validator;
            _SearchStringParser = searchStringParser;
            
            _Adapter = new FeedbackAdapter();
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.Feedback> GetAll(
            int maxNumberOfResults = 100)
        {
            var entityResults = _Repository.GetAll(maxNumberOfResults);
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.Feedback>();
            
            _Adapter.Adapt(entityResults, returnValues);
            
            BeforeReturnFromGet(returnValues);
            
            return returnValues;
        }
        
        public Benday.YamlDemoApp.Api.DomainModels.Feedback GetById(int id)
        {
            var entityResults = _Repository.GetById(id);
            
            if (entityResults == null)
            {
                return null;
            }
            else
            {
                var returnValue = new Benday.YamlDemoApp.Api.DomainModels.Feedback();
                
                _Adapter.Adapt(entityResults, returnValue);
                
                BeforeReturnFromGet(returnValue);
                
                return returnValue;
            }
        }
        
        public void Save(Benday.YamlDemoApp.Api.DomainModels.Feedback saveThis)
        {
            if (saveThis == null)
            throw new ArgumentNullException("saveThis", "saveThis is null.");
            
            if (_ValidatorInstance.IsValid(saveThis) == false)
            {
                ApiUtilities.ThrowValidationException("Item is invalid.");
            }
            else
            {
                Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity toValue;
                
                if (saveThis.Id == 0)
                {
                    toValue = new Benday.YamlDemoApp.Api.DataAccess.Entities.FeedbackEntity();
                }
                else
                {
                    toValue = _Repository.GetById(saveThis.Id);
                    
                    if (toValue == null)
                    {
                        ApiUtilities.ThrowUnknownObjectException("Feedback", saveThis.Id);
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
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.Feedback> SimpleSearch(
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
            "FeedbackType", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Sentiment", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Subject", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "FeedbackText", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Username", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "FirstName", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "LastName", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Referer", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "UserAgent", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "IpAddress", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "Status", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "CreatedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            search.AddArgument(
            "LastModifiedBy", SearchMethod.Contains, searchValue, SearchOperator.Or);
            
            
        }
        
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
            
            if (string.IsNullOrWhiteSpace(searchValueFeedbackType) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "FeedbackType", searchTypeFeedbackType, searchValueFeedbackType);
            }
            if (string.IsNullOrWhiteSpace(searchValueSentiment) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Sentiment", searchTypeSentiment, searchValueSentiment);
            }
            if (string.IsNullOrWhiteSpace(searchValueSubject) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Subject", searchTypeSubject, searchValueSubject);
            }
            if (string.IsNullOrWhiteSpace(searchValueFeedbackText) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "FeedbackText", searchTypeFeedbackText, searchValueFeedbackText);
            }
            if (string.IsNullOrWhiteSpace(searchValueUsername) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Username", searchTypeUsername, searchValueUsername);
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
            if (string.IsNullOrWhiteSpace(searchValueReferer) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "Referer", searchTypeReferer, searchValueReferer);
            }
            if (string.IsNullOrWhiteSpace(searchValueUserAgent) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "UserAgent", searchTypeUserAgent, searchValueUserAgent);
            }
            if (string.IsNullOrWhiteSpace(searchValueIpAddress) == false)
            {
                foundOneNonNullSearchValue = true;
                search.AddArgument(
                "IpAddress", searchTypeIpAddress, searchValueIpAddress);
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
            
            
            
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.Feedback>();
            
            if (foundOneNonNullSearchValue == true)
            {
                search.MaxNumberOfResults = maxNumberOfResults;
                
                var results = _Repository.Search(search);
                var entityResults = results.Results;
                
                _Adapter.Adapt(entityResults, returnValues);
            }
            
            return returnValues;
        }
        
        public IList<Benday.YamlDemoApp.Api.DomainModels.Feedback> Search(Search search)
        {
            var returnValues = new List<Benday.YamlDemoApp.Api.DomainModels.Feedback>();
            
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