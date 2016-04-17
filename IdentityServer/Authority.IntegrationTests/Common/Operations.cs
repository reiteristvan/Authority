using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.UnitOfWork.Developers;

namespace Authority.IntegrationTests.Common
{
    public static class Operations
    {
        public static async Task<Developer> RegisterDeveloper(AuthorityContext context, string password = "")
        {
            string email = RandomData.Email();
            string username = RandomData.RandomString();
            password = password == "" ? RandomData.RandomString(12, true) : password;

            DeveloperRegistration operation = new DeveloperRegistration(context, email, username, password);
            Developer developer = await operation.Do();

            await operation.CommitAsync();

            return developer;
        }

        public static async Task<Developer> RegisterAndActivateDeveloper(
            AuthorityContext context,
            string password = "")
        {
            string email = RandomData.Email();
            string username = RandomData.RandomString();
            password = password == "" ? RandomData.RandomString(12, true) : password;

            DeveloperRegistration registration = new DeveloperRegistration(context, email, username, password);
            Developer developer = await registration.Do();
            await registration.CommitAsync();

            DeveloperActivation activation = new DeveloperActivation(context, developer.PendingRegistrationId);
            await activation.Do();
            await activation.CommitAsync();

            return developer;
        }
    }
}
