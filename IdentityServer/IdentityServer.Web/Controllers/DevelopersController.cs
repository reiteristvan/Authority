using System.Threading.Tasks;
using System.Web.Mvc;
using IdentityServer.Services;
using IdentityServer.Web.Models.Developers;

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
            await _developerService.Register(model.Email, model.Username, model.Password);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login(LoginModel model)
        {
            return View();
        }
    }
}