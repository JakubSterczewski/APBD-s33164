namespace C2;

public class Rental
{
    public User User {get;set;}
    public Equipment Equipment {get;set;}
    private DateTime RentDate {get;set;}
    public DateTime DueDate {get;set;}
    public DateTime? ReturnDate {get;set;}

    public bool IsReturned()
    {
        return ReturnDate != null;
    }

    public Rental(User user, Equipment equipment, DateTime rentDate, DateTime dueDate)
    {
        User = user;
        Equipment = equipment;
        RentDate = rentDate;
        DueDate = dueDate;
    }
}