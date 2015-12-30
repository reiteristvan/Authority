using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.Tests.Common;
using IdentityServer.UnitOfWork;
using IdentityServer.UnitOfWork.Developers;
using Moq;
using Xunit;

namespace IdentityServer.Tests.Developers
{
    public sealed class DeveloperRegistrationTests
    {
        [Fact]
        public void PositiveRegistration()
        {
            Mock<IIdentityServerContext> mockContext = EntityFrameworkTest.CreateMockContext();
            mockContext
                .Setup(m => m.Developers)
                .Returns(EntityFrameworkTest.CreateEmptyDbSet<Developer>());

            string email = "test@test.com";
            string username = "testmaster";
            string password = "12Budapest99";

            DeveloperRegistration operation = new DeveloperRegistration(mockContext.Object);

            Developer result = operation.Register(email, username, password).Result;
            operation.Commit();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task RegistrationFailsWithExistingUser()
        {
            List<Developer> developers = new List<Developer>
            {
                new Developer
                {
                    Email = "test@test.com", DisplayName = "testmaster"
                }
            };

            Mock<IIdentityServerContext> mockContext = EntityFrameworkTest.CreateMockContext();
            mockContext
                .Setup(m => m.Developers)
                .Returns(EntityFrameworkTest.CreateDbSetWithData<Developer>(developers));

            string email = "test@test.com";
            string username = "testmaster";
            string password = "12Budapest99";

            DeveloperRegistration operation = new DeveloperRegistration(mockContext.Object);

            await AssertExtensions.ThrowAsync<RequirementFailedException>(async () =>
            {
                Developer result = await operation.Register(email, username, password);
                operation.Commit();
            });
        }
    }
}
