using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;

namespace Authority.Operations.Products
{
    public sealed class CreateProduct : OperationWithReturnValueAsync<Guid>
    {
        private readonly Guid _ownerId;
        private readonly string _name;
        private readonly string _siteUrl;
        private readonly string _landingPage;

        public CreateProduct(IAuthorityContext AuthorityContext, Guid ownerId, string name, string siteUrl, string landingPage)
            : base(AuthorityContext)
        {
            _ownerId = ownerId;
            _name = name;
            _siteUrl = siteUrl;
            _landingPage = landingPage;
        }

        public override async Task<Guid> Do()
        {
            await Check(() => IsNameAvailable(_name), ProductErrorCodes.NameNotAvailable);

            Product product = new Product
            {
                OwnerId = _ownerId,
                Name = _name,
                IsActive = true,
                IsPublic = false,
                SiteUrl = _siteUrl,
                LandingPage = _landingPage
            };

            Context.Products.Add(product);

            return product.Id;
        }

        private async Task<bool> IsNameAvailable(string name)
        {
            Product product = await Context.Products.FirstOrDefaultAsync(p => p.Name == name);
            return product == null;
        }
    }
}
