using System;
using System.Collections.Generic;

namespace Authority.Services.Dto
{
    public class ProductSimpleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
        public List<PolicyDto> Policies { get; set; } 
    }

    public class PolicyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<ClaimDto> Claims { get; set; } 
    }

    public class ClaimDto
    {
        public Guid Id { get; set; }
        public string Issuer { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
