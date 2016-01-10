using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Extensions;

namespace IdentityServer.UnitOfWork.Products
{
    public sealed class GetClaimsOfProduct : SafeOperation
    {
        public GetClaimsOfProduct(ISafeIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {
            
        }

        public async Task<IEnumerable<Claim>>  Retrieve(Guid userId, Guid productId)
        {
            Product product = await _identityServerContext.Products
                .Include(p => p.Policies)
                .Include(p => p.Policies.Select(po => po.Claims))
                .FirstOrDefaultAsync(p => p.Id == productId);

            Check(() => product.OwnerId == userId, ProductErrorCodes.UnAuthorizedAccess);

            return product.Policies.SelectMany(p => p.Claims).DistinctBy(c => c.Id);
        }
    }
}
