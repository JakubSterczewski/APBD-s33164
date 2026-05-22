using C8.DTOs;

namespace C8.Services;

public interface IDbService
{
    Task<IEnumerable<GetPatientDto>> GetPatientsAsync(string? search);
    Task AddBedAssignmentAsync(string pesel, addBedAssignmentDto addBedAssignmentWithDueDateDto);
}