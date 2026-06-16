namespace System_Uznawania_Przychodów.Domain.Entities;

public class SubscriptionPayment
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }

    public Subscription Subscription { get; set; } = null!;
}
