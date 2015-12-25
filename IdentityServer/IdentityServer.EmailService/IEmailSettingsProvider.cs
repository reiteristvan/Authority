namespace IdentityServer.EmailService
{
    public interface IEmailSettingsProvider
    {
        string TemplateFolderPath { get; }
    }
}
