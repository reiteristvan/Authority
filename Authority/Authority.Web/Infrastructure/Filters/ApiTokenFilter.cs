using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using IdentityServer.Services;
using Microsoft.Practices.Unity;

namespace IdentityServer.Web.Infrastructure.Filters
{
    public class ApiTokenFilter : ActionFilterAttribute
    {
        [Dependency]
        public IAccountService AccountService { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            AuthenticationHeaderValue authorizationHeader = actionContext.Request.Headers.Authorization;

            if (authorizationHeader == null || !authorizationHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }

            byte[] base64credentials = Convert.FromBase64String(authorizationHeader.Parameter);
            string credentials = Encoding.ASCII.GetString(base64credentials);

            int separator = credentials.IndexOf(':');
            string clientIdString = credentials.Substring(0, separator);
            string clientSecretString = credentials.Substring(separator + 1);

            Guid clientId = Guid.Parse(clientIdString);
            Guid clientSecret = Guid.Parse(clientSecretString);

            if(!AccountService.ValidateProductWithSecret(clientId, clientSecret).Result)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authorizationHeader = actionContext.Request.Headers.Authorization;

            if (authorizationHeader == null || !authorizationHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                return;
            }

            byte[] base64credentials = Convert.FromBase64String(authorizationHeader.Parameter);
            string credentials = Encoding.ASCII.GetString(base64credentials);

            int separator = credentials.IndexOf(':');
            string clientIdString = credentials.Substring(0, separator);
            string clientSecretString = credentials.Substring(separator + 1);

            Guid clientId = Guid.Parse(clientIdString);
            Guid clientSecret = Guid.Parse(clientSecretString);

            if (!await AccountService.ValidateProductWithSecret(clientId, clientSecret))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }
    }
}