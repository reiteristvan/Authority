using System;
using System.Threading.Tasks;
using Authority.EntityFramework;

namespace Authority.Operations
{
    public abstract class SafeOperation
    {
        protected readonly ISafeAuthorityContext _AuthorityContext;

        protected SafeOperation(ISafeAuthorityContext AuthorityContext)
        {
            _AuthorityContext = AuthorityContext;
        }

        public async Task Check(Func<Task<bool>> condition, int errorCode)
        {
            if (!(await condition()))
            {
                throw new RequirementFailedException(errorCode);
            }
        }

        public void Check(Func<bool> condition, int errorCode)
        {
            if (!condition())
            {
                throw new RequirementFailedException(errorCode);
            }
        }
    }
}
