using SuperRate.Domain.Enums;
using SuperRate.Domain.Orders;

namespace SuperRate.Application.Orders.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetAllByStatusAsync(int userId, Status status, CancellationToken cancellationToken);
    Task<List<Order>> GetAllAsync(int userId, CancellationToken cancellationToken);
    Task CreateAsync(Order order, CancellationToken cancellationToken);
    Task CancelOrderAsync(int orderId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int orderId, int userId, CancellationToken cancellationToken);
}