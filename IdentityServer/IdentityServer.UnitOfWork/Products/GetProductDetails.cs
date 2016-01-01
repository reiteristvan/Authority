using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Products
{
    public sealed class GetProductDetails : SafeOperation
    {
        public GetProductDetails(ISafeIdentityServerContext safeIdentityServerContext)
            : base(safeIdentityServerContext)
        {
            
        }

        public async Task<Product> GetDetails(Guid userId, Guid productId)
        {
            Product product = await _identityServerContext.Products
                .Include(p => p.Policies)
                .Include(p => p.Policies.Select(po => po.Claims))
                .FirstOrDefaultAsync(p => p.Id == productId);

            Check(() => product.OwnerId == userId, ProductErrorCodes.UnAuthorizedAccess);

            return product;
        }
    }
}
