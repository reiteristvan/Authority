using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Web.Models.Products
{
    public sealed class NewProductModel
    {
        [Required]
        public string Name { get; set; }
    }
}