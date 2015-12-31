using System;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Products
{
    public sealed class CreateProduct : Operation
    {
        public CreateProduct(IIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {
            
        }

        public async Task<Guid> Create(Guid ownerId, string name)
        {
            await Check(() => IsNameAvailable(name), ProductErrorCodes.NameNotAvailable);

            Product product = new Product
            {
                OwnerId = ownerId,
                Name = name,
                IsActive = true,
                IsPublic = false
            };

            _identityServerContext.Products.Add(product);

            return product.Id;
        }

        private async Task<bool> IsNameAvailable(string name)
        {
            Product product = await _identityServerContext.Products.FirstOrDefaultAsync(p => p.Name == name);
            return product == null;
        }
    }
}
