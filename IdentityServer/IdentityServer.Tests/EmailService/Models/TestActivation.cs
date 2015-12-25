using IdentityServer.EmailService;

namespace IdentityServer.Tests.EmailService.Models
{
    [EmailTemplateName("TestActivation")]
    public class TestActivation
    {
        public string Email { get; set; }
    }
}
