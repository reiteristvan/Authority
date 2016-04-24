using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;

namespace Authority.Operations.Account
{
    public sealed class UserActivation : OperationWithNoReturnAsync
    {
        private readonly Guid _clientId;
        private readonly Guid _activationCode;

        public UserActivation(IAuthorityContext authorityContext, Guid clientId, Guid activationCode)
            : base(authorityContext)
        {
            _clientId = clientId;
            _activationCode = activationCode;
        }

        public override async Task Do()
        {
            Product product = await Context.Products.FirstOrDefaultAsync(p => p.ClientId == _clientId);

            if (_activationCode == Guid.Empty || product == null || !product.IsActive || !product.IsPublic)
            {
                throw new RequirementFailedException(AccountErrorCodes.FailedActivation);
            }

            User user = await Context.Users
                .FirstOrDefaultAsync(u => u.ProductId == product.Id && u.PendingRegistrationId == _activationCode);

            if (user == null || !user.IsPending)
            {
                throw new RequirementFailedException(AccountErrorCodes.FailedActivation);
            }

            user.PendingRegistrationId = Guid.Empty;
            user.IsPending = false;
        }
    }
}
