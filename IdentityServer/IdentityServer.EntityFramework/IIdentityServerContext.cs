using System.Data;
using System.Threading.Tasks;

namespace IdentityServer.EntityFramework
{
    public interface IIdentityServerContext : ISafeIdentityServerContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void BeginTransaction(IsolationLevel isolationLevel);
        void CommitTransaction();
        void RollbackTransaction();
    }
}