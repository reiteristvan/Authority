using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.Operations.Utilities;

namespace Authority.Operations.Account
{
    public sealed class UserRegistration : OperationWithReturnValueAsync<User>
    {
        private readonly Guid _clientId;
        private readonly string _email;
        private readonly string _username;
        private readonly string _password;
        private readonly PasswordService _passwordService;

        public UserRegistration(IAuthorityContext AuthorityContext, 
            Guid clientId, string email, string username, string password)
            : base(AuthorityContext)
        {
            _clientId = clientId;
            _email = email;
            _username = username;
            _password = password;
            _passwordService = new PasswordService();
        }

        private async Task<bool> IsUserExist()
        {
            User user = await Context.Users.FirstOrDefaultAsync(u => u.Email == _email);
            return user == null;
        }

        private async Task<bool> IsUsernameAvailable()
        {
            User user = await Context.Users.FirstOrDefaultAsync(p => p.Username == _username);
            return user == null;
        }

        public override async Task<User> Do()
        {
            await Check(() => IsUserExist(), AccountErrorCodes.EmailAlreadyExists);
            await Check(() => IsUsernameAvailable(), AccountErrorCodes.UsernameNotAvailable);

            Product product = await Context.Products.FirstOrDefaultAsync(p => p.ClientId == _clientId);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(_password);
            byte[] saltBytes = _passwordService.CreateSalt();
            byte[] hashBytes = _passwordService.CreateHash(passwordBytes, saltBytes);

            User user = new User
            {
                ProductId = product.Id,
                Email = _email,
                Username = _username,
                Salt = Convert.ToBase64String(saltBytes),
                PasswordHash = Convert.ToBase64String(hashBytes),
                IsPending = true,
                PendingRegistrationId = Guid.NewGuid(),
                IsActive = true,
                IsExternal = false
            };

            product.Users.Add(user);

            return user;
        }
    }
}
