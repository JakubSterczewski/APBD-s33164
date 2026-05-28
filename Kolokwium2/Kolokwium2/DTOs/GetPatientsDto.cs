namespace Kolokwium2.DTOs;

public class GetPatientsDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; } = string.Empty;
    public List<GetAppointmentDto> Appointments { get; set; }
}

public class GetAppointmentDto
{
    public int AppointmentId { get; set; }
    public GetDoctorDto Doctor { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public string Status { get; set; }
    public List<GetAppointmentServicesDto> AppointmentServices { get; set; }
}

public class GetAppointmentServicesDto
{
    public int Quantity { get; set; }
    public DateOnly PerformedAt { get; set; }
    public GetMedicalServiceDto MedicalService { get; set; }
}

public class GetMedicalServiceDto
{
    public int ServiceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } =  string.Empty;
    public int Price { get; set; } // todo
    public int DurationMinutes { get; set; }
}

public class GetDoctorDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Phone { get; set; } =  string.Empty;
}