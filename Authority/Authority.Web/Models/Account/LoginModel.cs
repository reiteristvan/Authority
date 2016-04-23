using System;
using Newtonsoft.Json;

namespace IdentityServer.Web.Models.Account
{
    public sealed class LoginModel
    {
        [JsonProperty("clientId")]
        public Guid ClientId { get; set; }

        [JsonProperty("clientSecret")]
        public Guid ClientSecret { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}