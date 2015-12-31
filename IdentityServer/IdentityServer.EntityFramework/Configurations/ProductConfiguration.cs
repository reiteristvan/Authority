﻿using System.Data.Entity.ModelConfiguration;
using IdentityServer.DomainModel;

namespace IdentityServer.EntityFramework.Configurations
{
    public sealed class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Products");

            HasKey(e => e.Id);

            Property(p => p.OwnerId).IsRequired();
            Property(p => p.Name).IsRequired().HasMaxLength(128);
            Property(p => p.IsPublic).IsRequired();
            Property(p => p.IsActive).IsRequired();

            HasMany(p => p.Policies);
        }
    }
}
