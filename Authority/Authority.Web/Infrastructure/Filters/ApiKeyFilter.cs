using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using IdentityServer.Services;
using Microsoft.Practices.Unity;

namespace IdentityServer.Web.Infrastructure.Filters
{
    public class ApiKeyFilter : ActionFilterAttribute
    {
        private const string ApiKeyHeader = "X-ApiKey";

        [Dependency]
        public IAccountService AccountService { get; set; }

        private void ValidateRequest(HttpActionContext actionContext)
        {
            IEnumerable<string> apiKeyHeaders;
            if(!actionContext.Request.Headers.TryGetValues(ApiKeyHeader, out apiKeyHeaders))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            Guid apiKey;
            if (!Guid.TryParse(apiKeyHeaders.First(), out apiKey))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            ValidationResult validationResult = AccountService.ValidateProduct(apiKey).Result;

            if (!validationResult.IsValid)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            AuthorityApiController controller = actionContext.ControllerContext.Controller as AuthorityApiController;

            controller.Context = new RequestContext
            {
                ProductId = validationResult.ProductId,
                ApiKey = Guid.Parse(apiKeyHeaders.First())
            };
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ValidateRequest(actionContext);
        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            ValidateRequest(actionContext);
        }
    }
}