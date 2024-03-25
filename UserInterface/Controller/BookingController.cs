using Domain;
using Domain.CustomException;
using Domain.Models;
using Domain.Service_Interface;
using static Domain.InputHandling;
using static UserInterface.Utilities;

namespace UserInterface.Controller;

internal sealed class BookingController(
    FlightController flightController,
    IBookingService bookingService,
    IPassengerService passengerService,
    Domain.Models.Passenger passenger
    )
{
    private static int _inputLine;
    private string _passengerId = "";

    public IEnumerable<FlightDetails> FilterBookings()
    {
        var flights = HandleUserInput<NotValidUserInputException, EmptyQueryResultException,
            IEnumerable<FlightDetails>>(() =>
        {
            Console.Write("""
                          Search a Bookings By :
                          1) Flight Id
                          2) Departure Country
                          3) Arrival Country
                          4) Price (Under a number)
                          5) Price (Above a number)
                          6) Departure Airport
                          7) Arrival Airport
                          8) Class
                          9) Passenger Id
                          10) Log Out

                          Option : 
                          """);
            _inputLine = ReadOption();
            float price;
            string readString;
            const SearchState state = SearchState.All;
            IEnumerable<FlightDetails> flights = null;

            switch (_inputLine)
            {
                case 1:
                    readString = ReadString("Enter flight id : ");
                    flights = flightController.FindFlightById(readString);
                    break;
                case 2:
                    readString = ReadString("Enter departure country : ");
                    flights = flightController.FindFlightByDepartureCountry(readString, state);
                    break;
                case 3:
                    readString = ReadString("Enter arrival country : ");
                    flights = flightController.FindFlightByArrivalCountry(readString, state);
                    break;
                case 4:
                    price = ReadPrice("Enter price : "); //Price (Under a number)
                    flights = flightController.FindFlightsByPrice(0, price, state);
                    break;
                case 5:
                    price = ReadPrice("Enter price : "); //Price (Above a number)
                    flights = flightController.FindFlightsByPrice(price, float.MaxValue, state);
                    break;
                case 6:
                    readString = ReadString("Enter departure airport : ");
                    flights = flightController.FindFlightByDepartureAirport(readString, state);
                    break;
                case 7:
                    readString = ReadString("Enter arrival airport : ");
                    flights = flightController.FindFlightByArrivalAirport(readString, state);
                    break;
                case 8:
                    readString = ReadString("Enter flight class : ");
                    flights = flightController.FindFlightByClass(readString, state);
                    break;
                case 9:
                    _passengerId = ManagePassenger();
                    flights = flightController.FindFlights();
                    break;
                case 10:
                    Menu();
                    break;
                default:
                    throw new NotValidUserInputException(InvalidOption);
            }

            return flights;
        });

        var data = _passengerId.Length == 0
            ? bookingService.FindBookings(flights!).ToList()
            : bookingService.FindBookings(flights!, _passengerId).ToList();

        Console.WriteLine("Your Search Results Are : ");
        var iterator = 1;
        foreach (var flight in data)
        {
            Console.WriteLine($"{iterator} - {flight}");
            iterator++;
        }

        Console.WriteLine();
        return data;
    }

    private string ManagePassenger()
    {
        Console.WriteLine("Enter Passenger Id");
        var userId = Console.ReadLine();
        if (userId != null)
            passenger = passengerService.FindPassengerById(userId);
        else
            throw new NoAvailablePassengerException("Enter a Valid Id");

        return userId;
    }

    internal void BookingList()
    {
        HandleUserInput<NotValidFlightIdException>(() =>
        {
            Console.Write("""
                              To Book a Flight You Must Search One :

                              """);
            var flights = flightController.SearchFlights(true);
            BookFlight(flights);
            throw new BreakLoopException();
        });
    }

    private void BookFlight(IEnumerable<FlightDetails> flightsList)
    {
        var flights = flightsList.ToList();
        Console.Write("\nChoose a Flight it's Index : ");
        var index = ReadOption();
        if (index > flights.Count)
            throw new NotValidUserInputException(InvalidOption);

        var flight = flights.ElementAt(index - 1);
        bookingService.CreateBooking(flight, passenger.id);
    }

    internal void CancelBookings()
    {
        HandleUserInput<NotValidFlightIdException>(() =>
        {
            var bookingForDeletion = ChooseBooking("""
                                                   Choose The Booking You Want To Cancel By It's Index!!

                                                   Index :
                                                   """);
            bookingService.CancelBooking(bookingForDeletion);
            throw new BreakLoopException();
        });
    }

    internal void ModifyBookings()
    {
        HandleUserInput<NotValidFlightIdException>(() =>
        {
            var booking = ChooseBooking("""
                                                   Choose The Booking You Want To Modify By It's Index!!

                                                   Index :
                                                   """);

            Console.Write("""
                          You Can Modify Your Booking By:
                          
                          1) Changing the flight class to an available one.
                          2) Change the whole flight.
                          
                          Option : 
                          """);
            var option = ReadOption();
            Options(option, booking);
            throw new BreakLoopException();
        });
    }

    private Booking ChooseBooking(string message)
    {
        var bookings = ListBookings();

        Console.Write(message);
        var index = ReadOption();
        if (index > bookings.Count)
            throw new NotValidUserInputException(InvalidOption);

        var bookingForDeletion = bookings.ElementAt(index - 1);
        return bookingForDeletion;
    }

    public List<Booking> ListBookings()
    {
        Console.WriteLine("Here Are Your Bookings!!");
        var bookings = bookingService.GetAllBookings(passenger.id).ToList();
        int iterator = 1;
        foreach (var booking in bookings)
        {
            Console.WriteLine($"{iterator} - {booking}");
            iterator += 1;
        }

        return bookings;
    }

    private void Options(int option, Booking booking)
    {
        const int changeClass = 1, changeFlight = 2;
        switch (option)
        {
            case changeClass:
                ChangeBookingClass(booking);
                break;
            case changeFlight:
                ChangeFlight(booking);
                break;
            default:
                throw new NotValidUserInputException(InvalidOption);
        }
    }

    private void ChangeFlight(Booking booking)
    {
        var bookings = bookingService.GetAvailableFlights(booking).ToList();
        Console.WriteLine($"""
                           Here Is The Booking You Wish To Change!

                           {booking}

                           You Can Choose One of The Following Flights :
                           """);
        var bookingOptions = flightController.FilteredFlights(bookings).ToList();
        var iterator = 1;
        foreach (var flight in bookingOptions)
        {
            Console.WriteLine($"{iterator} - {flight}");
            iterator += 1;
        }

        Console.Write("Flight Index : ");
        var index = ReadOption();
        if (index > bookingOptions.Count)
            throw new NotValidUserInputException(InvalidOption);

        var newBooking = bookings.ElementAt(index - 1);
        bookingService.RemoveBooking(booking);
        bookingService.CreateBooking(newBooking.FlightId, newBooking.ClassId, passenger.id);
        ListBookings();
        throw new BreakLoopException();
    }

    private void ChangeBookingClass(Booking booking)
    {
        var bookings = bookingService.GetAvailableFlights(booking).ToList();
        Console.WriteLine($"""
                          Here Is The Booking You Wish To Change!
                          
                          {booking}
                          
                          You Can Choose One of The Following Flights : 
                          """);

        var flights = flightController.SearchFlights(true);
        BookFlight(flights);
        bookingService.RemoveBooking(booking);
        ListBookings();
        throw new BreakLoopException();
    }
}