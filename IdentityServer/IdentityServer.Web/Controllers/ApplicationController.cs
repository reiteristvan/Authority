using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentityServer.Web.Controllers
{
    [RoutePrefix("applications")]
    public class ApplicationController : Controller
    {
        [HttpGet]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("new")]
        public ActionResult Create()
        {
            return View();
        }
    }
}