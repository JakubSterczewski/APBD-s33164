namespace Kolokwium2.DTOs;

public class AddPatientDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Phone { get; set; } = string.Empty;
    public AddAppointmentDto Appointment { get; set; }
}

public class AddAppointmentDto
{
    public int DocctorId { get; set; }
    public DateOnly AppointmentDate { get; set; }
}