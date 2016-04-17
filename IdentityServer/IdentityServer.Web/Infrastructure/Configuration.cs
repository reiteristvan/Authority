using System.Configuration;
using Authority.Services;

namespace Authority.Web.Infrastructure
{
    public sealed class Configuration : IConfiguration
    {
        public string DeveloperActivationUrlTemplate
        {
            get { return ConfigurationManager.AppSettings["DeveloperActivationUrlTemplate"]; }
        }
    }
}