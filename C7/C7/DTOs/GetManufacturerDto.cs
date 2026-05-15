namespace C7.DTOs;

public class GetManufacturerDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public DateOnly FouncationDate { get; set; }
}