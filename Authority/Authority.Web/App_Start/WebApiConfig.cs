using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Filters;
using Authority.Web.Infrastructure;
using IdentityServer.Web.Infrastructure;
using Microsoft.Practices.Unity;

namespace Authority.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            IUnityContainer container = DependencyRegistrations.Register();
            config.DependencyResolver = new WebApiUnityDependencyResolver(container);

            List<IFilterProvider> filterProviders = config.Services.GetFilterProviders().ToList();
            IFilterProvider defaultFilterProvider = filterProviders.Single(p => p is ActionDescriptorFilterProvider);

            config.Services.Remove(typeof (IFilterProvider), defaultFilterProvider);
            config.Services.Add(typeof(IFilterProvider), new WebApiUnityFilterProvider(container));
        }
    }
}
