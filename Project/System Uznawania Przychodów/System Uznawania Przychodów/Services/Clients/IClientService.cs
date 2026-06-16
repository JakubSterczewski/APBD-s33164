using System_Uznawania_Przychodów.DTOs.Clients;

namespace System_Uznawania_Przychodów.Services;

public interface IClientService
{
    Task<int> AddIndividualClientAsync(AddIndividualClientDto dto);
    Task<int> AddCompanyClientAsync(AddCompanyClientDto dto);
    Task UpdateIndividualClientAsync(int id, UpdateIndividualClientDto dto);
    Task UpdateCompanyClientAsync(int id, UpdateCompanyClientDto dto);
    Task DeleteIndividualClientAsync(int id);
}
