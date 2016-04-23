using Newtonsoft.Json;

namespace IdentityServer.Web.Models.Account
{
    public sealed class LoginResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}