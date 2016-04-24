using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace IdentityServer.Web.Models.Account
{
    public sealed class ExchangeTokenModel
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
    }
}