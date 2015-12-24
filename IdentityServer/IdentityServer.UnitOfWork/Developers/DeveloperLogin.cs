using System;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Utilities;

namespace IdentityServer.UnitOfWork.Developers
{
    public sealed class DeveloperLogin : SafeOperation
    {
        private readonly PasswordService _passwordService;

        public DeveloperLogin(ISafeIdentityServerContext identityServerContext)
            :base(identityServerContext)
        {
            _passwordService = new PasswordService();
        }

        public async Task<bool> ValidateLogin(string email, string password)
        {
            Developer user = await _identityServerContext.Developers.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = _passwordService.CreateSalt();
            byte[] hashBytes = _passwordService.CreateHash(passwordBytes, saltBytes);
            string hash = Convert.ToBase64String(hashBytes);

            return user.PasswordHash == hash;
        }
    }
}
