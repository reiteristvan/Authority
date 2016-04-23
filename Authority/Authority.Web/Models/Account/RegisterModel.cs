using System;
using Newtonsoft.Json;

namespace IdentityServer.Web.Models.Account
{
    public class RegisterModel
    {
        [JsonProperty("clientId")]
        public Guid ClientId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}