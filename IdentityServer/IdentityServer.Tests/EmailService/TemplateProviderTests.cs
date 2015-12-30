using IdentityServer.Tests.EmailService.Models;
using Xunit;

namespace IdentityServer.Tests.EmailService
{
    public sealed class TemplateProviderTests : IClassFixture<TemplateTestFixture>
    {
        private readonly TemplateTestFixture _testFixture;

        public TemplateProviderTests(TemplateTestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        [Fact]
        public void Positive()
        {
            TestActivation model = new TestActivation
            {
                Email = "reiteristvan@gmail.com"
            };

            string result = _testFixture.TemplateProvider.Load(model);
           
            Assert.NotNull(result);
        }
    }
}
