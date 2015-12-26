using System;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Errors
{
    public sealed class RecordError : Operation
    {
        public RecordError(IIdentityServerContext identityServerContext)
            : base(identityServerContext)
        {
            
        }

        public Guid Record(string type, string message, string stackTrace)
        {
            Error error = new Error
            {
                Message = message,
                StackTrace = stackTrace,
                Type = type,
                Date = DateTime.UtcNow
            };

            _identityServerContext.Errors.Add(error);

            return error.Id;
        }
    }
}
