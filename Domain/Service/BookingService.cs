using Domain.Models;
using Domain.Repository;

namespace UserInterface.Service;

public class BookingService
{
    private readonly IFlightRepository _flightRepository;
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IFlightRepository flightRepository, IBookingRepository bookingRepository)
    {
        _flightRepository = flightRepository;
        _bookingRepository = bookingRepository;
    }

    public void CreateBooking(string flightId, string passengerId, string flightClass)
    {
        var flight = _flightRepository.FindById(flightId);

        if (flight == null)
        {
            throw new Exception("Flight not found.");
        }

        // var fClass = flight.Classes.FirstOrDefault(c => c.name.Equals(flightClass, StringComparison.InvariantCultureIgnoreCase));
        //
        // if (fClass == null)
        //     throw new Exception("Flight not found.");
        //
        // if (!fClass.IsSeatAvailable())
        // {
        //     throw new Exception("Seat is not available.");
        // }
        //
        // var booking = new Booking(flightId, passengerId, fClass.id);
        // flight.BookSeat(seatNumber);
        // _flightRepository.Save(flight);
        // _bookingRepository.SaveBooking(booking);
    }
}
