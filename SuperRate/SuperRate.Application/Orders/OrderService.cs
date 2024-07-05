using Mapster;
using SuperRate.Application.Exceptions;
using SuperRate.Application.IBans.Interfaces;
using SuperRate.Application.MatchingOrders.Interfaces;
using SuperRate.Application.Orders.Interfaces;
using SuperRate.Application.Orders.Requests;
using SuperRate.Application.Orders.Responses;
using SuperRate.Domain.Enums;
using SuperRate.Domain.MatchingOrders;
using SuperRate.Domain.Orders;

namespace SuperRate.Application.Orders;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IIBanService _iBanService;
    private readonly IMatchingOrderService _matchingOrderService;

    public OrderService(IOrderRepository orderRepository, IIBanService iBanService,
        IMatchingOrderService matchingOrderService)
    {
        _orderRepository = orderRepository;
        _iBanService = iBanService;
        _matchingOrderService = matchingOrderService;
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

    public async Task CheckMatchingOrdersAsync(int lastOrderId, CancellationToken cancellationToken)
    {
        int matchingOrderId = await _orderRepository.CheckMatchingOrderAsync(lastOrderId, cancellationToken);

        if (matchingOrderId != 0)
        {
            var matchingOrder = new MatchingOrder { FirstOrderId = lastOrderId, SecondOrderId = matchingOrderId };
            
            await _matchingOrderService.CreateAsync(matchingOrder, cancellationToken);
            
            await _orderRepository.CompleteOrderAsync(lastOrderId, cancellationToken);
            await _orderRepository.CompleteOrderAsync(matchingOrderId, cancellationToken);
        }
    }

    public async Task CreateOrderAsync(OrderRequestPostModel orderRequestPostModel, CancellationToken cancellationToken)
    {
        if (!await _iBanService.ExistsAsync(orderRequestPostModel.IBanId, orderRequestPostModel.UserId,
                cancellationToken))
            throw new IBanNotFoundException();

        var order = orderRequestPostModel.Adapt<Order>();

        var lastOrderId = await _orderRepository.CreateOrderAsync(order, cancellationToken);

        await CheckMatchingOrdersAsync(lastOrderId, cancellationToken);
    }

    public async Task CancelOrderAsync(int orderId, int userId, CancellationToken cancellationToken)
    {
        if (!await _orderRepository.ExistsAsync(orderId, userId, cancellationToken))
            throw new OrderNotFoundException();

        await _orderRepository.CancelOrderAsync(orderId, cancellationToken);
    }
}