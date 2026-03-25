namespace C2;

public class RentalService
{
    private List<Rental> RentalList = new List<Rental>();
    private List<Equipment> AllItems = new List<Equipment>();

    public void RentEquipment(User user, Equipment equipment)
    {
        if (!equipment.IsAvailable)
        {
            Console.WriteLine("Equipment niedostepny");
            return;
        }
        
        int active = RentalList.Count(r => !r.IsReturned() && r.User == user);
        if (user.maxRentals <= active)
        {
            Console.WriteLine("Maksymalny ilosc rentow");
            return;
        }

        var rental = new Rental(user, equipment, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now).AddDays(7));
        equipment.IsAvailable = false;
        RentalList.Add(rental);
        Console.WriteLine("Wypozyczono");
    }

    public void ReturnEquipment(Equipment equipment)
    {
        Rental? rental = RentalList.Where(r => r.Equipment.Equals(equipment) && !r.IsReturned()).LastOrDefault();

        if (rental != null)
        {
            rental.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
            rental.Equipment.IsAvailable = true;
            
            int lateDays = (rental.ReturnDate.Value.DayNumber - rental.DueDate.DayNumber);
            
            if (lateDays > 0)
            {
                Console.WriteLine("Po terminie");
                rental.Penalty += lateDays * 10;
            }
        }
        else
        {
            Console.WriteLine("Nie ma takiego renta");
        }
    }

    public List<Rental> GetUserRentals(User user)
    {
        return RentalList.Where(r => r.User == user).ToList();
    }

    public List<Rental> GetDelayedRentals()
    {
        return RentalList.Where(r => r.IsDelayed()).ToList();
    }

    public List<Rental> GetAllRentals()
    {
        return RentalList;
    }

    public void ShowRaport()
    {
        RentalList.ForEach(r =>
            Console.WriteLine(r)
        );
    }

    public void AddItem(Equipment equipment)
    {
        AllItems.Add(equipment);
    }

    public void ShowAvailable()
    {
        foreach (var equipment in AllItems.Where(e => !RentalList.Any(r => r.Equipment == e)))
        {
            Console.WriteLine("Dostepne:  " + equipment);
        }
    }
}