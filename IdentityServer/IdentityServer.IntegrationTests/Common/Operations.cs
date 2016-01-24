using System.Threading.Tasks;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Developers;

namespace IdentityServer.IntegrationTests.Common
{
    public static class Operations
    {
        public static async Task<Developer> RegisterDeveloper(IdentityServerContext context)
        {
            string email = RandomData.Email();
            string username = RandomData.RandomString();
            string password = RandomData.RandomString(12, true);

            DeveloperRegistration operation = new DeveloperRegistration(context);
            Developer developer = await operation.Register(email, username, password);

            await operation.CommitAsync();

            return developer;
        }
    }
}
