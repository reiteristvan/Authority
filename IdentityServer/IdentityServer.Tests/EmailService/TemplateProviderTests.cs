using IdentityServer.EmailService.cs;
using IdentityServer.Tests.EmailService.Models;
using Xunit;

namespace IdentityServer.Tests.EmailService
{
    public sealed class TemplateProviderTests
    {
        [Fact]
        public void Positive()
        {
            EmailTemplateProvider templateProvider = new EmailTemplateProvider(new TestEmailSettingsProvider());
            TestActivation model = new TestActivation
            {
                Email = "reiteristvan@gmail.com"
            };

            string result = templateProvider.Load(model);

            Assert.NotNull(result);
        }
    }
}
