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
using Benday.YamlDemoApp.Api.Security;

namespace Benday.YamlDemoApp.WebUi.Controllers
{
    [Authorize(Policy = SecurityConstants.Policy_IsAdministrator)]
    public partial class UserController : MvcControllerBase<UserEditorViewModel>
    {
        private readonly IValidatorStrategy<UserEditorViewModel> _Validator;
        private readonly ILookupService _LookupService;
        
        
        
        private readonly IUserService _Service;
        private readonly ILogger<UserController> _Logger;
        
        public UserController(
            IUserService service,
            IValidatorStrategy<UserEditorViewModel> validator,
            ILogger<UserController> logger,
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
            
            User item;
            UserEditorViewModel viewModel;
            
            if (id.Value == ApiConstants.UnsavedId)
            {
                // create new
                viewModel = new UserEditorViewModel();
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
                    viewModel = new UserEditorViewModel();
                    
                    var adapter = new UserEditorViewModelAdapter();
                    
                    adapter.Adapt(item, viewModel);
                    PopulateLookups(viewModel);
                }
            }
            
            BeforeReturnFromEdit(id, viewModel);
            
            return View(viewModel);
        }
        private void PopulateLookups(UserEditorViewModel viewModel)
        {
            viewModel.Statuses = WebUiUtilities.ToSelectListItems(
            _LookupService.GetAllByType("System.Lookup.StatusValues"));
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditorViewModel item)
        {
            if (_Validator.IsValid(item) == true)
            {
                User toValue;
                
                if (item.Id == ApiConstants.UnsavedId)
                {
                    toValue = new User();
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
                
                var adapter = new UserEditorViewModelAdapter();
                
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
            var viewModel = new UserSearchViewModel();
            
            return View(viewModel);
        }
        
        [HttpGet]
        public ActionResult Search(UserSearchViewModel item, string pageNumber, string sortBy)
        {
            if (item == null)
            {
                return View(new UserSearchViewModel());
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
        
        private ActionResult RunDetailedSearch(UserSearchViewModel item, string pageNumber, string sortBy)
        {
            var sortDirection = GetSortDirection(item, sortBy);
            
            ModelState.Clear();
            
            var results = _Service.Search(
            searchValueUsername: item.Username,
            searchValueSource: item.Source,
            searchValueEmailAddress: item.EmailAddress,
            searchValueFirstName: item.FirstName,
            searchValueLastName: item.LastName,
            searchValuePhoneNumber: item.PhoneNumber,
            searchValueStatus: item.Status,
            searchValueCreatedBy: item.CreatedBy,
            searchValueLastModifiedBy: item.LastModifiedBy,
            
            
            sortBy: sortBy, sortByDirection: sortDirection);
            
            var pageableResults = new PageableResults<User>();
            
            pageableResults.Initialize(results);
            
            pageableResults.CurrentPage = pageNumber.SafeToInt32(0);
            
            item.Results = pageableResults;
            item.CurrentSortDirection = sortDirection;
            item.CurrentSortProperty = sortBy;
            
            return View(item);
        }
        
        private ActionResult RunSimpleSearch(
            UserSearchViewModel item, string pageNumber, string sortBy)
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
            
            var pageableResults = new PageableResults<User>();
            
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
            
            User item;
            
            item = _Service.GetById(id.Value);
            
            if (item == null)
            {
                return NotFound();
            }
            
            return View(item);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User item)
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
