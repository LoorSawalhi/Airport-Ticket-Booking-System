using Domain.Models;
using Domain.Repository;
using Domain.Service;

namespace Domain.Service_Interface;

public interface IFlightService
{
    public Flight? FindFlightById(string id);

    public IEnumerable<dynamic> FindFlightById(IEnumerable<ClassFlightRelation> flightRs,
        IEnumerable<FlightClass> classes);

    public IEnumerable<Flight> FindFlightByDepartureCountry(string country);
    public IEnumerable<Flight> FindFlightByArrivalCountry(string country);
    public IEnumerable<Flight?> FindFlightByArrivalAirport(string name);
    public IEnumerable<Flight?> FindFlightByDepartureAirport(string name);
    public IEnumerable<dynamic> FindFlightsByPrice(float minPrice, float maxPrice);
}