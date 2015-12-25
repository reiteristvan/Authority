using System.Configuration;
using IdentityServer.EmailService;

namespace IdentityServer.Web.Infrastructure
{
    public sealed class EmailSettingsProvider : IEmailSettingsProvider
    {
        public string TemplateFolderPath { get { return ConfigurationManager.AppSettings["EmailTemplatePath"]; } }
    }
}