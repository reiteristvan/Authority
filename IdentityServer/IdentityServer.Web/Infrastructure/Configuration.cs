using System.Configuration;
using IdentityServer.Services;

namespace IdentityServer.Web.Infrastructure
{
    public sealed class Configuration : IConfiguration
    {
        public string DeveloperActivationEmailTemplate
        {
            get { return ConfigurationManager.AppSettings["DeveloperActivationEmailTemplate"]; }
        }
    }
}