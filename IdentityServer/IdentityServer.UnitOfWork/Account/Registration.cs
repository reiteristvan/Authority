using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Utilities;

namespace IdentityServer.UnitOfWork.Account
{
    public sealed class Registration : OperationWithReturnValueAsync<User>
    {
        private readonly string _email;
        private readonly string _username;
        private readonly string _password;
        private readonly PasswordService _passwordService;

        public Registration(IIdentityServerContext identityServerContext, string email, string username, string password)
            : base(identityServerContext)
        {
            _email = email;
            _username = username;
            _password = password;
            _passwordService = new PasswordService();
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

        public override async Task<User> Do()
        {
            await Check(() => IsUserExist(_email), AccountErrorCodes.EmailAlreadyExists);
            await Check(() => IsUsernameAvailable(_username), AccountErrorCodes.UsernameNotAvailable);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(_password);
            byte[] saltBytes = _passwordService.CreateSalt();
            byte[] hashBytes = _passwordService.CreateHash(passwordBytes, saltBytes);

            User user = new User
            {
                Email = _email,
                Username = _username,
                Salt = Convert.ToBase64String(saltBytes),
                PasswordHash = Convert.ToBase64String(hashBytes),
                IsPending = true,
                PendingRegistrationId = Guid.NewGuid(),
                IsActive = true,
                IsExternal = false
            };

            return user;
        }
    }
}
