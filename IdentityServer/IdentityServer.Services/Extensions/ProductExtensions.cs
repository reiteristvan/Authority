using IdentityServer.DomainModel;
using IdentityServer.Services.Dto;

namespace IdentityServer.Services.Extensions
{
    public static class ProductExtensions
    {
        public static ProductDto ToSimpleDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                IsActive = product.IsActive,
                IsPublic = product.IsPublic,
                Name = product.Name
            };
        }
    }
}
