namespace C2;

public class Rental
{
    public User User {get;set;}
    public Equipment Equipment {get;set;}
    private DateOnly RentDate {get;set;}
    public DateOnly DueDate {get;set;}
    public DateOnly? ReturnDate {get;set;}
    public double Penalty { get; set; } = 0;

    public bool IsReturned()
    {
        return ReturnDate != null;
    }

    public bool IsDelayed()
    {
        return (DateOnly.FromDateTime(DateTime.Now).DayNumber - DueDate.DayNumber) < 0;
    }

    public Rental(User user, Equipment equipment, DateOnly rentDate, DateOnly dueDate)
    {
        User = user;
        Equipment = equipment;
        RentDate = rentDate;
        DueDate = dueDate;
    }

    public override string ToString()
    {
        return
            $"{nameof(User)}: {User}, {nameof(Equipment)}: {Equipment}, {nameof(RentDate)}: {RentDate}, {nameof(DueDate)}: {DueDate}, {nameof(ReturnDate)}: {ReturnDate}, {nameof(Penalty)}: {Penalty}";
    }
}