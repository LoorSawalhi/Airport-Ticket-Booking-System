using Domain.CustomException;

namespace UserInterface;

internal class Utilities
{
    public const string InvalidOption = "Invalid Option !!! Try again.";
    private static int _inputLine;

    public static void Menu()
    {
        Domain.InputHandling.HandleUserInput<NotValidUserInputException>(() =>
            {
                Console.Write("""
                              Welcome to the Airport Booking System
                              Choose the way you are logging as:
                              1) Manager
                              2) Passenger

                              Option : 
                              """);
                _inputLine = ReadOption();
                Options(_inputLine);
            });
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
                    throw new NotValidUserInputException(InvalidOption);
            }
    }

    public static int ReadOption()
    {
        var readLine = Console.ReadLine();
        Console.WriteLine();
        if (readLine == null || !int.TryParse(readLine, out var option))
            throw new NotValidUserInputException(InvalidOption);

        return option;
    }

    public static string ReadString(string message)
    {
        Console.Write(message);
        var readLine = Console.ReadLine();
        Console.WriteLine();
        if (readLine == null)
            throw new EmptyStringException("Empty Input!!");

        return readLine;
    }

    public static float ReadPrice(string message)
    {
        Console.Write(message);
        var readLine = Console.ReadLine();
        Console.WriteLine();
        if (readLine == null || !float.TryParse(readLine, out var price) || price < 0)
            throw new NotValidUserInputException("Invalid Price");

        return price;
    }
}