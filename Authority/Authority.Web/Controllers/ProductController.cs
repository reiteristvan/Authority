using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Authority.Services;
using Authority.Services.Dto;
using Authority.Web.Infrastructure.Filters;
using Authority.Web.Infrastructure.Identity;
using Authority.Web.Models.Products;

namespace Authority.Web.Controllers
{
    [AuthorityAuthorization]
    [RoutePrefix("products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
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
            await _productService.Create(userId, model.Name, model.SiteUrl, model.LandingPage);
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
        [Route("secret/{productId}")]
        public async Task<Guid> GetSecretOfProduct(Guid productId)
        {
            Guid userId = HttpContext.User.GetUserId();
            Guid secret = await _productService.GetClientSecretForProduct(userId, productId);

            return secret;
        }
    }
}