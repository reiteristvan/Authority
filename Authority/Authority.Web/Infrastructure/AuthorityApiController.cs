using System;
using System.Web.Http;

namespace IdentityServer.Web.Infrastructure
{
    public class RequestContext
    {
        public Guid ProductId { get; set; }
        public Guid ApiKey { get; set; }
    }

    public abstract class AuthorityApiController : ApiController
    {
        public RequestContext Context { get; set; }
    }
}