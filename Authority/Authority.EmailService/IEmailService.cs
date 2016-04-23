using System.Threading.Tasks;
using Authority.EmailService.Models;

namespace Authority.EmailService
{
    public interface IEmailService
    {
        Task SendDeveloperActivation(string recipient, DeveloperActivationModel model);
        Task SendUserActivation(string recipient, UserActivationModel model);
    }
}