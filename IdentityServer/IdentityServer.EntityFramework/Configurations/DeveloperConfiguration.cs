﻿using System.Data.Entity.ModelConfiguration;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework.Configurations
{
    public sealed class DeveloperConfiguration : EntityTypeConfiguration<Developer>
    {
        public DeveloperConfiguration()
        {
            ToTable("Developers");

            HasKey(e => e.Id);

            Property(d => d.Email).IsRequired().HasMaxLength(128);
            Property(d => d.PasswordHash).IsRequired().HasMaxLength(128);
            Property(d => d.Salt).IsRequired().HasMaxLength(128);
            Property(d => d.IsPending).IsRequired();
            Property(d => d.IsActive).IsRequired();
            Property(d => d.PendingRegistrationId).IsRequired();

            HasMany(d => d.Applications);
        }
    }
}
