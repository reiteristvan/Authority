using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Utilities;

namespace IdentityServer.UnitOfWork.Developers
{
    public sealed class DeveloperRegistration : Operation
    {
        private readonly PasswordService _passwordService;

        public DeveloperRegistration(IIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {
            _passwordService = new PasswordService();
        }

        public async Task<Developer> Register(string email, string displayname, string password)
        {
            await Check(() => IsUserNotExist(email), DevelopersErrorCodes.EmailAlreadyExists);
            await Check(() => IsUsernameAvailable(displayname), DevelopersErrorCodes.DisplayNameNotAvailable);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = _passwordService.CreateSalt();
            byte[] hashBytes = _passwordService.CreateHash(passwordBytes, saltBytes);

            Developer developer = new Developer
            {
                Email = email,
                DisplayName = displayname,
                Salt = Convert.ToBase64String(saltBytes),
                PasswordHash = Convert.ToBase64String(hashBytes),
                IsActive = true,
                IsPending = true,
                PendingRegistrationId = Guid.NewGuid(),
                Created = DateTime.UtcNow
            };

            _identityServerContext.Developers.Add(developer);

            return developer;
        }

        private async Task<bool> IsUserNotExist(string email)
        {
            Developer user = await _identityServerContext.Developers.FirstOrDefaultAsync(u => u.Email == email);
            return user == null;
        }

        private async Task<bool> IsUsernameAvailable(string displayName)
        {
            Developer user = await _identityServerContext.Developers.FirstOrDefaultAsync(p => p.DisplayName == displayName);
            return user == null;
        }
    }
}
