using Authority.EmailService;

namespace Authority.Tests.EmailService
{
    public sealed class TestEmailSettingsProvider : IEmailSettingsProvider
    {
        public string TemplateFolderPath
        {
            get { return @"EmailService/Templates"; }
        }

        public string SendGridUsername { get { return ""; } }
        public string SendGridPassword { get { return ""; } }
    }
}
