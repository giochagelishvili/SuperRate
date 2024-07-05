using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperRate.Application.Users.Interfaces;
using SuperRate.Application.Users.Responses;

namespace SuperRate.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getUser")]
        public async Task<UserResponseModel> GetUserAsync()
        {
            var username = User.FindFirstValue(ClaimTypes.Name)!;

            return await _userService.GetUserByNameAsync(username);
        }
    }
}