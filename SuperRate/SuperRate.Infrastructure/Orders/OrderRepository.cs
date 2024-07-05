using Microsoft.EntityFrameworkCore;
using SuperRate.Application.Orders.Interfaces;
using SuperRate.Domain.Enums;
using SuperRate.Domain.Orders;
using SuperRate.Persistence.Context;

namespace SuperRate.Infrastructure.Orders;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(SuperRateContext context) : base(context)
    {
    }

    public async Task<List<Order>> GetAllByStatusAsync(int userId, Status status, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => x.UserId == userId && x.Status == status)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Order>> GetAllAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CreateOrderAsync(Order order, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return order.Id;
    }

    public async Task<int> CheckMatchingOrderAsync(int lastOrderId, CancellationToken cancellationToken)
    {
        var lastOrder = await _dbSet.FirstAsync(x => x.Id == lastOrderId, cancellationToken);

        var matchingOrder = await _dbSet.FirstOrDefaultAsync(
            x => x.Id != lastOrderId &&
                 x.UserId != lastOrder.UserId &&
                 x.Status == Status.Active &&
                 x.BuyingAmount == lastOrder.BuyingAmount &&
                 x.SellingAmount == lastOrder.SellingAmount &&
                 x.BuyingCurrency == lastOrder.BuyingCurrency &&
                 x.SellingCurrency == lastOrder.SellingCurrency,
            cancellationToken
        );

        return matchingOrder?.Id ?? 0;
    }

    public async Task CancelOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await _dbSet.FirstAsync(x => x.Id == orderId, cancellationToken);

        order.Status = Status.Canceled;

        _dbSet.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CompleteOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await _dbSet.FirstAsync(x => x.Id == orderId, cancellationToken);

        order.Status = Status.Completed;

        _dbSet.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int orderId, int userId, CancellationToken cancellationToken)
    {
        return await AnyAsync(x => x.Id == orderId && x.UserId == userId, cancellationToken);
    }
}