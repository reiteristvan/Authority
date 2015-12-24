using System.Collections.Generic;

namespace IdentityServer.DomainModel
{
    public sealed class Product : EntityBase
    {
        public Product()
        {
            Policies = new HashSet<Policy>();
        }

        public string Name { get; set; }
        public bool IsPublic { get; set; }

        public ICollection<Policy> Policies { get; set; }
    }
}
