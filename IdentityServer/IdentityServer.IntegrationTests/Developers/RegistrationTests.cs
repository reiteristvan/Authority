using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Developers;
using Xunit;

namespace IdentityServer.IntegrationTests.Developers
{
    public sealed class RegistrationTests
    {
        [Fact]
        public async Task RegistrationShouldSuccess()
        {
            string email = Random.Email();

            DeveloperRegistration operation = new DeveloperRegistration(new IdentityServerContext());
            Developer developer = await operation.Register(email, "", "");
        }
    }
}
