using SuperRate.Application.Accounts.Requests;

namespace SuperRate.Application.Accounts.Interfaces;

public interface IAccountService
{
    Task LoginAsync(LoginRequestModel loginRequestModel);
    Task RegisterAsync(RegisterRequestModel registerRequestModel);
}