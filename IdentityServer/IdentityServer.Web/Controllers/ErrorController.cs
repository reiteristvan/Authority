using System;
using System.Web.Mvc;

namespace IdentityServer.Web.Controllers
{
    [RoutePrefix("error")]
    public class ErrorController : Controller
    {
        [Route("show/{id}")]
        public ActionResult Show(Guid id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}