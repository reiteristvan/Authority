using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.Operations.Policies;
using Authority.Operations.Products;

namespace Authority.Services
{
    public interface IPolicyService
    {
        Task<IEnumerable<Policy>> GetPolicies(Guid userId, Guid productId);
        Task<Guid> CreatePolicy(Guid userId, Guid productId, string name);
    }

    public sealed class PolicyService : IPolicyService
    {
        private readonly IAuthorityContext _authorityContext;

        public PolicyService(IAuthorityContext AuthorityContext)
        {
            _authorityContext = AuthorityContext;
        }

        public async Task<IEnumerable<Policy>> GetPolicies(Guid userId, Guid productId)
        {
            GetProductDetails operation = new GetProductDetails(_authorityContext, userId, productId);
            Product product = await operation.Do();
            return product.Policies;
        }

        public async Task<Guid> CreatePolicy(Guid userId, Guid productId, string name)
        {
            CreatePolicy operation = new CreatePolicy(_authorityContext, userId, productId, name);
            Policy policy = await operation.Do();

            await operation.CommitAsync();
            return policy.Id;
        }
    }
}
