using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IdentityServer.Web.Models.Developer;

namespace IdentityServer.Web.Controllers
{
    [RoutePrefix("developers")]
    public class DeveloperController : Controller
    {
        [HttpGet]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public ActionResult Register(RegisterModel model)
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginModel model)
        {
            return View();
        }
    }
}