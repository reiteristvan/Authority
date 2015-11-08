using System;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;

namespace IdentityServer.EmailService.cs
{
    public class EmailTemplateProvider
    {
        private readonly IEmailSettingsProvider _emailSettingsProvider;

        public EmailTemplateProvider(IEmailSettingsProvider emailSettingsProvider)
        {
            _emailSettingsProvider = emailSettingsProvider;
        }

        public string Load(object model)
        {
            EmailTemplateNameAttribute templateAttribute = Attribute.GetCustomAttribute(
                model.GetType(), typeof(EmailTemplateNameAttribute)) as EmailTemplateNameAttribute;

            if (templateAttribute == null)
            {
                throw new ArgumentException("Email template not found");
            }

            string emailTemplatePath = _emailSettingsProvider.TemplateFolderPath + "/" + templateAttribute.TemplateName + ".cshtml";
            string template = File.ReadAllText(emailTemplatePath);
            string body = Engine.Razor.RunCompile(template, model.GetHashCode().ToString(), model.GetType(), model);
            return body;
        }
    }
}
