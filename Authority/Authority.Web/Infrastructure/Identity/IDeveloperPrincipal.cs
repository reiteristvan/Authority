using System;
using System.Security.Principal;

namespace Authority.Web.Infrastructure.Identity
{
    public interface IDeveloperPrincipal : IPrincipal
    {
        Guid Id { get; set; }
        string Email { get; set; }
    }
}