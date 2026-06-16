namespace System_Uznawania_Przychodów.DTOs.Contracts;

public class AddContractDto
{
    public int ClientId { get; set; }
    public int SoftwareId { get; set; }
    public string SoftwareVersion { get; set; } = null!;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int AdditionalSupportYears { get; set; } = 0;
}