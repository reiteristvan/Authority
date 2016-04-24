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
        public async Task Activate(ActivateModel model)
        {
            await _accountService.ActivateUser(model.ClientId, model.ActivationCode);
        }

        [ApiTokenFilter]
        [Route("login")]
        [HttpPost]
        public async Task<LoginResponse> Login(LoginModel model)
        {
            LoginResponse result = new LoginResponse
            {
                Success = false,
                AccessToken = ""
            };

            string accessToken = await _accountService.LogInUser(model.ClientId, model.Email, model.Password);

            if (string.IsNullOrEmpty(accessToken))
            {
                return result;
            }

            result.AccessToken = accessToken;
            result.Success = true;
            return result;
        }

        [ApiTokenFilter]
        [Route("token")]
        [HttpPost]
        public async Task ExchangeToken(ExchangeTokenModel model)
        {
            
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