using System;

namespace IdentityServer.Services.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public bool IsActive { get; set; }
    }
}
