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
    private readonly IBookingService _bookingService;

    private static Passenger _passenger;

    public FlightService(IFlightRepository flightRepository,
        IAirportService airportService,
        IRClassFlightService rClassFlightService, IFlightClassService flightClassService, IBookingService bookingService)
    {
        _flightRepository = flightRepository;
        _airportService = airportService;
        _rClassFlightService = rClassFlightService;
        _flightClassService = flightClassService;
        _bookingService = bookingService;
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

    public IEnumerable<FlightDetails> FindFlightByDepartureCountry(string country, SearchState state)
    {
        var airports = _airportService.FindAirportByCountry(country);
        var availableFlights = GetAvailableFlights();
        var flights = _flightRepository.GetFlightByDepartureAirport(airports, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"No Flights Arrives at {country}");

        var fullDetails = FindFullFlightDetails(flights);

        return fullDetails;
    }

    public IEnumerable<FlightDetails> FindFlightByArrivalCountry(string country, SearchState state)
    {
        var airports = _airportService.FindAirportByCountry(country);
        var availableFlights = GetAvailableFlights();

        var flights = _flightRepository.GetFlightByArrivalAirport(airports, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"No Flights Arrives at {country}");

        var fullDetails = FindFullFlightDetails(flights);

        return fullDetails;
    }

    public IEnumerable<FlightDetails> FindFlightByArrivalAirport(string name, SearchState state)
    {
        var airports = _airportService.FindAirportByName(name);
        var availableFlights = GetAvailableFlights();

        var flights = _flightRepository.GetFlightByArrivalAirport(airports, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"Cannot Find Flights Arrives At {name}");
        var fullDetails = FindFullFlightDetails(flights);

        return fullDetails;
    }

    public IEnumerable<FlightDetails> FindFlightByDepartureAirport(string name, SearchState state)
    {
        var departureAirports = _airportService.FindAirportByName(name);
        var availableFlights = GetAvailableFlights();

        var flights = _flightRepository.GetFlightByDepartureAirport(departureAirports, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"No Available Flights Departures from {name}");

        return FindFullFlightDetails(flights);
    }

    public IEnumerable<FlightDetails> FindFlightsByPrice(float minPrice, float maxPrice, SearchState state)
    {
        var flightRs = _rClassFlightService.FindFlightsByPrice(minPrice, maxPrice).ToList();
        CheckListIfEmpty(flightRs, $"No Flights with Price Range [{minPrice},{maxPrice}]");

        return FindFullFlightDetails(flightRs!);
    }

    public IEnumerable<FlightDetails> FindFlightByClass(string className, SearchState state)
    {
        var classf = _flightClassService.GetClassByName(className);
        var relations = _rClassFlightService.FindFlightsByClassId(classf.Id);
        var availableFlights = GetAvailableFlights().ToList();

        var flightsClasses = _flightRepository.GetFlightByClass(relations!, state, availableFlights).ToList();
        CheckListIfEmpty(flightsClasses, $"No Such Flights With Class {className}");

        var flights = _flightRepository.GetFlightsByRelations(flightsClasses, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"No Such Flights With Class {className}");

        IEnumerable<FlightClass> classes = new List<FlightClass> { classf };
        return FindFullFlightDetails(flights, classes);
    }

    public IEnumerable<FlightDetails> FindFlights(IEnumerable<ClassFlightRelation> flightsClasses)
    {
        var classes = _flightClassService.GetClassesById(flightsClasses);
        var flights = _flightRepository.GetFlightsByRelations(flightsClasses, SearchState.Available, flightsClasses).ToList();
        CheckListIfEmpty(flights, $"No Such Flights ");

        return FindFullFlightDetails(flights, classes);
    }

    public IEnumerable<ClassFlightRelation> GetAvailableFlights()
    {
        return _bookingService.GetAvailableFlights();
    }

    private IEnumerable<FlightDetails> FindFullFlightDetails(
        IEnumerable<ClassFlightRelation> classFlightRelations)
    {
        var flights = _flightRepository.GetAllFlights().ToList();
        CheckListIfEmpty(flights, $"No Available Flights");

        var flightsInfo = _airportService.GetFlightsInfo(flights!);
        var allClasses = _flightClassService.GetAllClasses();
        var flightsDetails = _rClassFlightService.FindFlightClassesAndPrice(flightsInfo, allClasses,
            classFlightRelations);
        return flightsDetails;
    }

    private IEnumerable<FlightDetails> FindFullFlightDetails(IEnumerable<Flight> flights, IEnumerable<FlightClass> classes)
    {
        var flightsInfo = _airportService.GetFlightsInfo(flights);
        var relations = _rClassFlightService.FindAllRelations();
        var flightsDetails = _rClassFlightService.FindFlightClassesAndPrice(flightsInfo, classes, relations);
        return flightsDetails;
    }

    private IEnumerable<FlightDetails> FindFullFlightDetails(IEnumerable<Flight> flights)
    {
        var flightsInfo = _airportService.GetFlightsInfo(flights);
        var allClasses = _flightClassService.GetAllClasses();
        var relations = _rClassFlightService.FindAllRelations();
        var flightsDetails = _rClassFlightService.FindFlightClassesAndPrice(flightsInfo, allClasses, relations);
        return flightsDetails;
    }
}