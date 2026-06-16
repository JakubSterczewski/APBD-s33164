namespace System_Uznawania_Przychodów.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public string Name { get; set; } = null!;
    public int RenewalPeriodMonths { get; set; }
    public decimal RenewalPrice { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime NextPeriodStart { get; set; }
    public bool HasPaid { get; set; } = true;
    public bool IsCancelled { get; set; } = false;

    public Client Client { get; set; } = null!;
    public Software Software { get; set; } = null!;
    public ICollection<SubscriptionPayment> Payments { get; set; } = new List<SubscriptionPayment>();
}
