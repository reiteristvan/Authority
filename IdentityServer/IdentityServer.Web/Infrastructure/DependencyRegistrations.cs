using IdentityServer.EntityFramework;
using Microsoft.Practices.Unity;

namespace IdentityServer.Web.Infrastructure
{
    public static class DependencyRegistrations
    {
        public static IUnityContainer Register()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<IIdentityServerContext, IdentityServerContext>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}