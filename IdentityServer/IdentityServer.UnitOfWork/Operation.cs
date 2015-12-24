using System;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork
{
    public abstract class Operation : IDisposable
    {
        protected readonly IIdentityServerContext _identityServerContext;
        protected readonly DbContextTransaction _transaction;

        protected Operation(IIdentityServerContext identityServerContext,
                             IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _identityServerContext = identityServerContext;
            _transaction = _identityServerContext.Database.BeginTransaction(isolationLevel);
        }

        public async Task Commit()
        {
            try
            {
                await _identityServerContext.SaveChangesAsync();
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
