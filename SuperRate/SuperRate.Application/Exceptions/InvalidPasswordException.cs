namespace SuperRate.Application.Exceptions;

public class InvalidPasswordException : Exception
{
    public static readonly string Code = "InvalidPassword";
    
    public InvalidPasswordException(string message = "Invalid password.") : base(message)
    {
    }
}