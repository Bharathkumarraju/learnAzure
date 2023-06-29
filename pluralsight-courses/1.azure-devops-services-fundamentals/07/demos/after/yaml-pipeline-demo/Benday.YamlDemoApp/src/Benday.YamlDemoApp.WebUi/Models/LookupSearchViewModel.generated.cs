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
    public partial class LookupSearchViewModel :
        SearchViewModelBase<Benday.YamlDemoApp.Api.DomainModels.Lookup>
    {
        public LookupSearchViewModel()
        {
            Results = new PageableResults<Benday.YamlDemoApp.Api.DomainModels.Lookup>();
            IsSimpleSearch = true;
            SimpleSearchValue = string.Empty;
        }
        
        [Display(Name = "Lookup Type")]
        public string LookupType { get; set; }
        
        private List<SelectListItem> _LookupTypes;
        [Display(Name = "Lookup Type")]
        public List<SelectListItem> LookupTypes
        {
            get
            {
                if (_LookupTypes == null)
                {
                    _LookupTypes = new List<SelectListItem>();
                }
                
                return _LookupTypes;
            }
            set
            {
                _LookupTypes = value;
            }
        }
        [Display(Name = "Lookup Key")]
        public string LookupKey { get; set; }
        [Display(Name = "Lookup Value")]
        public string LookupValue { get; set; }
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