using Domain.Models;
using Domain.Repository;
using Domain.Service;

namespace Domain.Service_Interface;

public interface IFlightService
{
    public Flight? FindFlightById(string id);

    public IEnumerable<FlightDetails> FindFlightByDepartureCountry(string country);
    public IEnumerable<FlightDetails> FindFlightByArrivalCountry(string country);
    public IEnumerable<FlightDetails> FindFlightByArrivalAirport(string name);
    public IEnumerable<FlightDetails> FindFlightByDepartureAirport(string name);
    public IEnumerable<FlightDetails> FindFlightsByPrice(float minPrice, float maxPrice);
    public IEnumerable<FlightDetails> FindFlightByClass(string className);
}