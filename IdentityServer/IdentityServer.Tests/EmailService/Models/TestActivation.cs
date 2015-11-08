using IdentityServer.EmailService.cs;

namespace IdentityServer.Tests.EmailService.Models
{
    [EmailTemplateName("TestActivation")]
    public class TestActivation
    {
        public string Email { get; set; }
    }
}
