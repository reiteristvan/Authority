namespace IdentityServer.EmailService.cs
{
    public interface IEmailSettingsProvider
    {
        string TemplateFolderPath { get; }
    }
}
