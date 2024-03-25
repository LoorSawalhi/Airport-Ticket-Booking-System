namespace Domain.CustomException;

public abstract class NotValidFlightIdException(string message) : Exception(message);