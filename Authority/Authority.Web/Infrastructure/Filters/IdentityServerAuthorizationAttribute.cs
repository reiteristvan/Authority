using System.Web.Mvc;

namespace Authority.Web.Infrastructure.Filters
{
    public sealed class AuthorityAuthorizationAttribute : AuthorizeAttribute
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