using System.Web;
using System.Web.Mvc;

namespace Authority.Web.Infrastructure.HtmlHelpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString RestLink(this HtmlHelper htmlHelper, string title, string format, params object[] parameters)
        {
            HttpRequest request = HttpContext.Current.Request;
            string baseUrl = request.Url.Scheme + "://" + request.Url.Authority +
            request.ApplicationPath.TrimEnd('/') + "/";

            string url = baseUrl + string.Format(format, parameters);
            return MvcHtmlString.Create(string.Format("<a href='{0}'>{1}</a>", url, title));
        }
    }
}