using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Authority.Web.Models;

namespace Authority.Web.Controllers
{
    [RoutePrefix("oauth")]
    public class OAuthController : Controller
    {
        [HttpGet]
        [Route]
        public ActionResult Login(string response_type, string client_id, string redirect_uri, string scope, string state = "")
        {
            return View();
        }

        [HttpPost]
        [Route]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                // TODO Error handling
            }

            return View();
        }
    }
}