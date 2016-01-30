using System.Data;
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
        protected SafeOperationWithReturnValue(IIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {

        }

        public abstract TReturn Do();
    }
}
