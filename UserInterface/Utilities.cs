using System.Runtime.InteropServices.JavaScript;
using Domain.Models;
using UserInterface.CustomException;

namespace UserInterface;

internal class Utilities
{
    public const string InvalidOption = "Invalid Option !!! Try again.";
    private static int _inputLine;

    public static void Menu()
    {
        HandleUserInput<NotValidOptionsException, VoidResult>(() =>
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
                return new VoidResult();
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
                    throw new NotValidOptionsException(InvalidOption);
            }
    }

    public static int ExitCond()
    {
        Console.Write("To exit type e or E => ");
        var e = Console.ReadLine() ?? string.Empty;
        Console.WriteLine();
        if (e.ToLower().Trim().Equals("e")) return -1;

        return 0;
    }

    public static int ReadOption()
    {
        var readLine = Console.ReadLine();
        Console.WriteLine();
        if (readLine == null || !int.TryParse(readLine, out var option))
            throw new NotValidOptionsException(InvalidOption);

        return option;
    }

    public static T? HandleUserInput<TException, T>(Func<T> optionAction) where TException : Exception
    {
        while (true)
            try
            {
                return optionAction();
            }
            catch (TException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                if (ExitCond() == -1)
                    break;
            }

        return default(T);
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
}