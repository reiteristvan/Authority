using System.Data;
using System.Threading.Tasks;
using Authority.EntityFramework;

namespace Authority.UnitOfWork
{
    public abstract class OperationWithNoReturn : Operation
    {
        protected OperationWithNoReturn(IAuthorityContext AuthorityContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
            : base(AuthorityContext, isolationLevel)
        {
            
        }

        public abstract void Do();
    }

    public abstract class SafeOperationWithNoReturn : SafeOperation
    {
        protected SafeOperationWithNoReturn(IAuthorityContext AuthorityContext)
            : base(AuthorityContext)
        {

        }

        public abstract void Do();
    }

    public abstract class OperationWithNoReturnAsync : Operation
    {
        protected OperationWithNoReturnAsync(IAuthorityContext AuthorityContext,
                            IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
            : base(AuthorityContext, isolationLevel)
        {

        }

        public abstract Task Do();
    }

    public abstract class SafeOperationWithNoReturnAsync : SafeOperation
    {
        protected SafeOperationWithNoReturnAsync(IAuthorityContext AuthorityContext)
            : base(AuthorityContext)
        {

        }

        public abstract Task Do();
    }
}
