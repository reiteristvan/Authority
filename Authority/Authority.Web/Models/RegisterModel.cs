using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Authority.Web.Models
{
    public sealed class RegisterModel
    {
        [Required]
        [EmailAddress]
        [JsonProperty("email")]
        public string Email { get; set; }

        [Required]
        [JsonProperty("username")]
        public string Username { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}