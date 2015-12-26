using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IdentityServer.Services;
using IdentityServer.Web.Infrastructure;
using IdentityServer.Web.Infrastructure.Identity;
using IdentityServer.Web.Models.Developers;

namespace IdentityServer.Web.Controllers
{
    [RoutePrefix("developers")]
    public class DevelopersController : Controller
    {
        private readonly IDeveloperService _developerService;

        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [Authorize]
        [HttpGet]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await _developerService.Register(model.Email, model.Username, model.Password);

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("activation/{activationCode}")]
        public async Task<ActionResult> Activate(Guid activationCode)
        {
            await _developerService.Activation(activationCode);

            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            LoginResult loginResult = await _developerService.Login(model.Email, model.Password);

            if (!loginResult.IsSuccess)
            {
                return RedirectToAction("Login");
            }

            HttpCookie cookie = CookieProvider.Create(loginResult.Id, loginResult.Email);
            HttpContext.Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        [Route("logout")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}