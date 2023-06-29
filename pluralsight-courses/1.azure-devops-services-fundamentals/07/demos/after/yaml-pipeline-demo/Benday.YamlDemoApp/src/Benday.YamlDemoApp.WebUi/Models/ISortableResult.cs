namespace Benday.YamlDemoApp.WebUi.Models
{
    public interface ISortableResult
    {
        string CurrentSortDirection { get; set; }
        string CurrentSortProperty { get; set; }
    }
}
