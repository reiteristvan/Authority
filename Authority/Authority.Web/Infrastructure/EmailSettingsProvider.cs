using System.Configuration;
using System.Web.Hosting;
using Authority.EmailService;
using IdentityServer.Web.Infrastructure;

namespace Authority.Web.Infrastructure
{
    public sealed class EmailSettingsProvider : IEmailSettingsProvider
    {
        public string TemplateFolderPath 
        {
            get
            {
                return HostingEnvironment.MapPath(ConfigurationManager.AppSettings["EmailTemplatePath"]);
            } 
        }

        public string SendGridUsername
        {
            get { return Vault.Keys("SendGridUsername"); }
        }

        public string SendGridPassword
        {
            get { return Vault.Keys("SendGridPassword"); }
        }
    }
}