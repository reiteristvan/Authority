using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.Services.Dto;
using Authority.Services.Extensions;
using Authority.Operations.Products;

namespace Authority.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductSimpleDto>> GetProductsOfUser(Guid ownerId);
        Task<Guid> Create(Guid ownerId, string name, string siteUrl, string notificationEmail, string activationUrl);
        Task<ProductDto> GetProductDetails(Guid ownerId, Guid productId);
        Task<bool> ToggleProductPublish(Guid ownerId, Guid productId);
        Task<Guid> GetApiKeyForProduct(Guid ownerId, Guid productId);
    }

    public sealed class ProductService : IProductService
    {
        private readonly IAuthorityContext _authorityContext;

        public ProductService(IAuthorityContext authorityContext)
        {
            _authorityContext = authorityContext;
        }

        public async Task<IEnumerable<ProductSimpleDto>> GetProductsOfUser(Guid ownerId)
        {
            List<Product> products = await _authorityContext.Products
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
            return products.Select(p => p.ToSimpleDto());
        }

        public async Task<Guid> Create(Guid ownerId, string name, string siteUrl, string notificationEmail, string activationUrl)
        {
            CreateProduct operation = new CreateProduct(_authorityContext, ownerId, name, siteUrl, notificationEmail, activationUrl);
            Guid id = await operation.Do();
            await operation.CommitAsync();

            return id;
        }

        public async Task<ProductDto> GetProductDetails(Guid ownerId, Guid productId)
        {
            GetProductDetails operation = new GetProductDetails(_authorityContext, ownerId, productId);
            Product product = await operation.Do();

            return product.ToDto();
        }

        public async Task<bool> ToggleProductPublish(Guid ownerId, Guid productId)
        {
            ToggleProductPublish operation = new ToggleProductPublish(_authorityContext, ownerId, productId);
            bool publishState = await operation.Do();
            await operation.CommitAsync();

            return publishState;
        }

        public async Task<Guid> GetApiKeyForProduct(Guid ownerId, Guid productId)
        {
            Product product = await _authorityContext.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.OwnerId == ownerId);

            return product.ApiKey;
        }
    }
}
