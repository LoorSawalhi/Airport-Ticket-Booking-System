using Domain;
using Domain.CustomException;
using Domain.Models;
using Domain.Service;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace UserInterface.Controller;

using static Utilities;

public sealed class FlightController
{
    private readonly IFlightService _flightService;
    private readonly IRClassFlightService _rClassFlightService;
    private readonly IFlightClassService _flightClassService;
    private readonly SearchState _state;

    private static Domain.Models.Passenger _passenger;
    private static int _inputLine;
    private SearchState _searchState = SearchState.All;

    public FlightController(IFlightService flightService, IRClassFlightService rClassFlightService,
        IFlightClassService flightClassService, Domain.Models.Passenger passenger, SearchState state)
    {
        _flightService = flightService;
        _rClassFlightService = rClassFlightService;
        _flightClassService = flightClassService;
        _passenger = passenger;
        _state = state;
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

                          Option : 
                          """);
            _inputLine = ReadOption();
            var price = 0.0f;
            var readString = "";
            IEnumerable<FlightDetails> data = null;
            switch (_inputLine)
            {
                case 1:
                    readString = ReadString("Enter departure country : ");
                    data = _flightService.FindFlightByDepartureCountry(readString, _searchState);
                    break;
                case 2:
                    readString = ReadString("Enter arrival country : ");
                    data = _flightService.FindFlightByArrivalCountry(readString, _searchState);
                    break;
                case 3:
                    price = ReadPrice("Enter price : "); //Price (Under a number)
                    data = _flightService.FindFlightsByPrice(0, price, _searchState);
                    break;
                case 4:
                    price = ReadPrice("Enter price : "); //Price (Above a number)
                    data = _flightService.FindFlightsByPrice(price, float.MaxValue, _searchState);
                    break;
                case 5:
                    readString = ReadString("Enter departure airport : ");
                    data = _flightService.FindFlightByDepartureAirport(readString, _searchState);
                    break;
                case 6:
                    readString = ReadString("Enter arrival airport : ");
                    data = _flightService.FindFlightByArrivalAirport(readString, _searchState);
                    break;
                case 7:
                    readString = ReadString("Enter flight class : ");
                    data = _flightService.FindFlightByClass(readString, _searchState);
                    break;
                default:
                    throw new NotValidUserInputException(InvalidOption);
            }

            return data;
        });

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

    public IEnumerable<FlightDetails> FilteredFlights(IEnumerable<ClassFlightRelation> flightsClasses)
    {
        return _flightService.FindFlights(flightsClasses);
    }
}