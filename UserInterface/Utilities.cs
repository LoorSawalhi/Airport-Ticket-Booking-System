using UserInterface.CustomException;

namespace UserInterface;

internal class Utilities
{
    public static readonly string InvalidOption = "Invalid Option !!! Try again.";

    public static void Menu()
    {
        while (true)
            try
            {
                Console.WriteLine("""
                                  Welcome to the Airport Booking System
                                  Choose the way you are logging as:
                                  1) Manager
                                  2) Passenger
                                  """);
                var readLine = Console.ReadLine();
                if (readLine != null && int.TryParse(readLine, out var option))
                    Options(option);
                else
                    throw new NotValidOptionsException(InvalidOption);
            }
            catch (NotValidOptionsException e)
            {
                Console.WriteLine(e.Message);
                if (ExitCond() == -1)
                    break;
            }
    }

    private static void Options(int option)
    {
            switch (option)
            {
                case 1:
                    Mannager.Menu();
                    break;
                case 2:
                    Passenger.Menu();
                    break;
                default:
                    throw new NotValidOptionsException(InvalidOption);
            }
    }

    public static int ExitCond()
    {
        Console.Write("To exit type e or E => ");
        var e = Console.ReadLine() ?? string.Empty;
        if (e.ToLower().Trim().Equals("e")) return -1;

        return 0;
    }
}