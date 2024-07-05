namespace SuperRate.Application.Users.Responses;

public class UserResponseModel
{
    public int Id { get; set; }
    public string UserName { get; set; } = default!;
    public string IdentificationNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}