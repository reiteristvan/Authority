using System.Collections.Generic;
using System.Linq;
using IdentityServer.DomainModel;

namespace IdentityServer.UnitOfWork.Account
{
    // 1XXX
    public static class AccountErrorCodes
    {
        public static int EmailAlreadyExists = 1000;
        public static int UsernameNotAvailable = 1001;
    }
}
