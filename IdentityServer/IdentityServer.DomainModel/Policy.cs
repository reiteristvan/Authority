using System.Collections.Generic;

namespace IdentityServer.DomainModel
{
    public sealed class Policy : EntityBase
    {
        public Policy()
        {
            Claims = new HashSet<Claim>();
            Users = new HashSet<User>();
        }

        public string Name { get; set; }

        public ICollection<Claim> Claims { get; set; }
        public ICollection<User> Users { get; set; } 
    }
}
