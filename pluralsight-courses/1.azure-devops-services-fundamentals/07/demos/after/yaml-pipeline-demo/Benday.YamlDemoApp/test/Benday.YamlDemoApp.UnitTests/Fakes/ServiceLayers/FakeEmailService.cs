using System.Threading.Tasks;
using Benday.YamlDemoApp.Api.ServiceLayers;

namespace Benday.YamlDemoApp.UnitTests.Fakes.ServiceLayers
{
    public class FakeEmailService : IEmailService
    {
        public Task SendEmail(string recipientEmail, string recipientName, string subject)
        {
            return Task.CompletedTask;
        }
    }
}
