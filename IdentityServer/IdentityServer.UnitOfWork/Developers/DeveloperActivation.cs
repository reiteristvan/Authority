using System;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Developers
{
    public sealed class DeveloperActivation : UnitOfWork
    {
        public DeveloperActivation(IIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {
            
        }

        public async Task Activation(Guid activationCode)
        {
            if (activationCode == Guid.Empty)
            {
                throw new RequirementFailedException(DevelopersErrorCodes.FailedActivation);
            }

            Developer developer = await _identityServerContext.Developers
                .FirstOrDefaultAsync(d => d.PendingRegistrationId == activationCode);

            if (developer == null || developer.IsPending == false)
            {
                throw new RequirementFailedException(DevelopersErrorCodes.FailedActivation);
            }

            developer.IsPending = false;
            developer.PendingRegistrationId = Guid.Empty;

            _identityServerContext.Developers.Attach(developer);
            _identityServerContext.Entry(developer).State = EntityState.Modified;
        }
    }
}
