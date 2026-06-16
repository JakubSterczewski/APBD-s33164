using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class CompanyClientConfiguration : IEntityTypeConfiguration<CompanyClient>
{
    public void Configure(EntityTypeBuilder<CompanyClient> builder)
    {
        builder.HasKey(c => c.ClientId);
        
        builder.Property(c => c.CompanyName).HasMaxLength(200);
        builder.Property(c => c.Krs).HasMaxLength(10);

        builder.HasIndex(c => c.Krs).IsUnique();
        
        builder.HasOne(c => c.Client)
            .WithOne(c => c.CompanyClient)
            .HasForeignKey<CompanyClient>(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new CompanyClient { ClientId = 4, CompanyName = "Corp1", Krs = "0000000001" },
            new CompanyClient { ClientId = 5, CompanyName = "Corp2", Krs = "0000000002" }
        );
    }
}
