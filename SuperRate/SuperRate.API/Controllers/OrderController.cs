using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperRate.Application.Orders.Interfaces;
using SuperRate.Application.Orders.Requests;
using SuperRate.Application.Orders.Responses;
using SuperRate.Domain.Enums;

namespace SuperRate.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getAllOrders")]
        public async Task<List<OrderResponseModel>> GetAllOrdersAsync(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            return await _orderService.GetAllAsync(userId, cancellationToken);
        }

        [HttpPost("createOrder")]
        public async Task CreateOrderAsync(OrderRequestPostModel orderRequestPostModel,
            CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            orderRequestPostModel.UserId = userId;

            await _orderService.CreateOrderAsync(orderRequestPostModel, cancellationToken);
        }
    }
}