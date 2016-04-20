using System.Web.Mvc;
using Authority.Web.Infrastructure.Identity;

namespace Authority.Web.Controllers
{
    [Route]
    public class HomeController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            if (User is DeveloperPrincipal)
            {
                return Redirect("/developers/index");
            }

            return View();
        }
    }
}