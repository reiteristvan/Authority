using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Authority.Services;
using Authority.Web.Infrastructure.Filters;
using Authority.Web.Infrastructure.Identity;
using Authority.Web.Models.Policies;

namespace Authority.Web.Controllers
{
    [AuthorityAuthorization]
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