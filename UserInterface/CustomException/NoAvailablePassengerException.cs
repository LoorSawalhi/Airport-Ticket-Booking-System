namespace UserInterface.CustomException;

internal sealed class NoAvailablePassengerException : Exception
{
    public NoAvailablePassengerException()
    {
    }

    public NoAvailablePassengerException(string message)
        : base(message)
    {
    }

    public NoAvailablePassengerException(string message, Exception inner)
        : base(message, inner)
    {
    }
}