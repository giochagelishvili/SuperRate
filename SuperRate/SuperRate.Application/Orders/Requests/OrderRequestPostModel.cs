using SuperRate.Domain.Enums;

namespace SuperRate.Application.Orders.Requests;

public class OrderRequestPostModel
{
    public Currency BuyingCurrency { get; set; }
    public Currency SellingCurrency { get; set; }
    public decimal BuyingAmount { get; set; }
    public decimal SellingAmount { get; set; }
    public Currency TaxCurrency { get; set; }
    public Currency TaxAmount { get; set; }
    public int IBanId { get; set; }
    public int UserId { get; set; }
}