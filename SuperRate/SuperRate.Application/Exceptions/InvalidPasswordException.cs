namespace SuperRate.Application.Exceptions;

public class InvalidPasswordException : Exception
{
    public readonly string Code = "InvalidPassword";
    
    public InvalidPasswordException(string message = "Invalid password.") : base(message)
    {
    }
}