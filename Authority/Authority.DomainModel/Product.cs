using System;
using System.Collections.Generic;

namespace Authority.DomainModel
{
    public sealed class Product : EntityBase
    {
        public Product()
        {
            Policies = new HashSet<Policy>();
        }

        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public string SiteUrl { get; set; }
        public string LandingPage { get; set; }
        public Guid ClientId { get; set; }
        public Guid ClientSecret { get; set; }

        public ICollection<Policy> Policies { get; set; }
        public ICollection<Claim> Claims { get; set; }
    }
}
