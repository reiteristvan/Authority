using IdentityServer.EmailService.cs;
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

            container.RegisterType<IEmailSettingsProvider, EmailSettingsProvider>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}