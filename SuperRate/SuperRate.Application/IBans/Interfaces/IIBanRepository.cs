using SuperRate.Domain.IBans;

namespace SuperRate.Application.IBans.Interfaces;

public interface IIBanRepository
{
    Task<List<IBan>> GetAllAsync(int userId, CancellationToken cancellationToken);
    Task CreateAsync(IBan iBan, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int iBanId, int userId, CancellationToken cancellationToken);
}