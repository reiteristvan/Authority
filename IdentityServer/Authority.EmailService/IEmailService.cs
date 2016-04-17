using System.Threading.Tasks;
using Authority.EmailService.cs.Models;

namespace Authority.EmailService
{
    public interface IEmailService
    {
        Task SendDeveloperActivation(string recipient, DeveloperActivationModel model);
    }
}