using System.Web.Mvc;

namespace IdentityServer.Web.Infrastructure.HtmlHelpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString RestLink(this HtmlHelper htmlHelper, string title, string format, params object[] parameters)
        {
            string url = string.Format(format, parameters);
            return MvcHtmlString.Create(string.Format("<a href='{0}'>{1}</a>", url, title));
        }
    }
}