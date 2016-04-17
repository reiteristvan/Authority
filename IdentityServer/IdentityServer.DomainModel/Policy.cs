using System.Collections.Generic;

namespace Authority.DomainModel
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
