using Domain.Models;

namespace Domain.Service_Interface;

public interface IPassengerService
{
    public Passenger FindPassengerById(string id);
}