using System;
using Newtonsoft.Json;

namespace Authority.Web.Infrastructure.Identity
{
    public sealed class DeveloperPrincipalSerializable
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}