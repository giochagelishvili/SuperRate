namespace SuperRate.Application.Users.Responses;

public class UserResponseModel
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
}