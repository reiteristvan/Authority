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

        public Guid RecordError(string type, string stackTrace, string message)
        {
            RecordError operation = new RecordError(_identityServerContext);
            Guid id = operation.Record(type, message, stackTrace);
            operation.Commit();

            return id;
        }
    }
}
