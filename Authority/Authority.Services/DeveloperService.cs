using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EmailService;
using Authority.EmailService.Models;
using Authority.EntityFramework;
using Authority.Operations.Developers;

namespace Authority.Services
{
    public sealed class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
    }

    public interface IDeveloperService : IService
    {
        Task Register(string email, string displayName, string password);
        Task Activation(Guid activationCode);
        Task<LoginResult> Login(string email, string password);
    }

    public sealed class DeveloperService : IDeveloperService
    {
        private readonly IAuthorityContext _AuthorityContext;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public DeveloperService(
            IAuthorityContext AuthorityContext, 
            IEmailService emailService, 
            IConfiguration configuration)
        {
            _AuthorityContext = AuthorityContext;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task Register(string email, string displayName, string password)
        {
            DeveloperRegistration operation = new DeveloperRegistration(_AuthorityContext, email, displayName, password);
            Developer developer = await operation.Do();
            await operation.CommitAsync();

            await _emailService.SendDeveloperActivation(developer.Email, new DeveloperActivationModel
            {
                ActivationUrl = string.Format(_configuration.DeveloperActivationUrlTemplate, developer.PendingRegistrationId)
            });
        }

        public async Task<LoginResult> Login(string email, string password)
        {
            LoginResult result = new LoginResult
            {
                IsSuccess = false
            };

            DeveloperLogin operation = new DeveloperLogin(_AuthorityContext, email, password);
            
            if (!await operation.Do())
            {
                return result;
            }

            result.IsSuccess = true;
            result.Email = email;

            Developer developer = await _AuthorityContext.Developers.FirstAsync(d => d.Email == email);
            result.Id = developer.Id;

            return result;
        }

        public async Task Activation(Guid activationCode)
        {
            DeveloperActivation operation = new DeveloperActivation(_AuthorityContext, activationCode);
            await operation.Do();
            await operation.CommitAsync();
        }
    }
}
