using Benday.YamlDemoApp.Api;
using Benday.YamlDemoApp.Api.ServiceLayers;
using Benday.YamlDemoApp.Api.DomainModels;
using Benday.YamlDemoApp.WebUi.Models;
using Benday.YamlDemoApp.WebUi.Models.Adapters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Benday.Common;
using Microsoft.AspNetCore.Authorization;

namespace Benday.YamlDemoApp.WebUi.Controllers
{
    [Authorize]
    public partial class FeedbackController : MvcControllerBase<FeedbackEditorViewModel>
    {
        private readonly IValidatorStrategy<FeedbackEditorViewModel> _Validator;
        private readonly ILookupService _LookupService;
        
        
        
        private readonly IFeedbackService _Service;
        private readonly ILogger<FeedbackController> _Logger;
        
        public FeedbackController(
            IFeedbackService service,
            IValidatorStrategy<FeedbackEditorViewModel> validator,
            ILogger<FeedbackController> logger,
            ILookupService lookupService)
        {
            if (service == null)
            throw new ArgumentNullException(nameof(service), "service is null.");
            
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator), "Argument cannot be null.");
            }
            
            _Validator = validator;
            _Logger = logger;
            _Service = service;
            _LookupService = lookupService;
            
            
            
        }
        
        public ActionResult Index()
        {
            var items = _Service.GetAll();
            
            return View(items);
        }
        
        [Route("/[controller]/[action]/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null || id.HasValue == false)
            {
                return new BadRequestResult();
            }
            
            var item = _Service.GetById(id.Value);
            
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                
            }
            
            return View(item);
        }
        
        public ActionResult Create()
        {
            return RedirectToAction("Edit", new { id = ApiConstants.UnsavedId });
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            
            Feedback item;
            FeedbackEditorViewModel viewModel;
            
            if (id.Value == ApiConstants.UnsavedId)
            {
                // create new
                viewModel = new FeedbackEditorViewModel();
                PopulateLookups(viewModel);
                return View(viewModel);
            }
            else
            {
                item = _Service.GetById(id.Value);
                
                if (item == null)
                {
                    return NotFound();
                }
                else
                {
                    viewModel = new FeedbackEditorViewModel();
                    
                    var adapter = new FeedbackEditorViewModelAdapter();
                    
                    adapter.Adapt(item, viewModel);
                    PopulateLookups(viewModel);
                }
            }
            
            BeforeReturnFromEdit(id, viewModel);
            
            return View(viewModel);
        }
        private void PopulateLookups(FeedbackEditorViewModel viewModel)
        {
            viewModel.FeedbackTypes = WebUiUtilities.ToSelectListItems(
            _LookupService.GetAllByType("System.Feedback.FeedbackType"));
            viewModel.Sentiments = WebUiUtilities.ToSelectListItems(
            _LookupService.GetAllByType("System.Feedback.SentimentType"));
            viewModel.Statuses = WebUiUtilities.ToSelectListItems(
            _LookupService.GetAllByType("System.Lookup.StatusValues"));
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FeedbackEditorViewModel item)
        {
            if (_Validator.IsValid(item) == true)
            {
                Feedback toValue;
                
                if (item.Id == ApiConstants.UnsavedId)
                {
                    toValue = new Feedback();
                }
                else
                {
                    toValue =
                    _Service.GetById(item.Id);
                    
                    if (toValue == null)
                    {
                        return NotFound();
                    }
                }
                
                var adapter = new FeedbackEditorViewModelAdapter();
                
                adapter.Adapt(item, toValue);
                
                _Service.Save(toValue);
                
                return RedirectToAction("Edit", new { id = toValue.Id });
            }
            else
            {
                return View(item);
            }
        }
        
        public ActionResult Search()
        {
            var viewModel = new FeedbackSearchViewModel();
            
            return View(viewModel);
        }
        
        [HttpGet]
        public ActionResult Search(FeedbackSearchViewModel item, string pageNumber, string sortBy)
        {
            if (item == null)
            {
                return View(new FeedbackSearchViewModel());
            }
            else if (item.IsSimpleSearch == true)
            {
                return RunSimpleSearch(item, pageNumber, sortBy);
            }
            else
            {
                return RunDetailedSearch(item, pageNumber, sortBy);
            }
        }
        
        private ActionResult RunDetailedSearch(FeedbackSearchViewModel item, string pageNumber, string sortBy)
        {
            var sortDirection = GetSortDirection(item, sortBy);
            
            ModelState.Clear();
            
            var results = _Service.Search(
            searchValueFeedbackType: item.FeedbackType,
            searchValueSentiment: item.Sentiment,
            searchValueSubject: item.Subject,
            searchValueFeedbackText: item.FeedbackText,
            searchValueUsername: item.Username,
            searchValueFirstName: item.FirstName,
            searchValueLastName: item.LastName,
            searchValueReferer: item.Referer,
            searchValueUserAgent: item.UserAgent,
            searchValueIpAddress: item.IpAddress,
            searchValueStatus: item.Status,
            searchValueCreatedBy: item.CreatedBy,
            searchValueLastModifiedBy: item.LastModifiedBy,
            
            
            sortBy: sortBy, sortByDirection: sortDirection);
            
            var pageableResults = new PageableResults<Feedback>();
            
            pageableResults.Initialize(results);
            
            pageableResults.CurrentPage = pageNumber.SafeToInt32(0);
            
            item.Results = pageableResults;
            item.CurrentSortDirection = sortDirection;
            item.CurrentSortProperty = sortBy;
            
            return View(item);
        }
        
        private ActionResult RunSimpleSearch(
            FeedbackSearchViewModel item, string pageNumber, string sortBy)
        {
            ModelState.Clear();
            
            string sortDirection;
            
            if (sortBy == null)
            {
                // the value didn't change because of HTTP POST
                sortBy = item.CurrentSortProperty;
                sortDirection = item.CurrentSortDirection;
            }
            else
            {
                sortDirection = GetSortDirection(item, sortBy);
            }
            
            var results = _Service.SimpleSearch(item.SimpleSearchValue,
            sortBy, sortDirection);
            
            var pageableResults = new PageableResults<Feedback>();
            
            pageableResults.Initialize(results);
            
            pageableResults.CurrentPage = pageNumber.SafeToInt32(0);
            
            item.Results = pageableResults;
            item.CurrentSortDirection = sortDirection;
            item.CurrentSortProperty = sortBy;
            
            return View(item);
        }
        
        
        private string GetSortDirection(ISortableResult viewModel, string sortBy)
        {
            if (string.IsNullOrWhiteSpace(sortBy) == true)
            {
                return SearchConstants.SortDirectionAscending;
            }
            else
            {
                if (string.Compare(sortBy, viewModel.CurrentSortProperty, true) == 0)
                {
                    if (viewModel.CurrentSortDirection == SearchConstants.SortDirectionAscending)
                    {
                        return SearchConstants.SortDirectionDescending;
                    }
                    else
                    {
                        return SearchConstants.SortDirectionAscending;
                    }
                }
                else
                {
                    return SearchConstants.SortDirectionAscending;
                }
            }
        }
        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            
            Feedback item;
            
            item = _Service.GetById(id.Value);
            
            if (item == null)
            {
                return NotFound();
            }
            
            return View(item);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Feedback item)
        {
            var deleteThis =
            _Service.GetById(item.Id);
            
            if (deleteThis == null)
            {
                return NotFound();
            }
            
            _Service.DeleteById(item.Id);
            
            return RedirectToAction("Index");
        }
    }
}
