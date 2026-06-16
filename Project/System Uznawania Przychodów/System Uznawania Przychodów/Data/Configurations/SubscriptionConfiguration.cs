using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(s => s.Id);
        builder.ToTable("Subscriptions");

        builder.Property(s => s.Name).HasMaxLength(150);
        builder.Property(s => s.RenewalPrice).HasColumnType("decimal(18,2)");
        builder.Property(s => s.IsCancelled).HasDefaultValue(false);

        builder.HasOne(s => s.Client)
            .WithMany(c => c.Subscriptions)
            .HasForeignKey(s => s.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(s => s.Software)
            .WithMany(sw => sw.Subscriptions)
            .HasForeignKey(s => s.SoftwareId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Subscription { Id = 1, ClientId = 4, SoftwareId = 1, Name = "Sub1", RenewalPeriodMonths = 1, RenewalPrice = 1000m, StartDate = new DateTime(2026, 5, 1), NextPeriodStart = new DateTime(2026, 6, 1), HasPaid = true, IsCancelled = false },
            new Subscription { Id = 2, ClientId = 5, SoftwareId = 2, Name = "Sub2", RenewalPeriodMonths = 12, RenewalPrice = 5000m, StartDate = new DateTime(2026, 1, 1), NextPeriodStart = new DateTime(2027, 1, 1), HasPaid = true, IsCancelled = false },
            new Subscription { Id = 3, ClientId = 1, SoftwareId = 3, Name = "Sub3", RenewalPeriodMonths = 1, RenewalPrice = 800m, StartDate = new DateTime(2025, 1, 1), NextPeriodStart = new DateTime(2025, 2, 1), HasPaid = false, IsCancelled = true }
        );
    }
}
