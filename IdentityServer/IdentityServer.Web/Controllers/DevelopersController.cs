using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityServer.Services;
using IdentityServer.Web.Areas.Developers.Models;

namespace IdentityServer.Web.Controllers
{
    [RoutePrefix("developers")]
    public class DevelopersController : Controller
    {
        private readonly IDeveloperService _developerService;

        public DevelopersController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        [HttpGet]
        [Route("index")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await _developerService.Register(model.Email, model.DisplayName, model.Password);

            return RedirectToAction("Index");
        }
    }
}