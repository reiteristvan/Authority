using Authority.EmailService;

namespace Authority.Tests.EmailService
{
    public sealed class TestEmailSettingsProvider : IEmailSettingsProvider
    {
        public string TemplateFolderPath
        {
            get { return @"EmailService/Templates"; }
        }
    }
}
