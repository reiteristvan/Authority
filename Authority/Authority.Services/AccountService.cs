using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;

namespace IdentityServer.Services
{
    public interface IAccountService
    {
        Task<bool> ValidateProduct(Guid clientId, string redirect_url);
    }

    public sealed class AccountService : IAccountService
    {
        private readonly IAuthorityContext _authorityContext;

        public AccountService(IAuthorityContext authorityContext)
        {
            if (authorityContext == null)
            {
                throw new ArgumentNullException("authorityContext");
            }

            _authorityContext = authorityContext;
        }

        public async Task<bool> ValidateProduct(Guid clientId, string redirect_url)
        {
            Product product = await _authorityContext.Products.FirstOrDefaultAsync(p => p.ClientId == clientId);
            return product != null && product.LandingPage.Equals(redirect_url, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
