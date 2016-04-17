using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;

namespace Authority.Operations.Products
{
    public sealed class CreateProduct : Operation
    {
        public CreateProduct(IAuthorityContext AuthorityContext)
            : base(AuthorityContext)
        {
            
        }

        public async Task<Guid> Create(Guid ownerId, string name, string siteUrl, string landingPage)
        {
            await Check(() => IsNameAvailable(name), ProductErrorCodes.NameNotAvailable);

            Product product = new Product
            {
                OwnerId = ownerId,
                Name = name,
                IsActive = true,
                IsPublic = false,
                SiteUrl = siteUrl,
                LandingPage = landingPage
            };

            Context.Products.Add(product);

            return product.Id;
        }

        private async Task<bool> IsNameAvailable(string name)
        {
            Product product = await Context.Products.FirstOrDefaultAsync(p => p.Name == name);
            return product == null;
        }
    }
}
