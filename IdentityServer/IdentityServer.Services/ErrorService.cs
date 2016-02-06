using System;
using IdentityServer.EntityFramework;
using IdentityServer.UnitOfWork.Errors;

namespace IdentityServer.Services
{
    public interface IErrorService : IService
    {
        Guid RecordError(string type, string stackTrace, string message);
    }

    public class ErrorService : IErrorService
    {
        private readonly IIdentityServerContext _identityServerContext;

        public ErrorService(IIdentityServerContext identityServerContext)
        {
            _identityServerContext = identityServerContext;
        }

        public Guid RecordError(string type, string message, string stacktrace)
        {
            RecordError operation = new RecordError(_identityServerContext, type, message, stacktrace);
            Guid id = operation.Do();
            operation.Commit();

            return id;
        }
    }
}
