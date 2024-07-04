using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperRate.Application.IBans.Interfaces;
using SuperRate.Application.IBans.Requests;
using SuperRate.Application.IBans.Responses;

namespace SuperRate.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IBanController : ControllerBase
    {
        private readonly IIBanService _iBanService;

        public IBanController(IIBanService iBanService)
        {
            _iBanService = iBanService;
        }

        [HttpGet("getAllIbans")]
        public async Task<List<IBanResponseModel>> GetAllAsync(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return await _iBanService.GetAllAsync(userId, cancellationToken);
        }

        [HttpPost("addIban")]
        public async Task CreateAsync(string iBanNumber, CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var requestPostModel = new IBanRequestPostModel
            {
                IBanNumber = iBanNumber,
                UserId = userId
            };

            await _iBanService.CreateAsync(requestPostModel, cancellationToken);
        }
    }
}