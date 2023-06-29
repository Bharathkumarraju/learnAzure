using System.Threading.Tasks;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public interface IEmailService
    {
        Task SendEmail(string recipientEmail, string recipientName, string subject);
    }
}
