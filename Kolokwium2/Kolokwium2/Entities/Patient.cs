namespace Kolokwium2.Entities;

public class Patient
{
    public int PatientId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; } = string.Empty;

    public List<Appointment> Appointments { get; set; } = new();
}