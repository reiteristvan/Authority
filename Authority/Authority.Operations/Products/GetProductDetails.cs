using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;

namespace Authority.Operations.Products
{
    public sealed class GetProductDetails : SafeOperation
    {
        public GetProductDetails(ISafeAuthorityContext safeAuthorityContext)
            : base(safeAuthorityContext)
        {
            
        }

        public async Task<Product> GetDetails(Guid userId, Guid productId)
        {
            Product product = await _authorityContext.Products
                .Include(p => p.Policies)
                .Include(p => p.Policies.Select(po => po.Claims))
                .FirstOrDefaultAsync(p => p.Id == productId);

            Check(() => product.OwnerId == userId, ProductErrorCodes.UnAuthorizedAccess);

            return product;
        }
    }
}
