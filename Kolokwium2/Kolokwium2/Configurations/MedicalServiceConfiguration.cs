using Kolokwium2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium2.Configurations;

public class MedicalServiceConfiguration : IEntityTypeConfiguration<MedicalService>
{
    public void Configure(EntityTypeBuilder<MedicalService> builder)
    {
        builder.HasKey(ms => ms.ServiceId);

        builder.Property(ms => ms.Name).HasMaxLength(100);
        builder.Property(ms => ms.Description).HasMaxLength(100);
        builder.Property(ms => ms.Price).HasColumnType("decimal(10,2)");

        builder.ToTable("Medical_Services");
        
        builder.HasData(new List<MedicalService>
        {
            new MedicalService() {ServiceId = 1, Name = "nazwa",  Description = "nazwa", Price = 100, DurationMinutes = 90},
        });
    }
}