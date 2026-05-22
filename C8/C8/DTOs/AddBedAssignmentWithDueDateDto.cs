namespace C8.DTOs;

public class addBedAssignmentDto
{
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public string BedType { get; set; } = string.Empty;
    public string Ward { get; set; } = string.Empty;
}