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
    public partial class UserClaimSearchViewModel :
        SearchViewModelBase<Benday.YamlDemoApp.Api.DomainModels.UserClaim>
    {
        public UserClaimSearchViewModel()
        {
            Results = new PageableResults<Benday.YamlDemoApp.Api.DomainModels.UserClaim>();
            IsSimpleSearch = true;
            SimpleSearchValue = string.Empty;
        }
        
        [Display(Name = "username")]
        public string Username { get; set; }
        [Display(Name = "claim name")]
        public string ClaimName { get; set; }
        [Display(Name = "claim type (normal / date)")]
        public string ClaimLogicType { get; set; }
        
        private List<SelectListItem> _ClaimLogicTypes;
        [Display(Name = "claim type (normal / date)")]
        public List<SelectListItem> ClaimLogicTypes
        {
            get
            {
                if (_ClaimLogicTypes == null)
                {
                    _ClaimLogicTypes = new List<SelectListItem>();
                }
                
                return _ClaimLogicTypes;
            }
            set
            {
                _ClaimLogicTypes = value;
            }
        }
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