﻿using System;
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

        public static MvcHtmlString ImageFormByteArray(byte[] image, string cssClass = "")
        {
            string imageBase64 = Convert.ToBase64String(image);
            string imageSource = string.Format("data:image/gif;base64,{0}", imageBase64);
            return MvcHtmlString.Create(string.Format("<img src=\"{0}\" />", imageSource));
        }
    }
}