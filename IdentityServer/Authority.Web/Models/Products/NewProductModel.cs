using System.ComponentModel.DataAnnotations;

namespace Authority.Web.Models.Products
{
    public sealed class NewProductModel
    {
        [Required]
        public string Name { get; set; }
    }
}