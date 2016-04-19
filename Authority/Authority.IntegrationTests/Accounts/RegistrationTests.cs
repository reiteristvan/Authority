using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.IntegrationTests;
using Authority.IntegrationTests.Common;
using Authority.IntegrationTests.Fixtures;
using Xunit;

namespace IdentityServer.IntegrationTests.Accounts
{
    public sealed class RegistrationTests : IClassFixture<SimpleFixture>
    {
        private readonly SimpleFixture _fixture;

        public RegistrationTests(SimpleFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task RegistrationShouldSucceed()
        {
            Product product = await TestOperations.CreateProduct(_fixture.Context);

            string email = RandomData.Email();
            string username = RandomData.RandomString();
            string password = RandomData.RandomString(12, true);
        }
    }
}
