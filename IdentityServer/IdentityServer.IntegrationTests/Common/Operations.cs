using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Developers;

namespace IdentityServer.IntegrationTests.Common
{
    public static class Operations
    {
        public static async Task<Developer> RegisterDeveloper(IdentityServerContext context, string password = "")
        {
            string email = RandomData.Email();
            string username = RandomData.RandomString();

            DeveloperRegistration operation = new DeveloperRegistration(context);
            Developer developer = await operation.Register(
                email, 
                username, 
                password == "" ? RandomData.RandomString(12, true) : password);

            await operation.CommitAsync();

            return developer;
        }

        public static async Task<Developer> RegisterAndActivateDeveloper(
            IdentityServerContext context,
            string password = "")
        {
            string email = RandomData.Email();
            string username = RandomData.RandomString();

            DeveloperRegistration registration = new DeveloperRegistration(context);
            Developer developer = await registration.Register(email, username, password ?? RandomData.RandomString(12, true));
            await registration.CommitAsync();

            DeveloperActivation activation = new DeveloperActivation(context);
            await activation.Activation(developer.PendingRegistrationId);
            await activation.CommitAsync();

            return developer;
        }
    }
}
