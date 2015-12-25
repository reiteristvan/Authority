using IdentityServer.EmailService;
using IdentityServer.EntityFramework;
using IdentityServer.Services;
using Microsoft.Practices.Unity;

namespace IdentityServer.Web.Infrastructure
{
    public static class DependencyRegistrations
    {
        public static IUnityContainer Register()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<IIdentityServerContext, IdentityServerContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISafeIdentityServerContext, IdentityServerContext>(new ContainerControlledLifetimeManager());

            container.RegisterType<IEmailSettingsProvider, EmailSettingsProvider>(new ContainerControlledLifetimeManager());

            container.RegisterType<IConfiguration, Configuration>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeveloperService, DeveloperService>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}