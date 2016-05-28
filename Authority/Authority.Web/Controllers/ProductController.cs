using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Authority.Services;
using Authority.Services.Dto;
using Authority.Web.Infrastructure.Filters;
using Authority.Web.Infrastructure.Identity;
using Authority.Web.Models.Policies;
using Authority.Web.Models.Products;

namespace Authority.Web.Controllers
{
    [AuthorityAuthorization]
    [RoutePrefix("products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IPolicyService _policyService;

        public ProductController(IProductService productService, IPolicyService policyService)
        {
            _productService = productService;
            _policyService = policyService;
        }

        [HttpGet]
        [Route]
        [Route("index")]
        public async Task<ActionResult> Index()
        {
            Guid userId = HttpContext.User.GetUserId();
            IEnumerable<ProductSimpleDto> products = await _productService.GetProductsOfUser(userId);

            return View(products);
        }

        [HttpGet]
        [Route("new")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("new")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(NewProductModel model)
        {
            Guid userId = HttpContext.User.GetUserId();
            await _productService.Create(userId, model.Name, model.SiteUrl, model.NotificationEmail, model.ActivationUrl);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<ActionResult> Details(Guid id)
        {
            Guid userId = HttpContext.User.GetUserId();
            ProductDto product = await _productService.GetProductDetails(userId, id);

            return View(product);
        }

        [HttpPut]
        [Route("publish/{id}")]
        public async Task<bool> TogglePublish(Guid id)
        {
            Guid userId = HttpContext.User.GetUserId();
            bool publishState = await _productService.ToggleProductPublish(userId, id);
            return publishState;
        }

        [HttpGet]
        [Route("apikey/{productId}")]
        public async Task<Guid> GetApiKeyOfProduct(Guid productId)
        {
            Guid userId = HttpContext.User.GetUserId();
            Guid apiKey = await _productService.GetApiKeyForProduct(userId, productId);

            return apiKey;
        }

        [HttpPost]
        [Route("{productId}/policies")]
        public async Task CreatePolicy(Guid productId, NewPolicyModel model)
        {
            Guid userId = HttpContext.User.GetUserId();
            await _policyService.CreatePolicy(userId, productId, model.Name);
        }
    }
}