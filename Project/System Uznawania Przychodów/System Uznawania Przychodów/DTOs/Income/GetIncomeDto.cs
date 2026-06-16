namespace System_Uznawania_Przychodów.DTOs.Income;

public class GetIncomeDto
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "PLN";
}