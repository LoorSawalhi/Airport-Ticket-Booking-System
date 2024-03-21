using Domain.Models;

namespace Domain.Repository;

public interface IFlightRepository
{
    //gets all flights from the csv file
    public IEnumerable<Flight?> GetAllFlights();

    //find a specific flight by its id
    public Flight? FindById(string id);
    public IEnumerable<Flight> GetFlightsByRelations(IEnumerable<ClassFlightRelation> relations);

    //get flight based on the departure airport
    public IEnumerable<Flight> GetFlightByDepartureAirport(IEnumerable<Airport?> departureAirports);

    //get flight based on the arrival airport
    public IEnumerable<Flight> GetFlightByArrivalAirport(IEnumerable<Airport?> airports);

    public IEnumerable<ClassFlightRelation> GetFlightByClass(IEnumerable<ClassFlightRelation> relations);
    public void Add(Flight flight);
    public void Delete(Flight flight);
    public Flight Update(Flight newFlight, string id);
}