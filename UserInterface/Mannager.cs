using Domain;
using Domain.CustomException;
using Domain.Service_Interface;
using UserInterface.Controller;
using static UserInterface.Utilities;
using static Domain.InputHandling;

namespace UserInterface;

internal class Mannager
{
    public static IAirportService AirportService;
    public static IFlightService FlightService;
    public static IRClassFlightService RClassFlightService;
    public static IFlightClassService FlightClassService;
    public static IBookingService BookingService;
    private static IPassengerService PassengerService;


    private static FlightController flightController;
    private static BookingController bookingController;

    public static Domain.Models.Passenger? passenger;

    public static void Menu()
    {
        HandleUserInput<NotValidUserInputException>(() =>
        {
            Console.WriteLine("""
                              Hey Manager !! Here are your options
                              1) Filter Bookings
                              2) Upload Flights
                              3) Log out
                              """);
            var readLine = Console.ReadLine();
            if (readLine != null && int.TryParse(readLine, out var option))
            {
                flightController = new FlightController(FlightService, RClassFlightService, FlightClassService,
                    passenger, SearchState.Available);
                bookingController = new BookingController(flightController, BookingService, PassengerService, passenger);
                Options(option);
            }
            else
            {
                throw new NotValidUserInputException(InvalidOption);
            }
        });
    }

    private static void Options(int option)
    {
        switch (option)
        {
            case 1:
                bookingController.FilterBookings();
                break;
            case 2:
                //Upload Flights
                break;
            case 3:
                Utilities.Menu();
                break;
            default:
                throw new NotValidUserInputException(InvalidOption);
        }
    }
}