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
    }
}
