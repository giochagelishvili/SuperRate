using SuperRate.Application.MatchingOrders.Interfaces;
using SuperRate.Domain.MatchingOrders;

namespace SuperRate.Application.MatchingOrders;

public class MatchingOrderService : IMatchingOrderService
{
    private readonly IMatchingOrderRepository _matchingOrderRepository;

    public MatchingOrderService(IMatchingOrderRepository matchingOrderRepository)
    {
        _matchingOrderRepository = matchingOrderRepository;
    }

    public async Task CreateAsync(MatchingOrder matchingOrder, CancellationToken cancellationToken)
    {
        await _matchingOrderRepository.CreateAsync(matchingOrder, cancellationToken);
    }
}