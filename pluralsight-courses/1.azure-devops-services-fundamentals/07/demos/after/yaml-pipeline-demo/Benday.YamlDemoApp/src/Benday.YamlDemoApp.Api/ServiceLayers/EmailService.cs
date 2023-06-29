using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Benday.YamlDemoApp.Api.ServiceLayers
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfigurationOptions _options;
        private readonly ILogger<EmailService> _logger;
        [SuppressMessage("csharp", "IDE0052")]
        private readonly IConfigurationItemService _configItemService;
        [SuppressMessage("csharp", "IDE0052")]
        private readonly IUserService _userService;

        public EmailService(
            ILogger<EmailService> logger,
            IConfigurationItemService configItemService,
            IOptionsMonitor<EmailConfigurationOptions> options,
            IUserService userService)
        {
            _options = options.CurrentValue;

            if (_options.SendGridApiKey == null)
            {
                throw new ArgumentNullException(nameof(_options.SendGridApiKey), "Argument cannot be null.");
            }

            _configItemService = configItemService;
            _logger = logger;
            _userService = userService;
        }

        public async Task SendEmail(
            string recipientEmail, string recipientName, string subject)
        {
            if (recipientEmail == null)
            {
                throw new ArgumentNullException(nameof(recipientEmail), "Argument cannot be null.");
            }

            if (recipientName == null)
            {
                throw new ArgumentNullException(nameof(recipientName), "Argument cannot be null.");
            }

            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress(_options.FromEmail, _options.FromName));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress(recipientEmail, recipientName)
            };

            msg.AddTos(recipients);

            msg.SetSubject(subject);

            msg.AddContent(MimeType.Text, "Hello World plain text!");
            msg.AddContent(MimeType.Html, "<p>Hello World!</p>");

            await SendEmail(msg, $"Subject '{subject}' to '{recipientName}' '{recipientEmail}'");
        }

        private async Task SendEmail(SendGridMessage msg, string msgDescriptionForLogging)
        {
            try
            {
                var apiKey = _options.SendGridApiKey;

                var client = new SendGridClient(apiKey);

                _logger.LogInformation($"Sending: {msgDescriptionForLogging}");

                var response = await client.SendEmailAsync(msg);

                var statusCode = "(null response)";

                if (response != null)
                {
                    statusCode = response.StatusCode.ToString();
                }

                _logger.LogInformation($"Sent: {msgDescriptionForLogging}, Status code: {statusCode}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problem sending email.");
                throw;
            }
        }
    }
}
