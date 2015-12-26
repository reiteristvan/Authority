using System.Data.Entity.ModelConfiguration;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework.Configurations
{
    public sealed class ErrorConfiguration : EntityTypeConfiguration<Error>
    {
        public ErrorConfiguration()
        {
            ToTable("Errors");

            HasKey(e => e.Id);

            Property(d => d.Type).IsRequired().HasMaxLength(256);
            Property(d => d.StackTrace).IsRequired();
            Property(d => d.Message).IsRequired();
        }
    }
}
