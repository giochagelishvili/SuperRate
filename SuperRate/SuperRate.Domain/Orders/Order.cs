using SuperRate.Domain.BaseEntity;
using SuperRate.Domain.Enums;
using SuperRate.Domain.IBans;
using SuperRate.Domain.Users;

namespace SuperRate.Domain.Orders;

public class Order : IEntity
{
    public int Id { get; set; }
    public Currency BuyingCurrency { get; set; }
    public Currency SellingCurrency { get; set; }
    public decimal BuyingAmount { get; set; }
    public decimal SellingAmount { get; set; }
    public Status Status { get; set; }
    public Currency TaxCurrency { get; set; }
    public decimal TaxAmount { get; set; }
    public int IBanId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }

    // Navigation Properties
    public User User { get; set; } = default!;
    public IBan IBanNumber { get; set; } = default!;
}