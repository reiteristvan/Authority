using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.Operations.Account;

namespace IdentityServer.Services
{
    public interface IAccountService
    {
        Task<bool> ValidateProduct(Guid clientId, string redirect_url);
        Task RegisterUser(Guid productId, string email, string username, string password);
        Task ActivateUser(Guid activationCode);
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
            return product != null && product.IsPublic && product.IsActive && product.LandingPage.Equals(redirect_url, StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task RegisterUser(Guid clientId, string email, string username, string password)
        {
            UserRegistration operation = new UserRegistration(_authorityContext, clientId, email, username, password);
            await operation.Do();
            await operation.CommitAsync();
        }

        public async Task ActivateUser(Guid activationCode)
        {
            UserActivation operation = new UserActivation(_authorityContext, activationCode);
            await operation.Do();
            await operation.CommitAsync();
        }
    }
}
