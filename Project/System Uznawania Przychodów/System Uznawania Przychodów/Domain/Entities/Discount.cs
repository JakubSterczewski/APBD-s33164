using System_Uznawania_Przychodów.Domain.Enums;

namespace System_Uznawania_Przychodów.Domain.Entities;

public class Discount
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public PurchaseModel PurchaseModel { get; set; }
    public decimal Percentage { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsOnAllProducts { get; set; }

    public ICollection<SoftwareDiscount> SoftwareDiscounts { get; set; } = new List<SoftwareDiscount>();
}
