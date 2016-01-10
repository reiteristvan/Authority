using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Utilities;

namespace IdentityServer.UnitOfWork.Account
{
    public sealed class Registration : Operation
    {
        private readonly PasswordService _passwordService;

        public Registration(IIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {
            _passwordService = new PasswordService();
        }

        public async Task<User> Register(string email, string username, string password)
        {
            await Requirement.Check(() => IsUserExist(email), AccountErrorCodes.EmailAlreadyExists);
            await Requirement.Check(() => IsUsernameAvailable(username), AccountErrorCodes.UsernameNotAvailable);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = _passwordService.CreateSalt();
            byte[] hashBytes = _passwordService.CreateHash(passwordBytes, saltBytes);

            User user = new User
            {
                Email = email,
                Username = username,
                Salt = Convert.ToBase64String(saltBytes),
                PasswordHash = Convert.ToBase64String(hashBytes),
                IsPending = true,
                PendingRegistrationId = Guid.NewGuid(),
                IsActive = true,
                IsExternal = false
            };

            return user;
        }

        private async Task<bool> IsUserExist(string email)
        {
            User user = await Context.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }

        private async Task<bool> IsUsernameAvailable(string username)
        {
            User user = await Context.Users.FirstOrDefaultAsync(p => p.Username == username);
            return user != null;
        }
    }
}
