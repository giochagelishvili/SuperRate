using SuperRate.Application.IBans.Requests;
using SuperRate.Application.IBans.Responses;

namespace SuperRate.Application.IBans.Interfaces;

public interface IIBanService
{
    Task<List<IBanResponseModel>> GetAllAsync(int userId, CancellationToken cancellationToken);
    Task CreateAsync(IBanRequestPostModel requestPostModel, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int iBanId, int userId, CancellationToken cancellationToken);
}