using Domain.Models;
using Domain.Repository;

namespace UserInterface.Service;

public class FlightServices
{
    private readonly IFlightRepository _flightRepository;
    private readonly IAirportRepository _airportRepository;

    public FlightServices(IFlightRepository flightRepository, IAirportRepository airportRepository)
    {
        _flightRepository = flightRepository;
        _airportRepository = airportRepository;
    }

    public Flight? FindFlightById(string id)
    {
        return _flightRepository.FindById(id);
    }

    public IEnumerable<Flight?> FindFlightByDepartureCountry(string country)
    {
        var airports = _airportRepository.GetAirportByCountry(country);
        List<Flight> flights = new List<Flight>();

        foreach (var airport in airports)
        {
            var flight = _flightRepository.GetFlightByDepartureAirport(airport.id);
            flights.AddRange(flight!);
        }

        return flights;
    }
}