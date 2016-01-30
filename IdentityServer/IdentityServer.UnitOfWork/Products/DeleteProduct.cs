using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Products
{
    public sealed class DeleteProduct : Operation
    {
        public DeleteProduct(IIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {
            
        }

        public async Task Delete(Guid userId, Guid productId)
        {
            
        }
    }
}
