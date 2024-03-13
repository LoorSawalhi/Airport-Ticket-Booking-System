using UserInterface.CustomException;
using static UserInterface.Utilities;

namespace UserInterface;

internal class Mannager
{
    public static void Menu()
    {
        while (true)
            try
            {
                Console.WriteLine("""
                                  Hey Manager !! Here are your options
                                  1) Filter Bookings
                                  2) Upload Flights
                                  3) Log out
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
                //Filter Bookings
                break;
            case 2:
                //Upload Flights
                break;
            case 3:
                //Log out
                break;
            default:
                throw new NotValidOptionsException(InvalidOption);
        }
    }
}