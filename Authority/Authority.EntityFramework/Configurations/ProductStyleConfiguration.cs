using System.Data.Entity.ModelConfiguration;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework.Configurations
{
    public sealed class ProductStyleConfiguration : EntityTypeConfiguration<ProductStyle>
    {
        public ProductStyleConfiguration()
        {
            ToTable("ProductStyles");

            HasKey(e => e.Id);

            Property(p => p.Logo).IsRequired();
        }
    }
}
