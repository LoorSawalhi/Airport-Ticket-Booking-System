namespace Domain.CustomException;

public sealed class NoAvailablePassengerException(string message) : Exception(message);