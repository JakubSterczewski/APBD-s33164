namespace Kolokwium2.Entities;

public class Doctor
{
    public int DoctorId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public List<Appointment> Appointments { get; set; } = new();
}