using System.Threading.Tasks;
using IdentityServer.EmailService.cs.Models;

namespace IdentityServer.EmailService.cs
{
    public interface IEmailService
    {
        Task SendDeveloperActivation(string recipient, DeveloperActivationModel model);
    }
}