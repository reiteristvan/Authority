using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Authority.Services;
using Authority.Services.Dto;
using Authority.Web.Infrastructure.Filters;
using Authority.Web.Models.Policies;
using Authority.Web.Models.Products;
using IdentityServer.Web.Infrastructure;

namespace Authority.Web.Controllers
{
    [AuthorityAuthorization]
    [RoutePrefix("products")]
    public class ProductController : AuthorityController
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
            IEnumerable<ProductSimpleDto> products = await _productService.GetProductsOfUser(CallingContext.UserId);

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
            await _productService.Create(
                CallingContext.UserId, 
                model.Name, 
                model.SiteUrl, 
                model.NotificationEmail, 
                model.ActivationUrl);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("edit/{id}")]
        public async Task<ActionResult> Details(Guid id)
        {
            ProductDto product = await _productService.GetProductDetails(CallingContext.UserId, id);

            return View(product);
        }

        [HttpPut]
        [Route("publish/{id}")]
        public async Task<bool> TogglePublish(Guid id)
        {
            bool publishState = await _productService.ToggleProductPublish(CallingContext.UserId, id);
            return publishState;
        }

        [HttpGet]
        [Route("apikey/{productId}")]
        public async Task<Guid> GetApiKeyOfProduct(Guid productId)
        {
            Guid apiKey = await _productService.GetApiKeyForProduct(CallingContext.UserId, productId);

            return apiKey;
        }

        [HttpPost]
        [Route("{productId}/policies")]
        public async Task CreatePolicy(Guid productId, NewPolicyModel model)
        {
            await _policyService.CreatePolicy(CallingContext.UserId, productId, model.Name);
        }
    }
}