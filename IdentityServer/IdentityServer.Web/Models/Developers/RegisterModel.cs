using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Web.Models.Developers
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}