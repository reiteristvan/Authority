using System;
using System.Threading.Tasks;

namespace IdentityServer.UnitOfWork
{
    public static class Requirement
    {
        public static async Task Check(Func<Task<bool>> condition, int errorCode)
        {
            if (!(await condition()))
            {
                throw new RequirementFailedException(errorCode);
            }
        }

        public static void Check(Func<bool> condition, int errorCode)
        {
            if (!condition())
            {
                throw new RequirementFailedException(errorCode);
            }
        }
    }
}
