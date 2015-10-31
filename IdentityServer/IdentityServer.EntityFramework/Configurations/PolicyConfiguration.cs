using System.Data.Entity.ModelConfiguration;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework.Configurations
{
    public sealed class PolicyConfiguration : EntityTypeConfiguration<Policy>
    {
        public PolicyConfiguration()
        {
            ToTable("Policies");

            HasKey(e => e.Id);

            Property(p => p.Name);

            HasMany(p => p.Claims);
        }
    }
}
