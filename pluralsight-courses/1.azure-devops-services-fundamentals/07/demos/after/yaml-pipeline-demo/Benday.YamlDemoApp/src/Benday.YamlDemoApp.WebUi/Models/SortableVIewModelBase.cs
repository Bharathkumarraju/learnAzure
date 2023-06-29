using System.ComponentModel.DataAnnotations;
using Benday.Common;

namespace Benday.YamlDemoApp.WebUi.Models
{
    public abstract class SortableViewModelBase<T> : ISortableResult
    {
        protected SortableViewModelBase()
        {
            CurrentSortProperty = string.Empty;
            CurrentSortDirection = SearchConstants.SortDirectionAscending;
        }

        [Display(Name = "Current Sort Property")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CurrentSortProperty { get; set; }

        [Display(Name = "Current Sort Direction")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string CurrentSortDirection { get; set; }

        public PageableResults<T> Results
        {
            get;
            set;
        }
    }
}
