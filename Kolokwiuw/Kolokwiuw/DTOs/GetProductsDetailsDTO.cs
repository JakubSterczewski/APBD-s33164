namespace Kolokwiuw.DTOs;

public class GetProductsDetailsDTO
{
    public int Id { get; set; }
    public string name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public decimal strickerPrice { get; set; }
    public GetProductTypeDTO productType { get; set; }
    public GetMakerDTO maker { get; set; }
    public GetVendorOfferDTO vendorOffer { get; set; }
}