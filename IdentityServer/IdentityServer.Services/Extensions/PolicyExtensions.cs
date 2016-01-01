using System.Linq;
using IdentityServer.DomainModel;
using IdentityServer.Services.Dto;

namespace IdentityServer.Services.Extensions
{
    public static class PolicyExtensions
    {
        public static PolicyDto ToDto(this Policy policy)
        {
            return new PolicyDto
            {
                Id = policy.Id,
                Name = policy.Name,
                Claims = policy.Claims.Select(c => c.ToDto()).ToList()
            };
        }
    }
}
