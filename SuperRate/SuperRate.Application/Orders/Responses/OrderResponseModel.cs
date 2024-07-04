namespace SuperRate.Application.Orders.Responses;

public class OrderResponseModel
{
    public int Id { get; set; }
    public string BuyingCurrency { get; set; } = default!;
    public string SellingCurrency { get; set; } = default!;
    public decimal BuyingAmount { get; set; }
    public decimal SellingAmount { get; set; }
    public string Status { get; set; } = default!;
    public string TaxCurrency { get; set; } = default!;
    public decimal TaxAmount { get; set; }
}