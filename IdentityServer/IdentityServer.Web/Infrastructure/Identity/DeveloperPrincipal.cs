using System;
using System.Security.Principal;

namespace IdentityServer.Web.Infrastructure.Identity
{
    public sealed class DeveloperPrincipal : IDeveloperPrincipal
    {
        public DeveloperPrincipal(Guid id, string email)
        {
            Identity = new GenericIdentity(email);
            Id = id;
            Email = email;
        }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}