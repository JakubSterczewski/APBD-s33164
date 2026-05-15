using C7.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C7.Configurations;

public class ComponentManufacturerConfiguration : IEntityTypeConfiguration<ComponentManufacturer>
{
    public void Configure(EntityTypeBuilder<ComponentManufacturer> builder)
    {
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Abbreviation).HasMaxLength(30);
        builder.Property(cm => cm.FullName).HasMaxLength(300);
            
        builder.ToTable("ComponentManufacturers");
        
        builder.HasData(new List<ComponentManufacturer>
        {
            new ComponentManufacturer { Id = 1, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateOnly(1969, 5, 1) },
            new ComponentManufacturer { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateOnly(1993, 4, 5) },
            new ComponentManufacturer { Id = 3, Abbreviation = "COR", FullName = "Corsair Gaming Inc.", FoundationDate = new DateOnly(1994, 1, 1) },
        });
    }
}