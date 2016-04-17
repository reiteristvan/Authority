namespace IdentityServer.EmailService.cs.Models
{
    [EmailTemplateName("DeveloperActivation")]
    public sealed class DeveloperActivationModel
    {
        public string ActivationUrl { get; set; }
    }
}
