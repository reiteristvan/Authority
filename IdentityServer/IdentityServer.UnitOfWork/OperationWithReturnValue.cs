using System.Data;
using System.Threading.Tasks;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork
{
    public abstract class OperationWithReturnValue<TReturn> : Operation
    {
        protected OperationWithReturnValue(IIdentityServerContext identityServerContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
            : base(identityServerContext, isolationLevel)
        {
            
        }

        public abstract TReturn Do();
    }

    public abstract class SafeOperationWithReturnValue<TReturn> : SafeOperation
    {
        protected SafeOperationWithReturnValue(ISafeIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {

        }

        public abstract TReturn Do();
    }

    public abstract class OperationWithReturnValueAsync<TReturn> : Operation
    {
        protected OperationWithReturnValueAsync(IIdentityServerContext identityServerContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
            : base(identityServerContext, isolationLevel)
        {

        }

        public abstract Task<TReturn> Do();
    }

    public abstract class SafeOperationWithReturnValueAsync<TReturn> : SafeOperation
    {
        protected SafeOperationWithReturnValueAsync(ISafeIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {

        }

        public abstract Task<TReturn> Do();
    }
}
