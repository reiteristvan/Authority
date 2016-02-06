using System;
using IdentityServer.DomainModel;
using IdentityServer.EntityFramework;

namespace IdentityServer.UnitOfWork.Errors
{
    public sealed class RecordError : OperationWithReturnValue<Guid>
    {
        private readonly string _type;
        private readonly string _message;
        private readonly string _stacktrace;

        public RecordError(IIdentityServerContext identityServerContext, string type, string message, string stacktrace)
            : base(identityServerContext)
        {
            _type = type;
            _message = message;
            _stacktrace = stacktrace;
        }

        public override Guid Do()
        {
            Error error = new Error
            {
                Message = _message,
                StackTrace = _stacktrace,
                Type = _type,
                Date = DateTime.UtcNow
            };

            Context.Errors.Add(error);

            return error.Id;
        }
    }
}
