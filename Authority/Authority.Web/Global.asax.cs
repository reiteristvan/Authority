using System;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Authority.Services;
using Authority.Web.Infrastructure;
using Authority.Web.Infrastructure.Filters;
using Authority.Web.Infrastructure.Identity;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using Newtonsoft.Json;

namespace Authority.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            IUnityContainer container = DependencyRegistrations.Register();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(container));

            GlobalFilters.Filters.Add(new ErrorHandler(container.Resolve<IErrorService>()));
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (!HttpContext.Current.Request.Cookies.AllKeys.Contains(FormsAuthentication.FormsCookieName))
            {
                return;
            }

            HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            DeveloperPrincipalSerializable userData = JsonConvert.DeserializeObject<DeveloperPrincipalSerializable>(ticket.UserData);

            if (userData == null)
            {
                return;
            }

            DeveloperPrincipal principal = new DeveloperPrincipal(userData.Id, userData.Email);

            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;
        }
    }
}
