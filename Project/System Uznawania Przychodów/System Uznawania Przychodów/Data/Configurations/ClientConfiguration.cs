using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System_Uznawania_Przychodów.Domain.Entities;

namespace System_Uznawania_Przychodów.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);
        builder.ToTable("Clients");

        builder.Property(c => c.Address).HasMaxLength(150);
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.Property(c => c.Phone).HasMaxLength(15);
        builder.Property(c => c.DeletedAt).IsRequired(false);

        builder.HasData(
            new Client { Id = 1, Address = "Addr1", Email = "client1@test.com", Phone = "100000001" },
            new Client { Id = 2, Address = "Addr2", Email = "client2@test.com", Phone = "100000002" },
            new Client { Id = 3, Address = "Addr3", Email = "client3@test.com", Phone = "100000003" },
            new Client { Id = 4, Address = "Addr4", Email = "client4@test.com", Phone = "100000004" },
            new Client { Id = 5, Address = "Addr5", Email = "client5@test.com", Phone = "100000005" }
        );
    }
}
