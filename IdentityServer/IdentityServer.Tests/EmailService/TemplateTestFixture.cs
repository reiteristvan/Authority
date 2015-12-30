﻿using System;
using IdentityServer.EmailService;

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
}