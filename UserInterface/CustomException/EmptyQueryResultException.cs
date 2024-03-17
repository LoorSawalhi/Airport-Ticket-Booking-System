namespace UserInterface.CustomException;

public class EmptyQueryResultException : Exception
{
    public EmptyQueryResultException()
    {
    }

    public EmptyQueryResultException(string message)
        : base(message)
    {
    }

    public EmptyQueryResultException(string message, Exception inner)
        : base(message, inner)
    {
    }
}