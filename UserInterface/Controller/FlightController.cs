using Domain.Models;
using UserInterface.CustomException;
using UserInterface.Service;

namespace UserInterface.Controller;

using static Utilities;

internal sealed class FlightController
{
    private readonly FlightServices _flightServices;
    private static int _inputLine;

    public FlightController(FlightServices flightServices)
    {
        _flightServices = flightServices;
    }

    public void Book(Flight flight)
    {
        //
    }

    public void SearchFlights()
    {
        HandleUserInput<NotValidOptionsException, VoidResult>(() =>
        {
            Console.Write("""
                          To book a flight, search a flight by:
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
            switch (_inputLine)
            {
                case 1:
                    SearchByDepartureCountry();
                    break;
                case 2:
                    SearchByArrivalCountry();
                    break;
                case 3:
                    //Cancel Your Bookings
                    break;
                case 4:
                    //Modify Your Booking
                    break;
                case 5:
                    //View Personal Bookings
                    break;
                case 6:
                    //Log out
                    break;
                default:
                    throw new NotValidOptionsException(InvalidOption);
            }

            return new VoidResult();
        });
    }

    private void SearchByDepartureCountry()
    {
        HandleUserInput<EmptyQueryResultException, IEnumerable<Flight>>(() =>
        {
            var country = ReadString("Enter departure country: ");
            var flights = _flightServices.FindFlightByDepartureCountry(country).ToList();
            if (flights.ToList().Capacity == 0)
                throw new EmptyQueryResultException($"No Flights Departure From {country}");

            var iterator = 1;
            foreach (var flight in flights)
            {
                Console.WriteLine($"{iterator}- {flight}");
            }

            return flights;
        });
    }

    private void SearchByArrivalCountry()
    {
        HandleUserInput<EmptyQueryResultException, IEnumerable<Flight>>(() =>
        {
            var country = ReadString("Enter arrival country: ");
            var flights = _flightServices.FindFlightByArrivalCountry(country).ToList();
            if (flights.Capacity == 0)
                throw new EmptyQueryResultException($"No Flights Arrives at {country}");

            foreach (var flight in flights)
            {
                Console.WriteLine(flight);
            }

            return flights;
        });
    }

    // Add more methods as needed
}