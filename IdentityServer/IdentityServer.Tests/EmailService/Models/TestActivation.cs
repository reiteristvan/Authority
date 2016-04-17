using Authority.EmailService;

namespace Authority.Tests.EmailService.Models
{
    [EmailTemplateName("TestActivation")]
    public class TestActivation
    {
        public string Email { get; set; }
    }
}
