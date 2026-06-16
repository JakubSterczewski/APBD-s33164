namespace System_Uznawania_Przychodów.Domain.Entities;

public class Contract
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public string SoftwareVersion { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal Price { get; set; }
    public int AdditionalSupportYears { get; set; }
    public DateOnly? SignedAt { get; set; }
    public decimal TotalPaid { get; set; }

    public Client Client { get; set; } = null!;
    public Software Software { get; set; } = null!;
    public ICollection<ContractPayment> Payments { get; set; } = new List<ContractPayment>();
}
