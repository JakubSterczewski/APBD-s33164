namespace C7.Entities;

public class Component
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Descritpion { get; set; } = string.Empty;
    public int ComponentManufacturersId { get; set; }
    public int ComponentTypesId { get; set; }

    public ICollection<PcComponent> PcComponents { get; set; } = [];
    public ComponentManufacturer ComponentManufacturer { get; set; }
    public ComponentType ComponentType { get; set; }
}