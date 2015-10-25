using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework
{
    public interface IIdentityServerContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Developer> Developers { get; set; } 
        DbSet<Product> Products { get; set; }
        DbSet<ClientApplication> ClientApplications { get; set; }
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        DbSet Set(Type entityType);
        DbEntityEntry Entry(object entity);
    }
}