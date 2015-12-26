using System;
using System.Data;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork
{
    public abstract class Operation : IDisposable
    {
        protected IIdentityServerContext _identityServerContext;
        protected readonly DbContextTransaction _transaction;

        protected Operation(IIdentityServerContext identityServerContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _identityServerContext = identityServerContext;
            _transaction = _identityServerContext.Database.BeginTransaction(isolationLevel);
        }

        public async Task Check(Func<Task<bool>> condition, int errorCode)
        {
            if (!(await condition()))
            {
                _transaction.Rollback();
                throw new RequirementFailedException(errorCode);
            }
        }

        public void Check(Func<bool> condition, int errorCode)
        {
            if (!condition())
            {
                _transaction.Rollback();
                throw new RequirementFailedException(errorCode);
            }
        }

        public async Task CommitAsync()
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

        public void Commit()
        {
            try
            {
                _identityServerContext.SaveChanges();
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
            if (_transaction != null && _transaction.UnderlyingTransaction.Connection.State != ConnectionState.Closed)
            {
                _transaction.Dispose();
            }
        }
    }
}
