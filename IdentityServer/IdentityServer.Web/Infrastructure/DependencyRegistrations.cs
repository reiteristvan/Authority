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

            container.RegisterType<IErrorService, ErrorService>(new ContainerControlledLifetimeManager());

            container.RegisterType<IIdentityServerContext, IdentityServerContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISafeIdentityServerContext, IdentityServerContext>(new ContainerControlledLifetimeManager());

            container.RegisterType<IEmailService, EmailService.EmailService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailSettingsProvider, EmailSettingsProvider>(new ContainerControlledLifetimeManager());

            container.RegisterType<IConfiguration, Configuration>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeveloperService, DeveloperService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductService, ProductService>(new ContainerControlledLifetimeManager());

            return container;
        }
    }
}