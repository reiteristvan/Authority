using System.ComponentModel.DataAnnotations;

namespace Authority.Web.Models.Developers
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