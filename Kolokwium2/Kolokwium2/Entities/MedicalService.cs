namespace Kolokwium2.Entities;

public class MedicalService
{
    public int ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public int DurationMinutes { get; set; }

    public List<AppointmentService> AppointmentServices { get; set; } = new();
}