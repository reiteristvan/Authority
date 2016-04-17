using System.Data;
using System.Threading.Tasks;
using Authority.EntityFramework;

namespace Authority.UnitOfWork
{
    public abstract class OperationWithReturnValue<TReturn> : Operation
    {
        protected OperationWithReturnValue(IAuthorityContext AuthorityContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
            : base(AuthorityContext, isolationLevel)
        {
            
        }

        public abstract TReturn Do();
    }

    public abstract class SafeOperationWithReturnValue<TReturn> : SafeOperation
    {
        protected SafeOperationWithReturnValue(ISafeAuthorityContext AuthorityContext)
            : base(AuthorityContext)
        {

        }

        public abstract TReturn Do();
    }

    public abstract class OperationWithReturnValueAsync<TReturn> : Operation
    {
        protected OperationWithReturnValueAsync(IAuthorityContext AuthorityContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
            : base(AuthorityContext, isolationLevel)
        {

        }

        public abstract Task<TReturn> Do();
    }

    public abstract class SafeOperationWithReturnValueAsync<TReturn> : SafeOperation
    {
        protected SafeOperationWithReturnValueAsync(ISafeAuthorityContext AuthorityContext)
            : base(AuthorityContext)
        {

        }

        public abstract Task<TReturn> Do();
    }
}
