using Domain.Models;

namespace Domain.Repository;

public interface IBookingRepository
{
    public IEnumerable<Booking> GetAllBookings();
    public int GetBookingsCount(string flightId, string classId);
    public void AddNewBooking(string flightId, string passengerId, string flightClass);
    public IEnumerable<ClassFlightRelation> GetAvailableFlights(IEnumerable<FlightClass> classes);
    public IEnumerable<ClassFlightRelation> GetAvailableFlights(IEnumerable<FlightClass> classes, string flightId);
    public IEnumerable<Booking> GetBookingsById(string passengerId);
    public void DeleteBooking(Booking booking);
    public IEnumerable<BookingDetails> FilterFlightsByBookings(IEnumerable<FlightDetails> flights);
    public IEnumerable<BookingDetails> FilterFlightsByPassengerId(IEnumerable<FlightDetails> flights, string passengerId);

}