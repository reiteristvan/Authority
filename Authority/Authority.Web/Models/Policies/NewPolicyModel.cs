using System.ComponentModel.DataAnnotations;

namespace Authority.Web.Models.Policies
{
    public sealed class NewPolicyModel
    {
        [Required]
        public string Name { get; set; }
    }
}