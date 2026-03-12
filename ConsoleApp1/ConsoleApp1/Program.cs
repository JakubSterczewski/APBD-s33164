public class InputValidator
{
    public static void Main(string[] args)
    {
        int enteredNumber = GetNumberFromUser();
        Console.Write("Entered value: " + enteredNumber);
    }

    public static int GetNumberFromUser()
    {
        while (true)
        {
            Console.Write("Please enter a number: ");
            string input = Console.ReadLine();

            try
            {
                int enteredNumber = int.Parse(input);
                return enteredNumber;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}