namespace Domain.CustomException;

public sealed class NotValidFlightIdException(string message) : Exception(message);