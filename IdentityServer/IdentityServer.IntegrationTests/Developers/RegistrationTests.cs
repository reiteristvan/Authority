using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.IntegrationTests.Common;
using IdentityServer.IntegrationTests.Fixtures;
using IdentityServer.UnitOfWork;
using IdentityServer.UnitOfWork.Developers;
using Xunit;

namespace IdentityServer.IntegrationTests.Developers
{
    public sealed class RegistrationTests : IClassFixture<SimpleFixture>
    {
        private readonly SimpleFixture _fixture;

        public RegistrationTests(SimpleFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task RegistrationShouldSuccess()
        {
            string email = RandomData.Email();
            string username = RandomData.RandomString();
            string password = RandomData.RandomString(12, true);

            DeveloperRegistration operation = new DeveloperRegistration(_fixture.Context);
            Developer developer = await operation.Register(email, username, password);
            
            await operation.CommitAsync();

            Developer developerInDb = await _fixture.Context.Developers
                .FirstOrDefaultAsync(d => d.Id == developer.Id);

            Assert.NotNull(developerInDb);
            Assert.Equal(developerInDb.DisplayName, username);
        }

        [Fact]
        public async Task RegistrationDuplicateUserShouldFail()
        {
            string email = RandomData.Email();
            string username = RandomData.RandomString();
            string password = RandomData.RandomString(12, true);

            DeveloperRegistration operation = new DeveloperRegistration(_fixture.Context);
            Developer developer = await operation.Register(email, username, password);

            await operation.CommitAsync();

            await AssertExtensions.ThrowAsync<RequirementFailedException>(async () =>
            {
                DeveloperRegistration failOperation = new DeveloperRegistration(_fixture.Context);
                Developer failDeveloper = await failOperation.Register(email, username, password);
            });          
        }

        [Fact]
        public async Task RegistrationDuplicateUsernameShoudlFail()
        {
            string email = RandomData.Email();
            string username = RandomData.RandomString();
            string password = RandomData.RandomString(12, true);

            DeveloperRegistration operation = new DeveloperRegistration(_fixture.Context);
            Developer developer = await operation.Register(email, username, password);

            await operation.CommitAsync();

            await AssertExtensions.ThrowAsync<RequirementFailedException>(async () =>
            {
                string newEmail = RandomData.Email();
                DeveloperRegistration failOperation = new DeveloperRegistration(_fixture.Context);
                Developer failDeveloper = await failOperation.Register(newEmail, username, password);
            });     
        }
    }
}
