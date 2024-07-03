using SuperRate.Application.Users.Responses;

namespace SuperRate.Application.Users.Interfaces;

public interface IUserService
{
    Task<UserResponseModel> GetUserByNameAsync(string userName);
}