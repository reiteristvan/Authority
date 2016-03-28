using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Web.Models.Products
{
    public sealed class EditProductModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string SiteUrl { get; set; }

        [Required]
        public string LandingPage { get; set; }
    }
}