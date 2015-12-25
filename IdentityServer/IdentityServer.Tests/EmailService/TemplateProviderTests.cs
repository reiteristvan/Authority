using System;
using IdentityServer.EmailService;
using IdentityServer.Tests.EmailService.Models;
using Xunit;

namespace IdentityServer.Tests.EmailService
{
    public sealed class TemplateTestFixture : IDisposable
    {
        private readonly EmailTemplateProvider _templateProvider;

        public TemplateTestFixture()
        {
            _templateProvider = new EmailTemplateProvider(new TestEmailSettingsProvider());
        }

        public EmailTemplateProvider TemplateProvider { get { return _templateProvider; } }

        public void Dispose()
        {
        }
    }

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
