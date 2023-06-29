namespace Benday.YamlDemoApp.Api.AzureStorage
{
    public class AzureBlobImageStorageOptions
    {
        public bool UseDevelopmentStorage { get; set; }
        public string AccountKey { get; set; }
        public string AccountName { get; set; }
        public string ContainerName { get; set; }
        public int ReadTokenExpirationInSeconds { get; set; }
    }
}
