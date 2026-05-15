using C7.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace C7.Configurations;

public class PcComponentConfiguration : IEntityTypeConfiguration<PcComponent>
{
    public void Configure(EntityTypeBuilder<PcComponent> builder)
    {
        builder.HasKey(pcc => new { PCId = pcc.PcId, pcc.ComponentCode });
        builder.Property(pcc => pcc.ComponentCode).HasColumnType("char(10)");
            
        builder.HasOne(pcc => pcc.Pc)
            .WithMany(pc => pc.PcComponents)
            .HasForeignKey(pcc => pcc.PcId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasOne(pcc => pcc.Component)
            .WithMany(c => c.PcComponents)
            .HasForeignKey(pcc => pcc.ComponentCode)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.ToTable("PCComponents");
        
        builder.HasData(new List<PcComponent>
        {
            new PcComponent { PcId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new PcComponent { PcId = 1, ComponentCode = "GPU0000001", Amount = 1 },
            new PcComponent { PcId = 1, ComponentCode = "RAM0000001", Amount = 2 },
        });
    }
}