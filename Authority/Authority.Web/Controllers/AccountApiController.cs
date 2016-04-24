using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityServer.Services;
using IdentityServer.Web.Infrastructure.Filters;
using IdentityServer.Web.Models.Account;

namespace IdentityServer.Web.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountApiController : ApiController
    {
        private readonly IAccountService _accountService;

        public AccountApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<HttpResponseMessage> Register(RegisterModel model)
        {
            if (!await _accountService.ValidateProduct(model.ClientId))
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            await _accountService.RegisterUser(model.ClientId, model.Email, model.Username, model.Password);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [ApiTokenFilter]
        [Route("activate")]
        [HttpPost]
        public async Task<HttpResponseMessage> Activate(ActivateModel model)
        {
            if (!await _accountService.ValidateProductWithSecret(model.ClientId, model.ClientSecret))
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            await _accountService.ActivateUser(model.ActivationCode);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [ApiTokenFilter]
        [Route("login")]
        [HttpPost]
        public async Task<LoginResponse> Login(LoginModel model)
        {
            if (!await _accountService.ValidateProductWithSecret(model.ClientId, model.ClientSecret))
            {
                
            }

            return new LoginResponse
            {
                AccessToken = ""
            };
        }

        [ApiTokenFilter]
        [Route("test")]
        [HttpGet]
        public string Test()
        {
            return "alma";
        }
    }
}