using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Web.Areas.Developers.Models
{
    public sealed class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}