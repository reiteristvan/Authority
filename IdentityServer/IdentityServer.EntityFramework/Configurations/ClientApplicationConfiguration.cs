using System.Data.Entity.ModelConfiguration;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework.Configurations
{
    public sealed class ClientApplicationConfiguration : EntityTypeConfiguration<ClientApplication>
    {
        public ClientApplicationConfiguration()
        {
            ToTable("ClientApplications");

            HasKey(e => e.Id);

            Property(c => c.Name).HasMaxLength(128);
            Property(c => c.ApplicationId).HasMaxLength(128);
            Property(c => c.ApplicationSecret).HasMaxLength(128);
            Property(c => c.RedirectUrl).HasMaxLength(128);
            Property(c => c.IsEnabled);
            
            HasRequired(c => c.Product);
        }
    }
}
