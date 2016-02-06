using System;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Policies
{
    public class CreatePolicy : OperationWithReturnValueAsync<Policy>
    {
        private readonly Guid _userId;
        private readonly Guid _productId;
        private readonly string _name;

        public CreatePolicy(IIdentityServerContext identityServerContext, Guid userId, Guid productId, string name)
            : base(identityServerContext)
        {
            _userId = userId;
            _productId = productId;
            _name = name;
        }

        public bool IsUserOwnsProduct(Guid userId, Product product)
        {
            if (product == null)
            {
                return false;
            }

            return product.OwnerId == userId;
        }

        public override async Task<Policy> Do()
        {
            Product product = await Context.Products
                .FirstOrDefaultAsync(p => p.Id == _productId);

            Check(() => IsUserOwnsProduct(_userId, product), PolicyErrorCodes.UnAuthorizedAccess);

            Policy policy = new Policy
            {
                Name = _name
            };

            Context.Policies.Add(policy);

            return policy;
        }
    }
}
