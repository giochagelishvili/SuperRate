namespace SuperRate.Application.Exceptions;

public class OrderNotFoundException : Exception
{
    public readonly string Code = "Order not found.";
    
    public OrderNotFoundException(string message = "Order not found.") : base(message)
    {
    }
}