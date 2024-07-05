namespace SuperRate.Application.Exceptions;

public class EmailAlreadyExistsException : Exception
{
    public static readonly string Code = "EmailAlreadyExists";

    public EmailAlreadyExistsException(string message = "Email already exists.") : base(message)
    {
    }
}