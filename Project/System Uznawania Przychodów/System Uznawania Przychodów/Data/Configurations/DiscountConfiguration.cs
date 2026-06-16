using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;
using System_Uznawania_Przychodów.Domain.Enums;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(d => d.Id);
        builder.ToTable("Discounts");

        builder.Property(d => d.Name).HasMaxLength(150);
        builder.Property(d => d.Percentage).HasColumnType("decimal(5,2)");
        builder.Property(d => d.IsOnAllProducts).HasDefaultValue(false);

        builder.HasData(
            new Discount { Id = 1, Name = "Disc1", PurchaseModel = PurchaseModel.License, Percentage = 10m, StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 12, 31), IsOnAllProducts = true },
            new Discount { Id = 2, Name = "Disc2", PurchaseModel = PurchaseModel.Subscription, Percentage = 15m, StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 12, 31), IsOnAllProducts = true },
            new Discount { Id = 3, Name = "Disc3", PurchaseModel = PurchaseModel.License, Percentage = 8m, StartDate = new DateTime(2026, 1, 1), EndDate = new DateTime(2026, 12, 31), IsOnAllProducts = false },
            new Discount { Id = 4, Name = "Disc4", PurchaseModel = PurchaseModel.License, Percentage = 20m, StartDate = new DateTime(2025, 1, 1), EndDate = new DateTime(2025, 12, 31), IsOnAllProducts = true }
        );
    }
}
