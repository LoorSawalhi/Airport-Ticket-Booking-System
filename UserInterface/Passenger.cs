using Domain;
using Infrastructre.Repository;
using UserInterface.Controller;
using Domain.Service;
using Domain.CustomException;
using Domain.Service_Interface;
using Microsoft.Extensions.DependencyInjection;
using static Domain.InputHandling;
using static UserInterface.Utilities;


namespace UserInterface;

internal class Passenger
{
    private const string InvalidPassenger = "No Available Passenger with This ID";
    private const int cancelBooking = 3;
    private const int modifyBooking = 4;
    private const int ListBookings = 5;
    private const int LogOut = 6;
    private static int _inputLine;
    public static IAirportService AirportService;
    public static IFlightService FlightService;
    public static IRClassFlightService RClassFlightService;
    public static IFlightClassService FlightClassService;
    public static IBookingService BookingService;

    public static FlightController flightController;
    public static BookingController bookingController;

    public static Domain.Models.Passenger? passenger;


    public static void Menu()
    {
        InitServices(out AirportService, out FlightService, out var passengerService, out RClassFlightService,
            out BookingService, out FlightClassService);
        HandleUserInput<NoAvailablePassengerException>(() =>
        {
            Console.Write("""
                          You must be a passenger, Welcome!!
                          To be able to gain your features, enter your ID.

                          Id :
                          """);
            var userId = Console.ReadLine();
            if (userId != null)
            {
                passenger = passengerService.FindPassengerById(userId);

                flightController = new FlightController(FlightService, RClassFlightService, FlightClassService, passenger, SearchState.Available);
                bookingController = new BookingController(flightController, BookingService, FlightClassService, RClassFlightService, passenger);
                PassengerOptions();
            }
            else
            {
                throw new NoAvailablePassengerException(InvalidPassenger);
            }
        });
    }

    private static void InitServices(out IAirportService airportService, out IFlightService flightService,
        out IPassengerService passengerService, out IRClassFlightService rClassFlightService, out IBookingService bookingService
        , out IFlightClassService flightClassService)
    {
        var services = ServiceConfiguration.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        airportService = serviceProvider.GetRequiredService<IAirportService>();
        passengerService = serviceProvider.GetRequiredService<IPassengerService>();
        rClassFlightService = serviceProvider.GetRequiredService<IRClassFlightService>();
        bookingService = serviceProvider.GetRequiredService<IBookingService>();
        flightClassService = serviceProvider.GetRequiredService<IFlightClassService>();
        flightService = serviceProvider.GetRequiredService<IFlightService>();
    }

    private static void PassengerOptions()
    {
        HandleUserInput<NotValidUserInputException, EmptyQueryResultException>(() =>
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
        });
    }

    private static void Options(int option)
    {
        const int bookFlight = 1, searchFlight = 2;
        switch (option)
        {
            case bookFlight:
                bookingController.BookingList();
                break;
            case searchFlight:
                flightController.SearchFlights(false);
                break;
            case cancelBooking:
                bookingController.CancelBookings();
                break;
            case modifyBooking:
                bookingController.ModifyBookings();
                break;
            case ListBookings:
                bookingController.ListBookings();
                break;
            case LogOut:
                Utilities.Menu();
                break;
            default:
                throw new NotValidUserInputException(InvalidOption);
        }
    }
}