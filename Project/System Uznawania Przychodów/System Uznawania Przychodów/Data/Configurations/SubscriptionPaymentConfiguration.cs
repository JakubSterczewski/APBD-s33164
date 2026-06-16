using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class SubscriptionPaymentConfiguration : IEntityTypeConfiguration<SubscriptionPayment>
{
    public void Configure(EntityTypeBuilder<SubscriptionPayment> builder)
    {
        builder.HasKey(p => p.Id);
        builder.ToTable("SubscriptionPayments");

        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");
            
        builder.HasOne(p => p.Subscription)
            .WithMany(s => s.Payments)
            .HasForeignKey(p => p.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new SubscriptionPayment { Id = 1, SubscriptionId = 1, Amount = 1000m, PaidAt = new DateTime(2026, 5, 1) },
            new SubscriptionPayment { Id = 2, SubscriptionId = 1, Amount = 1000m, PaidAt = new DateTime(2026, 6, 1) },
            new SubscriptionPayment { Id = 3, SubscriptionId = 2, Amount = 5000m, PaidAt = new DateTime(2026, 1, 1) },
            new SubscriptionPayment { Id = 4, SubscriptionId = 3, Amount = 800m, PaidAt = new DateTime(2025, 1, 1) }
        );
    }
}
