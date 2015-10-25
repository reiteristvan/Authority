using System.Collections.Generic;

namespace IdentityServer.DomainModel
{
    public sealed class Product : EntityBase
    {
        public Product()
        {
            Claims = new HashSet<Claim>();
        }

        public string Name { get; set; }

        public ICollection<Claim> Claims { get; set; } 
    }
}
