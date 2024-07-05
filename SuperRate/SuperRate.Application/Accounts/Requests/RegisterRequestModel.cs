namespace SuperRate.Application.Accounts.Requests;

public class RegisterRequestModel
{
    public string IdentificationNumber { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public bool AgreeToTerms { get; set; }
}