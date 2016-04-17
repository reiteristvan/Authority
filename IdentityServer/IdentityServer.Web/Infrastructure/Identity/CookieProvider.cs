using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace Authority.Web.Infrastructure.Identity
{
    public static  class CookieProvider
    {
        public static HttpCookie Create(Guid id, string email)
        {
            DeveloperPrincipalSerializable developer = new DeveloperPrincipalSerializable
            {
                Email = email,
                Id = id
            };

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                email,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(7),
                true,
                JsonConvert.SerializeObject(developer));

            return new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                Expires = DateTime.UtcNow.AddDays(7),
                Path = "/"
            };
        }
    }
}