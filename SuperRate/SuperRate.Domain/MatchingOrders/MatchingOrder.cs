namespace SuperRate.Domain.MatchingOrders;

public class MatchingOrder
{
    public int Id { get; set; }
    public int FirstOrderId { get; set; }
    public int SecondOrderId { get; set; }
}