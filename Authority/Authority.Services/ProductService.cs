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
    }

    public sealed class ProductService : IProductService
    {
        private readonly IAuthorityContext _AuthorityContext;

        public ProductService(IAuthorityContext AuthorityContext)
        {
            _AuthorityContext = AuthorityContext;
        }

        public async Task<IEnumerable<ProductSimpleDto>> GetProductsOfUser(Guid ownerId)
        {
            List<Product> products = await _AuthorityContext.Products
                .Where(p => p.OwnerId == ownerId)
                .ToListAsync();
            return products.Select(p => p.ToSimpleDto());
        }

        public async Task<Guid> Create(Guid ownerId, string name, string siteUrl, string landingPage)
        {
            CreateProduct operation = new CreateProduct(_AuthorityContext);
            Guid id = await operation.Create(ownerId, name, siteUrl, landingPage);
            await operation.CommitAsync();

            return id;
        }

        public async Task<ProductDto> GetProductDetails(Guid ownerId, Guid proudctId)
        {
            GetProductDetails operation = new GetProductDetails(_AuthorityContext);
            Product product = await operation.GetDetails(ownerId, proudctId);

            return product.ToDto();
        }
    }
}
