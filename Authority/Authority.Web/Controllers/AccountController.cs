using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Authority.Web.Models;
using IdentityServer.Services;

namespace Authority.Web.Controllers
{
    [System.Web.Mvc.RoutePrefix("account")]
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

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("register")]
        public async Task<ActionResult> Register(Guid client_id, string redirect_url, string state = "")
        {
            if (!await _accountService.ValidateProduct(client_id, redirect_url))
            {
                Redirect("/register");
            }

            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("register")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Guid clientId, string redirect_url, [FromBody]RegisterModel model)
        {
            await _accountService.RegisterUser(model.Email, model.Username, model.Password);

            return View();
        }

        [System.Web.Mvc.HttpGet]
        [System.Web.Mvc.Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            return View();
        }
    }
}