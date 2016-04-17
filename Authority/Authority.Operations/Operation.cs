using System;
using System.Data;
using System.Threading.Tasks;
using Authority.EntityFramework;

namespace Authority.Operations
{
    public abstract class Operation
    {
        private readonly IAuthorityContext _AuthorityContext;

        protected Operation(IAuthorityContext AuthorityContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _AuthorityContext = AuthorityContext;
            _AuthorityContext.BeginTransaction(isolationLevel);
        }

        protected ISafeAuthorityContext Context { get { return _AuthorityContext; } }

        public async Task Check(Func<Task<bool>> condition, int errorCode)
        {
            if (!(await condition()))
            {
                _AuthorityContext.RollbackTransaction();
                throw new RequirementFailedException(errorCode);
            }
        }

        public void Check(Func<bool> condition, int errorCode)
        {
            if (!condition())
            {
                _AuthorityContext.RollbackTransaction();
                throw new RequirementFailedException(errorCode);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _AuthorityContext.SaveChangesAsync();
                _AuthorityContext.CommitTransaction();
            }
            catch (Exception)
            {
                _AuthorityContext.RollbackTransaction();
                throw;
            }
        }

        public void Commit()
        {
            try
            {
                _AuthorityContext.SaveChanges();
                _AuthorityContext.CommitTransaction();
            }
            catch (Exception)
            {
                _AuthorityContext.RollbackTransaction();
                throw;
            }
        }
    }
}
