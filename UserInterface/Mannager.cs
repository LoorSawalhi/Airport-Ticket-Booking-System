using Domain.CustomException;
using static UserInterface.Utilities;
using static Domain.InputHandling;

namespace UserInterface;

internal class Mannager
{
    public static void Menu()
    {
        HandleUserInput<NotValidUserInputException>(() =>
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
                throw new NotValidUserInputException(InvalidOption);
        });
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
                throw new NotValidUserInputException(InvalidOption);
        }
    }
}