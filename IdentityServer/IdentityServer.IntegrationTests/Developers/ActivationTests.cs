using System;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.IntegrationTests.Common;
using IdentityServer.IntegrationTests.Fixtures;
using IdentityServer.UnitOfWork;
using IdentityServer.UnitOfWork.Developers;
using Xunit;

namespace IdentityServer.IntegrationTests.Developers
{
    public sealed class ActivationTests : IClassFixture<SimpleFixture>
    {
        private readonly SimpleFixture _fixture;

        public ActivationTests(SimpleFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task ActivationShouldSuccess()
        {
            Developer developer = await Operations.RegisterDeveloper(_fixture.Context);

            DeveloperActivation activationOperation = new DeveloperActivation(_fixture.Context);
            await activationOperation.Activation(developer.PendingRegistrationId);

            await activationOperation.CommitAsync();

            Developer existingDeveloper = _fixture.Context.ReloadEntity<Developer>(developer.Id);

            Assert.False(existingDeveloper.IsPending);
        }

        [Fact]
        public async Task ActivationInvalidActivationCodeShouldFail()
        {
            await AssertExtensions.ThrowAsync<RequirementFailedException>(async () =>
            {
                DeveloperActivation operation = new DeveloperActivation(_fixture.Context);
                await operation.Activation(Guid.Empty);
            }); 
        }
    }
}
