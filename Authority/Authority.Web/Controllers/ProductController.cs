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

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<ActionResult> Edit(Guid id, EditProductModel model)
        {
            Guid userId = HttpContext.User.GetUserId();
            ProductDto product = await _productService.GetProductDetails(userId, id);

            return Redirect("edit/" + id);
        }
    }
}