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
        Task<Guid> Create(Guid ownerId, string name, string siteUrl, string landingPage);
        Task<ProductDto> GetProductDetails(Guid ownerId, Guid proudctId);
        Task ToggleProductPublish(Guid ownerId, Guid productId);
        Task<Guid> GetClientSecretForProduct(Guid ownerId, Guid productId);
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

        public async Task<Guid> Create(Guid ownerId, string name, string siteUrl, string landingPage)
        {
            CreateProduct operation = new CreateProduct(_authorityContext, ownerId, name, siteUrl, landingPage);
            Guid id = await operation.Do();
            await operation.CommitAsync();

            return id;
        }

        public async Task<ProductDto> GetProductDetails(Guid ownerId, Guid proudctId)
        {
            GetProductDetails operation = new GetProductDetails(_authorityContext);
            Product product = await operation.GetDetails(ownerId, proudctId);

            return product.ToDto();
        }

        public async Task ToggleProductPublish(Guid ownerId, Guid productId)
        {
            ToggleProductPublish operation = new ToggleProductPublish(_authorityContext, ownerId, productId);
            await operation.Do();
            await operation.CommitAsync();
        }

        public async Task<Guid> GetClientSecretForProduct(Guid ownerId, Guid productId)
        {
            Product product = await _authorityContext.Products
                .FirstOrDefaultAsync(p => p.Id == productId && p.OwnerId == ownerId);

            return product.ClientSecret;
        }
    }
}
