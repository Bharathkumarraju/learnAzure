using Benday.YamlDemoApp.Api;

namespace Benday.YamlDemoApp.UnitTests.Fakes.Security
{
    public class FakeUsernameProvider : IUsernameProvider
    {
        public string GetUsernameReturnValue { get; set; }

        public string Username => GetUsernameReturnValue;
    }
}
