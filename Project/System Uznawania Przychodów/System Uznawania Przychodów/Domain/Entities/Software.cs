namespace System_Uznawania_Przychodów.Domain.Entities;

public class Software
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string CurrentVersion { get; set; } = null!;
    public string Category { get; set; } = null!;
    public decimal YearlyLicensePrice { get; set; }
    public decimal MonthlySubscriptionPrice { get; set; }

    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; } = new List<SoftwareDiscount>();
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public ICollection<SoftwareVersion> Versions { get; set; } = new List<SoftwareVersion>();
}
