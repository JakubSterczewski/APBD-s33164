namespace System_Uznawania_Przychodów.Domain.Entities;

public class Client
{
    public int Id { get; set; }
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime? DeletedAt { get; set; }

    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    public CompanyClient? CompanyClient { get; set; } = null!;
    public IndividualClient? IndividualClient { get; set; } = null!;
}
