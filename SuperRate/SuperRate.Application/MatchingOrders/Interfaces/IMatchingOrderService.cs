using SuperRate.Domain.MatchingOrders;

namespace SuperRate.Application.MatchingOrders.Interfaces;

public interface IMatchingOrderService
{
    Task CreateAsync(MatchingOrder matchingOrder, CancellationToken cancellationToken);
}