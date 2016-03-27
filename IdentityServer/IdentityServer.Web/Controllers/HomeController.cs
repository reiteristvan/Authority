using System.Web.Mvc;
using IdentityServer.Web.Infrastructure.Identity;

namespace IdentityServer.Web.Controllers
{
    [Route]
    public class HomeController : Controller
    {
        [Route]
        public ActionResult Index()
        {
            if (User != null && User is DeveloperPrincipal)
            {
                return Redirect("/developers/index");
            }

            return View();
        }
    }
}