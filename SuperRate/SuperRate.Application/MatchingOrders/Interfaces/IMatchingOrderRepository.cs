using SuperRate.Domain.MatchingOrders;

namespace SuperRate.Application.MatchingOrders.Interfaces;

public interface IMatchingOrderRepository
{
    Task CreateAsync(MatchingOrder matchingOrder, CancellationToken cancellationToken);
}