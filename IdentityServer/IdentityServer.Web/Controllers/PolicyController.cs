﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityServer.Services;
using IdentityServer.Web.Infrastructure.Filters;
using IdentityServer.Web.Infrastructure.Identity;
using IdentityServer.Web.Models.Policies;

namespace IdentityServer.Web.Controllers
{
    [IdentityServerAuthorization]
    [RoutePrefix("policies")]
    public class PolicyController : Controller
    {
        private readonly IPolicyService _policyService;

        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet]
        [Route("new/{productId}")]
        public ActionResult New(Guid productId)
        {
            return View();
        }

        [HttpPost]
        [Route("new/{productId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(Guid productId, NewPolicyModel model)
        {
            Guid userId = HttpContext.User.GetUserId();
            Guid policyId = await _policyService.CreatePolicy(userId, productId, model.Name);
            return RedirectToAction("Edit", new [] { policyId = policyId});
        }

        [HttpGet]
        [Route("edit/{policyId}")]
        public ActionResult Edit(Guid policyId)
        {
            return View();
        }
    }
}