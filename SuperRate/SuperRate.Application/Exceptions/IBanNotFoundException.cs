namespace SuperRate.Application.Exceptions;

public class IBanNotFoundException : Exception
{
    public readonly string Code = "IBanNotFound";
    
    public IBanNotFoundException(string message = "IBan not found.") : base(message)
    {
    }
}