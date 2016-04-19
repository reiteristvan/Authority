using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.Operations.Utilities;

namespace Authority.Operations.Account
{
    public sealed class Registration : OperationWithReturnValueAsync<User>
    {
        private readonly string _email;
        private readonly string _username;
        private readonly string _password;
        private readonly PasswordService _passwordService;

        public Registration(IAuthorityContext AuthorityContext, string email, string username, string password)
            : base(AuthorityContext)
        {
            _email = email;
            _username = username;
            _password = password;
            _passwordService = new PasswordService();
        }

        private async Task<bool> IsUserExist()
        {
            User user = await Context.Users.FirstOrDefaultAsync(u => u.Email == _email);
            return user != null;
        }

        private async Task<bool> IsUsernameAvailable()
        {
            User user = await Context.Users.FirstOrDefaultAsync(p => p.Username == _username);
            return user != null;
        }

        public override async Task<User> Do()
        {
            await Check(() => IsUserExist(), AccountErrorCodes.EmailAlreadyExists);
            await Check(() => IsUsernameAvailable(), AccountErrorCodes.UsernameNotAvailable);

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
