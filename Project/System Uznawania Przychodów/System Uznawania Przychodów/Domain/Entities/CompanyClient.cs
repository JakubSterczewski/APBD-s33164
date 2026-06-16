namespace System_Uznawania_Przychodów.Domain.Entities;

public class CompanyClient
{
    public int ClientId { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Krs { get; set; } = null!;

    public Client Client { get; set; } = null!;
}
