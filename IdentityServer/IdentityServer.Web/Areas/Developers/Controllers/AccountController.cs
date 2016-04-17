using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityServer.Web.Areas.Developers.Models;

namespace IdentityServer.Web.Areas.Developers.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(RegisterModel model)
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginModel model)
        {
            return View();
        }
    }
}