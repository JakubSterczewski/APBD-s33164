using Kolokwium2.DTOs;

namespace Kolokwium2;

public interface IDbService
{
    Task<List<GetPatientsDto>> GetPatientsAsync(string? lastName);
    Task AddPatientAsync(AddPatientDto addPatientDto);
}