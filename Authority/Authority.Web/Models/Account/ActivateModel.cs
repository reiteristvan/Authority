using System;
using Newtonsoft.Json;

namespace IdentityServer.Web.Models.Account
{
    public sealed class ActivateModel
    {
        [JsonProperty("activationCode")]
        public Guid ActivationCode { get; set; }
    }
}