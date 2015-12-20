using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EmailService.cs;
using IdentityServer.EmailService.cs.Models;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Developers;

namespace IdentityServer.Services
{
    public interface IDeveloperService
    {
        Task Register(string email, string displayName, string password);
    }

    public sealed class DeveloperService : IDeveloperService
    {
        private readonly IIdentityServerContext _identityServerContext;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public DeveloperService(
            IIdentityServerContext identityServerContext, 
            IEmailService emailService, 
            IConfiguration configuration)
        {
            _identityServerContext = identityServerContext;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task Register(string email, string displayName, string password)
        {
            DeveloperRegistration operation = new DeveloperRegistration(_identityServerContext);
            Developer developer = await operation.Register(email, displayName, password);
            await operation.Commit();

            await _emailService.SendDeveloperActivation(developer.Email, new DeveloperActivationModel
            {
                ActivationUrl = string.Format(_configuration.DeveloperActivationEmailTemplate, developer.PendingRegistrationId)
            });
        }
    }
}
