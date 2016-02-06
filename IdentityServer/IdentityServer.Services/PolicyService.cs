using System;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Policies;

namespace IdentityServer.Services
{
    public interface IPolicyService
    {
        Task<Guid> CreatePolicy(Guid userId, Guid productId, string name);
    }

    public sealed class PolicyService : IPolicyService
    {
        private readonly IIdentityServerContext _identityServerContext;

        public PolicyService(IIdentityServerContext identityServerContext)
        {
            _identityServerContext = identityServerContext;
        }

        public async Task<Guid> CreatePolicy(Guid userId, Guid productId, string name)
        {
            CreatePolicy operation = new CreatePolicy(_identityServerContext, userId, productId, name);
            Policy policy = await operation.Do();

            await operation.CommitAsync();
            return policy.Id;
        }
    }
}
