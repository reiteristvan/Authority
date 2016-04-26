using System;
using Authority.DomainModel;
using Authority.EmailService;
using Authority.EntityFramework;
using Authority.Operations.Account;
using Authority.Operations.Developers;
using Authority.Operations.Products;
using Authority.Services;

namespace Authority.Benchmark
{
    public sealed class EmailSettingsProvider : IEmailSettingsProvider
    {
        public string TemplateFolderPath
        {
            get { return ""; }
        }
    }

    public sealed class Configuration : IConfiguration
    {
        public string DeveloperActivationUrlTemplate
        {
            get { return ""; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            AuthorityContext context = new AuthorityContext();

            DeveloperRegistration registration = new DeveloperRegistration(context, "dev@dev.hu", "ireiter", "almabeka");
            Developer dev = registration.Do().Result;
            registration.Commit();

            DeveloperActivation activation = new DeveloperActivation(context, dev.PendingRegistrationId);
            activation.Do().Wait();
            activation.Commit();

            CreateProduct create = new CreateProduct(context, dev.Id, "Awsome", "http://alma.com", "alma@alam.com", "http://alma.com");
            Guid id = create.Do().Result;
            create.Commit();

            int max = 1000000;
            for (int i = 0; i < 1000000; ++i)
            {
                Console.WriteLine("Creating user {0}/{1}", i + 1, max);

                string email = Guid.NewGuid() + "@test.com";
                UserRegistration userReg = new UserRegistration(context, id, email, email, "12Budapest99");
                var user = userReg.Do().Result;
                userReg.Commit();

                Console.WriteLine("User created");
            }


            //context.Database.Delete();
            Console.WriteLine("Done...");
        }
    }
}
