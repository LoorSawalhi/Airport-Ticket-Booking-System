using Domain;
using Domain.CustomException;
using Domain.Models;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace UserInterface.Controller;

using static Utilities;

internal sealed class FlightController
{
    private readonly IFlightService _flightService;

    private static int _inputLine;
    private SearchState _searchState = SearchState.All;

    internal FlightController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public IEnumerable<FlightDetails> SearchFlights(bool booking)
    {
        var flights = HandleUserInput<NotValidUserInputException, EmptyQueryResultException,
            IEnumerable<FlightDetails>>(() =>
        {
            _searchState = booking ? SearchState.Available : SearchState.All;
            Console.Write("""
                          Search a Flight By :
                          1) Departure Country
                          2) Arrival Country
                          3) Price (Under a number)
                          4) Price (Above a number)
                          5) Departure Airport
                          6) Arrival Airport
                          7) Class
                          8) Log Out

                          Option : 
                          """);
            _inputLine = ReadOption();
            float price;
            string readString;
            IEnumerable<FlightDetails> data = null;
            switch (_inputLine)
            {
                case 1:
                    readString = ReadString("Enter departure country : ");
                    data = FindFlightByDepartureCountry(readString, _searchState);
                    break;
                case 2:
                    readString = ReadString("Enter arrival country : ");
                    data = FindFlightByArrivalCountry(readString, _searchState);
                    break;
                case 3:
                    price = ReadPrice("Enter price : "); //Price (Under a number)
                    data = FindFlightsByPrice(0, price, _searchState);
                    break;
                case 4:
                    price = ReadPrice("Enter price : "); //Price (Above a number)
                    data = FindFlightsByPrice(price, float.MaxValue, _searchState);
                    break;
                case 5:
                    readString = ReadString("Enter departure airport : ");
                    data = FindFlightByDepartureAirport(readString, _searchState);
                    break;
                case 6:
                    readString = ReadString("Enter arrival airport : ");
                    data = FindFlightByArrivalAirport(readString, _searchState);
                    break;
                case 7:
                    readString = ReadString("Enter flight class : ");
                    data = FindFlightByClass(readString, _searchState);
                    break;
                case 8:
                    Menu();
                    break;
                default:
                    throw new NotValidUserInputException(InvalidOption);
            }

            return data;
        }).ToList();

        Console.WriteLine("Your Search Results Are : ");
        var iterator = 1;
        foreach (var flight in flights)
        {
            Console.WriteLine($"{iterator} - {flight}");
            iterator++;
        }

        Console.WriteLine();
        return flights;
    }

    public IEnumerable<FlightDetails> FindFlightByClass(string readString, SearchState state)
    {
        return _flightService.FindFlightByClass(readString, state);
    }

    public IEnumerable<FlightDetails> FindFlightsByPrice(float minPrice, float maxPrice, SearchState state)
    {
        return _flightService.FindFlightsByPrice(minPrice, maxPrice, state);
    }

    public IEnumerable<FlightDetails> FilteredFlights(IEnumerable<ClassFlightRelation> flightsClasses)
    {
        return _flightService.FindFlights(flightsClasses);
    }

    public IEnumerable<FlightDetails> FindFlightByDepartureCountry(string country, SearchState state)
    {
        return _flightService.FindFlightByDepartureCountry(country, state);
    }

    public IEnumerable<FlightDetails> FindFlightByArrivalCountry(string airport, SearchState state)
    {
        return _flightService.FindFlightByArrivalCountry(airport, state);
    }

    public IEnumerable<FlightDetails> FindFlightByDepartureAirport(string airport, SearchState state)
    {
        return _flightService.FindFlightByArrivalAirport(airport, state);
    }

    public IEnumerable<FlightDetails> FindFlightByArrivalAirport(string airport, SearchState state)
    {
        return _flightService.FindFlightByDepartureAirport(airport, state);
    }

    public IEnumerable<FlightDetails> FindFlightById(string id)
    {
        return _flightService.FindFlightById(id);
    }

    public IEnumerable<FlightDetails> FindFlightByDate(DateTime date)
    {
        return _flightService.FindFlightByDate(date);
    }

    public IEnumerable<FlightDetails> FindFlights()
    {
        return _flightService.GetFlights();
    }
}