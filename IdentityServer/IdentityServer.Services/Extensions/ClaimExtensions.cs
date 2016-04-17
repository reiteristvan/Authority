using Authority.DomainModel;
using Authority.Services.Dto;

namespace Authority.Services.Extensions
{
    public static class ClaimExtensions
    {
        public static ClaimDto ToDto(this Claim claim)
        {
            return new ClaimDto
            {
                Id = claim.Id,
                Issuer = claim.Issuer,
                Type = claim.Type,
                Value = claim.Value
            };
        }
    }
}
