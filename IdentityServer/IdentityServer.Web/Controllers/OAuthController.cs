using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityServer.Web.Controllers
{
    [RoutePrefix("oauth")]
    public class OAuthController : Controller
    {
        [Route]
        public ActionResult Index(string response_type, string client_id, string redirect_uri, string scope, string state = "")
        {
            return View();
        }
    }
}