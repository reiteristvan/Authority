using Authority.EmailService;
using Authority.EntityFramework;
using Authority.Services;
using IdentityServer.Services;
using IdentityServer.Web.Infrastructure.Filters;
using Microsoft.Practices.Unity;

namespace Authority.Web.Infrastructure
{
    public static class DependencyRegistrations
    {
        public static IUnityContainer Register()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<IErrorService, ErrorService>(new ContainerControlledLifetimeManager());

            container.RegisterType<IAuthorityContext, AuthorityContext>(new ContainerControlledLifetimeManager());
            container.RegisterType<ISafeAuthorityContext, AuthorityContext>(new ContainerControlledLifetimeManager());

            container.RegisterType<IEmailService, EmailService.EmailService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IEmailSettingsProvider, EmailSettingsProvider>(new ContainerControlledLifetimeManager());

            container.RegisterType<IConfiguration, Configuration>(new ContainerControlledLifetimeManager());
            container.RegisterType<IDeveloperService, DeveloperService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IProductService, ProductService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPolicyService, PolicyService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAccountService, AccountService>(new ContainerControlledLifetimeManager());

            container.RegisterType<ApiKeyFilter>(new InjectionProperty("AccountService"));

            return container;
        }
    }
}