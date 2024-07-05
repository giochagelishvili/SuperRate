namespace SuperRate.Application.Exceptions;

public class UserNotFoundException : Exception
{
    public static readonly string Code = "UserNotFound";
    
    public UserNotFoundException(string message = "User not found.") : base(message)
    {
    }
}