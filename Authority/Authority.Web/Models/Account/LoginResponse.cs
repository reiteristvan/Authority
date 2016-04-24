using Newtonsoft.Json;

namespace IdentityServer.Web.Models.Account
{
    public sealed class LoginResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}