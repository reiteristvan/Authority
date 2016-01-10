using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Web.Models.Policies
{
    public sealed class NewPolicyModel
    {
        [Required]
        public string Name { get; set; }
    }
}