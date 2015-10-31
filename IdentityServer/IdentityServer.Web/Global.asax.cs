using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using IdentityServer.Web.Infrastructure;
using Microsoft.Practices.Unity.Mvc;

namespace IdentityServer.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            DependencyResolver.SetResolver(new UnityDependencyResolver(DependencyRegistrations.Register()));
        }
    }
}
