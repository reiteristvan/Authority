using System.Data;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork
{
    public abstract class OperationWithNoReturn : Operation
    {
        protected OperationWithNoReturn(IIdentityServerContext identityServerContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
            : base(identityServerContext, isolationLevel)
        {
            
        }

        public abstract void Do();
    }

    public abstract class SafeOperationWithNoReturn : SafeOperation
    {
        protected SafeOperationWithNoReturn(IIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {

        }

        public abstract void Do();
    }
}
