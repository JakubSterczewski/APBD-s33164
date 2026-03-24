namespace C2;

public class Equipment
{
    private int Id {get;}
    private string Producer { get; set; }
    private string Name { get; set; }
    
    public Equipment(string producer, string name)
    {
        Id = Guid.NewGuid().GetHashCode();
        Producer = producer;
        Name = name;
    }
}