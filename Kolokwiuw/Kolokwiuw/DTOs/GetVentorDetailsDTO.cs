namespace Kolokwiuw.DTOs;

public class GetVendorDetailsDTO
{
    public string code { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public List<GetProductsDetailsDTO> products { get; set; } = [];
}