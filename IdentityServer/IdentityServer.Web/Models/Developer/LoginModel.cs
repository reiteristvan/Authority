using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Web.Models.Developer
{
    public sealed class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}