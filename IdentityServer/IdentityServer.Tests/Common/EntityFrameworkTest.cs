using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Authority.DomainModel;
using Authority.EntityFramework;
using Moq;

namespace Authority.Tests.Common
{
    public static class EntityFrameworkTest
    {
        public static Mock<IAuthorityContext> CreateMockContext()
        {
            Mock<IAuthorityContext> mockContext = new Mock<IAuthorityContext>();
            mockContext.Setup(m => m.BeginTransaction(It.IsAny<IsolationLevel>())).Verifiable();
            mockContext.Setup(m => m.CommitTransaction()).Verifiable();
            mockContext.Setup(m => m.RollbackTransaction()).Verifiable();

            return mockContext;
        }

        public static DbSet<TEntity> CreateEmptyDbSet<TEntity>()
            where TEntity : class
        {
            IQueryable<TEntity> data = new List<TEntity>().AsQueryable();

            Mock<DbSet<TEntity>> mockDataSet = new Mock<DbSet<TEntity>>();

            mockDataSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Developer>(data.Provider));

            mockDataSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockDataSet.As<IQueryable<TEntity>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockDataSet.As<IQueryable<TEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            mockDataSet.As<IDbAsyncEnumerable<TEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(data.GetEnumerator()));

            return mockDataSet.Object;
        }

        public static DbSet<TEntity> CreateDbSetWithData<TEntity>(IEnumerable<TEntity> input)
            where TEntity : class
        {
            IQueryable<TEntity> data = new List<TEntity>(input).AsQueryable();

            Mock<DbSet<TEntity>> mockDataSet= new Mock<DbSet<TEntity>>();

            mockDataSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Developer>(data.Provider));

            mockDataSet.As<IQueryable<TEntity>>()
                .Setup(m => m.Expression)
                .Returns(data.Expression);

            mockDataSet.As<IQueryable<TEntity>>()
                .Setup(m => m.ElementType)
                .Returns(data.ElementType);

            mockDataSet.As<IQueryable<TEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(data.GetEnumerator());

            mockDataSet.As<IDbAsyncEnumerable<TEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TEntity>(data.GetEnumerator()));

            return mockDataSet.Object;
        }
    }
}
