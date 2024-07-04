using Microsoft.EntityFrameworkCore;
using SuperRate.Application.IBans.Interfaces;
using SuperRate.Domain.IBans;
using SuperRate.Persistence.Context;

namespace SuperRate.Infrastructure.IBans;

public class IBanRepository : BaseRepository<IBan>, IIBanRepository
{
    public IBanRepository(SuperRateContext context) : base(context)
    {
    }

    public async Task<List<IBan>> GetAllAsync(int userId, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int iBanId, int userId, CancellationToken cancellationToken)
    {
        return await AnyAsync(x => x.Id == iBanId && x.UserId == userId, cancellationToken);
    }
}