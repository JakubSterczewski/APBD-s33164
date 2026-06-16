namespace System_Uznawania_Przychodów.Domain.Entities;

public class SoftwareDiscount
{
    public int SoftwareId { get; set; }
    public int DiscountId { get; set; }

    public Discount Discount { get; set; } = null!;
    public Software Software { get; set; } = null!;
}