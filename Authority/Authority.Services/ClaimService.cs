using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.Services.Dto;
using Authority.Services.Extensions;
using Authority.UnitOfWork.Products;

namespace Authority.Services
{
    public interface IClaimService
    {
        Task<IEnumerable<ClaimDto>>  GetClaimsOfProduct(Guid userId, Guid productId);

        Task CreateClaimsForProduct(
            Guid userId, Guid productId, string friendlyName, string issuer, string type, string value);
    }

    public sealed class ClaimService : IClaimService
    {
        private readonly IAuthorityContext _AuthorityContext;

        public ClaimService(IAuthorityContext AuthorityContext)
        {
            _AuthorityContext = AuthorityContext;
        }

        public async Task<IEnumerable<ClaimDto>>  GetClaimsOfProduct(Guid userId, Guid productId)
        {
            GetClaimsOfProduct operation = new GetClaimsOfProduct(_AuthorityContext);
            IEnumerable<Claim> claims = await operation.Retrieve(userId, productId);

            return claims.Select(c => c.ToDto());
        }

        public async Task CreateClaimsForProduct(
            Guid userId, Guid productId, string friendlyName, string issuer, string type, string value)
        {
            CreateClaimForProduct operation = new CreateClaimForProduct(_AuthorityContext);
            await operation.Execute(userId, productId, friendlyName, issuer, type, value);
            await operation.CommitAsync();
        }
    }
}
