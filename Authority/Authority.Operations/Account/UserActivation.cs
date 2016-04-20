using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;

namespace Authority.Operations.Account
{
    public sealed class UserActivation : OperationWithNoReturnAsync
    {
        private readonly Guid _activationCode;

        public UserActivation(IAuthorityContext authorityContext, Guid activationCode)
            : base(authorityContext)
        {
            _activationCode = activationCode;
        }

        public override async Task Do()
        {
            if (_activationCode == Guid.Empty)
            {
                throw new RequirementFailedException(AccountErrorCodes.FailedActivation);
            }

            User user = await Context.Users.FirstOrDefaultAsync(u => u.PendingRegistrationId == _activationCode);

            if (user == null || !user.IsPending)
            {
                throw new RequirementFailedException(AccountErrorCodes.FailedActivation);
            }

            user.PendingRegistrationId = Guid.Empty;
            user.IsPending = false;
        }
    }
}
