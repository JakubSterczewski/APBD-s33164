namespace Kolokwium2.Entities;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public string Status { get; set; } = string.Empty;

    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public List<AppointmentService> AppointmentServices { get; set; } = new List<AppointmentService>();
}