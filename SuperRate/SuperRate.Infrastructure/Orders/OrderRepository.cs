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

    public async Task CancelOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        var order = await _dbSet.FirstAsync(x => x.Id == orderId, cancellationToken);

        order.Status = Status.Canceled;

        _dbSet.Update(order);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int orderId, int userId, CancellationToken cancellationToken)
    {
        return await AnyAsync(x => x.Id == orderId && x.UserId == userId, cancellationToken);
    }
}