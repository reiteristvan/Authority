using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EmailService;
using Authority.EmailService.Models;
using Authority.EntityFramework;
using Authority.Operations.Account;

namespace IdentityServer.Services
{
    public interface IAccountService
    {
        Task<bool> ValidateProduct(Guid clientId);
        Task<bool> ValidateProductWithSecret(Guid clientId, Guid clientSecret);
        Task RegisterUser(Guid productId, string email, string username, string password);
        Task ActivateUser(Guid clientId, Guid activationCode);
        Task<string> LogInUser(Guid clientId, string email, string password);
    }

    public sealed class AccountService : IAccountService
    {
        private readonly IAuthorityContext _authorityContext;
        private readonly IEmailService _emailService;

        public AccountService(IAuthorityContext authorityContext, IEmailService emailService)
        {
            if (authorityContext == null)
            {
                throw new ArgumentNullException("authorityContext");
            }

            if (emailService == null)
            {
                throw new ArgumentNullException("emailService");
            }

            _authorityContext = authorityContext;
            _emailService = emailService;
        }

        public async Task<bool> ValidateProduct(Guid clientId)
        {
            Product product = await _authorityContext.Products.FirstOrDefaultAsync(p => p.ClientId == clientId);
            return product != null && product.IsPublic && product.IsActive;
        }

        public async Task<bool> ValidateProductWithSecret(Guid clientId, Guid clientSecret)
        {
            Product product = await _authorityContext.Products.FirstOrDefaultAsync(p => p.ClientId == clientId);
            return product != null && product.IsPublic && product.IsActive && product.ClientSecret == clientSecret;
        }

        public async Task RegisterUser(Guid clientId, string email, string username, string password)
        {
            Product product = await _authorityContext.Products.FirstOrDefaultAsync(p => p.ClientId == clientId);

            UserRegistration operation = new UserRegistration(_authorityContext, product.Id, email, username, password);
            User user = await operation.Do();
            await operation.CommitAsync();

            await _emailService.SendUserActivation(email, new UserActivationModel
            {
                ActivationUrl = string.Format("{0}/{1}", product.ActivationUrl, user.PendingRegistrationId),
                ProductName = product.Name,
                Sender = product.NotificationEmail
            });
        }

        public async Task ActivateUser(Guid clientId, Guid activationCode)
        {
            UserActivation operation = new UserActivation(_authorityContext, clientId, activationCode);
            await operation.Do();
            await operation.CommitAsync();
        }

        public Task<string> LogInUser(Guid clientId, string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> LoginUser()
        {
            return Guid.NewGuid();
        }
    }
}
