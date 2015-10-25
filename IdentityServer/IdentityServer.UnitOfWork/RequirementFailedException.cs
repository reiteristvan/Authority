using System;

namespace IdentityServer.UnitOfWork
{
    public sealed class RequirementFailedException : Exception
    {
        public RequirementFailedException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; private set; }
    }
}
