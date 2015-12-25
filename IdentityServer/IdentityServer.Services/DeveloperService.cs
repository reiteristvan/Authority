using System;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EmailService;
using IdentityServer.EmailService.cs.Models;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Developers;

namespace IdentityServer.Services
{
    public sealed class LoginResult
    {
        public LoginResult()
        {
            IsSuccess = false;
        }

        public bool IsSuccess { get; set; }
        public string Email { get; set; }
        public Guid Id { get; set; }
    }

    public interface IDeveloperService
    {
        Task Register(string email, string displayName, string password);
        Task Activation(Guid activationCode);
        Task<LoginResult> Login(string email, string password);
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

        public async Task<LoginResult> Login(string email, string password)
        {
            LoginResult result = new LoginResult();
            DeveloperLogin operation = new DeveloperLogin(_identityServerContext);
            
            if (!await operation.ValidateLogin(email, password))
            {
                return result;
            }

            result.IsSuccess = true;
            result.Email = email;

            Developer developer = await _identityServerContext.Developers.FirstAsync(d => d.Email == email);
            result.Id = developer.Id;

            return result;
        }

        public async Task Activation(Guid activationCode)
        {
            DeveloperActivation operation = new DeveloperActivation(_identityServerContext);
            await operation.Activation(activationCode);
            await operation.Commit();
        }
    }
}
