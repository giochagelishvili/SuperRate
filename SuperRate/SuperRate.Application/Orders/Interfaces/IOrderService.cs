using SuperRate.Application.Orders.Requests;
using SuperRate.Application.Orders.Responses;
using SuperRate.Domain.Enums;

namespace SuperRate.Application.Orders.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponseModel>> GetAllByStatusAsync(int userId, Status status, CancellationToken cancellationToken);
    Task<List<OrderResponseModel>> GetAllAsync(int userId, CancellationToken cancellationToken);
    Task CheckMatchingOrdersAsync(int lastOrderId, CancellationToken cancellationToken);
    Task CreateOrderAsync(OrderRequestPostModel orderRequestPostModel, CancellationToken cancellationToken);
    Task CancelOrderAsync(int orderId, int userId, CancellationToken cancellationToken);
}