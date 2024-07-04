using Mapster;
using SuperRate.Application.IBans.Interfaces;
using SuperRate.Application.IBans.Requests;
using SuperRate.Application.IBans.Responses;
using SuperRate.Domain.IBans;

namespace SuperRate.Application.IBans;

public class IBanService : IIBanService
{
    private readonly IIBanRepository _iBanRepository;

    public IBanService(IIBanRepository iBanRepository)
    {
        _iBanRepository = iBanRepository;
    }

    public async Task<List<IBanResponseModel>> GetAllAsync(int userId, CancellationToken cancellationToken)
    {
        var result = await _iBanRepository.GetAllAsync(userId, cancellationToken);

        return result.Adapt<List<IBanResponseModel>>();
    }

    public async Task CreateAsync(IBanRequestPostModel requestPostModel, CancellationToken cancellationToken)
    {
        var iBan = requestPostModel.Adapt<IBan>();

        await _iBanRepository.CreateAsync(iBan, cancellationToken);
    }

    public async Task<bool> ExistsAsync(int iBanId, int userId, CancellationToken cancellationToken)
    {
        return await _iBanRepository.ExistsAsync(iBanId, userId, cancellationToken);
    }
}