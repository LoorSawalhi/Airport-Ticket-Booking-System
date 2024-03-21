using Domain.CustomException;
using Domain.Models;
using Domain.Service;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace UserInterface.Controller;

using static Utilities;

internal sealed class FlightController
{
    private readonly IFlightService _flightService;
    private readonly IRClassFlightService _rClassFlightService;
    private readonly IFlightClassService _flightClassService;

    private static Domain.Models.Passenger _passenger;
    private static int _inputLine;

    public FlightController(IFlightService flightService, IRClassFlightService rClassFlightService, IFlightClassService flightClassService, Domain.Models.Passenger passenger)
    {
        _flightService = flightService;
        _rClassFlightService = rClassFlightService;
        _flightClassService = flightClassService;
        _passenger = passenger;
    }

    public IEnumerable<FlightDetails> SearchFlights()
    {
        var flights = HandleUserInput<NotValidUserInputException, EmptyQueryResultException,
            IEnumerable<FlightDetails>>(() =>
        {
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
                    data = _flightService.FindFlightByDepartureCountry(readString);
                    break;
                case 2:
                    readString = ReadString("Enter arrival country : ");
                    data = _flightService.FindFlightByArrivalCountry(readString);
                    break;
                case 3:
                    price = ReadPrice("Enter price : "); //Price (Under a number)
                    data = _flightService.FindFlightsByPrice(0, price);
                    break;
                case 4:
                    price = ReadPrice("Enter price : "); //Price (Above a number)
                    data = _flightService.FindFlightsByPrice(price, float.MaxValue);
                    break;
                case 5:
                    readString = ReadString("Enter departure airport : ");
                    data = _flightService.FindFlightByDepartureAirport(readString);
                    break;
                case 6:
                    readString = ReadString("Enter arrival airport : ");
                    data = _flightService.FindFlightByArrivalAirport(readString);
                    break;
                case 7:
                    readString = ReadString("Enter flight class : ");
                    data = _flightService.FindFlightByClass(readString);
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

    public void BookingList()
    {
        HandleUserInput<NotValidFlightIdException>(() =>
        {
            Console.WriteLine("""
                              To Book a Flight You Must Search One : 
                              
                              """);
            var flights = SearchFlights();
            var flightId = ReadString("Choose a Flight By Id : ");
            var flight = flights.FirstOrDefault(f => f.id.Equals(flightId));
            if (flight == null)
                throw new NotValidFlightIdException("Wrong Flight Id !!");

            Console.WriteLine(flight);
            throw new BreakLoopException();
        });
    }
}