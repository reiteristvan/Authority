using System.Linq;
using Authority.DomainModel;
using Authority.Services.Dto;

namespace Authority.Services.Extensions
{
    public static class ProductExtensions
    {
        public static ProductSimpleDto ToSimpleDto(this Product product)
        {
            return new ProductSimpleDto
            {
                Id = product.Id,
                IsActive = product.IsActive,
                IsPublic = product.IsPublic,
                Name = product.Name
            };
        }

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                IsActive = product.IsActive,
                IsPublic = product.IsPublic,
                Name = product.Name,
                Policies = product.Policies.Select(p => p.ToDto()).ToList()
            };
        }
    }
}
