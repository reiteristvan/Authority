using System;
using System.Security.Principal;

namespace IdentityServer.Web.Infrastructure.Identity
{
    public interface IDeveloperPrincipal : IPrincipal
    {
        Guid Id { get; set; }
        string Email { get; set; }
    }
}