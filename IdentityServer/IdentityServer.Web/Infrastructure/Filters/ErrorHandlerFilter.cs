using System;
using System.Web.Mvc;
using System.Web.Routing;
using Authority.Services;

namespace Authority.Web.Infrastructure.Filters
{
    public class ErrorHandler : IExceptionFilter
    {
        private readonly IErrorService _errorService;

        public ErrorHandler(IErrorService errorService)
        {
            _errorService = errorService;
        }

        public void OnException(ExceptionContext filterContext)
        {
            string type = filterContext.Exception.GetType().FullName;
            string stackTrace = filterContext.Exception.StackTrace;
            string message = filterContext.Exception.Message;

            filterContext.ExceptionHandled = true;

            Guid id = _errorService.RecordError(type, stackTrace, message);
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "Error", 
                action = "Show", 
                id = id
            }));
        }
    }
}