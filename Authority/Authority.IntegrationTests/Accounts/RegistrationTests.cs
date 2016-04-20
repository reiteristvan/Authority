﻿using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.IntegrationTests;
using Authority.IntegrationTests.Common;
using Authority.IntegrationTests.Fixtures;
using Authority.Operations.Account;
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
        public async Task RegistrationShuldSucceed()
        {
            Product product = await TestOperations.CreateProduct(_fixture.Context);

            string email = RandomData.Email();
            string username = RandomData.RandomString();
            string password = RandomData.RandomString(12, true);

            UserRegistration operation = new UserRegistration(_fixture.Context, product.ClientId, email, username, password);
            await operation.Do();
            await operation.CommitAsync();
        }
    }
}
