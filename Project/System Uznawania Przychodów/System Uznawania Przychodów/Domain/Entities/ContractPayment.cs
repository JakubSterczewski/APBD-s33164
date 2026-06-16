namespace System_Uznawania_Przychodów.Domain.Entities;

public class ContractPayment
{
    public int Id { get; set; }
    public int ContractId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }

    public Contract Contract { get; set; } = null!;
}
