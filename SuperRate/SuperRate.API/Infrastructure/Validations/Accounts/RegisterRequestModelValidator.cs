using FluentValidation;
using SuperRate.API.Localizations;
using SuperRate.Application.Accounts.Requests;

namespace SuperRate.API.Infrastructure.Validations.Accounts;

public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
{
    public RegisterRequestModelValidator()
    {
        RuleFor(x => x.IdentificationNumber)
            .NotNull()
            .WithMessage(ErrorMessages.IdentificationNumberRequired)
            .Matches("^[0-9]{6,}$")
            .WithMessage(ErrorMessages.IdentificationNumberInvalidFormat);

        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage(ErrorMessages.UsernameRequired)
            .Matches("^[a-zA-Z]{4,20}$")
            .WithMessage(ErrorMessages.UsernameInvalidFormat);

        RuleFor(x => x.Email)
            .NotNull()
            .WithMessage(ErrorMessages.EmailRequired)
            .Matches("[A-Z0-9a-z._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}")
            .WithMessage(ErrorMessages.EmailInvalidFormat);

        RuleFor(x => x.PhoneNumber)
            .NotNull()
            .WithMessage(ErrorMessages.PhoneNumberRequired)
            .Matches("^5[0-9]{8}$")
            .WithMessage(ErrorMessages.PhoneNumberInvalidFormat);

        RuleFor(x => x.Password)
            .NotNull()
            .WithMessage(ErrorMessages.PasswordRequired)
            .Matches("^(?=.*[!@#$%^&*()_+=\\-{}[\\]|\\\\:;\"'<>,.?/~`]).{12,20}$")
            .WithMessage(ErrorMessages.PasswordInvalidFormat);

        RuleFor(x => x.ConfirmPassword)
            .NotNull()
            .WithMessage(ErrorMessages.ConfirmPasswordRequired)
            .Equal(x => x.Password)
            .WithMessage(ErrorMessages.PasswordDoesNotMatch);
    }
}