using FluentValidation;
using SuperRate.API.Localizations;
using SuperRate.Application.Accounts.Requests;

namespace SuperRate.API.Infrastructure.Validations.Accounts;

public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
{
    public LoginRequestModelValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage(ErrorMessages.UsernameRequired)
            .Matches("^[a-zA-Z]{4,20}$")
            .WithMessage(ErrorMessages.UsernameInvalidFormat);

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage(ErrorMessages.PasswordRequired)
            .Matches("^(?=.*[!@#$%^&*()_+=\\-{}[\\]|\\\\:;\"'<>,.?/~`]).{12,20}$")
            .WithMessage(ErrorMessages.PasswordInvalidFormat);
    }
}