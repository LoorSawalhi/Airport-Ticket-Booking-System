using Domain.CustomException;
using Domain.Models;
using Domain.Service_Interface;
using Infrastructre.Repository;
using static Domain.InputHandling;
using static UserInterface.Utilities;

namespace UserInterface.Controller;

internal sealed class BookingController(
    FlightController flightController,
    IBookingService bookingService,
    IFlightClassService flightClassService,
    IRClassFlightService rClassFlightService,
    Domain.Models.Passenger passenger
    )
{
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