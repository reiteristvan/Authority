using System.Configuration;
using IdentityServer.EmailService.cs;

namespace IdentityServer.Web.Infrastructure
{
    public sealed class EmailSettingsProvider : IEmailSettingsProvider
    {
        public string TemplateFolderPath { get { return ConfigurationManager.AppSettings["EmailTemplatePath"]; } }
    }
}