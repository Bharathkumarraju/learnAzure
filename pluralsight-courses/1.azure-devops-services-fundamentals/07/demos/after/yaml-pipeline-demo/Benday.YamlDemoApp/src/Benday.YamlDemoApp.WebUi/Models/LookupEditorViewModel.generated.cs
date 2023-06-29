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
    public partial class LookupEditorViewModel : IInt32Identity, ISelectable, IDeleteable
    {
        public bool IsSelected { get; set; }
        public bool IsMarkedForDelete { get; set; }
        
        [Display(Name = "Id")]
        public int Id { get; set; }
        
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
        
        [Display(Name = "Lookup Type")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Required]
        public string LookupType { get; set; }
        private List<SelectListItem> _LookupTypes;
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
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LookupKey { get; set; }
        
        [Display(Name = "Lookup Value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string LookupValue { get; set; }
        
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