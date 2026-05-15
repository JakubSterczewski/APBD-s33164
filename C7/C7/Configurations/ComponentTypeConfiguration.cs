using C7.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C7.Configurations;

public class ComponentTypeConfiguration : IEntityTypeConfiguration<ComponentType>
{
    public void Configure(EntityTypeBuilder<ComponentType> builder)
    {
        builder.HasKey(ct => ct.Id);
        builder.Property(ct => ct.Abbreviation).HasMaxLength(30);
        builder.Property(ct => ct.Name).HasMaxLength(150);
            
        builder.ToTable("ComponentTypes");
        
        builder.HasData(new List<ComponentType>
        {
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" },
        });
    }
}