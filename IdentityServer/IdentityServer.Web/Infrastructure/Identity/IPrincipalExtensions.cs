using System;
using System.Security.Principal;

namespace IdentityServer.Web.Infrastructure.Identity
{
    public static class IPrincipalExtensions
    {
        public static Guid GetUserId(this IPrincipal principal)
        {
            DeveloperPrincipal developerPrincipal = principal as DeveloperPrincipal;

            if (developerPrincipal == null)
            {
                throw new ArgumentException("principal");
            }

            return developerPrincipal.Id;
        }
    }
}