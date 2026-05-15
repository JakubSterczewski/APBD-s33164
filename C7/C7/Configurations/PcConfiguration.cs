using C7.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C7.Configurations;

public class PcConfiguration : IEntityTypeConfiguration<Pc>
{
    public void Configure(EntityTypeBuilder<Pc> builder)
    {
        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.Name).HasMaxLength(50);
        builder.Property(pc => pc.Weight).HasColumnType("float(5)");
        builder.Property(pc => pc.CreatedAt).HasColumnType("datetime");
            
        builder.ToTable("PCs");
        
        builder.HasData(new List<Pc>
        {
            new Pc { Id = 1, Name = "Gaming Beast X", Weight = 12.5f, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
            new Pc { Id = 2, Name = "Office Mini Pro", Weight = 4.2f, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
            new Pc { Id = 3, Name = "Workstation Ultra", Weight = 8.0f, Warranty = 36, CreatedAt = new DateTime(2026, 3, 1, 10, 0, 0), Stock = 3 },
        });

    }
}