using Benday.EfCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Benday.Common;

namespace Benday.YamlDemoApp.WebUi.Models
{
    public partial class FeedbackSearchViewModel :
        SearchViewModelBase<Benday.YamlDemoApp.Api.DomainModels.Feedback>
    {
        public FeedbackSearchViewModel()
        {
            Results = new PageableResults<Benday.YamlDemoApp.Api.DomainModels.Feedback>();
            IsSimpleSearch = true;
            SimpleSearchValue = string.Empty;
        }
        
        [Display(Name = "feedback type")]
        public string FeedbackType { get; set; }
        
        private List<SelectListItem> _FeedbackTypes;
        [Display(Name = "feedback type")]
        public List<SelectListItem> FeedbackTypes
        {
            get
            {
                if (_FeedbackTypes == null)
                {
                    _FeedbackTypes = new List<SelectListItem>();
                }
                
                return _FeedbackTypes;
            }
            set
            {
                _FeedbackTypes = value;
            }
        }
        [Display(Name = "sentiment")]
        public string Sentiment { get; set; }
        
        private List<SelectListItem> _Sentiments;
        [Display(Name = "sentiment")]
        public List<SelectListItem> Sentiments
        {
            get
            {
                if (_Sentiments == null)
                {
                    _Sentiments = new List<SelectListItem>();
                }
                
                return _Sentiments;
            }
            set
            {
                _Sentiments = value;
            }
        }
        [Display(Name = "notification subject")]
        public string Subject { get; set; }
        [Display(Name = "message")]
        public string FeedbackText { get; set; }
        [Display(Name = "username")]
        public string Username { get; set; }
        [Display(Name = "first name")]
        public string FirstName { get; set; }
        [Display(Name = "last name")]
        public string LastName { get; set; }
        [Display(Name = "referer")]
        public string Referer { get; set; }
        [Display(Name = "user agent")]
        public string UserAgent { get; set; }
        [Display(Name = "ip address")]
        public string IpAddress { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        
        private List<SelectListItem> _Statuses;
        [Display(Name = "Status")]
        public List<SelectListItem> Statuses
        {
            get
            {
                if (_Statuses == null)
                {
                    _Statuses = new List<SelectListItem>();
                }
                
                return _Statuses;
            }
            set
            {
                _Statuses = value;
            }
        }
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }
        
        
    }
}