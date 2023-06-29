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
    public partial class UserClaimEditorViewModel : IInt32Identity, ISelectable, IDeleteable
    {
        public bool IsSelected { get; set; }
        public bool IsMarkedForDelete { get; set; }
        
        [Display(Name = "Id")]
        public int Id { get; set; }
        
        [Display(Name = "username")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Username { get; set; }
        
        [Display(Name = "claim name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ClaimName { get; set; }
        
        [Display(Name = "claim value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ClaimValue { get; set; }
        
        [Display(Name = "UserId")]
        public int UserId { get; set; }
        
        [Display(Name = "claim type (normal / date)")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [Required]
        public string ClaimLogicType { get; set; }
        private List<SelectListItem> _ClaimLogicTypes;
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
        
        [Display(Name = "claim start date")]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }
        
        [Display(Name = "claim end date")]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }
        
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