﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.Services.Dto;
using IdentityServer.Services.Extensions;
using IdentityServer.UnitOfWork.Products;

namespace IdentityServer.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsOfUser(Guid ownerId);
        Task<Guid> Create(Guid ownerId, string name);
    }

    public sealed class ProductService : IProductService
    {
        private readonly IIdentityServerContext _identityServerContext;

        public ProductService(IIdentityServerContext identityServerContext)
        {
            _identityServerContext = identityServerContext;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsOfUser(Guid ownerId)
        {
            List<Product> products = await _identityServerContext.Products
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
            return products.Select(p => p.ToSimpleDto());
        }

        public async Task<Guid> Create(Guid ownerId, string name)
        {
            CreateProduct operation = new CreateProduct(_identityServerContext);
            Guid id = await operation.Create(ownerId, name);
            await operation.CommitAsync();

            return id;
        }
    }
}
