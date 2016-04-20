using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Authority.EmailService.Models;
using SendGrid;

namespace Authority.EmailService
{
    public sealed class EmailService : IEmailService
    {
        private readonly IEmailSettingsProvider _emailSettingsProvider;
        private readonly EmailTemplateProvider _templateProvider;

        public EmailService(IEmailSettingsProvider emailSettingsProvider)
        {
            _emailSettingsProvider = emailSettingsProvider;
            _templateProvider = new EmailTemplateProvider(_emailSettingsProvider);
        }

        public async Task SendDeveloperActivation(string recipient, DeveloperActivationModel model)
        {
            string body = _templateProvider.Load(model);
            await Send(recipient, "Activate your account at Authority", body);
        }

        public async Task SendUserActivation(string recipient, UserActivationModel model)
        {
            string body = _templateProvider.Load(model);
            await Send(recipient, "Activate your account at Authority", body);
        }

        private async Task Send(string recipient, string subject, string body)
        {
            SendGridMessage message = new SendGridMessage();
            message.From = new MailAddress(ConfigurationManager.AppSettings["DoNotReplyEmailAddress"]);
            message.Subject = subject;
            message.AddTo(new List<string> { recipient });
            message.Html = body;

            Web credentials = new Web(new NetworkCredential(
                ConfigurationManager.AppSettings["SendGridUsername"],
                ConfigurationManager.AppSettings["SendGridPassword"]));

            await credentials.DeliverAsync(message);
        }
    }
}
