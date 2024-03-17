using System.Net.Http.Headers;
using Domain.Models;
using Domain.Repository;
using UserInterface.CustomException;

namespace UserInterface.Service;

public class FlightServices
{
    private readonly IFlightRepository _flightRepository;
    private readonly AirportServices _airportServices;

    public FlightServices(IFlightRepository flightRepository, AirportServices airportServices)
    {
        _flightRepository = flightRepository;
        _airportServices = airportServices;
    }

    public Flight? FindFlightById(string id)
    {
        return _flightRepository.FindById(id);
    }

    internal IEnumerable<Flight?> FindFlightByDepartureCountry(string country)
    {
            var airports = _airportServices.FindAirportByCountry(country).ToList();

            if (airports.Capacity == 0)
                throw new EmptyQueryResultException($"No Such Airport in {country}");

            foreach (var airport in airports)
            {
                Console.WriteLine(airport.ToString());
            }

            return _flightRepository.GetFlightByDepartureAirport(airports);
    }

    public IEnumerable<Flight?> FindFlightByArrivalCountry(string country)
    {
        var airports = _airportServices.FindAirportByCountry(country);
        return _flightRepository.GetFlightByArrivalAirport(airports);
    }

    public IEnumerable<Flight?> FindFlightByArrivalAirport(string name)
    {
        var airports = _airportServices.FindAirportByName(name);
        return _flightRepository.GetFlightByArrivalAirport(airports);
    }

    public IEnumerable<Flight?> FindFlightByDepartureAirport(string name)
    {
        var airports = _airportServices.FindAirportByName(name);
        return _flightRepository.GetFlightByDepartureAirport(airports);
    }
}