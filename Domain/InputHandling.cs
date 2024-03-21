using Domain.CustomException;

namespace Domain;

public class InputHandling
{
    public static T? HandleUserInput<TException, T>(Func<T> optionAction) where TException : Exception
    {
        while (true)
            try
            {
                return optionAction();
            }
            catch (BreakLoopException)
            {
                break;
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
    
    public static T? HandleUserInput<TException,QException, T>(Func<T> optionAction)
        where TException : Exception
        where QException : Exception
    {
        while (true)
            try
            {
                return optionAction();
            }
            catch (BreakLoopException)
            {
                break;
            }
            catch (Exception e) when (e is TException or QException)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                if (ExitCond() == -1)
                    break;
            }

        return default(T);
    }

    public static void HandleUserInput<TException>(Action optionAction) where TException : Exception
    {
        while (true)
            try
            {
                optionAction();
            }
            catch (BreakLoopException)
            {
                break;
            }
            catch (TException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                if (ExitCond() == -1)
                    break;
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

    public static void CheckListIfEmpty<T>(IEnumerable<T> list, string message)
    {
        if (!list.Any())
        {
            throw new EmptyQueryResultException(message);
        }
    }
}