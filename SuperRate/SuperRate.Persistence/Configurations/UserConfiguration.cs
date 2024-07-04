using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperRate.Domain.Users;

namespace SuperRate.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(x => x.Orders)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.IBans)
            .WithOne(iban => iban.User)
            .OnDelete(DeleteBehavior.NoAction);
    }
}