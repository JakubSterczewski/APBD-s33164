namespace Kolokwiuw.DTOs;

public class CreateVendorDTO
{
    public string code { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public List<CreateVendorProductsDTO> products { get; set; } = [];
}