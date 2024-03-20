using Domain.CustomException;
using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace Domain.Service;

public sealed class FlightService : IFlightService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IAirportService _airportService;
    private readonly IRClassFlightService _rClassFlightService;
    private readonly IFlightClassService _flightClassService;

    private static Passenger _passenger;

    public FlightService(IFlightRepository flightRepository,
        IAirportService airportService,
        IRClassFlightService rClassFlightService, IFlightClassService flightClassService)
    {
        _flightRepository = flightRepository;
        _airportService = airportService;
        _rClassFlightService = rClassFlightService;
        _flightClassService = flightClassService;
    }

    public static Passenger passenger
    {
        get => _passenger;
        set => _passenger = value;
    }

    public Flight? FindFlightById(string id)
    {
        return _flightRepository.FindById(id);
    }

    public IEnumerable<dynamic> FindFlightById(IEnumerable<ClassFlightRelation> flightRs, IEnumerable<FlightClass> classes)
    {
        return _flightRepository.FindById(flightRs, classes);
    }

    public IEnumerable<Flight> FindFlightByDepartureCountry(string country)
    {
            var airports = _airportService.FindAirportByCountry(country).ToList();

            if (airports.Capacity == 0)
                throw new EmptyQueryResultException($"No Such Airport in {country}");

            var flights = _flightRepository.GetFlightByDepartureAirport(airports);
            if (flights.ToList().Count == 0)
                throw new EmptyQueryResultException($"No Flights Departure From {country}");

            return flights;
    }

    public IEnumerable<Flight> FindFlightByArrivalCountry(string country)
    {
        return HandleUserInput<EmptyQueryResultException, IEnumerable<Flight>>(() =>
        {
            var airports = _airportService.FindAirportByCountry(country);
            if (airports.ToList().Capacity == 0)
                throw new EmptyQueryResultException($"No Such Airport in {country}");

            var flights = _flightRepository.GetFlightByArrivalAirport(airports);
            if (flights.ToList().Capacity == 0)
                throw new EmptyQueryResultException($"No Flights Arrives at {country}");

            return flights;
        });
    }

    public IEnumerable<Flight?> FindFlightByArrivalAirport(string name)
    {
        var airports = _airportService.FindAirportByName(name);
        return _flightRepository.GetFlightByArrivalAirport(airports);
    }

    public IEnumerable<Flight?> FindFlightByDepartureAirport(string name)
    {
        var airports = _airportService.FindAirportByName(name);
        return _flightRepository.GetFlightByDepartureAirport(airports);
    }

    public IEnumerable<dynamic> FindFlightsByPrice(float minPrice, float maxPrice)
    {
        return HandleUserInput<EmptyQueryResultException, IEnumerable<dynamic>>(() =>
        {
            var flightRs = _rClassFlightService.FindFlightsByPrice(minPrice, maxPrice);
            if (flightRs.ToList().Capacity == 0)
                throw new EmptyQueryResultException($"No Flights with Price Range [{minPrice},{maxPrice}]");

            var classes = _flightClassService.GetClassesById(flightRs);
            var flights = _flightRepository.FindById(flightRs, classes);
            if (flights.ToList().Capacity == 0)
                throw new EmptyQueryResultException($"No Flights Available");

            return flights;
        });
    }
}