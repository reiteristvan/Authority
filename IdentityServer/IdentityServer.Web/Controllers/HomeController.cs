using System.Web.Mvc;

namespace IdentityServer.Web.Controllers
{
    [Route]
    public class HomeController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            return View();
        }
    }
}