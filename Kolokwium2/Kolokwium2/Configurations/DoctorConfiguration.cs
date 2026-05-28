using Kolokwium2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium2.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(a => a.DoctorId);

        builder.Property(a => a.FirstName).HasMaxLength(50);
        builder.Property(a => a.LastName).HasMaxLength(100);
        builder.Property(a => a.Specialization).HasMaxLength(100);
        builder.Property(a => a.Phone).HasMaxLength(9);

        builder.ToTable("Doctors");
    }
}