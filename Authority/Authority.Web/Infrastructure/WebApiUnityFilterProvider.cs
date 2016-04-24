using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;

namespace IdentityServer.Web.Infrastructure
{
    public class WebApiUnityFilterProvider : IFilterProvider
    {
        private readonly IUnityContainer _container;
        private readonly ActionDescriptorFilterProvider _defaultProvider;

        public WebApiUnityFilterProvider(IUnityContainer container)
        {
            _container = container;
            _defaultProvider = new ActionDescriptorFilterProvider();
        }

        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            IEnumerable<FilterInfo> attributes = _defaultProvider.GetFilters(configuration, actionDescriptor);

            foreach (FilterInfo attribute in attributes)
            {
                _container.BuildUp(attribute.Instance.GetType(), attribute.Instance);
            }
            return attributes;
        }
    }
}