using Domain.Models;

namespace Domain.Repository;

public interface IFlightRepository
{
    public IEnumerable<Flight> GetAllFlights();

    public Flight FindById(string id);
    public void Add(Flight flight);
    public void Delete(Flight flight);
    public Flight Update(Flight newFlight, string id);
}