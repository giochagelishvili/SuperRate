namespace SuperRate.Application.Exceptions;

public class UsernameAlreadyExistsException : Exception
{
    public static readonly string Code = "UsernameAlreadyExists";

    public UsernameAlreadyExistsException(string message = "Username already exists.") : base(message)
    {
    }
}