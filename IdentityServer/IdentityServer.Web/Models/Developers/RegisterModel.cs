using System.ComponentModel.DataAnnotations;

namespace Authority.Web.Models.Developers
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