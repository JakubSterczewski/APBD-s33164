using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class SoftwareDiscountConfiguration : IEntityTypeConfiguration<SoftwareDiscount>
{
    public void Configure(EntityTypeBuilder<SoftwareDiscount> builder)
    {
        builder.HasKey(sd => new { sd.SoftwareId, sd.DiscountId });
        builder.ToTable("SoftwareDiscounts");

        builder.HasOne(sd => sd.Software)
            .WithMany(s => s.SoftwareDiscounts)
            .HasForeignKey(sd => sd.SoftwareId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sd => sd.Discount)
            .WithMany(d => d.SoftwareDiscounts)
            .HasForeignKey(sd => sd.DiscountId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new SoftwareDiscount { SoftwareId = 1, DiscountId = 3 },
            new SoftwareDiscount { SoftwareId = 2, DiscountId = 3 }
        );
    }
}
