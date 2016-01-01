using System;
using System.Threading.Tasks;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork
{
    public abstract class SafeOperation
    {
        protected readonly ISafeIdentityServerContext _identityServerContext;

        protected SafeOperation(ISafeIdentityServerContext identityServerContext)
        {
            _identityServerContext = identityServerContext;
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
