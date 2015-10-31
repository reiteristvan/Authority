using System.Data.Entity.ModelConfiguration;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework.Configurations
{
    public sealed class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Products");

            HasKey(e => e.Id);

            Property(p => p.Name).IsRequired().HasMaxLength(128);

            HasMany(p => p.Policies);
        }
    }
}
