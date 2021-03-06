﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Authority.Services;
using Authority.Services.Dto;
using Authority.Web.Infrastructure.Filters;
using Authority.Web.Infrastructure.Identity;
using Authority.Web.Models.Developers;
using IdentityServer.Web.Infrastructure;

namespace Authority.Web.Controllers
{
    [RoutePrefix("developers")]
    public class DevelopersController : AuthorityController
    {
        private const string ActivationSuccessQuerystringParameter = "activated";

        private readonly IDeveloperService _developerService;
        private readonly IProductService _productService;

        public DevelopersController(IDeveloperService developerService, IProductService productService)
        {
            _developerService = developerService;
            _productService = productService;
        }

        [AuthorityAuthorization]
        [HttpGet]
        [Route]
        [Route("index")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<ProductSimpleDto> products = await _productService.GetProductsOfUser(CallingContext.UserId);

            return View(products);
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
        [ValidateAntiForgeryToken]
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

            return RedirectToAction("Login", new { activated = true });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            NameValueCollection queryStringValues = HttpContext.Request.QueryString;

            if (queryStringValues.AllKeys.Contains(ActivationSuccessQuerystringParameter))
            {
                ViewBag.Message = queryStringValues[ActivationSuccessQuerystringParameter] == "true" ? 
                    "Activation successfull, please log in" : "";
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ValidateAntiForgeryToken]
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

        [AuthorityAuthorization]
        [HttpGet]
        [Route("logout")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}