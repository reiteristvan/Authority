using System;

namespace Authority.EmailService
{
    public sealed class EmailTemplateNameAttribute : Attribute
    {
        private readonly string _templateName;

        public EmailTemplateNameAttribute(string templateName)
        {
            _templateName = templateName;
        }

        public string TemplateName { get { return _templateName; } }
    }
}
