namespace System_Uznawania_Przychodów.Domain.Entities;

public class IndividualClient
{
    public int ClientId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Pesel { get; set; } = null!;

    public Client Client { get; set; } = null!;
}
