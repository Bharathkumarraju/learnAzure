namespace Benday.YamlDemoApp.Api.Security
{
    public interface ISecurityConfiguration
    {
        string AuthType { get; }
        bool AzureActiveDirectory { get; }
        bool DevelopmentMode { get; }
        bool Facebook { get; }
        bool Google { get; }
        string LoginPath { get; }
        string PostLoginPath { get; }
        string PostLogoutPath { get; }
        string LogoutPath { get; }
        string RegisterPath { get; }
        string UserAccountPath { get; }
        bool MicrosoftAccount { get; }
        bool Twitter { get; }
    }
}
