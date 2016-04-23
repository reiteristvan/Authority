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
                IsPublic = product.IsPublic,
                Site = product.SiteUrl,
                NotificationEmail = product.NotificationEmail,
                ActivationUrl = product.ActivationUrl,
                Name = product.Name,
                ClientId = product.ClientId,
                Policies = product.Policies.Select(p => p.ToDto()).ToList()
            };
        }
    }
}
