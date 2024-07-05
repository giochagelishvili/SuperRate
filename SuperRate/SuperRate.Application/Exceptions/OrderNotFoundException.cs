namespace SuperRate.Application.Exceptions;

public class OrderNotFoundException : Exception
{
    public static readonly string Code = "Order not found.";
    
    public OrderNotFoundException(string message = "Order not found.") : base(message)
    {
    }
}