using System;
using System.Web.Mvc;
using Authority.Web.Infrastructure.Identity;

namespace IdentityServer.Web.Infrastructure
{
    public sealed class CallingContext
    {
        public Guid UserId { get; set; }
    }

    public abstract class AuthorityController : Controller
    {
        private CallingContext _callingContext;

        protected CallingContext CallingContext { get { return _callingContext; } }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            _callingContext = new CallingContext
            {
                UserId = requestContext.HttpContext.User.GetUserId()
            };
        }
    }
}