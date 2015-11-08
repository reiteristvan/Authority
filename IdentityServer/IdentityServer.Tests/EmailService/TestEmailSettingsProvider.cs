using IdentityServer.EmailService.cs;

namespace IdentityServer.Tests.EmailService
{
    public sealed class TestEmailSettingsProvider : IEmailSettingsProvider
    {
        public string TemplateFolderPath
        {
            get { return @"EmailService/Templates"; }
        }
    }
}
