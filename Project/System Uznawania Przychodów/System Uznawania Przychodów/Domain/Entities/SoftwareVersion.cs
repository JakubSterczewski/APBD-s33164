namespace System_Uznawania_Przychodów.Domain.Entities;

public class SoftwareVersion
{
    public int SoftwareId { get; set; }
    public string Version { get; set; } = null!;

    public Software Software { get; set; } = null!;
}