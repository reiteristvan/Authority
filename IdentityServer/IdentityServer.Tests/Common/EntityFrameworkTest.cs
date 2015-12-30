using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using Moq;

namespace IdentityServer.Tests.Common
{
    public static class EntityFrameworkTest
    {
        public static Mock<IIdentityServerContext> CreateMockContext()
        {
            Mock<IIdentityServerContext> mockContext = new Mock<IIdentityServerContext>();
            mockContext.Setup(m => m.BeginTransaction(It.IsAny<IsolationLevel>())).Verifiable();
            mockContext.Setup(m => m.CommitTransaction()).Verifiable();
            mockContext.Setup(m => m.RollbackTransaction()).Verifiable();

            return mockContext;
        }

        public static DbSet<TEntity> CreateEmptyDbSet<TEntity>()
            where TEntity : class
        {
            IQueryable<TEntity> data = new List<TEntity>().AsQueryable();

            Mock<DbSet<TEntity>> mockDeveloperSet = new Mock<DbSet<TEntity>>();

            mockDeveloperSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Developer>(data.Provider));

            mockDeveloperSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockDeveloperSet.As<IQueryable<TEntity>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockDeveloperSet.As<IQueryable<TEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            mockDeveloperSet.As<IDbAsyncEnumerable<TEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(data.GetEnumerator()));

            return mockDeveloperSet.Object;
        }

        public static DbSet<TEntity> CreateDbSetWithData<TEntity>(IEnumerable<TEntity> input)
            where TEntity : class
        {
            IQueryable<TEntity> data = new List<TEntity>(input).AsQueryable();

            Mock<DbSet<TEntity>> mockDeveloperSet = new Mock<DbSet<TEntity>>();

            mockDeveloperSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Developer>(data.Provider));

            mockDeveloperSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockDeveloperSet.As<IQueryable<TEntity>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockDeveloperSet.As<IQueryable<TEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            mockDeveloperSet.As<IDbAsyncEnumerable<TEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(data.GetEnumerator()));

            return mockDeveloperSet.Object;
        }
    }
}
