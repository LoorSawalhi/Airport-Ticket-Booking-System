using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace Domain.Service;

public sealed class FlightService(
    IFlightRepository flightRepository,
    IAirportService airportService,
    IRClassFlightService rClassFlightService,
    IFlightClassService flightClassService,
    IBookingService bookingService)
    : IFlightService
{
    private static Passenger? _passenger;

    public static Passenger? passenger
    {
        get => _passenger;
        set => _passenger = value;
    }

    public IEnumerable<FlightDetails> FindFlightById(string id)
    {
        var flight = new List<Flight?> { flightRepository.FindById(id) };
        CheckListIfEmpty(flight, $"No Flights With Such {id}");
        var fullDetails = FindFullFlightDetails(flight!);

        return fullDetails;
    }

    public IEnumerable<FlightDetails> FindFlightByDepartureCountry(string country, SearchState state)
    {
        var airports = airportService.FindAirportByCountry(country);
        var availableFlights = GetAvailableFlights();
        var flights = flightRepository.GetFlightByDepartureAirport(airports, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"No Flights Arrives at {country}");

        var fullDetails = FindFullFlightDetails(flights);

        return fullDetails;
    }

    public IEnumerable<FlightDetails> FindFlightByArrivalCountry(string country, SearchState state)
    {
        var airports = airportService.FindAirportByCountry(country);
        var availableFlights = GetAvailableFlights();

        var flights = flightRepository.GetFlightByArrivalAirport(airports, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"No Flights Arrives at {country}");

        var fullDetails = FindFullFlightDetails(flights);

        return fullDetails;
    }

    public IEnumerable<FlightDetails> FindFlightByArrivalAirport(string name, SearchState state)
    {
        var airports = airportService.FindAirportByName(name);
        var availableFlights = GetAvailableFlights();

        var flights = flightRepository.GetFlightByArrivalAirport(airports, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"Cannot Find Flights Arrives At {name}");
        var fullDetails = FindFullFlightDetails(flights);

        return fullDetails;
    }

    public IEnumerable<FlightDetails> FindFlightByDepartureAirport(string name, SearchState state)
    {
        var departureAirports = airportService.FindAirportByName(name);
        var availableFlights = GetAvailableFlights();

        var flights = flightRepository.GetFlightByDepartureAirport(departureAirports, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"No Available Flights Departures from {name}");

        return FindFullFlightDetails(flights);
    }

    public IEnumerable<FlightDetails> FindFlightsByPrice(float minPrice, float maxPrice, SearchState state)
    {
        var flightRs = rClassFlightService.FindFlightsByPrice(minPrice, maxPrice).ToList();
        CheckListIfEmpty(flightRs, $"No Flights with Price Range [{minPrice},{maxPrice}]");

        return FindFullFlightDetails(flightRs!);
    }

    public IEnumerable<FlightDetails> FindFlightByClass(string className, SearchState state)
    {
        var classf = flightClassService.GetClassByName(className);
        var relations = rClassFlightService.FindFlightsByClassId(classf.Id);
        var availableFlights = GetAvailableFlights().ToList();

        var flightsClasses = flightRepository.GetFlightByClass(relations!, state, availableFlights).ToList();
        CheckListIfEmpty(flightsClasses, $"No Such Flights With Class {className}");

        var flights = flightRepository.GetFlightsByRelations(flightsClasses, state, availableFlights).ToList();
        CheckListIfEmpty(flights, $"No Such Flights With Class {className}");

        IEnumerable<FlightClass> classes = new List<FlightClass> { classf };
        return FindFullFlightDetails(flights, classes);
    }

    public IEnumerable<FlightDetails> FindFlights(IEnumerable<ClassFlightRelation> flightsClasses)
    {
        flightsClasses = flightsClasses.ToList();
        var classes = flightClassService.GetClassesById(flightsClasses);
        var flights = flightRepository.GetFlightsByRelations(flightsClasses, SearchState.Available, flightsClasses).ToList();
        CheckListIfEmpty(flights, $"No Such Flights ");

        return FindFullFlightDetails(flights, classes);
    }

    public IEnumerable<ClassFlightRelation> GetAvailableFlights()
    {
        return bookingService.GetAvailableFlights();
    }

    public IEnumerable<FlightDetails> GetFlights()
    {
        var flights = flightRepository.GetAllFlights().ToList();
        CheckListIfEmpty(flights, $"No Available Flights");

        var flightsInfo = airportService.GetFlightsInfo(flights!);
        var relations = rClassFlightService.FindAllRelations();
        var allClasses = flightClassService.GetAllClasses();
        var flightsDetails = rClassFlightService.FindFlightClassesAndPrice(flightsInfo, allClasses, relations);
        return flightsDetails;
    }

    private IEnumerable<FlightDetails> FindFullFlightDetails(
        IEnumerable<ClassFlightRelation> classFlightRelations)
    {
        var flights = flightRepository.GetAllFlights().ToList();
        CheckListIfEmpty(flights, $"No Available Flights");

        var flightsInfo = airportService.GetFlightsInfo(flights!);
        var allClasses = flightClassService.GetAllClasses();
        var flightsDetails = rClassFlightService.FindFlightClassesAndPrice(flightsInfo, allClasses,
            classFlightRelations);
        return flightsDetails;
    }

    private IEnumerable<FlightDetails> FindFullFlightDetails(IEnumerable<Flight> flights, IEnumerable<FlightClass> classes)
    {
        var flightsInfo = airportService.GetFlightsInfo(flights);
        var relations = rClassFlightService.FindAllRelations();
        var flightsDetails = rClassFlightService.FindFlightClassesAndPrice(flightsInfo, classes, relations);
        return flightsDetails;
    }

    private IEnumerable<FlightDetails> FindFullFlightDetails(IEnumerable<Flight> flights)
    {
        var flightsInfo = airportService.GetFlightsInfo(flights);
        var allClasses = flightClassService.GetAllClasses();
        var relations = rClassFlightService.FindAllRelations();
        var flightsDetails = rClassFlightService.FindFlightClassesAndPrice(flightsInfo, allClasses, relations);
        return flightsDetails;
    }
}