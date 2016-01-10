﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Developers
{
    public sealed class DeveloperActivation : Operation
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

            Developer developer = await Context.Developers
                .FirstOrDefaultAsync(d => d.PendingRegistrationId == activationCode);

            if (developer == null || developer.IsPending == false || developer.PendingRegistrationId != activationCode)
            {
                throw new RequirementFailedException(DevelopersErrorCodes.FailedActivation);
            }

            developer.IsPending = false;
            developer.PendingRegistrationId = Guid.Empty;

            Context.Developers.Attach(developer);
            Context.Entry(developer).State = EntityState.Modified;
        }
    }
}
