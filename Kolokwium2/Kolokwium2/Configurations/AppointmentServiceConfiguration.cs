using Kolokwium2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kolokwium2.Configurations;

public class AppointmentServiceConfiguration : IEntityTypeConfiguration<AppointmentService>
{
    public void Configure(EntityTypeBuilder<AppointmentService> builder)
    {
        builder.HasKey(aps => new { aps.AppointmentId, aps.ServiceId });

        builder.HasOne(aps => aps.Appointment)
            .WithMany(ap => ap.AppointmentServices)
            .HasForeignKey(a => a.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(aps => aps.MedicalService)
            .WithMany(ms => ms.AppointmentServices)
            .HasForeignKey(aps => aps.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Appointment_Services");
    }
}