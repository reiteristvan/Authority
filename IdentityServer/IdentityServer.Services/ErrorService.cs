using System;
using Authority.EntityFramework;
using Authority.UnitOfWork.Errors;

namespace Authority.Services
{
    public interface IErrorService : IService
    {
        Guid RecordError(string type, string stackTrace, string message);
    }

    public class ErrorService : IErrorService
    {
        private readonly IAuthorityContext _AuthorityContext;

        public ErrorService(IAuthorityContext AuthorityContext)
        {
            _AuthorityContext = AuthorityContext;
        }

        public Guid RecordError(string type, string message, string stacktrace)
        {
            RecordError operation = new RecordError(_AuthorityContext, type, message, stacktrace);
            Guid id = operation.Do();
            operation.Commit();

            return id;
        }
    }
}
