using System.Collections.Generic;
using Newtonsoft.Json;

namespace IdentityServer.Web.Models.Account
{
    public sealed class LoginResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("claims")]
        public List<AuthorityClaim> Claims { get; set; } 
    }

    public sealed class AuthorityClaim
    {
        [JsonProperty("issuer")]
        public string Issuer { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}