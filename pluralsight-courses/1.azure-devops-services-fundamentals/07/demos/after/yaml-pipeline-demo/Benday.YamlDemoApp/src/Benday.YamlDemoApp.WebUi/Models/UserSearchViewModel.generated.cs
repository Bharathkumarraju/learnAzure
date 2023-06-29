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
    public partial class UserSearchViewModel :
        SearchViewModelBase<Benday.YamlDemoApp.Api.DomainModels.User>
    {
        public UserSearchViewModel()
        {
            Results = new PageableResults<Benday.YamlDemoApp.Api.DomainModels.User>();
            IsSimpleSearch = true;
            SimpleSearchValue = string.Empty;
        }
        
        [Display(Name = "username")]
        public string Username { get; set; }
        [Display(Name = "Source")]
        public string Source { get; set; }
        [Display(Name = "email address")]
        public string EmailAddress { get; set; }
        [Display(Name = "first name")]
        public string FirstName { get; set; }
        [Display(Name = "last name")]
        public string LastName { get; set; }
        [Display(Name = "phone number")]
        public string PhoneNumber { get; set; }
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