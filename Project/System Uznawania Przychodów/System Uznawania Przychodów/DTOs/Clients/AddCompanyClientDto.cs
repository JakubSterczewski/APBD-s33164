namespace System_Uznawania_Przychodów.DTOs.Clients;

public class AddCompanyClientDto
{
    public string CompanyName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Krs { get; set; } = null!;
}
