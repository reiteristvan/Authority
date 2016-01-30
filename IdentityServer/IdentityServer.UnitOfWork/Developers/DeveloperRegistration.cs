using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Utilities;

namespace IdentityServer.UnitOfWork.Developers
{
    public sealed class DeveloperRegistration : OperationWithReturnValue<Task<Developer>>
    {
        private readonly string _email;
        private readonly string _displayname;
        private readonly string _password;
        private readonly PasswordService _passwordService;

        public DeveloperRegistration(IIdentityServerContext identityServerContext, string email, string displayname, string password)
            : base(identityServerContext)
        {
            _email = email;
            _displayname = displayname;
            _password = password;
            _passwordService = new PasswordService();
        }

        //public async Task<Developer> Register(string email, string displayname, string password)
        //{
        //    await Check(() => IsUserNotExist(email), DevelopersErrorCodes.EmailAlreadyExists);
        //    await Check(() => IsUsernameAvailable(displayname), DevelopersErrorCodes.DisplayNameNotAvailable);

        //    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        //    byte[] saltBytes = _passwordService.CreateSalt();
        //    byte[] hashBytes = _passwordService.CreateHash(passwordBytes, saltBytes);

        //    Developer developer = new Developer
        //    {
        //        Email = email,
        //        DisplayName = displayname,
        //        Salt = Convert.ToBase64String(saltBytes),
        //        PasswordHash = Convert.ToBase64String(hashBytes),
        //        IsActive = true,
        //        IsPending = true,
        //        PendingRegistrationId = Guid.NewGuid(),
        //        Created = DateTime.UtcNow
        //    };

        //    Context.Developers.Add(developer);

        //    return developer;
        //}

        private async Task<bool> IsUserNotExist(string email)
        {
            Developer user = await Context.Developers.FirstOrDefaultAsync(u => u.Email == email);
            return user == null;
        }

        private async Task<bool> IsUsernameAvailable(string displayName)
        {
            Developer user = await Context.Developers.FirstOrDefaultAsync(p => p.DisplayName == displayName);
            return user == null;
        }

        public override async Task<Developer> Do()
        {
            await Check(() => IsUserNotExist(_email), DevelopersErrorCodes.EmailAlreadyExists);
            await Check(() => IsUsernameAvailable(_displayname), DevelopersErrorCodes.DisplayNameNotAvailable);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(_password);
            byte[] saltBytes = _passwordService.CreateSalt();
            byte[] hashBytes = _passwordService.CreateHash(passwordBytes, saltBytes);

            Developer developer = new Developer
            {
                Email = _email,
                DisplayName = _displayname,
                Salt = Convert.ToBase64String(saltBytes),
                PasswordHash = Convert.ToBase64String(hashBytes),
                IsActive = true,
                IsPending = true,
                PendingRegistrationId = Guid.NewGuid(),
                Created = DateTime.UtcNow
            };

            Context.Developers.Add(developer);

            return developer;
        }
    }
}
