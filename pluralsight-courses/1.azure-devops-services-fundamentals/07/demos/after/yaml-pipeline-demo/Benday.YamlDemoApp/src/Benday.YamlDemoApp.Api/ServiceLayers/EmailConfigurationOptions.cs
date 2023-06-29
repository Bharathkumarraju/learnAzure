namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public class EmailConfigurationOptions
    {
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string SendGridApiKey { get; set; }
    }
}
