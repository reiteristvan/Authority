using System.Configuration;
using System.Web.Hosting;
using Authority.EmailService;

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
    }
}