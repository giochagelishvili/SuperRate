using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperRate.Domain.IBans;

namespace SuperRate.Persistence.Configurations;

public class IBanConfiguration : IEntityTypeConfiguration<IBan>
{
    public void Configure(EntityTypeBuilder<IBan> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.IBanNumber)
            .IsRequired()
            .HasMaxLength(34);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ModifiedAt)
            .IsRequired();

        builder.HasOne(Iban => Iban.User)
            .WithMany(user => user.IBans)
            .HasForeignKey(Iban => Iban.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(Iban => Iban.Orders)
            .WithOne(order => order.IBanNumber)
            .OnDelete(DeleteBehavior.NoAction);
    }
}