using System.Linq;
using Authority.DomainModel;
using Authority.Services.Dto;

namespace Authority.Services.Extensions
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
