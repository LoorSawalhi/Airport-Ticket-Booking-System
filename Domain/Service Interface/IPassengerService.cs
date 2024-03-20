namespace Domain.Service_Interface;

public interface IPassengerService
{
    public Domain.Models.Passenger? FindPassengerById(string id);
}