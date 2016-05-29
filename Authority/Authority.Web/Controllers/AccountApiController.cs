using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Authority.DomainModel;
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
                Success = false
            };

            if (!await _accountService.LogInUser(Context.ProductId, model.Email, model.Password))
            {
                return result;
            }

            List<Claim> claims = await _accountService.GetUserClaims(model.Email);

            result.Email = model.Email;
            result.Claims = claims.Select(c => new AuthorityClaim
            {
                Issuer = c.Issuer,
                Type = c.Type,
                Value = c.Value
            }).ToList();

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