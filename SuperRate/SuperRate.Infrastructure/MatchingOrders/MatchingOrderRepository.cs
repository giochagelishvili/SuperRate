using SuperRate.Application.MatchingOrders.Interfaces;
using SuperRate.Domain.MatchingOrders;
using SuperRate.Persistence.Context;

namespace SuperRate.Infrastructure.MatchingOrders;

public class MatchingOrderRepository : BaseRepository<MatchingOrder>, IMatchingOrderRepository
{
    public MatchingOrderRepository(SuperRateContext context) : base(context)
    {
    }
}