using Infrastructre.Repository;
using UserInterface.Controller;
using UserInterface.CustomException;
using UserInterface.Service;
using Domain.Models;
using static UserInterface.Utilities;

namespace UserInterface;

internal class Passenger
{
    private const string InvalidPassenger = "No Available Passenger with This ID";
    private static int _inputLine;
    public static AirportServices airportService;
    public static FlightServices flightService;
    public static FlightController flightController;


    public static void Menu()
    {
        InitServices(out airportService, out flightService, out var passengerService);
        HandleUserInput<NoAvailablePassengerException, VoidResult>(() =>
        {
            Console.Write("""
                          You must be a passenger, Welcome!!
                          To be able to gain your features, enter your ID.

                          Id :
                          """);
            var userId = Console.ReadLine();
            if (userId != null)
            {
                var passenger = passengerService.FindPassengerById(userId);
                if (passenger == null)
                    throw new NoAvailablePassengerException(InvalidPassenger);

                PassengerOptions();
            }
            else
            {
                throw new NoAvailablePassengerException(InvalidPassenger);
            }

            return new VoidResult();
        });
    }

    private static void InitServices(out AirportServices airportService, out FlightServices flightService, out PassengerService passengerService)
    {
        var passengerRepository = new PassengerRepository("/home/loor/Desktop/Foothill Training/C#/AirportTicketBookingSystem/Infrastructure/passengers.csv");
        passengerService = new PassengerService(passengerRepository);
        var airportRepository = new AirportRepository("/home/loor/Desktop/Foothill Training/C#/AirportTicketBookingSystem/Infrastructure/airports.csv");
        var flightRepository = new FlightRepository("/home/loor/Desktop/Foothill Training/C#/AirportTicketBookingSystem/Infrastructure/flights.csv");
        airportService = new AirportServices(airportRepository);
        flightService = new FlightServices(flightRepository, airportService);
        flightController = new FlightController(flightService);
    }

    private static void PassengerOptions()
    {
        HandleUserInput<NotValidOptionsException, VoidResult>(() =>
        {
            Console.Write("""
                          Welcome Again !! Here are your options

                          1) Book a Flight
                          2) Search for Flights
                          3) Cancel Your Bookings
                          4) Modify Your Booking
                          5) View Personal Bookings
                          6) Log out

                          Option :
                          """);
            _inputLine = ReadOption();
            Options(_inputLine);
            return new VoidResult();
        });
    }

    private static void Options(int option)
    {
        const int bookFlight = 1, searchFlight = 2;
        switch (option)
        {
            case bookFlight:
                flightController.SearchFlights();
                break;
            case searchFlight:
                //Search for Flights
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
    }
}