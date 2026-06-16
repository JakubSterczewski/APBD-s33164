using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(c => c.Id);
        builder.ToTable("Contracts");

        builder.Property(c => c.SoftwareVersion).HasMaxLength(20);
        builder.Property(c => c.Price).HasColumnType("decimal(18,2)");
        builder.Property(c => c.TotalPaid).HasColumnType("decimal(18,2)").HasDefaultValue(0);
        builder.Property(c => c.SignedAt).IsRequired(false);
        builder.Property(c => c.IsActive).HasDefaultValue(true);

        builder.HasOne(c => c.Client)
            .WithMany(cl => cl.Contracts)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Software)
            .WithMany(s => s.Contracts)
            .HasForeignKey(c => c.SoftwareId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Contract { Id = 1, ClientId = 1, SoftwareId = 1, SoftwareVersion = "3.2.1", StartDate = new DateOnly(2026, 1, 1), EndDate = new DateOnly(2026, 1, 15), Price = 10000m, TotalPaid = 10000m, AdditionalSupportYears = 0, IsActive = true, SignedAt = new DateOnly(2026, 1, 10) },
            new Contract { Id = 2, ClientId = 2, SoftwareId = 2, SoftwareVersion = "1.8.0", StartDate = new DateOnly(2026, 6, 1), EndDate = new DateOnly(2026, 6, 20), Price = 5000m, TotalPaid = 2000m, AdditionalSupportYears = 1, IsActive = true, SignedAt = null },
            new Contract { Id = 3, ClientId = 3, SoftwareId = 3, SoftwareVersion = "2.0.4", StartDate = new DateOnly(2026, 5, 1), EndDate = new DateOnly(2026, 5, 20), Price = 8000m, TotalPaid = 0m, AdditionalSupportYears = 0, IsActive = false, SignedAt = null }
        );
    }
}
