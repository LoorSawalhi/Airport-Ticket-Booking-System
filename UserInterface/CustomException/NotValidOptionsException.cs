namespace UserInterface.CustomException;

internal sealed class NotValidOptionsException : Exception
{
    public NotValidOptionsException()
    {
    }

    public NotValidOptionsException(string message)
        : base(message)
    {
    }

    public NotValidOptionsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}