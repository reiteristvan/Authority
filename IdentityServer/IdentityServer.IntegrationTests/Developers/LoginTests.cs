using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.IntegrationTests.Common;
using IdentityServer.IntegrationTests.Fixtures;
using IdentityServer.UnitOfWork.Developers;
using Xunit;

namespace IdentityServer.IntegrationTests.Developers
{
    public sealed class LoginTests : IClassFixture<SimpleFixture>
    {
        private readonly SimpleFixture _fixture;

        public LoginTests(SimpleFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task LoginShouldSucceed()
        {
            string password = "12Budapest99";
            Developer developer = await Operations.RegisterAndActivateDeveloper(_fixture.Context, password);

            DeveloperLogin login = new DeveloperLogin(_fixture.Context);
            bool result = await login.ValidateLogin(developer.Email, password);

            Assert.True(result);
        }

        [Fact]
        public async Task LoginNotActivatedShouldFail()
        {
            string password = "12Budapest99";
            Developer developer = await Operations.RegisterDeveloper(_fixture.Context, password);

            DeveloperLogin login = new DeveloperLogin(_fixture.Context);
            bool result = await login.ValidateLogin(developer.Email, password);

            Assert.False(result);
        }

        [Fact]
        public async Task LoginNotActiveShouldFail()
        {
            string password = "12Budapest99";
            Developer developer = await Operations.RegisterAndActivateDeveloper(_fixture.Context, password);

            developer.IsActive = false;
            _fixture.Context.Entry(developer).State = EntityState.Modified;
            await _fixture.Context.SaveChangesAsync();

            DeveloperLogin login = new DeveloperLogin(_fixture.Context);
            bool result = await login.ValidateLogin(developer.Email, password);

            Assert.False(result);
        }
    }
}
