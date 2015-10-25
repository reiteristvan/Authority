using System.Data.Entity.ModelConfiguration;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework.Configurations
{
    public sealed class DeveloperConfiguration : EntityTypeConfiguration<Developer>
    {
        public DeveloperConfiguration()
        {
            ToTable("Developers");

            HasMany(d => d.Applications);
        }
    }
}
