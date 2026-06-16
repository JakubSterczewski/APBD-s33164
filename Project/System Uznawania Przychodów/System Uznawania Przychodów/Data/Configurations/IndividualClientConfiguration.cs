using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class IndividualClientConfiguration : IEntityTypeConfiguration<IndividualClient>
{
    public void Configure(EntityTypeBuilder<IndividualClient> builder)
    {
        builder.HasKey(c => c.ClientId);

        builder.Property(c => c.FirstName).HasMaxLength(100);
        builder.Property(c => c.LastName).HasMaxLength(100);
        builder.Property(c => c.Pesel).HasMaxLength(11);

        builder.HasIndex(c => c.Pesel).IsUnique();

        builder.HasOne(c => c.Client)
            .WithOne(c => c.IndividualClient)
            .HasForeignKey<IndividualClient>(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new IndividualClient { ClientId = 1, FirstName = "First1", LastName = "Last1", Pesel = "00000000001" },
            new IndividualClient { ClientId = 2, FirstName = "First2", LastName = "Last2", Pesel = "00000000002" },
            new IndividualClient { ClientId = 3, FirstName = "First3", LastName = "Last3", Pesel = "00000000003" }
        );
    }
}
