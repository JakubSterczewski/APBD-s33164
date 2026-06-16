namespace System_Uznawania_Przychodów.DTOs.Subscriptions;

public class AddSubscriptionDto
{
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public string Name { get; set; } = null!;
    public int RenewalPeriodMonths { get; set; }
}
