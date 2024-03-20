namespace Domain.CustomException;

public sealed class EmptyStringException(string message) : Exception(message);