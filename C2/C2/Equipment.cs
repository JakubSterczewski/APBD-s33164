namespace C2;

public class Equipment
{
    private string Producer { get; set; }
    private string Name { get; set; }
    
    public Equipment(string producer, string name)
    {
        Producer = producer;
        Name = name;
    }
}