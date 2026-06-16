using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class SoftwareVersionConfiguration : IEntityTypeConfiguration<SoftwareVersion>
{
    public void Configure(EntityTypeBuilder<SoftwareVersion> builder)
    {
        builder.HasKey(x => new {x.SoftwareId, x.Version});
        builder.ToTable("SoftwareVersions");

        builder.Property(x => x.Version).HasMaxLength(50);

        builder.HasData(
            new SoftwareVersion { SoftwareId = 1, Version = "1.0.0" },
            new SoftwareVersion { SoftwareId = 1, Version = "2.0.0" },
            new SoftwareVersion { SoftwareId = 1, Version = "3.2.1" },
            new SoftwareVersion { SoftwareId = 2, Version = "1.0.0" },
            new SoftwareVersion { SoftwareId = 2, Version = "1.8.0" },
            new SoftwareVersion { SoftwareId = 3, Version = "1.0.0" },
            new SoftwareVersion { SoftwareId = 3, Version = "2.0.4" }
        );
    }
}
