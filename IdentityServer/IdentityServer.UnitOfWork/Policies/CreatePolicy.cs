using System.Collections.Generic;
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

        public async Task<Policy> Create(string name, List<Claim> claims)
        {
            Policy policy = new Policy();

            policy.Claims = claims;

            return policy;
        } 
    }
}
