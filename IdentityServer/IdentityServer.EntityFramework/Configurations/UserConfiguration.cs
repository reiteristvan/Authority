using System.Data.Entity.ModelConfiguration;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework.Configurations
{
    public sealed class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            HasKey(e => e.Id);

            Property(u => u.Email).IsRequired().HasMaxLength(256);
            Property(u => u.Username).IsRequired().HasMaxLength(128);

            HasMany(u => u.Claims);
        }
    }
}
