using Domain.Models;

namespace Domain.Service_Interface;

public interface IBookingService
{
    public void CreateBooking(FlightDetails flight, string passengerId);
    public void CreateBooking(string flightId, string classId, string passengerId);

    public int GetClassCurrentSeats(string classId, string flightId);
    public bool IsFlightAvailable(string flightId, string classId);
    public IEnumerable<ClassFlightRelation> GetAvailableFlights();
    public IEnumerable<ClassFlightRelation> GetAvailableFlights(Booking booking);

    public IEnumerable<Booking> GetAllBookings(string passengerId);
    public void CancelBooking(Booking booking);
    public void RemoveBooking(Booking booking);
    public IEnumerable<BookingDetails> FindBookings(IEnumerable<FlightDetails> flights);
    public IEnumerable<BookingDetails> FindBookings(IEnumerable<FlightDetails> flights, string passengerId);

}