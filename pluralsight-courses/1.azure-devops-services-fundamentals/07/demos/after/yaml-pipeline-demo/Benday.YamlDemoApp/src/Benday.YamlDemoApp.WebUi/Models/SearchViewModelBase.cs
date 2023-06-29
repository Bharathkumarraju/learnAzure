using System.ComponentModel.DataAnnotations;

namespace Benday.YamlDemoApp.WebUi.Models
{
    public abstract class SearchViewModelBase<T> : SortableViewModelBase<T>
    {
        protected SearchViewModelBase()
        {
            IsSimpleSearch = true;
        }

        [Display(Name = "Simple Search")]
        public bool IsSimpleSearch { get; set; }

        [Display(Name = "Simple Search Value")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string SimpleSearchValue { get; set; }
    }
}
