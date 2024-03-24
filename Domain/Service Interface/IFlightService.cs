using Domain.Models;
using Domain.Repository;
using Domain.Service;

namespace Domain.Service_Interface;

public interface IFlightService
{
    public Flight? FindFlightById(string id);

    public IEnumerable<FlightDetails> FindFlightByDepartureCountry(string country, SearchState state);
    public IEnumerable<FlightDetails> FindFlightByArrivalCountry(string country, SearchState state);
    public IEnumerable<FlightDetails> FindFlightByArrivalAirport(string name, SearchState state);
    public IEnumerable<FlightDetails> FindFlightByDepartureAirport(string name, SearchState state);
    public IEnumerable<FlightDetails> FindFlightsByPrice(float minPrice, float maxPrice, SearchState state);
    public IEnumerable<FlightDetails> FindFlightByClass(string className, SearchState state);
    public IEnumerable<ClassFlightRelation> GetAvailableFlights();
}