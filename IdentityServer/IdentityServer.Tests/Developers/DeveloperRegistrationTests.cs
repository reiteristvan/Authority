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
            IQueryable<Developer> developers = new List<Developer>().AsQueryable();

            Mock<DbSet<Developer>> mockDeveloperSet = new Mock<DbSet<Developer>>();
            mockDeveloperSet.As<IQueryable<Developer>>().Setup(m => m.Provider).Returns(new TestDbAsyncQueryProvider<Developer>(developers.Provider));
            mockDeveloperSet.As<IQueryable<Developer>>().Setup(m => m.Expression).Returns(developers.Expression);
            mockDeveloperSet.As<IQueryable<Developer>>().Setup(m => m.ElementType).Returns(developers.ElementType);
            mockDeveloperSet.As<IQueryable<Developer>>().Setup(m => m.GetEnumerator()).Returns(developers.GetEnumerator());
            mockDeveloperSet.As<IDbAsyncEnumerable<Developer>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Developer>(developers.GetEnumerator()));

            Mock<IIdentityServerContext> mockContext = new Mock<IIdentityServerContext>();
            mockContext.Setup(m => m.BeginTransaction(It.IsAny<IsolationLevel>())).Verifiable();
            mockContext.Setup(m => m.CommitTransaction()).Verifiable();
            mockContext.Setup(m => m.RollbackTransaction()).Verifiable();

            mockContext.Setup(m => m.Developers).Returns(mockDeveloperSet.Object);

            string email = "test@test.com";
            string username = "testmaster";
            string password = "12Budapest99";

            DeveloperRegistration operation = new DeveloperRegistration(mockContext.Object);

            Developer result = operation.Register(email, username, password).Result;
            operation.Commit();

            Assert.NotNull(result);
        }
    }
}
