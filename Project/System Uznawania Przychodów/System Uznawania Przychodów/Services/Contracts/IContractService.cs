using System_Uznawania_Przychodów.DTOs.Contracts;

namespace System_Uznawania_Przychodów.Services;

public interface IContractService
{
    Task<int> CreateContractAsync(AddContractDto dto);
    Task AddPaymentAsync(int contractId, AddContractPaymentDto dto);
}
