using Mapster;
using Microsoft.AspNetCore.Identity;
using SuperRate.Application.Accounts.Interfaces;
using SuperRate.Application.Accounts.Requests;
using SuperRate.Application.Exceptions;
using SuperRate.Domain.Users;

namespace SuperRate.Application.Accounts;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task LoginAsync(LoginRequestModel loginRequestModel)
    {
        var user = await _userManager.FindByNameAsync(loginRequestModel.UserName);

        if (user == null)
            throw new UserNotFoundException();

        var signInResult =
            await _signInManager.PasswordSignInAsync(loginRequestModel.UserName, loginRequestModel.Password, false,
                false);

        if (!signInResult.Succeeded)
            throw new InvalidPasswordException();
    }

    public async Task RegisterAsync(RegisterRequestModel registerRequestModel)
    {
        if (await _userManager.FindByEmailAsync(registerRequestModel.Email) != null)
            throw new EmailAlreadyExistsException();

        if (await _userManager.FindByNameAsync(registerRequestModel.UserName) != null)
            throw new UsernameAlreadyExistsException();

        var user = registerRequestModel.Adapt<User>();
        var registerResult = await _userManager.CreateAsync(user, registerRequestModel.Password);

        if (!registerResult.Succeeded)
            throw new CouldNotRegisterException();
    }
}