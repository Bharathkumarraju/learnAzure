namespace Benday.YamlDemoApp.WebUi.Models
{
    public interface IPageableResults
    {
        int TotalCount { get; }
        int ItemsPerPage { get; set; }
        int PageCount { get; set; }
        int CurrentPage { get; set; }
    }
}
