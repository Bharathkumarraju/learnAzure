using Benday.EfCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Benday.Common;
using Benday.YamlDemoApp.Api;

namespace Benday.YamlDemoApp.WebUi.Models
{
    public partial class FeedbackEditorViewModel : IInt32Identity, ISelectable, IDeleteable
    {
        public bool IsSelected { get; set; }
        public bool IsMarkedForDelete { get; set; }
        
        [Display(Name = "Id")]
        public int Id { get; set; }
        
        [Display(Name = "feedback type")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Required]
        public string FeedbackType { get; set; }
        private List<SelectListItem> _FeedbackTypes;
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
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Required]
        public string Sentiment { get; set; }
        private List<SelectListItem> _Sentiments;
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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Subject { get; set; }
        
        [Display(Name = "message")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FeedbackText { get; set; }
        
        [Display(Name = "username")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Username { get; set; }
        
        [Display(Name = "first name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FirstName { get; set; }
        
        [Display(Name = "last name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LastName { get; set; }
        
        [Display(Name = "referer")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Referer { get; set; }
        
        [Display(Name = "user agent")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserAgent { get; set; }
        
        [Display(Name = "ip address")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string IpAddress { get; set; }
        
        [Display(Name = "yes, I'd like a reply")]
        public bool IsContactRequest { get; set; }
        
        [Display(Name = "Status")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Required]
        public string Status { get; set; } = ApiConstants.StatusActive;
        private List<SelectListItem> _Statuses;
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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CreatedBy { get; set; }
        
        [Display(Name = "Created Date")]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:d}")]
        public DateTime CreatedDate { get; set; }
        
        [Display(Name = "Last Modified By")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LastModifiedBy { get; set; }
        
        [Display(Name = "Last Modified Date")]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:d}")]
        public DateTime LastModifiedDate { get; set; }
        
        [Display(Name = "Timestamp")]
        public byte[] Timestamp { get; set; }
        
        
    }
}