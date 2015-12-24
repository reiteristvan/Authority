using System.Threading.Tasks;

namespace IdentityServer.EntityFramework
{
    public interface IIdentityServerContext : ISafeIdentityServerContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}