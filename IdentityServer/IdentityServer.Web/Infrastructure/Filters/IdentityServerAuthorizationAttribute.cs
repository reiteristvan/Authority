using System.Web.Mvc;

namespace IdentityServer.Web.Infrastructure.Filters
{
    public sealed class IdentityServerAuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthorizeCore(filterContext.HttpContext))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                filterContext.Result = new RedirectResult("/developers/login");
            }
        }
    }
}