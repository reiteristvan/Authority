using System;
using System.Data;
using System.Threading.Tasks;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork
{
    public abstract class Operation
    {
        private readonly IIdentityServerContext _identityServerContext;

        protected Operation(IIdentityServerContext identityServerContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _identityServerContext = identityServerContext;
            _identityServerContext.BeginTransaction(isolationLevel);
        }

        protected ISafeIdentityServerContext Context { get { return _identityServerContext; } }

        public async Task Check(Func<Task<bool>> condition, int errorCode)
        {
            if (!(await condition()))
            {
                _identityServerContext.RollbackTransaction();
                throw new RequirementFailedException(errorCode);
            }
        }

        public void Check(Func<bool> condition, int errorCode)
        {
            if (!condition())
            {
                _identityServerContext.RollbackTransaction();
                throw new RequirementFailedException(errorCode);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _identityServerContext.SaveChangesAsync();
                _identityServerContext.CommitTransaction();
            }
            catch (Exception)
            {
                _identityServerContext.RollbackTransaction();
                throw;
            }
        }

        public void Commit()
        {
            try
            {
                _identityServerContext.SaveChanges();
                _identityServerContext.CommitTransaction();
            }
            catch (Exception)
            {
                _identityServerContext.RollbackTransaction();
                throw;
            }
        }
    }
}
