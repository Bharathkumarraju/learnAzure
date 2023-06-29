namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public interface ISearchStringParserStrategy
    {
        string[] Parse(string parseThis);
    }
}
