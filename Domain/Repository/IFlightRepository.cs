using Domain.Models;

namespace Domain.Repository;

public interface IFlightRepository
{
    //gets all flights from the csv file
    public IEnumerable<Flight?> GetAllFlights();
    public IEnumerable<Flight?> GetAllFlights(SearchState state, IEnumerable<ClassFlightRelation> availableFlights);

    //find a specific flight by its id
    public Flight? FindById(string id);

    public IEnumerable<Flight> GetFlightsByRelations(IEnumerable<ClassFlightRelation> relations, SearchState state,
        IEnumerable<ClassFlightRelation> availableFlights);

    //get flight based on the departure airport
    public IEnumerable<Flight> GetFlightByDepartureAirport(IEnumerable<Airport?> departureAirports, SearchState state,
        IEnumerable<ClassFlightRelation> availableFlights);

    //get flight based on the arrival airport
    public IEnumerable<Flight> GetFlightByArrivalAirport(IEnumerable<Airport?> airports, SearchState state,
        IEnumerable<ClassFlightRelation> availableFlights);

    public IEnumerable<ClassFlightRelation> GetFlightByClass(IEnumerable<ClassFlightRelation> relations, SearchState state,
        IEnumerable<ClassFlightRelation> availableFlights);

    public IEnumerable<Flight> GetFlightByDate(DateTime date);
}