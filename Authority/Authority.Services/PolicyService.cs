using System;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.Operations.Policies;

namespace Authority.Services
{
    public interface IPolicyService
    {
        Task<Guid> CreatePolicy(Guid userId, Guid productId, string name);
    }

    public sealed class PolicyService : IPolicyService
    {
        private readonly IAuthorityContext _AuthorityContext;

        public PolicyService(IAuthorityContext AuthorityContext)
        {
            _AuthorityContext = AuthorityContext;
        }

        public async Task<Guid> CreatePolicy(Guid userId, Guid productId, string name)
        {
            CreatePolicy operation = new CreatePolicy(_AuthorityContext, userId, productId, name);
            Policy policy = await operation.Do();

            await operation.CommitAsync();
            return policy.Id;
        }
    }
}
