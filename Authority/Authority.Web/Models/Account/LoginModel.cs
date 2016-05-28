using Newtonsoft.Json;

namespace IdentityServer.Web.Models.Account
{
    public sealed class LoginModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}