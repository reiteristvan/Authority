using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.Services.Dto;
using IdentityServer.Services.Extensions;
using IdentityServer.UnitOfWork.Products;

namespace IdentityServer.Services
{
    public interface IClaimService
    {
        Task<IEnumerable<ClaimDto>>  GetClaimsOfProduct(Guid userId, Guid productId);

        Task CreateClaimsForProduct(
            Guid userId, Guid productId, string friendlyName, string issuer, string type, string value);
    }

    public sealed class ClaimService : IClaimService
    {
        private readonly IIdentityServerContext _identityServerContext;

        public ClaimService(IIdentityServerContext identityServerContext)
        {
            _identityServerContext = identityServerContext;
        }

        public async Task<IEnumerable<ClaimDto>>  GetClaimsOfProduct(Guid userId, Guid productId)
        {
            GetClaimsOfProduct operation = new GetClaimsOfProduct(_identityServerContext);
            IEnumerable<Claim> claims = await operation.Retrieve(userId, productId);

            return claims.Select(c => c.ToDto());
        }

        public async Task CreateClaimsForProduct(
            Guid userId, Guid productId, string friendlyName, string issuer, string type, string value)
        {
            CreateClaimForProduct operation = new CreateClaimForProduct(_identityServerContext);
            await operation.Execute(userId, productId, friendlyName, issuer, type, value);
            await operation.CommitAsync();
        }
    }
}
