using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class ContractPaymentConfiguration : IEntityTypeConfiguration<ContractPayment>
{
    public void Configure(EntityTypeBuilder<ContractPayment> builder)
    {
        builder.HasKey(p => p.Id);
        builder.ToTable("ContractPayments");

        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");

        builder.HasOne(p => p.Contract)
            .WithMany(c => c.Payments)
            .HasForeignKey(p => p.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new ContractPayment { Id = 1, ContractId = 1, Amount = 10000m, PaidAt = new DateTime(2026, 1, 10) },
            new ContractPayment { Id = 2, ContractId = 2, Amount = 2000m, PaidAt = new DateTime(2026, 6, 5) }
        );
    }
}
