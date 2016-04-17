using System.Web.Mvc;
using Authority.Web.Models;

namespace Authority.Web.Controllers
{
    [RoutePrefix("account")]
    public sealed class AccountController : Controller
    {
        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        [ValidateAntiForgeryToken]
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
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            return View();
        }
    }
}