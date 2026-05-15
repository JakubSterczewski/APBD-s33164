namespace C7.DTOs;

public class GetComponentDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public GetManufacturerDto Manufacturer { get; set; }
    public GetTypeDto Type { get; set; }
}