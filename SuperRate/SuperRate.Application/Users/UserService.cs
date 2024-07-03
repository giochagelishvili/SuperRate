using Mapster;
using Microsoft.AspNetCore.Identity;
using SuperRate.Application.Exceptions;
using SuperRate.Application.Users.Interfaces;
using SuperRate.Application.Users.Responses;
using SuperRate.Domain.Users;

namespace SuperRate.Application.Users;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserResponseModel> GetUserByNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
            throw new UserNotFoundException();

        return user.Adapt<UserResponseModel>();
    }
}