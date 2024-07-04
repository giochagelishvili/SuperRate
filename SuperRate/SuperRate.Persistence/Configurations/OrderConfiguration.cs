using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperRate.Domain.Enums;
using SuperRate.Domain.Orders;

namespace SuperRate.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.BuyingCurrency)
            .IsRequired();

        builder.Property(x => x.SellingCurrency)
            .IsRequired();

        builder.Property(x => x.BuyingAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.SellingAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Status)
            .IsRequired()
            .HasDefaultValue(Status.Active);

        builder.Property(x => x.TaxCurrency)
            .IsRequired();

        builder.Property(x => x.TaxAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.IBanId)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ModifiedAt)
            .IsRequired();

        builder.HasOne(order => order.User)
            .WithMany(company => company.Orders)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(order => order.IBanNumber)
            .WithMany(iban => iban.Orders)
            .HasForeignKey(order => order.IBanId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}