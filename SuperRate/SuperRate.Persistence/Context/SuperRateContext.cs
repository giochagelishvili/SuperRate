using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperRate.Domain.BaseEntity;
using SuperRate.Domain.IBans;
using SuperRate.Domain.Orders;
using SuperRate.Domain.Users;

namespace SuperRate.Persistence.Context;

public class SuperRateContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public SuperRateContext(DbContextOptions<SuperRateContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
        modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
        modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
        modelBuilder.Entity<Order>().ToTable("Orders");
        modelBuilder.Entity<IBan>().ToTable("IBans");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var trackedEntries = base.ChangeTracker.Entries<IEntity>()
            .Where(q => q.State is EntityState.Added or EntityState.Modified);

        foreach (var entry in trackedEntries)
        {
            entry.Entity.ModifiedAt = DateTime.Now.ToUniversalTime();

            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.Now.ToUniversalTime();
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}