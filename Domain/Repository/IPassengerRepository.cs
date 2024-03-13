using Domain.Models;

namespace Domain.Repository;

public interface IPassengerRepository
{
    public IEnumerable<Passenger> GetAllPassengers();
    public Passenger FindById(string id);
    public void Add(Passenger passenger);
    public void Delete(Passenger passenger);
    public Passenger Update(Passenger newPassenger, string id);
}