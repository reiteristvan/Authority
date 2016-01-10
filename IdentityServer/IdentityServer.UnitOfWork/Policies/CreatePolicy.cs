using System;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Policies
{
    public class CreatePolicy : Operation
    {
        public CreatePolicy(IIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {
            
        }

        public async Task<Policy> Create(Guid userId, Guid productId, string name)
        {
            Product product = await Context.Products
                .FirstOrDefaultAsync(p => p.Id == productId);

            Check(() => IsUserOwnsProduct(userId, product), PolicyErrorCodes.UnAuthorizedAccess);

            Policy policy = new Policy
            {
                Name = name
            };

            Context.Policies.Add(policy);

            return policy;
        }

        public bool IsUserOwnsProduct(Guid userId, Product product)
        {
            if (product == null)
            {
                return false;
            }

            return product.OwnerId == userId;
        }
    }
}
