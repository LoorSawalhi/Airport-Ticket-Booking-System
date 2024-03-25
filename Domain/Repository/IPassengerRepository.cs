using Domain.Models;

namespace Domain.Repository;

public interface IPassengerRepository
{
    public Passenger? FindById(string id);
}