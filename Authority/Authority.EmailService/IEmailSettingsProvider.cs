namespace Authority.EmailService
{
    public interface IEmailSettingsProvider
    {
        string TemplateFolderPath { get; }
        string SendGridUsername { get; }
        string SendGridPassword { get; }
    }
}
