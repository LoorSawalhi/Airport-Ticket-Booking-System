namespace UserInterface.CustomException;

internal sealed class NoAvailablePassenger : Exception
{
    public NoAvailablePassenger()
    {
    }

    public NoAvailablePassenger(string message)
        : base(message)
    {
    }

    public NoAvailablePassenger(string message, Exception inner)
        : base(message, inner)
    {
    }
}