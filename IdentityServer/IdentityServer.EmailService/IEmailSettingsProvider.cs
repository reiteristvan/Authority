namespace Authority.EmailService
{
    public interface IEmailSettingsProvider
    {
        string TemplateFolderPath { get; }
    }
}
