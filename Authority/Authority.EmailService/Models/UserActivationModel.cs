namespace Authority.EmailService.Models
{
    [EmailTemplateName("UserActivation")]
    public sealed class UserActivationModel
    {
        public string ProductName { get; set; }
        public string Sender { get; set; }
        public string ActivationUrl { get; set; }
    }
}
