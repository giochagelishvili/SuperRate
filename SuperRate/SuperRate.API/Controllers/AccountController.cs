using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SuperRate.API.Infrastructure.Authorization;
using SuperRate.Application.Accounts.Interfaces;
using SuperRate.Application.Accounts.Requests;
using SuperRate.Application.Users.Interfaces;

namespace SuperRate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AccountController(IAccountService accountService, IUserService userService, IConfiguration config)
        {
            _accountService = accountService;
            _userService = userService;
            _config = config;
        }

        [Authorize]
        [HttpGet]
        public string Get()
        {
            return "it works";
        }

        [HttpPost("login")]
        public async Task<string> LoginAsync(LoginRequestModel loginRequestModel)
        {
            await _accountService.LoginAsync(loginRequestModel);

            var user = await _userService.GetUserByNameAsync(loginRequestModel.UserName);

            var token = JwtHelper.GenerateToken(user, _config);

            return JsonConvert.SerializeObject(token);
        }

        [HttpPost("register")]
        public async Task RegisterAsync(RegisterRequestModel registerRequestModel)
        {
            await _accountService.RegisterAsync(registerRequestModel);
        }
    }
}