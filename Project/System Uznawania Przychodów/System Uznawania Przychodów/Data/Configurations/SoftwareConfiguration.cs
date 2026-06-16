using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class SoftwareConfiguration : IEntityTypeConfiguration<Software>
{
    public void Configure(EntityTypeBuilder<Software> builder)
    {
        builder.HasKey(s => s.Id);
        builder.ToTable("Softwares");

        builder.Property(s => s.Name).HasMaxLength(150);
        builder.Property(s => s.Description).HasMaxLength(500);
        builder.Property(s => s.CurrentVersion).HasMaxLength(20);
        builder.Property(s => s.Category).HasMaxLength(100);
        builder.Property(s => s.YearlyLicensePrice).HasColumnType("decimal(18,2)");
        builder.Property(s => s.MonthlySubscriptionPrice).HasColumnType("decimal(18,2)");

        builder.HasData(
            new Software { Id = 1, Name = "Prog1", Description = "Desc1", CurrentVersion = "3.2.1", Category = "Finance", YearlyLicensePrice = 10000m, MonthlySubscriptionPrice = 1000m },
            new Software { Id = 2, Name = "Prog2", Description = "Desc2", CurrentVersion = "1.8.0", Category = "Education", YearlyLicensePrice = 5000m, MonthlySubscriptionPrice = 500m },
            new Software { Id = 3, Name = "Prog3", Description = "Desc3", CurrentVersion = "2.0.4", Category = "Education", YearlyLicensePrice = 8000m, MonthlySubscriptionPrice = 800m }
        );
    }
}
