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

    public IEnumerable<Flight> SearchFlights()
    {
        IEnumerable<Flight> flights = HandleUserInput<NotValidUserInputException, IEnumerable<Flight>>(() =>
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
            var country = "";
            IEnumerable<dynamic> data;
            switch (_inputLine)
            {
                case 1:
                    country = ReadString("Enter departure country : ");
                    return _flightService.FindFlightByDepartureCountry(country);
                case 2:
                    country = ReadString("Enter arrival country : ");
                    return _flightService.FindFlightByArrivalCountry(country);
                case 3:
                    price = ReadPrice("Enter price : "); //Price (Under a number)
                    data = _flightService.FindFlightsByPrice(0, price);

                    break;
                case 4:
                    price = ReadPrice("Enter price : "); //Price (Above a number)
                    data = _flightService.FindFlightsByPrice(0, price);
                break;
                case 5:
                    //Departure Airport
                    break;
                case 6:
                    //Arrival Airport
                    break;
                case 7:
                    //Class
                    break;
                default:
                    throw new NotValidUserInputException(InvalidOption);
            }

            return null;
        });

        Console.WriteLine("Your Search Results Are : ");
        var iterator = 1;
        foreach (var flight in flights)
        {
            Console.WriteLine($"{iterator} - {flight}");
            iterator++;
        }

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
            var flight = flights.FirstOrDefault(f => f.Id.Equals(flightId));
            if (flight == null)
                throw new NotValidFlightIdException("Wrong Flight Id !!");

            Console.WriteLine(flight);
            throw new BreakLoopException();
        });
    }
}