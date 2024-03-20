namespace Domain.CustomException;

public sealed class NotValidUserInputException(string message) : Exception(message);