using System;
using System.Configuration;
using Authority.EntityFramework;

namespace Authority.Library
{
    internal static class Database
    {
        internal const string ConnectionStringName = "AuthorityConnection";

        public static IAuthorityContext CreateContext()
        {
            object connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            if (connectionString == null)
            {
                throw new ArgumentException("Cannot find connectionstring: {0}", ConnectionStringName);
            }

            return new AuthorityContext(ConnectionStringName);
        }
    }
}
