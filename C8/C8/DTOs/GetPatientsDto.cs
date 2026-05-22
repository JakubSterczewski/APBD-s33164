using C8.Models;

namespace C8.DTOs;

public class GetPatientDto
{
    public string Pesel { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Sex { get; set; } = string.Empty;
    public List<GetPatientAdmissionDto> PatientAdmissions { get; set; }
    public List<GetPatientBedAssignmentDto> PatientBedAssignments { get; set; }
}

public class GetPatientAdmissionDto
{
    public int Id { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    public GetWardDto Ward { get; set; }
}

public class GetWardDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class GetPatientBedAssignmentDto
{
    public int Id { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public GetBedDto Bed { get; set; }
}

public class GetBedDto
{
    public int Id { get; set; }
    public GetBedTypeDto BedType { get; set; }
    public GetRoomDto Room { get; set; }
}

public class GetBedTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } =  string.Empty;
    public string Description { get; set; } =  string.Empty;
}

public class GetRoomDto
{
    public string Id { get; set; } = string.Empty;
    public bool HasTv { get; set; }
    public GetWardDto Ward { get; set; }
}