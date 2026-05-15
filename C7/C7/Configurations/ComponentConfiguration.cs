using C7.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C7.Configurations;

public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        builder.HasKey(c => c.Code);
        builder.Property(c => c.Code).HasColumnType("char(10)");
        builder.Property(c => c.Name).HasMaxLength(300);
            
        builder.HasOne(c => c.ComponentManufacturer)
            .WithMany(cm => cm.Components)
            .HasForeignKey(c => c.ComponentManufacturersId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(c => c.ComponentType)
            .WithMany(ct => ct.Components)
            .HasForeignKey(c => c.ComponentTypesId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.ToTable("Components");
        
        builder.HasData(new List<Component>
        {
            new Component { Code = "CPU0000001", Name = "Ryzen 7 7800X3D", Descritpion = "8-core gaming processor", ComponentManufacturersId = 1, ComponentTypesId = 1 },
            new Component { Code = "GPU0000001", Name = "RTX 4080 Super", Descritpion = "High-end gaming graphics card", ComponentManufacturersId = 2, ComponentTypesId = 2 },
            new Component { Code = "RAM0000001", Name = "Corsair Vengeance DDR5 16GB", Descritpion = "DDR5 RAM module 16GB", ComponentManufacturersId = 3, ComponentTypesId = 3 },
        });
    }
}