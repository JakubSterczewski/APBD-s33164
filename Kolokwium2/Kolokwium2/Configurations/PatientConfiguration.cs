using Kolokwium2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium2.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasKey(p => p.PatientId);

        builder.Property(p => p.FirstName).HasMaxLength(50);
        builder.Property(p => p.LastName).HasMaxLength(100);
        builder.Property(p => p.DateOfBirth).HasColumnType("datetime");
        builder.Property(p => p.Phone).HasMaxLength(9);

        builder.ToTable("Patients");

        builder.HasData(new List<Patient>
        {
            new Patient() {PatientId = 1, FirstName =  "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1)},
        });
    }
}