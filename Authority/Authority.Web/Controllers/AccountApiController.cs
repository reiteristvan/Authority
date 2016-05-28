using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using IdentityServer.Services;
using IdentityServer.Web.Infrastructure;
using IdentityServer.Web.Infrastructure.Filters;
using IdentityServer.Web.Models.Account;

namespace IdentityServer.Web.Controllers
{
    [ApiKeyFilter]
    [RoutePrefix("api/account")]
    public class AccountApiController : AuthorityApiController
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
            await _accountService.RegisterUser(Context.ApiKey, model.Email, model.Username, model.Password);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [Route("activate")]
        [HttpPost]
        public async Task Activate(ActivateModel model)
        {
            await _accountService.ActivateUser(Context.ApiKey, model.ActivationCode);
        }

        [Route("login")]
        [HttpPost]
        public async Task<LoginResponse> Login(LoginModel model)
        {
            LoginResponse result = new LoginResponse
            {
                Success = false,
                AccessToken = ""
            };

            string accessToken = await _accountService.LogInUser(Context.ApiKey, model.Email, model.Password);

            if (string.IsNullOrEmpty(accessToken))
            {
                return result;
            }

            result.AccessToken = accessToken;
            result.Success = true;
            return result;
        }

        [Route("test")]
        [HttpGet]
        public string Test()
        {
            return "alma";
        }
    }
}