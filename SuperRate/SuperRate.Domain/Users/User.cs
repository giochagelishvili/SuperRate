using Microsoft.AspNetCore.Identity;
using SuperRate.Domain.BaseEntity;

namespace SuperRate.Domain.Users;

public class User : IdentityUser<int>, IEntity
{
    public string IdentificationNumber { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}