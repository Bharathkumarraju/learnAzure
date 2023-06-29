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
    public partial class ConfigurationItemSearchViewModel :
        SearchViewModelBase<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>
    {
        public ConfigurationItemSearchViewModel()
        {
            Results = new PageableResults<Benday.YamlDemoApp.Api.DomainModels.ConfigurationItem>();
            IsSimpleSearch = true;
            SimpleSearchValue = string.Empty;
        }
        
        [Display(Name = "category")]
        public string Category { get; set; }
        [Display(Name = "configuration key")]
        public string ConfigurationKey { get; set; }
        [Display(Name = "description")]
        public string Description { get; set; }
        [Display(Name = "configuration value")]
        public string ConfigurationValue { get; set; }
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