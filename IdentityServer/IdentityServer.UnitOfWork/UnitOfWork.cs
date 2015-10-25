using System;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork
{
    public abstract class UnitOfWork : IDisposable
    {
        protected readonly IIdentityServerContext _IdentityServerContext;
        protected readonly DbContextTransaction _transaction;

        protected UnitOfWork(IIdentityServerContext identityServerContext,
                             IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _IdentityServerContext = identityServerContext;
            _transaction = _IdentityServerContext.Database.BeginTransaction(isolationLevel);
        }

        public async Task Commit()
        {
            try
            {
                await _IdentityServerContext.SaveChangesAsync();
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
