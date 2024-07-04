using Microsoft.AspNetCore.Identity;
using SuperRate.Domain.BaseEntity;
using SuperRate.Domain.IBans;
using SuperRate.Domain.Orders;

namespace SuperRate.Domain.Users;

public class User : IdentityUser<int>, IEntity
{
    public string IdentificationNumber { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    
    // Navigation properties
    public List<Order> Orders { get; set; } = default!;
    public List<IBan> IBans { get; set; } = default!;
}