using System;
using System.Web.Mvc;

namespace IdentityServer.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Show(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}