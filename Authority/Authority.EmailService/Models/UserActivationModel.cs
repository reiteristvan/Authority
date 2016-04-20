namespace Authority.EmailService.Models
{
    [EmailTemplateName("UserActivation")]
    public sealed class UserActivationModel
    {
        public string ActivationUrl { get; set; }
    }
}
