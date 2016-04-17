using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.UnitOfWork.Extensions;

namespace Authority.UnitOfWork.Products
{
    public sealed class GetClaimsOfProduct : SafeOperation
    {
        public GetClaimsOfProduct(ISafeAuthorityContext AuthorityContext)
            : base(AuthorityContext)
        {
            
        }

        public async Task<IEnumerable<Claim>>  Retrieve(Guid userId, Guid productId)
        {
            Product product = await _AuthorityContext.Products
                .Include(p => p.Policies)
                .Include(p => p.Policies.Select(po => po.Claims))
                .FirstOrDefaultAsync(p => p.Id == productId);

            Check(() => product.OwnerId == userId, ProductErrorCodes.UnAuthorizedAccess);

            return product.Policies.SelectMany(p => p.Claims).DistinctBy(c => c.Id);
        }
    }
}
