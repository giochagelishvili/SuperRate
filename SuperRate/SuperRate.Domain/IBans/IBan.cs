using SuperRate.Domain.BaseEntity;
using SuperRate.Domain.Orders;
using SuperRate.Domain.Users;

namespace SuperRate.Domain.IBans;

public class IBan : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string IBanNumber { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    
    // Navigation properties
    public User User { get; set; } = default!;
    public List<Order> Orders { get; set; } = default!;
}