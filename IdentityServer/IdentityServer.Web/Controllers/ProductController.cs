using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityServer.Services;
using IdentityServer.Services.Dto;
using IdentityServer.Web.Infrastructure.Identity;
using IdentityServer.Web.Models.Products;

namespace IdentityServer.Web.Controllers
{
    [Authorize]
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
            IEnumerable<ProductDto> products = await _productService.GetProductsOfUser(userId);

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
            await _productService.Create(userId, model.Name);
            return RedirectToAction("Index");
        }
    }
}