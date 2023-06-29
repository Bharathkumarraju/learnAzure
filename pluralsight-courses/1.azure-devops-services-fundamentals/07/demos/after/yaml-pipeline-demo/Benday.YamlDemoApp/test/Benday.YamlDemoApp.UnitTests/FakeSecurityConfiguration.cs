using Benday.YamlDemoApp.Api.Security;

namespace Benday.YamlDemoApp.UnitTests
{
    public class FakeSecurityConfiguration : ISecurityConfiguration
    {
        public string AuthType => null;

        public bool AzureActiveDirectory => true;

        public bool DevelopmentMode => true;

        public bool Facebook => true;

        public bool Google => true;

        public string LoginPath => null;

        public string PostLoginPath => null;

        public string PostLogoutPath => null;

        public string LogoutPath => null;

        public string RegisterPath => null;

        public string UserAccountPath => null;

        public bool MicrosoftAccount => true;

        public bool Twitter => true;
    }
}
