using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Authority.Web.Models;
using IdentityServer.Services;

namespace Authority.Web.Controllers
{
    [RoutePrefix("account")]
    public sealed class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            if (accountService == null)
            {
                throw new ArgumentNullException("accountService");
            }

            _accountService = accountService;
        }

        [HttpGet]
        [Route("register")]
        public async Task<ActionResult> Register(Guid client_id, string redirect_url, string state = "")
        {
            if (!await _accountService.ValidateProduct(client_id, redirect_url))
            {
                Redirect("/register");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Guid client_id, string redirect_url, RegisterModel model)
        {
            await _accountService.RegisterUser(client_id, model.Email, model.Username, model.Password);

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