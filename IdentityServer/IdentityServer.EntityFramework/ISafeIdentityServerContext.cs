using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework
{
    public interface ISafeIdentityServerContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Developer> Developers { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Claim> Claims { get; set; }
        DbSet<Policy> Policies { get; set; }
        DbSet<ClientApplication> ClientApplications { get; set; }
        Database Database { get; }
        DbChangeTracker ChangeTracker { get; }
        DbContextConfiguration Configuration { get; }
        DbSet Set(Type entityType);
        DbEntityEntry Entry(object entity);
    }
}
