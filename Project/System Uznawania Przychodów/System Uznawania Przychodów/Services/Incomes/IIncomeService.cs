using System_Uznawania_Przychodów.DTOs.Income;

namespace System_Uznawania_Przychodów.Services;

public interface IIncomeService
{
    Task<GetIncomeDto> CalculateActualIncome(string? currency);
    Task<GetIncomeDto> CalculateActualIncomeForSoftwareId(int id, string? currency);
    Task<GetIncomeDto> CalculateEstimatedIncome(string? currency);
    Task<GetIncomeDto> CalculateEstimatedIncomeForSoftwareId(int id, string? currency);
}