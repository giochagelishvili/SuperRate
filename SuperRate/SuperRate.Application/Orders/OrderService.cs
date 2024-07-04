using Mapster;
using SuperRate.Application.Exceptions;
using SuperRate.Application.IBans.Interfaces;
using SuperRate.Application.Orders.Interfaces;
using SuperRate.Application.Orders.Requests;
using SuperRate.Application.Orders.Responses;
using SuperRate.Domain.Enums;
using SuperRate.Domain.Orders;

namespace SuperRate.Application.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IIBanService _iBanService;

    public OrderService(IOrderRepository orderRepository, IIBanService iBanService)
    {
        _orderRepository = orderRepository;
        _iBanService = iBanService;
    }

    public async Task<List<OrderResponseModel>> GetAllByStatusAsync(int userId, Status status,
        CancellationToken cancellationToken)
    {
        var result = await _orderRepository.GetAllByStatusAsync(userId, status, cancellationToken);

        return result.Adapt<List<OrderResponseModel>>();
    }

    public async Task<List<OrderResponseModel>> GetAllAsync(int userId, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.GetAllAsync(userId, cancellationToken);

        return result.Adapt<List<OrderResponseModel>>();
    }

    public async Task CreateOrderAsync(OrderRequestPostModel orderRequestPostModel, CancellationToken cancellationToken)
    {
        if (!await _iBanService.ExistsAsync(orderRequestPostModel.IBanId, orderRequestPostModel.UserId,
                cancellationToken))
            throw new IBanNotFoundException();

        var order = orderRequestPostModel.Adapt<Order>();

        await _orderRepository.CreateAsync(order, cancellationToken);
    }

    public async Task CancelOrderAsync(int orderId, int userId, CancellationToken cancellationToken)
    {
        if (!await _orderRepository.ExistsAsync(orderId, userId, cancellationToken))
            throw new OrderNotFoundException();

        await _orderRepository.CancelOrderAsync(orderId, cancellationToken);
    }
}