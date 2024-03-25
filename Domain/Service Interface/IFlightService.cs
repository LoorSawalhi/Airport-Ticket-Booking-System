using Domain.Models;

namespace Domain.Service_Interface;

public interface IFlightService
{
    public IEnumerable<FlightDetails> FindFlightById(string id);

    public IEnumerable<FlightDetails> FindFlightByDepartureCountry(string country, SearchState state);
    public IEnumerable<FlightDetails> FindFlightByArrivalCountry(string country, SearchState state);
    public IEnumerable<FlightDetails> FindFlightByArrivalAirport(string name, SearchState state);
    public IEnumerable<FlightDetails> FindFlightByDepartureAirport(string name, SearchState state);
    public IEnumerable<FlightDetails> FindFlightsByPrice(float minPrice, float maxPrice, SearchState state);
    public IEnumerable<FlightDetails> FindFlightByClass(string className, SearchState state);
    public IEnumerable<FlightDetails> FindFlightByDate(DateTime date);

    public IEnumerable<FlightDetails> FindFlights(IEnumerable<ClassFlightRelation> flightsClasses);
    public IEnumerable<FlightDetails> GetFlights();
}